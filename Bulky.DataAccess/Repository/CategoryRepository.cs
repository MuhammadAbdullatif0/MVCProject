using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IGenericRepository;
using Bulky.Models;

namespace Bulky.DataAccess.Repository;

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    private readonly AppDBContext _appDBContext;
    public CategoryRepository(AppDBContext appDBContext) : base(appDBContext)
    {
        _appDBContext = appDBContext;
    }
    void ICategoryRepository.Update(Category category)
    {
        _appDBContext.Update(category);
    }
}
