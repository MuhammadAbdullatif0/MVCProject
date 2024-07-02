using System.Linq.Expressions;

namespace Bulky.DataAccess.Repository.IGenericRepository;

public interface IGenericRepository<T> where T : class
{
    IEnumerable<T> GetAll(string? NavigationProperty = null);
    T Get(Expression<Func<T ,bool>> filter , string? NavigationProperty = null);
    void Add(T entity);
    void Delete(T entity);
    void DeleteRange(IEnumerable<T> entities);
}
