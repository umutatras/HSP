using HSP.Core.Dtos;
using HSP.Core.Dtos.Categories;
using HSP.Core.IServices;
using HSP.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace HSP.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoriesService _categoryService;

    public CategoriesController(ICategoriesService categoryService)
    {
        _categoryService = categoryService;
    }
    [HttpPost("add")]
    public IActionResult Add(CategoryAddDto dto)
    {
        var response =  _categoryService.Add(dto);
        return Ok(response);
    }
    [HttpGet("list")]
    public IActionResult List()
    {
        var response = _categoryService.GetCategories();
        return Ok(response);
    }
    [HttpDelete("delete/{id}")]
    public IActionResult Remove(int id)
    {
       _categoryService.Delete(id);
        return NoContent();
      
    }

}
