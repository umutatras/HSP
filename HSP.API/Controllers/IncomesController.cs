using HSP.Core.Dtos.Incomes;
using HSP.Core.IServices;
using Microsoft.AspNetCore.Mvc;

namespace HSP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomesController : ControllerBase
    {
        private readonly IIncomesService _incomesService;

        public IncomesController(IIncomesService incomesService)
        {
            _incomesService = incomesService;
        }
        [HttpPost("add")]
        public IActionResult Add(IncomesAddDto dto)
        {
            var response = _incomesService.Add(dto);
            return Ok(response);
        }
        [HttpGet("list")]
        public IActionResult List()
        {
            var response = _incomesService.GetIncomes();
            return Ok(response);
        }
        [HttpDelete("delete/{id}")]
        public IActionResult Remove(int id)
        {
            _incomesService.Delete(id);
            return NoContent();

        }
        [HttpPut("update")]
        public IActionResult Update(IncomesUpdateDto dto)
        {
            var response = _incomesService.Update(dto);
            return Ok(response);

        }
    }
}
