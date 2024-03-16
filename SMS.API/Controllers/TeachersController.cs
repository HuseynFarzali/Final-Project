using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SMS.BLL.Data_Transfer_Objects;
using SMS.BLL.Services.Contracts;

namespace SMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly ITeacherCrudService _teacherCrudService;

        public TeachersController(ITeacherCrudService teacherCrudService)
        {
            _teacherCrudService = teacherCrudService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var teachers = await _teacherCrudService.GetRange(
                includePathQuery: query => query
                .Include(teacher => teacher.AppUser)
                .Include(teacher => teacher.CourseTeachers!)
                    .ThenInclude(ct => ct.Course)!
                        .ThenInclude(course => course.CourseStudents)!.
                            ThenInclude(cs => cs.Student)!,

                predicate: teacher => !teacher.IsDeleted,
                enableTracking: true);

            return Ok(teachers);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var teacher = await _teacherCrudService.Get(id);
            return Ok(teacher);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TeacherCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _teacherCrudService.Add(dto);
            return Ok();
        }

        [HttpPost("all")]
        public async Task<IActionResult> CreateAll([FromBody] IEnumerable<TeacherCreateDto> dtos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _teacherCrudService.AddRange(dtos);
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] TeacherUpdateDto dto)
        {
            await _teacherCrudService.Update(id, dto);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _teacherCrudService.Delete(id);
            return Ok();
        }
    }
}
