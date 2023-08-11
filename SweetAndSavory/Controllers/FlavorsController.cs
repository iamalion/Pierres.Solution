using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SweetAndSavory.Models;
using System.Collections.Generic;
using System.Linq;

namespace SweetAndSavory.Controllers
{
  public class FlavorsController : Controller
  {
    private readonly SweetAndSavoryContext _db;

    public FlavorsController(SweetAndSavoryContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Flavor> model = _db.Flavors.ToList();
      ViewBag.PageTitle = "View Flavors";
      return View(model);
    }

    public ActionResult Create()
    {
      ViewBag.DepartmentId = new SelectList(_db.Departments.OrderBy(d => d.DepartmentName), "DepartmentId", "DepartmentName");
      ViewBag.PageTitle = "Create Flavor";
      return View();
    }

    [HttpPost]
    public ActionResult Create(Flavor flavor)
        
        {
        _db.Flavors.Add(flavor);
        _db.SaveChanges();
        return RedirectToAction("Index");
        }
    }
    public ActionResult Details(int id)
    {
        Flavor thisFlavor = _db.Flavors
                          .Include(flavor => flavor.JoinEntities)
                          .ThenInclude(join => join.Treat)
                          .FirstOrDefault(flavor => flavor.FlavorId == id);
        ViewBag.PageTitle = "Flavor Details";
        return View(thisFlavor);
    }
    public ActionResult Edit(int id)
    {
        Flavor thisFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
        ViewBag.PageTitle = "Edit Flavor";
        return View(thisFlavor);
    }

    [HttpPost]
    public ActionResult Edit(Flavor flavor)
    {
        _db.Flavors.Update(flavor);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }
    public ActionResult Delete(int id)
    {
        Flavor thisFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
        ViewBag.PageTitle = "Delete Flavor";
        return View(thisFlavor);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
        Flavor thisFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
        _db.Flavors.Remove(thisFlavor);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }
    public ActionResult AddTreat(int id)
    {
        Flavor thisFlavor = _db.Flavors.FirstOrDefault(flavors => flavors.FlavorId == id);
        List<Treat> treats = _db.Treats.ToList();
        SelectList treatList = new SelectList(treats, "TreatId", "TreatName");
        ViewBag.TreatId = treatList;
        return View(thisFlavor);
    }

    [HttpPost]
    public ActionResult AddTreat(Flavor flavor, int treatId)
    {
      #nullable enable
      FlavorTreat? joinEntity = _db.FlavorTreats.FirstOrDefault(join => (join.TreatId == treatId && join.FlavorId == flavor.FlavorId));
      #nullable disable
      if (joinEntity == null && treatId != 0)
        {
            _db.FlavorTreats.Add(new FlavorTreat() { TreatId = treatId, FlavorId = flavor.FlavorId });
            _db.SaveChanges();
        }
      return RedirectToAction("Details", new { id = flavor.FlavorId });
    }
    
}


