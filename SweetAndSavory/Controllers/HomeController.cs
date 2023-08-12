using Microsoft.AspNetCore.Mvc;
using SweetAndSavory.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace SweetAndSavory.Controllers
{
    public class HomeController : Controller
    {
        private readonly SweetAndSavoryContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(UserManager<ApplicationUser> userManager, SweetAndSavoryContext db)
        {
          _userManager = userManager;
          _db = db;
        }
        [HttpGet("/")]
        public async Task<IActionResult> Index()
        {
            ViewBag.PageTitle = "Pierre's Bakery";

            var flavors = _db.Flavors.ToList();
            var treats = _db.Treats.ToList();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            var model = new Dictionary<string, object>
            {
                { "flavors", flavors },
                { "treats", treats },
                // { "user", user } // Add user to the model if needed
            };

            return View(model);
        }
     }
}