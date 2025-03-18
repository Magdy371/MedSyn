using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Newtonsoft.Json.Linq;
using OutpatientClinic.Business.Services.Interfaces;
using OutpatientClinic.Core.DTOs;
using OutpatientClinic.Core.DTOS;
using OutpatientClinic.DataAccess.Entities;
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
            return Ok("Login Success")/*View()*/;
        }

        // POST: /Auth/Login
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromBody]LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState)/*View(model)*/;

            if (string.IsNullOrEmpty(model.Username))
            {
                ModelState.AddModelError("", "Username is required.");
                return BadRequest("Empty")/*View(model)*/;
            }

            if (string.IsNullOrEmpty(model.Password))
            {
                ModelState.AddModelError("", "Password is required.");
                return BadRequest("Empty")/*View(model)*/;
            }

            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return BadRequest(ModelState)/*View(model)*/;
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, isPersistent: false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return BadRequest(ModelState)/*View(model)*/;
            }

            // Generate JWT token and optionally store in TempData or Session if required
            var roles = await _userManager.GetRolesAsync(user);
            string token = await _authService.GenerateToken(user, roles[0]);
            TempData["Token"] = token; // This is optional and depends on how you want to use the token

            return Ok(new { message = "Login successful", token })/*RedirectToAction("Index", "Home")*/;
        }

        // GET: /Auth/Register
        public IActionResult Register()
        {
            return /*View()*/Ok();
        }

        // POST: /Auth/Register
        //[ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);//View(model);

            if (string.IsNullOrEmpty(model.Password))
            {
                ModelState.AddModelError("", "Password is required.");
                return BadRequest("Empty"); /*View(model);*/
            }

            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email,
                FullName = model.FullName
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);
                return BadRequest(result.Errors);//View(model);
            }

            // Assign role if provided (e.g., "Admin", "Doctor", "Patient", or "Staff")
            if (!string.IsNullOrEmpty(model.Role))
            {
                var roleResult = await _userManager.AddToRoleAsync(user, model.Role);
                if (!roleResult.Succeeded)
                {
                    foreach (var error in roleResult.Errors)
                        ModelState.AddModelError("", error.Description);
                    return BadRequest(roleResult.Errors);//View(model);
                }
            }

            await _signInManager.SignInAsync(user, isPersistent: false);
            var roles = await _userManager.GetRolesAsync(user);
            var token = await _authService.GenerateToken(user, roles[0]);
            return Ok(new { message = "Registration successful", token })/*RedirectToAction("Index", "Home")*/;
        }

        // POST: /Auth/Logout
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Auth");
        }
    }
}
