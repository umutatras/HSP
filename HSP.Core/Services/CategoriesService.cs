using HSP.Core.Dtos.Categories;
using HSP.Core.IServices;
using HSP.Data.UnitOfWork;
using HSP.Entities;

namespace HSP.Core.Services;

public class CategoriesService:ICategoriesService
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoriesService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public string Add(CategoryAddDto dto)
    {
        if(dto == null)
        {
            throw new Exception("lütfen name alanını doldudurunuz");
        }
        _unitOfWork.GetRepository<Categories>().Create(new Categories
        {
            Name = dto.Name,

        });
        if(_unitOfWork.SaveChanges()>0)
        {
            return "Kategori Eklemesi başarılı";
        }
        return "Kategori Eklemesi başarısız";
       
    }

    public List<CategoryListDto> GetCategories()
    {
        var categories = _unitOfWork.GetRepository<Categories>().GetQuery()
             .Select(s => new CategoryListDto()
             {
                 Id=s.Id,
                 Name=s.Name,
             })
             .ToList();
        return categories;
    }

    public void Delete(int id)
    {
        var currentEntity=_unitOfWork.GetRepository<Categories>().Find(id);
        if(currentEntity != null)
        {
            _unitOfWork.GetRepository<Categories>().Remove(currentEntity);
            _unitOfWork.SaveChanges();
        }
    }

    public CategoryListDto Update(CategoryUpdateDto dto)
    {
        var category=_unitOfWork.GetRepository<Categories>().Find(dto.Id);
        if(category == null)
        {
            throw new Exception("Category bulunamadı");
        }
        category.Name = dto.Name;
        _unitOfWork.GetRepository<Categories>().Update(category);
        _unitOfWork.SaveChanges();
        return new CategoryListDto { Id = category.Id, Name = category.Name, };
    }
}
