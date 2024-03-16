using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SMS.BLL.Data_Transfer_Objects;
using SMS.BLL.Services.Contracts;

namespace SMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonsController : ControllerBase
    {
        private readonly ILessonCrudService _lessonCrudService;

        public LessonsController(ILessonCrudService lessonCrudService)
        {
            _lessonCrudService = lessonCrudService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var lessons = await _lessonCrudService.GetRange(
                includePathQuery: query => query
                .Include(lesson => lesson.LessonModules)!
                    .ThenInclude(lm => lm.Module)!,

                predicate: lesson => !lesson.IsDeleted);

            return Ok(lessons);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var lesson = await _lessonCrudService.Get(
                id: id,
                includePathQuery: query => query
                .Include(lesson => lesson.LessonModules)!
                    .ThenInclude(lm => lm.Module)!);

            return Ok(lesson);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LessonCreateDto dto)
        {
            if (ModelState.IsValid is false)
                return BadRequest(ModelState);

            await _lessonCrudService.Add(dto);
            return Ok();
        }

        [HttpPost("all")]
        public async Task<IActionResult> Post([FromBody] IEnumerable<LessonCreateDto> dtos)
        {
            if (ModelState.IsValid is false)
                return BadRequest(ModelState);

            await _lessonCrudService.AddRange(dtos);
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] LessonUpdateDto dto)
        {
            if (ModelState.IsValid is false)
                return BadRequest(ModelState);

            await _lessonCrudService.Update(id, dto);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _lessonCrudService.Delete(id);
            return Ok();
        }
    }
}
