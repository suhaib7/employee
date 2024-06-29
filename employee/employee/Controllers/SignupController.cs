using DataAccess.Data;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using Models.ViewModels;
using Utility.Helper;

namespace employee.Controllers
{
    public class SignupController : Controller
    {
        private IConfiguration _config;
        CommonHelper _helper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _db;

        public SignupController(IConfiguration config, IUnitOfWork unitOfWork, ApplicationDbContext db)
        {
            _config = config;
            _helper = new CommonHelper(_config);
            _unitOfWork = unitOfWork;
            _db = db;
        }

        public void PopulateTrainingList()
        {
            IEnumerable<SelectListItem> GetTrainings = _db.Trainings.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });

            ViewBag.TrainingList = GetTrainings;
        }

        [HttpGet]
        public IActionResult Register()
        {
            PopulateDropdowns();
            return View();
        }

        private void PopulateDropdowns()
        {
            // Populate roles
            ViewBag.Roles = new List<SelectListItem>
            {
                new SelectListItem { Text = "Admin", Value = "1" },
                new SelectListItem { Text = "Employee", Value = "2" }
            };

            // Populate trainings from the database
            ViewBag.TrainingList = _unitOfWork.Training.GetAll().Select(t => new SelectListItem
            {
                Text = t.Name,
                Value = t.Id.ToString()
            }).ToList();
        }


        [HttpPost]
        public IActionResult Register(RegisterVM vm)
        {
            // Check if user exists
            string UserExistsQuery = $"Select * from [Employees] where Name='{vm.Name}'" +
                $" OR Email='{vm.Email}'";
            bool userExists = _helper.UserAlreadyExist(UserExistsQuery);

            if (userExists)
            {
                ViewBag.Error = "Username or Email Already Exists!";
                return View(vm);
            }

            // Insert the new employee
            string Query = "INSERT INTO [Employees] (Name, Email, Password, Phone, RoleId) " +
               $"VALUES ('{vm.Name}', '{vm.Email}', '{vm.Password}', '{vm.Phone}', {vm.RoleId})";

            int result = _helper.DMLTransaction(Query);

            if (result > 0)
            {
                // Insert the selected trainings for the employee
                int employeeId = _db.Employees.FirstOrDefault(e => e.Email == vm.Email).Id;
                foreach (var trainingId in vm.SelectedTrainingIds)
                {
                    var employeeTraining = new EmployeeTraining
                    {
                        EmployeeId = employeeId,
                        TrainingId = trainingId
                    };
                    _db.EmployeeTrainings.Add(employeeTraining);
                }
                _db.SaveChanges();

                // Entry into session and redirect
                EntryIntoSession(vm.Name);
                ViewBag.Success = "Register Success";
                return RedirectToAction("Index", "Employee");
            }

            return View(vm);
        }


        private void EntryIntoSession(string name)
        {
            HttpContext.Session.SetString("Name", name);
        }

        public IActionResult Index()
        {
            PopulateDropdowns();
            var trainings = _db.Trainings.ToList();

            //trainings.Add(new Training() {
            //    Id = 0,
            //    Name = "--Select Trainings--"
            //});

            ViewBag.Trainingss = trainings;

            return View();
        }
    }
}
