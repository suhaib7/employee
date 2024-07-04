using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace mongoEmployee.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _db;
        public DepartmentController(IDepartmentRepository db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Department> departments = _db.GetAllDepartments().ToList();
            return View(departments);
        }
        public IActionResult Create(Department department) {
            _db.Create(department);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult SaveDepartment([FromBody] Department department)
        {
            try
            {
                if (department == null)
                {
                    return BadRequest("Department data is null");
                }

                _db.Create(department);

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public void SaveNewDepartment(string nameEn, string nameAr)
        {
            try
            {
                Department department = new Department
                {
                    NameEN = nameEn,
                    NameAR = nameAr
                };

                _db.Create(department);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    
        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                List<Department> departments = _db.GetAllDepartments().ToList();
                return Json(new { data = departments });
            }
            catch (Exception ex)
            {
               return Json(new { data = new List<Department>() });
            }
        }

        [HttpDelete("department/delete/{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                var departmentToDelete = _db.GetDepartmentDetails(id);

                if (departmentToDelete == null)
                {
                    return Json(new { success = false, message = "Department not found" });
                }

                _db.Delete(id);
                return Json(new { success = true, message = "Department deleted successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error deleting Department: {ex.Message}" });
            }
        }

        #endregion
    }
}
