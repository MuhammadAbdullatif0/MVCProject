using Microsoft.AspNetCore.Mvc;
using Bulky.Models;
using System.Diagnostics;
using Bulky.DataAccess.Repository.IGenericRepository;

namespace MVCProject.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _db;
        public HomeController(ILogger<HomeController> logger , IUnitOfWork db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> list = _db.productRepository.GetAll("Category").ToList();
            return View(list);
        }
        public IActionResult Details(int productId)
        {
            Product product = _db.productRepository.Get(p => p.Id == productId, "Category");
            return View(product);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}