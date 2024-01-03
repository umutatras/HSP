using HSP.Data.Abstractions;
using HSP.Data.Repositories;
using HSP.Entities;

namespace HSP.Data.UnitOfWork;

public class UnitOfWork:IUnitOfWork
{
    private readonly HspDbContext _context;

    public UnitOfWork(HspDbContext context)
    {
        _context = context;
    }
    public IRepository<T> GetRepository<T>() where T : BaseEntity
    {
        return new Repository<T>(_context);
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }
}
