using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;

namespace mongoEmployee.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _context;
        public EmployeeController(IEmployeeRepository context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Employee> employees = _context.GetAllEmployees().ToList();
            return View(employees);
        }

        public IActionResult Create() {
            PopulateDropdowns();
            return View();
        }

        private void PopulateDropdowns()
        {
            ViewBag.Roles = new List<SelectListItem>
            {
                new SelectListItem { Text = "Admin", Value = "1" },
                new SelectListItem { Text = "Employee", Value = "2" }
            };
        }

        //[HttpGet]
        //public IActionResult Delete(string id)
        //{
        //    var md = _context.GetEmployeeDetails(id);
        //    return View(md);
        //}

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll() {
            List<Employee> employees = _context.GetAllEmployees().ToList();
            return Json(new { data = employees });
        }

        [HttpPost]
        public IActionResult CreatePost(Employee employee) { 
            _context.Create(employee);
            return RedirectToAction("Index");
        }

        [HttpDelete("employee/delete/{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                var empToDelete = _context.GetEmployeeDetails(id);

                if (empToDelete == null)
                {
                    return Json(new { success = false, message = "Employee not found" });
                }

                _context.Delete(id);
                return Json(new { success = true, message = "Employee deleted successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error deleting employee: {ex.Message}" });
            }
        }
        #endregion
    }
}
