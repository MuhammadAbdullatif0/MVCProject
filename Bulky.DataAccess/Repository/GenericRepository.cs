using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IGenericRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bulky.DataAccess.Repository;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly AppDBContext _db;
    public GenericRepository(AppDBContext appDBContext)
    {
        _db = appDBContext;
    }
    public void Add(T entity)
    {
        _db.Set<T>().Add(entity);
    }

    public void Delete(T entity)
    {
        _db.Set<T>().Remove(entity);
    }

    public void DeleteRange(IEnumerable<T> entities)
    {
        _db.Set<T>().RemoveRange(entities);
    }

    public T Get(Expression<Func<T, bool>> filter , string? NavigationProperty = null)
    {
        IQueryable<T> query = _db.Set<T>().Where(filter);
        if (!string.IsNullOrEmpty(NavigationProperty))
        {
            foreach (var n in NavigationProperty.
                Split(',', StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(n);
            }
        }
        return query.FirstOrDefault();
    }

    public IEnumerable<T> GetAll(string? NavigationProperty = null)
    {
        IQueryable<T> query = _db.Set<T>();
        if (!string.IsNullOrEmpty(NavigationProperty))
        {
            foreach (var n in NavigationProperty.
                Split(',', StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(n);
            }
        }
        return query.ToList();
    }
}
