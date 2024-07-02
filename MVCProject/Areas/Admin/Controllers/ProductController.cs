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
        List<Product> products = _dbContext.productRepository.GetAll(NavigationProperty: "Category").ToList();
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

                if (!string.IsNullOrEmpty(obj.Product.ProductImages))
                {
                    var oldPath = Path.Combine(wwwRootPath, obj.Product.ProductImages.Trim('\\'));
                    if (System.IO.File.Exists(oldPath)) { 
                        System.IO.File.Delete(oldPath);
                    }
                }

                using(var fileStream = new FileStream(Path.Combine(productPath , fileName) , FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                obj.Product.ProductImages = @"\images\product\" + fileName;
            }
            if(obj.Product.Id == 0)
            {
                _dbContext.productRepository.Add(obj.Product);
            }
            else
            {
                _dbContext.productRepository.Update(obj.Product);
            }           
            _dbContext.Save();
            TempData["Message"] = "Added Successfully!";
            return RedirectToAction("Index");
        }
        return View();
    }

    #region API Call
    [HttpGet]
    public IActionResult GetAll() {
        List<Product> objProductList = _dbContext.productRepository.GetAll(NavigationProperty: "Category").ToList();
        return Json(objProductList);
    }
    [HttpDelete]
    public IActionResult Delete(int? id)
    {
        var productToBeDeleted =  _dbContext.productRepository.Get(u => u.Id == id);
        if (productToBeDeleted == null)
        {
            return Json(new { success = false, message = "Error while deleting" });
        }

        string productPath = @"images\products\product-" + id;
        string finalPath = Path.Combine(_webHost.WebRootPath, productPath);

        if (Directory.Exists(finalPath))
        {
            string[] filePaths = Directory.GetFiles(finalPath);
            foreach (string filePath in filePaths)
            {
                System.IO.File.Delete(filePath);
            }

            Directory.Delete(finalPath);
        }


         _dbContext.productRepository.Delete(productToBeDeleted);
        _dbContext.Save();

        return Json(new { success = true, message = "Delete Successful" });
    }

    #endregion
}
