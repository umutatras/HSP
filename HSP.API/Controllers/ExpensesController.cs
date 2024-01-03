using HSP.Core.Dtos.Expenses;
using HSP.Core.Dtos.Incomes;
using HSP.Core.IServices;
using HSP.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HSP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly IExpensesService _expService;

        public ExpensesController(IExpensesService expService)
        {
            _expService = expService;
        }
        [HttpPost("add")]
        public IActionResult Add(ExpensesAddDto dto)
        {
            var response = _expService.Add(dto);
            return Ok(response);
        }
        [HttpGet("list")]
        public IActionResult List()
        {
            var response = _expService.GetExpenses();
            return Ok(response);
        }
        [HttpDelete("delete/{id}")]
        public IActionResult Remove(int id)
        {
            _expService.Delete(id);
            return NoContent();

        }
        [HttpPut("update")]
        public IActionResult Update(ExpensesUpdateDto dto)
        {
            var response = _expService.Update(dto);
            return Ok(response);

        }

    }
}
