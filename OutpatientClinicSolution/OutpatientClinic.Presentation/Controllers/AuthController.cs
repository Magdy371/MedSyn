using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using OutpatientClinic.Business.Services.Interfaces;
using OutpatientClinic.Core.DTOS;
using OutpatientClinic.DataAccess.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace OutpatientClinic.Presentation.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IAuthService _authService;

        public AuthController(UserManager<ApplicationUser> userManager,
                              SignInManager<ApplicationUser> signInManager,
                              IAuthService authService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authService = authService;
        }

        // GET: /Auth/Login
        public IActionResult Login()
        {
            return View();
            //return Ok();
        }

        // POST: /Auth/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            //return BadRequest(ModelState);

            if (string.IsNullOrEmpty(model.Username))
            {
                ModelState.AddModelError("", "Username is required.");
                return View(model);
                //return BadRequest(ModelState);
            }

            if (string.IsNullOrEmpty(model.Password))
            {
                ModelState.AddModelError("", "Password is required.");
                return View(model);
                //return BadRequest(ModelState);
            }

            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(model);
                //return BadRequest(ModelState);
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, isPersistent: false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(model);
                //return BadRequest(ModelState);
            }

            // Generate JWT Token
            var roles = await _userManager.GetRolesAsync(user);
            string? role = roles.FirstOrDefault();
            if (role == null)
            {
                ModelState.AddModelError("", "User does not have an assigned role.");
                return View(model);
                //return BadRequest(ModelState);
            }
            string token = await _authService.GenerateToken(user, role);
            TempData["Token"] = token; // Store token for possible API use

            // Redirect Based on Role
            if (roles.Contains("Admin"))
                return RedirectToAction("Index", "Admin");
            else if (roles.Contains("Doctor"))
                return RedirectToAction("Index", "Doctor");
            else if (roles.Contains("Patient"))
                return RedirectToAction("Index", "Patient");
            else if (roles.Contains("Staff"))
                return RedirectToAction("Index", "Staff");

            return RedirectToAction("Index", "Home"); // Default redirection
            //return Ok(new { message = "Login successful", token });
        }

        // GET: /Auth/Register
        public IActionResult Register()
        {
            return View();
            //return Ok(new { message = "Registration successful"});
        }

        // POST: /Auth/Register
        // POST: /Auth/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Check for existing username
            var existingUserByUsername = await _userManager.FindByNameAsync(model.Username);
            if (existingUserByUsername != null)
            {
                ModelState.AddModelError("Username", "Username is already taken.");
                return View(model);
            }

            // Check for existing email
            var existingUserByEmail = await _userManager.FindByEmailAsync(model.Email);
            if (existingUserByEmail != null)
            {
                ModelState.AddModelError("Email", "Email is already registered.");
                return View(model);
            }

            // Check for existing phone number
            var existingUserByPhone = _userManager.Users.FirstOrDefault(u => u.PhoneNumber == model.PhoneNumber);
            if (existingUserByPhone != null)
            {
                ModelState.AddModelError("PhoneNumber", "Phone number is already in use.");
                return View(model);
            }

            // Proceed if all fields are unique
            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email,
                FullName = model.FullName,
                PhoneNumber = model.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);
                return View(model);
                //return BadRequest(result.Errors);
            }

            // Assign role if provided
            if (!string.IsNullOrEmpty(model.Role))
            {
                var roleResult = await _userManager.AddToRoleAsync(user, model.Role);
                if (!roleResult.Succeeded)
                {
                    foreach (var error in roleResult.Errors)
                        ModelState.AddModelError("", error.Description);
                    return View(model);
                    //return BadRequest(roleResult.Errors);
                }
            }

            await _signInManager.SignInAsync(user, isPersistent: false);
            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault();
            if (role == null)
            {
                ModelState.AddModelError("", "User does not have an assigned role.");
                return View(model);
                //return BadRequest(ModelState);
            }
            var token = await _authService.GenerateToken(user, role);

            // Redirect Based on Role
            if (roles.Contains("Admin"))
                return RedirectToAction("Index", "Admin");
            else if (roles.Contains("Doctor"))
                return RedirectToAction("Index", "Doctor");
            else if (roles.Contains("Patient"))
                return RedirectToAction("Index", "Patient");
            else if (roles.Contains("Staff"))
                return RedirectToAction("Index", "Staff");
            // Add new role redirects
            else if (roles.Contains("Receptionist"))
                return RedirectToAction("Index", "Receptionist");
            else if (roles.Contains("Nurse"))
                return RedirectToAction("Index", "Nurse");
            else if (roles.Contains("Technical_Support"))
                return RedirectToAction("Index", "TechnicalSupport");

            return RedirectToAction("Index", "Home"); // Default redirection
            //return Ok(new { message = "Registration successful", token });
        }

        // POST: /Auth/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Auth");
        }
    }
}
