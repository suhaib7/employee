using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace employee.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DepartmentController> _logger;

        public DepartmentController(IUnitOfWork unitOfWork, ILogger<DepartmentController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public IActionResult Index()
        {
            try
            {
                List<Department> departments = _unitOfWork.Department.GetAll().ToList();
                ViewBag.Role = HttpContext.Session.GetString("Role");
                return View(departments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving departments");
                throw;
            }
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

                _unitOfWork.Department.Add(department);
                _unitOfWork.Save();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving department");
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

                _unitOfWork.Department.Add(department);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving new department");
                throw; // Consider handling this exception to display a friendly error message
            }
        }

        [HttpDelete, ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                Department obj = _unitOfWork.Department.Get(u => u.Id == id);

                if (obj == null)
                {
                    return NotFound();
                }

                _unitOfWork.Department.Remove(obj);
                _unitOfWork.Save();
                return Json(new { success = true, message = "Department deleted successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting department");
                return Json(new { success = false, message = ex.Message });
            }
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                List<Department> departments = _unitOfWork.Department.GetAll().ToList();
                return Json(new { data = departments });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all departments");
                return Json(new { data = new List<Department>() }); // Return an empty list or handle the exception
            }
        }

        [HttpDelete("DeleteById/{id}")]
        public IActionResult DeleteById(int id)
        {
            try
            {
                var departmentToDelete = _unitOfWork.Department.Get(u => u.Id == id);

                if (departmentToDelete == null)
                {
                    return Json(new { success = false, message = "Department not found" });
                }

                _unitOfWork.Department.Remove(departmentToDelete);
                _unitOfWork.Save();
                return Json(new { success = true, message = "Department deleted successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting department by ID");
                return Json(new { success = false, message = ex.Message });
            }
        }

        #endregion
    }
}
