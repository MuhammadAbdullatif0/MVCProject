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

    public void Update(Product product)
    {
        _db.Update(product);
    }
}
