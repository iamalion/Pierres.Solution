using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using SweetAndSavory.Models;
using System.Threading.Tasks;
using SweetAndSavory.ViewModels;

namespace SweetAndSavory.Controllers
{
  public class AccountController : Controller
  {
    private readonly SweetAndSavoryContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AccountController (UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, SweetAndSavoryContext db)
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _db = db;
    }

    public ActionResult Index()
    {
      return View();
    }

    public IActionResult Register()
    {
      return View();
    }

   [HttpPost]
    public async Task<ActionResult> Register(RegisterViewModel model, string role)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        // if (role !="Admin" && !="Guest")
        // {
        //     ModelState.AddModeError("", "Please select a role.")
        //     return View(Model);
        // }
        var user = new ApplicationUser { UserName = model.Email };
        var result = await _userManager.CreateAsync(user, model.Password);

    if (result.Succeeded)
    {
        await _userManager.AddToRoleAsync(user, role);
        await _signInManager.SignInAsync(user, isPersistent: false);
        return RedirectToAction("Index", "Home");
    }

    foreach (var error in result.Errors)
    {
        ModelState.AddModelError("", error.Description);
    }

    return View(model);
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
          ModelState.AddModelError("", "Please try again.");
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