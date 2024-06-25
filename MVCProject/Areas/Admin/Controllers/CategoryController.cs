using Microsoft.AspNetCore.Mvc;
using Bulky.Models;
using Bulky.DataAccess.Repository.IGenericRepository;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVCProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _dbContext;
        public CategoryController(IUnitOfWork db)
        {
            _dbContext = db;
        }
        public IActionResult Index()
        {
            List<Category> categories = _dbContext.CategoryRepository.GetAll().ToList();
            
            return View("category", categories);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "Not right to add name is equal to order");
            }
            if (ModelState.IsValid)
            {
                _dbContext.CategoryRepository.Add(obj);
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
            Category? category = _dbContext.CategoryRepository.Get(e => e.Id == id);
            if (category == null)
            {
                return BadRequest();
            }
            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _dbContext.CategoryRepository.Update(obj);
                _dbContext.Save();
                TempData["Message"] = "Updated Successfully!";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Delete(int? id)
        {
            Category category = _dbContext.CategoryRepository.Get(e => e.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            _dbContext.CategoryRepository.Delete(category);
            _dbContext.Save();
            TempData["Message"] = "Deleted Successfully!";
            return RedirectToAction("Index");
        }
    }
}
