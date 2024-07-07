using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System.Collections.Generic;
using System.Linq;

namespace mongoEmployee.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _context;
        private readonly ITrainingRepository _training;

        public EmployeeController(IEmployeeRepository context, ITrainingRepository training)
        {
            _context = context;
            _training = training;
        }

        public IActionResult Index()
        {
            List<Employee> employees = _context.GetAllEmployees().ToList();
            foreach (var employee in employees)
            {
                PopulateTrainingDetails(employee);
            }
            return View(employees);
        }

        public IActionResult Create()
        {
            PopulateDropdowns();
            PopulateTraining();
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

        private void PopulateTraining()
        {
            var trainings = _training.GetAllTrainings().Select(t => new SelectListItem
            {
                Text = t.Name,
                Value = t._id.ToString()
            }).ToList();

            ViewBag.Trainings = trainings;
        }

        private void PopulateTrainingDetails(Employee employee)
        {
            if (employee.EmployeeTraining != null && employee.EmployeeTraining.Any())
            {
                employee.Trainings = _training.GetTrainingsByIds(employee.EmployeeTraining).ToList();
                employee.TrainingNames = employee.Trainings.Select(t => t.Name).ToList();
            }
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Employee> employees = _context.GetAllEmployees().ToList();
            foreach (var employee in employees)
            {
                PopulateTrainingDetails(employee);
            }
            return Json(new { data = employees });
        }

        [HttpPost]
        public IActionResult CreatePost(Employee employee)
        {
            if (employee.EmployeeTraining == null)
            {
                employee.EmployeeTraining = new List<string>();
            }

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
