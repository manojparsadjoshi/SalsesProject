using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalsesProject.Models;
using SalsesProject.Utils;
using System.Diagnostics;
using Sales.Services.User;

namespace SalsesProject.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly IUserService _userService;
        public LoginController(IUserService userService)
        {
            _userService = userService;
        }


        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(LogInModel model)
        {
            if (ModelState.IsValid)
            {
                UserModel user = _userService.GetUserWithRole(model.Username, model.PassWord);
                if (user != null)
                {
                    IdentityUtils.AddingClaimIdentity(model, user.Roless ?? "user", HttpContext);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Username or Password");
                }
            }
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "LogIn");
        }
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(SignUpModel model)
        {
            if (ModelState.IsValid)
            {
                UserModel user = new UserModel()
                {
                    Username = model.Email,
                    Password = model.Password,
                    Roless = "user"
                };

                bool result = _userService.RegisterUser(user);
                if (result)
                {
                    return RedirectToAction("Index", "Login");
                }
                return View();
            }
            return View();
        }
    }
}
