using Microsoft.AspNetCore.Mvc;
using MyWebApplication.Data;
using MyWebApplication.Models;

namespace MyWebApplication.Controllers;

public class CategoryController : Controller
{
    private readonly ApplicationDbContext _db;

    public CategoryController(ApplicationDbContext db)
    {
        _db = db;
    }

    public IActionResult Index()
    {
        IEnumerable<Category> objList = _db.Category;
        return View(objList);
    }

    //get - create
    public IActionResult Create()
    {
        return View();
    }

    //post - create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Category category)
    {
        if (ModelState.IsValid)
        {
            _db.Category.Add(category);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        return View(category);
    }

    public IActionResult Delete()
    {
        throw new NotImplementedException();
    }

    //Get - Edit
    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        var obj = _db.Category.Find(id);
        if (obj == null || id == 0)
        {
            return NotFound();
        }

        return View(obj);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Category category)
    {
        if (ModelState.IsValid)
        {
            _db.Category.Update(category);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        return View(category);
    }

    //Get - Delete
    [HttpGet]
    public IActionResult Delete(int? id)
    {
        var obj = _db.Category.Find(id);
        if (obj == null || id == 0)
        {
            return NotFound();
        }

        return View(obj);
    }

    //Post - Delete
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePost(int? id)
    {
        var obj = _db.Category.Find(id);
        if (obj != null) 
            _db.Category.Remove(obj);
        
        _db.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
}