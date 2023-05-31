using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Library.Models;
using System.Threading.Tasks;
using Library.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace Library.Controllers;

public class RoleController : Controller
{
  private readonly RoleManager<IdentityRole> _roleManager;

  public RoleController(RoleManager<IdentityRole> RoleMgr)
  {
    _roleManager = RoleMgr;
  }

  public IActionResult Create() => View();

  [HttpPost]
  public async Task<IActionResult> Create([Required] string name)
  {
    if (ModelState.IsValid)
    {
      IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));
      if (result.Succeeded)
      {
        return RedirectToAction("Index", "Account");
      }
      else
      {
        return View();
      }
    }
    else
    {
      return View();
    }
  }
}