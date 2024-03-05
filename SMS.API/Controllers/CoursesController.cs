using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] bool detailed = false)
        {
            IEnumerable<CourseDto>? courseDtos;

            if (detailed)
                courseDtos = await _courseCrudService.GetRange(includePathQuery: query => query.Include(course => course.CourseStudents).ThenInclude(cs => cs.Student), predicate: course => !course.IsDeleted);

            else courseDtos = await _courseCrudService.GetRange(predicate: course => !course.IsDeleted);

            return Ok(courseDtos);
        }

        [HttpGet("paginate")]
        public async Task<IActionResult> GetRange(
            [FromQuery] bool detailed = false, [FromQuery] int? take = null, [FromQuery] int? skip = null)
        {
            IEnumerable<CourseDto>? courseDtos;

            if (detailed)
                courseDtos = await _courseCrudService.GetRange(includePathQuery: query => query.Include(course => course.CourseStudents).ThenInclude(cs => cs.Student), predicate: course => !course.IsDeleted);

            else courseDtos = await _courseCrudService.GetRange(predicate: course => !course.IsDeleted);

            var cachedResults = courseDtos;
            if (skip.HasValue)
            {
                cachedResults = courseDtos.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                cachedResults = courseDtos.Take(take.Value);
            }

            return Ok(cachedResults);
        }
    }
}
