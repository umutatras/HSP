using HSP.Core.Dtos.Categories;
using HSP.Core.IServices;
using HSP.Entities;
using Moq;
using Shouldly;

namespace Hsp.Api.UnitTest
{
    public class CategoriesTest
    {
        private readonly Mock<ICategoriesService> _companyService;

        public CategoriesTest()
        {
            _companyService = new();
        }

        [Fact]
        public void Test1()
        {
            List<CategoryListDto> categories = _companyService.Object.GetCategories();
            categories.ShouldBeNull();
        }
        [Fact]
        public void Test2() {
           var  category = new CategoryAddDto
            {                
                Name="umut bilgisayar"
            };
            
            var response = _companyService.Object.Add(category);

            response.ShouldBeNull(response);
        }
    }
}