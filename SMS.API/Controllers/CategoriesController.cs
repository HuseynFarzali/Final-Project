using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SMS.BLL.Data_Transfer_Objects;
using SMS.BLL.Services.Contracts;

namespace SMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryCrudService _categoryCrudService;

        public CategoriesController(ICategoryCrudService categoryCrudService)
        {
            _categoryCrudService = categoryCrudService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll([FromQuery] bool detail = false)
        {
            IEnumerable<CategoryDto> categories;
            if (detail)
            {
                categories = await _categoryCrudService.GetRange(
                    predicate: c => !c.IsDeleted,
                    includePathQuery: query => query.Include(category => category.Courses));
            }
            else
            {
                categories = await _categoryCrudService.GetRange(
                    predicate: c => !c.IsDeleted);
            }


            return Ok(categories);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CategoryCreateDto dto)
        {
            await _categoryCrudService.Add(dto);
            return Ok();
        }

        [HttpPost("range")]
        public async Task<IActionResult> Post([FromBody] IEnumerable<CategoryCreateDto> dtos)
        {
            await _categoryCrudService.AddRange(dtos);
            return Ok();
        }
    }
}
