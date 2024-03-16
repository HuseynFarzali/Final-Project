using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SMS.BLL.Data_Transfer_Objects;
using SMS.BLL.Services.Contracts;

namespace SMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private IStudentCrudService _studentCrudService;

        public StudentsController(IStudentCrudService studentCrudService)
        {
            _studentCrudService = studentCrudService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var students = await _studentCrudService.GetRange(
                includePathQuery: q => q
                    .Include(student => student.CourseStudents)!
                        .ThenInclude(cs => cs.Course)!
                            .ThenInclude(course => course!.CourseModules)!
                                .ThenInclude(cm => cm.Module)!
                                    .ThenInclude(module => module.LessonModules)!
                                        .ThenInclude(lm => lm.Lesson)!
                                        .Include(student => student.AppUser)!);

            return Ok(students);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var student = await _studentCrudService.Get(id, includePathQuery: q => q
                    .Include(student => student.CourseStudents)!
                        .ThenInclude(cs => cs.Course)!
                            .ThenInclude(course => course!.CourseModules)!
                                .ThenInclude(cm => cm.Module)!
                                    .ThenInclude(module => module.LessonModules)!
                                        .ThenInclude(lm => lm.Lesson)!
                                        .Include(student => student.AppUser)!);
            return Ok(student);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] StudentCreateDto dto)
        {
            if (ModelState.IsValid is false)
                return BadRequest(ModelState);

            await _studentCrudService.Add(dto);
            return Ok();
        }

        [HttpPost("all")]
        public async Task<IActionResult> Post([FromBody] IEnumerable<StudentCreateDto> dtos)
        {
            if (ModelState?.IsValid is false)
                return BadRequest(ModelState);

            await _studentCrudService.AddRange(dtos);
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] StudentUpdateDto dto)
        {
            if (ModelState.IsValid is false)
                return BadRequest(ModelState);

            await _studentCrudService.Update(id, dto);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _studentCrudService.Delete(id);
            return Ok();
        }
    }
}
