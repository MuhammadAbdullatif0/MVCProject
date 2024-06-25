namespace Bulky.DataAccess.Repository.IGenericRepository;

public interface IUnitOfWork
{
    ICategoryRepository CategoryRepository { get; }
    IProductRepository productRepository { get; }
    void Save();
}
