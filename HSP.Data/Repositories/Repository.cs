using HSP.Common;
using HSP.Data.Abstractions;
using HSP.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HSP.Data.Repositories;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    private readonly HspDbContext _context;

    public Repository(HspDbContext context)
    {
        _context = context;
    }

    public IQueryable<T> Query()
    {
        return _context.Set<T>().AsNoTracking().AsQueryable();
    }
    public IQueryable<T> Query(Expression<Func<T, bool>> predicate)
    {
        return _context.Set<T>().Where(predicate).AsNoTracking().AsQueryable();
    }
    public IQueryable<T> Query<TKey>(Expression<Func<T, TKey>> selector, OrderByType orderByType = OrderByType.ASC)
    {
        return orderByType == OrderByType.ASC ? _context.Set<T>().AsNoTracking().OrderBy(selector).AsQueryable() : _context.Set<T>().AsNoTracking().OrderByDescending(selector).AsQueryable();
    }
    public IQueryable<T> Query<TKey>(Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> selector, OrderByType orderByType = OrderByType.ASC)
    {
        return orderByType == OrderByType.ASC ? _context.Set<T>().Where(predicate).AsNoTracking().OrderBy(selector).AsQueryable() : _context.Set<T>().Where(predicate).AsNoTracking().OrderByDescending(selector).AsQueryable();
    }
    //bütün veriyi getirmek için
    public List<T> GetAll()
    {
        return _context.Set<T>().AsNoTracking().ToList();
    }
    //filtreleyerek getir
    public List<T> GetAll(Expression<Func<T, bool>> predicate)
    {
        return _context.Set<T>().Where(predicate).AsNoTracking().ToList();
    }
    //sıralayarak getir
    public List<T> GetAll<TKey>(Expression<Func<T, TKey>> selector, OrderByType orderByType = OrderByType.ASC)
    {
        return orderByType == OrderByType.ASC ? _context.Set<T>().AsNoTracking().OrderBy(selector).ToList() : _context.Set<T>().AsNoTracking().OrderByDescending(selector).ToList();
    }
    //hem sıralı hem filtreli getir
    public List<T> GetAll<TKey>(Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> selector, OrderByType orderByType = OrderByType.ASC)
    {
        return orderByType == OrderByType.ASC ? _context.Set<T>().Where(predicate).AsNoTracking().OrderBy(selector).ToList() : _context.Set<T>().Where(predicate).AsNoTracking().OrderByDescending(selector).ToList();
    }
    public T Find(object id)
    {
        return _context.Set<T>().Find(id);
    }
    public T GetByFilter(Expression<Func<T, bool>> filter, bool asNoTracking = false)
    {
        return !asNoTracking ? _context.Set<T>().AsNoTracking().SingleOrDefault(filter) : _context.Set<T>().SingleOrDefault(filter);
    }

    public IQueryable<T> GetQuery()
    {
        return _context.Set<T>().AsQueryable();
    }
    public void Remove(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    public void Create(T entity)
    {
        _context.Set<T>().Add(entity);
    }

    public void Update(T entity, T unchanged)
    {
        _context.Entry(unchanged).CurrentValues.SetValues(entity);
    }

    public void Update(T entity)
    {
        _context.Set<T>().Update(entity);
    }
}
