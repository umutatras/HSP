using HSP.Core.Dtos.Categories;
using HSP.Core.Dtos.Incomes;
using HSP.Core.IServices;
using HSP.Data.UnitOfWork;
using HSP.Entities;

namespace HSP.Core.Services;

public class IncomesService : IIncomesService
{
    private readonly IUnitOfWork _unitOfWork;

    public IncomesService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public string Add(IncomesAddDto dto)
    {
        if (dto == null)
        {
            throw new Exception("lütfen name alanını doldudurunuz");
        }
        _unitOfWork.GetRepository<Incomes>().Create(new Incomes
        {
            Name = dto.Name,
            Description = dto.Description,
            InDate = dto.InDate,
            Price = dto.Price

        });
        if (_unitOfWork.SaveChanges() > 0)
        {
            return "Incomes Eklemesi başarılı";
        }
        return "Incomes Eklemesi başarısız";

    }

    public void Delete(int id)
    {
        var currentEntity = _unitOfWork.GetRepository<Incomes>().Find(id);
        if (currentEntity != null)
        {
            _unitOfWork.GetRepository<Incomes>().Remove(currentEntity);
            _unitOfWork.SaveChanges();
        }
    }

    public List<IncomesListDto> GetIncomes()
    {
        var incomes = _unitOfWork.GetRepository<Incomes>().GetQuery()
           .Select(s => new IncomesListDto()
           {
               Id = s.Id,
               Name = s.Name,
               Description=s.Description,
               InDate= s.InDate,
               Price = s.Price
               
           })
           .ToList();
        return incomes;
    }

    public IncomesListDto Update(IncomesUpdateDto dto)
    {
        var incomes = _unitOfWork.GetRepository<Incomes>().Find(dto.Id);
        if (incomes == null)
        {
            throw new Exception("İncomes bulunamadı");
        }
        incomes.Name = dto.Name;
        _unitOfWork.GetRepository<Incomes>().Update(incomes);
        _unitOfWork.SaveChanges();
        return new IncomesListDto { Id = incomes.Id, Name = incomes.Name,Price=incomes.Price,InDate=incomes.InDate,Description=incomes.Description };
    }
}
