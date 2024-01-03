using HSP.Common;
using HSP.Entities;
using System.Linq.Expressions;

namespace HSP.Data.Abstractions;

public interface IRepository<T> where T : BaseEntity
{
    IQueryable<T> Query();
    IQueryable<T> Query(Expression<Func<T, bool>> predicate);
    IQueryable<T> Query<TKey>(Expression<Func<T, TKey>> selector, OrderByType orderByType = OrderByType.ASC);
    IQueryable<T> Query<TKey>(Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> selector, OrderByType orderByType = OrderByType.ASC);
    List<T> GetAll();
    List<T> GetAll(Expression<Func<T, bool>> predicate);
    List<T> GetAll<TKey>(Expression<Func<T, TKey>> selector, OrderByType orderByType = OrderByType.ASC);
    List<T> GetAll<TKey>(Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> selector, OrderByType orderByType = OrderByType.ASC);
    T Find(object id);
    T GetByFilter(Expression<Func<T, bool>> filter, bool asNoTracking = false);
    IQueryable<T> GetQuery();
    void Remove(T entity);
    void Create(T entity);
    void Update(T entity, T unchanged);
    void Update(T entity);
}
