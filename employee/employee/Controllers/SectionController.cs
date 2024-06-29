using DataAccess.Data;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using Models.Dtos;

namespace employee.Controllers
{
    public class SectionController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _db;
        //private readonly ISectionRepository _sectionRepository;
        public SectionController(IUnitOfWork unitOfWork,ApplicationDbContext db)
        {
            _unitOfWork = unitOfWork;
            _db = db;
            //_sectionRepository = sectionRepository;
        }
        public IActionResult Index()
        {
            var sections = _unitOfWork.Section.GetAll(includeProperties:"Department").ToList();
            var departments = _unitOfWork.Department.GetAll().ToList();
            ViewBag.Departments = departments;
            ViewBag.Role = HttpContext.Session.GetString("Role");

            return View(sections);
        }
        public void PopulateDapartmentList()
        {
            IEnumerable<SelectListItem> GetDepartments = _db.Departments.Select(i => new SelectListItem
            {
                Text = i.NameEN,
                Value = i.Id.ToString()
            });

            ViewBag.DepartmentList = GetDepartments;
        }

        [HttpPost]
        public IActionResult SaveSection(string NameEn, string NameAr, int DepartmentId)
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

        public void SaveNewSection(string nameEn, string nameAr, int departmentId)
        {
            Section section = new Section
            {
                NameEN = nameEn,
                NameAR = nameAr,
                DepartmentId = departmentId
            };

            _unitOfWork.Section.Add(section);
            _unitOfWork.Save();
        }

        [HttpDelete, ActionName("Delete")]
        public IActionResult Delete(int? id) {
            Section sectionToBeDeleted = _unitOfWork.Section.Get(u => u.Id == id);

            if (sectionToBeDeleted == null) {
                return NotFound();
            }

            _unitOfWork.Section.Remove(sectionToBeDeleted);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            var sections = _unitOfWork.Section.GetAll(includeProperties: "Department").ToList();
            var sectionDTOs = sections.Select(s => new SectionDTO
            {
                Id = s.Id,
                NameEN = s.NameEN,
                NameAR = s.NameAR,
                DepartmentId = s.DepartmentId,
                DepartmentNameEN = s.Department.NameEN,
                DepartmentNameAR = s.Department.NameAR
            });

            return Json(new { data = sectionDTOs });
        }

        [HttpGet("api/departments")]
        public IActionResult GetDepartments()
        {
            var departments = _unitOfWork.Department.GetAll().Select(d => new {
                Id = d.Id,
                NameEN = d.NameEN,
                NameAR = d.NameAR
            }).ToList();

            return Json(departments);
        }


        [HttpDelete("DeleteById/{id}")]
        public IActionResult DeleteById(int id)
        {
            var sectionToDelete = _unitOfWork.Section.Get(u => u.Id == id);

            if (sectionToDelete == null)
            {
                return Json(new { success = false, message = "Error While Deleting" });
            }
            _unitOfWork.Section.Remove(sectionToDelete);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Section deleted successfully" });
        }

        #endregion
    }
}
