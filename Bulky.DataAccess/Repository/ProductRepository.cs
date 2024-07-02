using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IGenericRepository;
using Bulky.Models;

namespace Bulky.DataAccess.Repository;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    private readonly AppDBContext _db;

    public ProductRepository(AppDBContext appDBContext) : base(appDBContext)
    {
        _db = appDBContext;
    }

    public void Update(Product obj)
    {
        var objFromDb = _db.Products.FirstOrDefault(u => u.Id == obj.Id);
        if (objFromDb != null)
        {
            objFromDb.Title = obj.Title;
            objFromDb.ISBN = obj.ISBN;
            objFromDb.Price = obj.Price;
            objFromDb.Price50 = obj.Price50;
            objFromDb.ListPrice = obj.ListPrice;
            objFromDb.Price100 = obj.Price100;
            objFromDb.Description = obj.Description;
            objFromDb.CategoryId = obj.CategoryId;
            objFromDb.Author = obj.Author;
            objFromDb.ProductImages = obj.ProductImages;
            if (obj.ProductImages != null)
            {
                objFromDb.ProductImages = obj.ProductImages;
            }
        }
    }
}
