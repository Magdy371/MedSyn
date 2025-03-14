using Microsoft.AspNetCore.Mvc;
using OutpatientClinic.Presentation.Models;

namespace OutpatientClinic.Presentation.Controllers
{
    public class AccountController : Controller
    {
            [HttpGet]
            public IActionResult Login()
            {
                return View();
            }

            [HttpPost]
            public IActionResult Login(LoginViewModel model)
            {
                if (!ModelState.IsValid)
                {
                    // Return the same view if validation fails
                    return View(model);
                }

                // TODO: Implement actual login logic here
                // e.g. check the user credentials, set up cookies, etc.

                // If success, redirect to your dashboard or home
                return RedirectToAction("Index", "Home");
            }

            public IActionResult ForgotPassword()
            {
                // Placeholder for forgot password logic
                return View();
            }

            public IActionResult Register()
            {
                // Placeholder for register logic
                return View();
            }
    }
}
