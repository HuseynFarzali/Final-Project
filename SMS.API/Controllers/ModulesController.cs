using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using SMS.BLL.Data_Transfer_Objects;
using SMS.BLL.Services.Contracts;

namespace SMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModulesController : ControllerBase
    {
        private readonly IModuleCrudService _moduleCrudService;

        public ModulesController(IModuleCrudService moduleCrudService)
        {
            _moduleCrudService = moduleCrudService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var modules = await _moduleCrudService.GetRange(
                includePathQuery: query => query
                .Include(module => module.LessonModules)!
                    .ThenInclude(lm => lm.Lesson)
                .Include(module => module.CourseModules)!
                    .ThenInclude(cm => cm.Course)!,
                
                predicate: module => !module.IsDeleted);

            return Ok(modules);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var module = await _moduleCrudService.Get(
                id: id,
                includePathQuery: query => query
                .Include(module => module.LessonModules)!
                    .ThenInclude(lm => lm.Lesson)
                .Include(module => module.CourseModules)!
                    .ThenInclude(cm => cm.Course)!);

            return Ok(module);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ModuleCreateDto dto)
        {
            if (ModelState.IsValid is false)
                return BadRequest(ModelState);

            await _moduleCrudService.Add(dto);
            return Ok();
        }

        [HttpPost("all")]
        public async Task<IActionResult> Post([FromBody] IEnumerable<ModuleCreateDto> dtos)
        {
            if (ModelState.IsValid is false)
                return BadRequest(ModelState);

            await _moduleCrudService.AddRange(dtos);
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] ModuleUpdateDto dto)
        {
            if (ModelState.IsValid is false)
                return BadRequest(ModelState);

            await _moduleCrudService.Update(id, dto);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _moduleCrudService.Delete(id);
            return Ok();
        }
    }
}
