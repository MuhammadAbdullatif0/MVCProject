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
    private readonly IWebHostEnvironment _webHost;
    public ProductController(IUnitOfWork db , IWebHostEnvironment webHost)
    {
        _dbContext = db;
        _webHost = webHost;
    }
    public IActionResult Index()
    {
        List<Product> products = _dbContext.productRepository.GetAll().ToList();
        return View("Product", products);
    }
    public IActionResult Upsert(int? id)
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
        if(id is null || id == 0)
        {
            return View(productVM);
        }
        else
        {
            productVM.Product = _dbContext.productRepository.Get(e => e.Id == id);
            if (productVM.Product == null)
            {
                return BadRequest();
            }
            return View(productVM);
        }      
    }
    [HttpPost]
    public IActionResult Upsert(ProductVM obj  , IFormFile? file)
    {
        if (ModelState.IsValid)
        {
            var wwwRootPath = _webHost.WebRootPath;
            if (file != null) {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string productPath = Path.Combine(wwwRootPath, @"images\product");

                using(var fileStream = new FileStream(Path.Combine(productPath , fileName) , FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                obj.Product.ProductImages = @"\images\product\" + fileName;
            }

            _dbContext.productRepository.Add(obj.Product);
            _dbContext.Save();
            TempData["Message"] = "Added Successfully!";
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
