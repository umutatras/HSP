using HSP.Data.Abstractions;
using HSP.Entities;

namespace HSP.Data.UnitOfWork;

public interface IUnitOfWork
{
    IRepository<T> GetRepository<T>() where T : BaseEntity;
    int SaveChanges();
}
