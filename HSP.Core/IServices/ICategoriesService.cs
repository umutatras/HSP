using HSP.Core.Dtos.Categories;

namespace HSP.Core.IServices;

public interface ICategoriesService
{
    string Add(CategoryAddDto dto);
    List<CategoryListDto> GetCategories();
    void Delete(int id);
}
