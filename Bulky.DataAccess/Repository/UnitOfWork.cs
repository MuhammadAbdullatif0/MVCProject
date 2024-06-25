using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IGenericRepository;

namespace Bulky.DataAccess.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDBContext _db;
    public ICategoryRepository CategoryRepository { get; private set; }

    public IProductRepository productRepository { get; private set; }

    public UnitOfWork(AppDBContext context)
    {
        _db = context;
        CategoryRepository = new CategoryRepository(_db);
        productRepository = new ProductRepository(_db);
    }

    public void Save()
    {
        _db.SaveChanges();
    }
}
