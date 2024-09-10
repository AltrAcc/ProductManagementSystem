using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProductManagementSystem.Controllers;
using ProductsManagementSystem.DTO;
using ProductsManagementSystem.Enums;
using ProductsManagementSystem.Models;

namespace ProductsManagementSystem.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        // Inject DI in constructor
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        [Authorize("NotAuthorized")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Authorize("NotAuthorized")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(t => t.Errors).Select(t => t.ErrorMessage);
                return View(registerDTO);
            }

            ApplicationUser user = new ApplicationUser()
            {
                Email = registerDTO.Email,
                UserName = registerDTO.Email,
                PersonName = registerDTO.PersonName,
            };

            IdentityResult result = await _userManager.CreateAsync(user, registerDTO.Password); 
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("Register", error.Description);
                }
                return View(registerDTO);
            }

            if (registerDTO.UserType == UserRoleOptions.Admin)
            {
                if (await _roleManager.FindByNameAsync(UserRoleOptions.Admin.ToString()) is null)
                {
                    ApplicationRole applicationRole = new ApplicationRole() { Name = UserRoleOptions.Admin.ToString() };
                    await _roleManager.CreateAsync(applicationRole);
                }
                await _userManager.AddToRoleAsync(user, UserRoleOptions.Admin.ToString());
            }
            else
            {
                if (await _roleManager.FindByNameAsync(UserRoleOptions.User.ToString()) is null)
                {
                    ApplicationRole applicationRole = new ApplicationRole() { Name = UserRoleOptions.User.ToString() };
                    await _roleManager.CreateAsync(applicationRole);
                }
                await _userManager.AddToRoleAsync(user, UserRoleOptions.User.ToString());
            }

            await _signInManager.SignInAsync(user, false);
            return RedirectToAction(nameof(PartiesController.Index), "Parties");
        }

        [HttpGet]
        [Authorize("NotAuthorized")]
        public IActionResult Login()
        {
            return View("Login");
        }


        [HttpPost]
        [Authorize("NotAuthorized")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Erros = ModelState.Values.SelectMany(t => t.Errors).Select(t => t.ErrorMessage);
                return View(loginDTO);
            }

            var user = await _signInManager.PasswordSignInAsync(loginDTO.Email, loginDTO.Password, false, false);

            if (!user.Succeeded)
            {
                ModelState.AddModelError("Login", "Invalid credentials");
                return View(loginDTO);
            }

            return RedirectToAction(nameof(PartiesController.Index), "Parties");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(PartiesController.Index), "Parties");
        }

    }
}
