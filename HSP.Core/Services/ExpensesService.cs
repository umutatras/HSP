using HSP.Core.Dtos.Categories;
using HSP.Core.Dtos.Expenses;
using HSP.Core.IServices;
using HSP.Data.UnitOfWork;
using HSP.Entities;

namespace HSP.Core.Services;

public class ExpensesService : IExpensesService
{
    private readonly IUnitOfWork _unitOfWork;

    public ExpensesService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public string Add(ExpensesAddDto dto)
    {
        if (dto == null)
        {
            throw new Exception("lütfen name alanını doldudurunuz");
        }
        _unitOfWork.GetRepository<Expenses>().Create(new Expenses
        {
            CategoryId = dto.CategoryId,
            Price = dto.Price,
            InDate = dto.InDate,
            UserId = dto.UserId,
            Description = dto.Description,            

        });
        if (_unitOfWork.SaveChanges() > 0)
        {
            return "Kategori Eklemesi başarılı";
        }
        return "Kategori Eklemesi başarısız";
    }

    public void Delete(int id)
    {
        var currentEntity = _unitOfWork.GetRepository<Expenses>().Find(id);
        if (currentEntity != null)
        {
            _unitOfWork.GetRepository<Expenses>().Remove(currentEntity);
            _unitOfWork.SaveChanges();
        }
    }

    public List<ExpensesListDto> GetExpenses()
    {
        var categories = _unitOfWork.GetRepository<Expenses>().GetQuery()
             .Select(s => new ExpensesListDto()
             {
                 Id = s.Id,
                 Description=s.Description,
                 Price = s.Price,
                 InDate=s.InDate
             })
             .ToList();
        return categories;
    }

    public ExpensesListDto Update(ExpensesUpdateDto dto)
    {
        var expenses = _unitOfWork.GetRepository<Expenses>().Find(dto.Id);
        if (expenses == null)
        {
            throw new Exception("Category bulunamadı");
        }
        expenses.Description = dto.Description;
        expenses.InDate = dto.InDate;
        expenses.Price = dto.Price;
        _unitOfWork.GetRepository<Expenses>().Update(expenses);
        _unitOfWork.SaveChanges();
        return new ExpensesListDto { Id = expenses.Id, Description = expenses.Description,InDate=expenses.InDate,Price=expenses.Price };
    }
}
