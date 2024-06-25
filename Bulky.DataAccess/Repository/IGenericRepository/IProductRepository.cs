using Bulky.Models;

namespace Bulky.DataAccess.Repository.IGenericRepository;

public interface IProductRepository : IGenericRepository<Product>
{
    void Update(Product product);
}
