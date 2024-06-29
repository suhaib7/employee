using DataAccess.Repository.IRepository;
using employee.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Dtos;
using Models.ViewModels;

namespace employee.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public EmployeeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<EmployeeDTO> employees = _unitOfWork.Employee.GetAllEmployeesWithTrainings().ToList();
            ViewBag.Role = HttpContext.Session.GetString("Role");
            return View(employees);
        }

        [HttpDelete, ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Employee? obj = _unitOfWork.Employee.Get(u => u.Id == id);

            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.Employee.Remove(obj);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Employee deleted successfully" });
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll() {
            List<EmployeeDTO> employees = _unitOfWork.Employee.GetAllEmployeesWithTrainings().ToList();
            return Json(new { data = employees });
        }

        [HttpDelete("DeleteById/{id}")]
        public IActionResult DeleteById(int id)
        {
            var empToDelete = _unitOfWork.Employee.Get(u => u.Id == id);

            if (empToDelete == null)
            {
                return Json(new { success = false, message = "Error While Deleting" });
            }
            _unitOfWork.Employee.Remove(empToDelete);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Employee deleted successfully" });
        }

        #endregion
    }
}
