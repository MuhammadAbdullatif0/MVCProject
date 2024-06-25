using Bulky.DataAccess.Repository.IGenericRepository;
using Bulky.Models;
using Bulky.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVCProject.Areas.Admin.Controllers;
[Area("Admin")]
public class ProductController : Controller
{
    private readonly IUnitOfWork _dbContext;
    public ProductController(IUnitOfWork db)
    {
        _dbContext = db;
    }
    public IActionResult Index()
    {
        List<Product> products = _dbContext.productRepository.GetAll().ToList();
        return View("Product", products);
    }
    public IActionResult Create()
    {
        IEnumerable<SelectListItem> list = _dbContext.CategoryRepository.GetAll()
            .Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
        ProductVM productVM = new()
        {
            CategoryList = list,
            Product = new Product()
        };
        return View(productVM);
    }
    [HttpPost]
    public IActionResult Create(ProductVM obj)
    {
        if (ModelState.IsValid)
        {
            _dbContext.productRepository.Add(obj.Product);
            _dbContext.Save();
            TempData["Message"] = "Added Successfully!";
            return RedirectToAction("Index");
        }
        return View();
    }
    public IActionResult Edit(int? id)
    {
        if (id is null || id == 0)
        {
            return NotFound();
        }
        Product? Product = _dbContext.productRepository.Get(e => e.Id == id);
        if (Product == null)
        {
            return BadRequest();
        }
        return View(Product);
    }
    [HttpPost]
    public IActionResult Edit(Product obj)
    {
        if (ModelState.IsValid)
        {
            _dbContext.productRepository.Update(obj);
            _dbContext.Save();
            TempData["Message"] = "Updated Successfully!";
            return RedirectToAction("Index");
        }
        return View();
    }
    public IActionResult Delete(int? id)
    {
        Product Product = _dbContext.productRepository.Get(e => e.Id == id);
        if (Product == null)
        {
            return NotFound();
        }
        _dbContext.productRepository.Delete(Product);
        _dbContext.Save();
        TempData["Message"] = "Deleted Successfully!";
        return RedirectToAction("Index");
    }

}
