using AutoMapper;
using SMS.Authentication.Models;
using SMS.BLL.Data_Transfer_Objects;
using SMS.DAL.Data.Entities.Concrete;
using SMS.Tools.Extensions;

namespace SMS.BLL.Auto_Mapper
{
    public class Profiles : AutoMapper.Profile
    {

        public Profiles()
        {
            #region Student mappings
            CreateMap<Student, StudentDto>()
                .ForMember(dto => dto.EnrolledCourseIds, options =>
                {
                    options.AllowNull();
                    options.MapFrom(entity => entity.CourseStudents.Select(cs => cs.CourseId));
                })
                .ForMember(dto => dto.EnrolledCourses, options =>
                {
                    options.AllowNull();
                    options.MapFrom(entity => entity.CourseStudents.Select(cs => cs.Course));
                });

            CreateMap<StudentCreateDto, Student>();

            CreateMap<StudentUpdateDto, Student>()
                .ForMember(entity => entity.CourseStudents, o => o.MapFrom(dto => dto.EnrolledCourseIds.Select(courseId => new CourseStudent { CourseId = courseId })));

            CreateMap<StudentDto, StudentUpdateDto>();
            #endregion
            #region Course mappings
            CreateMap<Course, CourseDto>()
                .ForMember(dto => dto.EnrolledStudentIds, options =>
                {
                    options.AllowNull();
                    options.MapFrom(entity => entity.CourseStudents.Select(cs => cs.StudentId));
                })
                .ForMember(dto => dto.EnrolledStudents, options =>
                {
                    options.AllowNull();
                    options.MapFrom(entity => entity.CourseStudents.Select(cs => cs.Student));
                });

            CreateMap<CourseCreateDto, Course>();

            CreateMap<CourseUpdateDto, Course>()
                .ForMember(entity => entity.CourseStudents, o => o.MapFrom(dto => dto.EnrolledStudentIds.Select(studentId => new CourseStudent { StudentId = studentId })));

            CreateMap<CourseDto, CourseUpdateDto>();
            #endregion
            #region AppUser mappings
            CreateMap<AppUser, TokenRequestModel>();
            CreateMap<RegisterRequestModel, AppUser>();
            CreateMap<RegisterRequestModel, TokenRequestModel>();
            #endregion
            #region Category mappings
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryUpdateDto, Category>()
                .ForMember(entity => entity.Courses, o => o.MapFrom(dto => dto.CoursesIds.Select(courseId => new Course { Id = courseId })));
            #endregion
        }
    }
}
