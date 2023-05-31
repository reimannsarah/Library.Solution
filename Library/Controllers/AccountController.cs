using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Library.Models;
using System.Threading.Tasks;
using Library.ViewModels;
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;

namespace Library.Controllers
{
    public class AccountController : Controller
    {
        private readonly LibraryContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> RoleMgr, LibraryContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = RoleMgr;
            _db = db;
        }

        public ActionResult Index()
        {
            // string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            // ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
            // var roles = await _userManager.GetRolesAsync(currentUser);
            // var role = roles.FirstOrDefault();
            // var viewModel = new LoginViewModel
            // {
            //     Role = role
            // };
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                
                ApplicationUser user = new ApplicationUser { UserName = model.Email };
                IdentityResult result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, model.Role);
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(model);
                }
            }
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: true, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "There is something wrong with your email or username. Please try again.");
                    return View(model);
                }
            }
        }

        [HttpPost]
        public async Task<ActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}
