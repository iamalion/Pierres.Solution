using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SweetAndSavory.Models;
using System.Collections.Generic;
using System.Linq;

namespace SweetAndSavory.Controllers
{
  public class TreatsController : Controller
  {
    private readonly SweetAndSavoryContext _db;

    public TreatsController(SweetAndSavoryContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Treat> model = _db.Treats.ToList();
      ViewBag.PageTitle = "Treats";
      return View(model);
    }

    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Create(Treat treat)
    {
        _db.Treats.Add(treat);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }
    public ActionResult Details(int id)
    {
        Treat thisTreat = _db.Treats
                                .Include (treat => treat.JoinEntities)
                                .ThenInclude(join => join.Flavor)
                                .FirstOrDefault(treat => treat.TreatId == id);
        return View(thisTreat);
    }

    public ActionResult Edit(int id)
    {
        Treat thisTreat = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
       
        return View(thisTreat);
    }

    [HttpPost]
    public ActionResult Edit(Treat treat)
    {
        _db.Treats.Update(treat);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }
    public ActionResult Delete(int id)
    {
        Treat thisTreat = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
        ViewBag.PageTitle = "Delete Treat";
        return View(thisTreat);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
        Treat thisTreat = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
        _db.Treats.Remove(thisTreat);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }
    public ActionResult AddFlavor(int id)
    {
      Treat thisTreat = _db.Treats.FirstOrDefault(treats => treats.TreatId == id);
      List<Flavor> flavors = _db.Flavors.ToList();
      SelectList flavorList = new SelectList(flavors, "FlavorId", "FlavorName");
      ViewBag.FlavorId = flavorList;
      return View(thisTreat);
    }

    [HttpPost]
    public ActionResult AddFlavor(Treat treat, int flavorId)
    {
      #nullable enable
      FlavorTreat? joinEntity = _db.FlavorTreats.FirstOrDefault(join => (join.FlavorId == flavorId && join.TreatId == treat.TreatId));
      #nullable disable
      if (joinEntity == null && flavorId != 0)
      {
        _db.FlavorTreats.Add(new FlavorTreat() { FlavorId = flavorId, TreatId = treat.TreatId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = treat.TreatId });
    }   
  }
}