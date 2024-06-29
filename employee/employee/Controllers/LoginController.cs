using employee.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Models.ViewModels;
using Utility.Helper;

namespace employee.Controllers
{
    public class LoginController : Controller
    {
        private IConfiguration _config;
        CommonHelper _helper;
        private readonly IHttpContextAccessor _contextAccessor;

        public LoginController(IConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            _config = config;
            _helper = new CommonHelper(_config);
            _contextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public IActionResult Login() {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginVM vm) {
            if (string.IsNullOrEmpty(vm.Email) && string.IsNullOrEmpty(vm.Password))
            {
                return View();
            }
            else {
                bool Isfind = SignInMethod(vm.Email, vm.Password);
                if (Isfind == true) {
                    ViewBag.Role = GetRole();
                    return RedirectToAction("Index", "Home");
                }
                return View("Login");
            }
        }

        public string GetRole() {
            string role = _contextAccessor.HttpContext.Session.GetString("Role");
            return role;
        }
        private bool SignInMethod(string email, string password)
        {
            bool flag = false;

            string userQuery = "SELECT * FROM [Employees] WHERE Email = @Email AND Password = @Password";
            var userDetails = _helper.GetUserByEmail(userQuery, email, password);

            if (userDetails == null || userDetails.Name == null)
            {
                ViewBag.Error = "Username & Password are Wrong";
                return flag;
            }

            string roleQuery = "SELECT * FROM [Role] WHERE Id = @RoleId";
            var roles = _helper.GetEntityById(roleQuery, userDetails.RoleId);

            if (roles == null || string.IsNullOrEmpty(roles.Name))
            {
                ViewBag.Error = "User role not found";
                return flag;
            }

            RoleVM vm = new RoleVM
            {
                Id = roles.Id,
                Name = roles.Name
            };

            flag = true;
            HttpContext.Session.SetString("Role", vm.Name == "Admin" ? "Admin" : "Employee");
            HttpContext.Session.SetString("Name", userDetails.Name);
            GetRole();
            return flag;
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
