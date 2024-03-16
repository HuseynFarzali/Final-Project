using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query;
using SMS.BLL.Data_Transfer_Objects;
using SMS.BLL.Services.Contracts;
using SMS.DAL.Data.Entities.Concrete;

namespace SMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseCrudService _courseCrudService;

        public CoursesController(ICourseCrudService courseCrudService)
        {
            _courseCrudService = courseCrudService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var courseDtos = await _courseCrudService.GetRange(
                includePathQuery: query => query
                .Include(course => course.CourseStudents)!
                    .ThenInclude(cs => cs.Student)
                .Include(course => course.CourseTeachers)!
                    .ThenInclude(ct => ct.Teacher)
                .Include(course => course.CourseModules)!
                    .ThenInclude(cm => cm.Module)!
                        .ThenInclude(module => module!.LessonModules)!
                            .ThenInclude(lm => lm.Lesson)!,

                predicate: course => !course.IsDeleted);

            return Ok(courseDtos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var course = await _courseCrudService.Get(
                id: id,
                includePathQuery: query => query
                .Include(course => course.CourseStudents)!
                    .ThenInclude(cs => cs.Student)
                .Include(course => course.CourseTeachers)!
                    .ThenInclude(ct => ct.Teacher)!
                .Include(course => course.CourseModules)!
                    .ThenInclude(cm => cm.Module)!);

            return Ok(course);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CourseCreateDto dto)
        {
            if (ModelState.IsValid is false)
                return BadRequest(ModelState);

            await _courseCrudService.Add(dto);
            return Ok();
        }

        [HttpPost("all")]
        public async Task<IActionResult> Post([FromBody] IEnumerable<CourseCreateDto> dtos)
        {
            if (ModelState.IsValid is false)
                return BadRequest(ModelState);

            await _courseCrudService.AddRange(dtos);
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] CourseUpdateDto dto)
        {
            if (ModelState.IsValid is false)
                return BadRequest(ModelState);

            await _courseCrudService.Update(id, dto);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (ModelState.IsValid is false)
                return BadRequest(ModelState);

            await _courseCrudService.Delete(id);
            return Ok();
        }
    }
}
