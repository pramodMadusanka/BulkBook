using Microsoft.AspNetCore.Mvc;
using BulkBookWeb.Data;
using BulkBookWeb.Models;

namespace BulkBookWeb.Controllers;

public class CategoryController : Controller
{
    private readonly ApplicationDbContext _db;
    public CategoryController(ApplicationDbContext db)
    {
        _db = db;
    }
    public IActionResult Index()
    {
        IEnumerable<Category> categoryEnumarable = _db.Categories;
        return View(categoryEnumarable);
    }
    public IActionResult Create()
    {
        
        return View();
    }
    [HttpPost]
    public IActionResult Create(Category obj)
    {
        IEnumerable<Category> categoryEnumarable = _db.Categories;
        if(!ModelState.IsValid) return View(obj);
        foreach(var category in categoryEnumarable)
            if(category.Name == obj.Name)
            {
                ModelState.AddModelError("name", $"The category \"{obj.Name}\" already exists");
                return View();
            }

        _db.Categories.Add(obj);
        _db.SaveChanges();
        TempData["success"] = "Category created successfully";
        return RedirectToAction("Index");
    }
    public IActionResult Edit(int? id)
    {
        if(id==null || id==0) return NotFound();

        var categoryObj = _db.Categories.Find(id);

        if(categoryObj == null) return NotFound();

        return View(categoryObj);
    }
    [HttpPost]
    public IActionResult Edit(Category obj)
    {
        if(!ModelState.IsValid) return View(obj);
        foreach(var category in _db.Categories)
            if(category.Name == obj.Name)
            {
                ModelState.AddModelError("name", $"The category \"{obj.Name}\" already exists");
                return View(obj);
            }

        _db.Categories.Update(obj);
        _db.SaveChanges();
        TempData["success"] = "Category updated successfully";
        return RedirectToAction("Index");
    }
    public IActionResult Delete(int? id)
    {
        if(id==null || id==0) return NotFound();
        var category = _db.Categories.Find(id);
        if(category == null) return NotFound();
        return View(category);
    }
    [HttpPost, ActionName("Delete")]
    public IActionResult DeletePOST(int id)
    {
        var obj = _db.Categories.Find(id);
        _db.Categories.Remove(obj);
        _db.SaveChanges();
        TempData["success"] = "Category deleted successfully";
        return RedirectToAction("Index");
    }
    /* public IActionResult Delete(Category obj)
    {
        _db.Categories.Remove(obj);
        _db.SaveChanges();
        return RedirectToAction("Index");
    } */
}