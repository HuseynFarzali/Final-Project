using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using SMS.Authentication.Models;
using SMS.Authentication.Services.Contracts;
using SMS.BLL;
using SMS.BLL.Data_Transfer_Objects;
using SMS.BLL.Services;
using SMS.BLL.Services.Contracts;
using SMS.DAL.Data.Database_Context;
using SMS.DAL.Data.Entities.Concrete;
using SMS.DAL.Repositories.Contracts;
using SMS.Tools.Enums;
using System.Runtime.CompilerServices;

namespace SMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        //private readonly IStudentCrudService _studentCrudService;
        //private readonly ICourseCrudService _courseCrudService;
        //private readonly IAppUserRepository _userRepository;
        //private readonly IAppAuthenticationService _authService;
        //private readonly IMapper _mapper;

        //public TestController(IStudentCrudService studentCrudService, ICourseCrudService courseCrudService, IAppUserRepository userRepository, IAppAuthenticationService authService, IMapper mapper)
        //{
        //    _studentCrudService = studentCrudService;
        //    _courseCrudService = courseCrudService;
        //    _userRepository = userRepository;
        //    _authService = authService;
        //    _mapper = mapper;
        //}

        //[HttpPost("student-create")]
        //public async Task<IActionResult> StudentPostAsync([FromBody] StudentCreateDto dto)
        //{
        //    await _studentCrudService.Add(dto);
        //    return Ok(dto);
        //}

        //[HttpPut("student-update/{id:int}")]
        //public async Task<IActionResult> StudentPutAsync([FromRoute] int id, [FromBody] StudentUpdateDto dto)
        //{
        //    await _studentCrudService.Update(id, dto);
        //    return Ok();
        //}

        //[HttpDelete("boundStudent-delete")]
        //public async Task<IActionResult> DeleteAsync(int studentId)
        //{
        //    await _studentCrudService.Delete(studentId);
        //    return Ok();
        //}

        //[HttpPut("course-update/{id:int}")]
        //public async Task<IActionResult> CoursePutAsync([FromRoute] int id, [FromBody] CourseUpdateDto dto)
        //{
        //    await _courseCrudService.Update(id, dto);
        //    return Ok();
        //}

        //[HttpPost("course-create")]
        //public async Task<IActionResult> CoursePostAsync([FromBody] CourseCreateDto dto)
        //{
        //    await _courseCrudService.Add(dto);
        //    return Ok(dto);
        //}
        

        //[HttpGet("detailed")]
        //public async Task<IActionResult> IndexDetailed()
        //{
        //    var studentDtos = await _studentCrudService.GetRange(
        //        includePathQuery: query => query.Include(student => student.CourseStudents).ThenInclude(cs => cs.Course));

        //    return Ok(studentDtos);
        //}

        //[HttpGet("detailedCourse")]
        //public async Task<IActionResult> IndexCourseDetailed()
        //{
        //    var courseDtos = await _courseCrudService.GetRange(
        //        includePathQuery: query => query.Include(course => course.CourseStudents).ThenInclude(cs => cs.Student));

        //    return Ok(courseDtos);
        //}
        
        //[HttpGet("users")]
        //public async Task<IActionResult> Users()
        //{
        //    var appUserDtos = await _userRepository.GetRange();
        //    return Ok(appUserDtos);
        //}

        ////[HttpPut("user-connect")]
        ////public async Task<IActionResult> UserConnect(int appUserId, int entityId, UserType userType)
        ////{
        ////    await _authService.ConnectEntity(appUserId, entityId, userType);
        ////    return Ok();
        ////}

        //[HttpPost("login")]
        //public async Task<IActionResult> LoginAsync(LoginRequestModel model)
        //{
        //    return Ok(await _authService.Login(model));
        //}

        //[HttpPost("register")]
        //public async Task<IActionResult> Register(RegisterRequestModel model)
        //{
        //    return Ok(await _authService.Register(model));
        //}

        //[HttpPost("mocking")]
        //public IActionResult Mock([FromBody] StudentCreateDto s)
        //{
        //    return Ok();
        //}
    }
}
