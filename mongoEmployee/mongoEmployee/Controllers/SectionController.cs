using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;

namespace mongoEmployee.Controllers
{
    public class SectionController : Controller
    {
        private readonly ISectionRepository _db;
        private readonly IDepartmentRepository _dbDepartment;
        public SectionController(ISectionRepository db, IDepartmentRepository dbDepartment)
        {
            _db = db;
            _dbDepartment = dbDepartment;
            PopulateDapartmentList();
        }
        public IActionResult Index()
        {
            PopulateDapartmentList();
            List<Section> sections = _db.GetAllsections().ToList();
            return View(sections);
        }

        public void PopulateDapartmentList()
        {
            IEnumerable<SelectListItem> GetDepartments = _dbDepartment.GetAllDepartments().Select(i => new SelectListItem
            {
                Text = i.NameEN,
                Value = i._id.ToString()
            });

            ViewBag.DepartmentList = GetDepartments;
        }

        [HttpPost]
        public IActionResult SaveSection(string NameEn, string NameAr, string DepartmentId)
        {
            try
            {
                SaveNewSection(NameEn, NameAr, DepartmentId);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public void SaveNewSection(string nameEn, string nameAr, string departmentId)
        {
            Section section = new Section
            {
                NameEN = nameEn,
                NameAR = nameAr,
                DepartmentId = departmentId
            };

            _db.Create(section);
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Section> sections = _db.GetAllsections().ToList();
            foreach (var section in sections)
            {
                section.Department = _dbDepartment.GetDepartmentDetails(section.DepartmentId);
            }
            return Json(new { data = sections });
        }


        [HttpDelete("section/delete/{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                var sectionToDelete = _db.GetSectionDetails(id);

                if (sectionToDelete == null)
                {
                    return Json(new { success = false, message = "Section not found" });
                }

                _db.Delete(id);
                return Json(new { success = true, message = "Section deleted successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error deleting Section: {ex.Message}" });
            }
        }

        #endregion
    }
}
