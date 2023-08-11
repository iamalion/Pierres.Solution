using Microsoft.AspNetCore.Mvc;

namespace SweetAndSavory.Controllers
{
    public class HomeController : Controller
    {
      [HttpGet("/")]
      public ActionResult Index()
      {
        ViewBag.PageTitle = "Pierre's Bakery";
        return View();
      }

    }
}