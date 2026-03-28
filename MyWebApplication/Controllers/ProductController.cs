using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyWebApplication.Data;
using MyWebApplication.Models;
using MyWebApplication.Models.ViewModels;

namespace MyWebApplication.Controllers;

public class ProductController : Controller
{
    private readonly ApplicationDbContext _db;

    public ProductController(ApplicationDbContext db)
    {
        _db = db;
    }
    
    public IActionResult Index()
    {
        IEnumerable<Product> objList = _db.Product;

        foreach (var obj in objList)
        {
            obj.Category = _db.Category.FirstOrDefault(u => u.Id == obj.CategoryId);
        }
        
        return View(objList);
    }

    //get - Upsert
    public IActionResult Upsert(int? id)
    {
        // IEnumerable<SelectListItem> categoryDropdown = _db.Category.Select(i => new SelectListItem
        // {
        //     Text = i.Name,
        //     Value = i.Id.ToString()
        // });
        //
        // ViewBag.CategoryDropdown = categoryDropdown;

        // Product product = new Product();
        
        ProductVM productVM = new ProductVM()
        {
            Product = new Product(),
            CategorySelectList = _db.Category.Select(i => new SelectListItem
             {
                 Text = i.Name,
                 Value = i.Id.ToString()
             })
        };
        
        
        if (id == null)
        {
            return View(productVM);
        }

        productVM.Product = _db.Product.Find(id);
        if (productVM.Product == null)
        {
            return NotFound();
        }
        return View(productVM);
    }

    //post - Upsert
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Upsert(Product product)
    {
        if (ModelState.IsValid)
        {
            _db.Product.Add(product);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        return View(product);
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