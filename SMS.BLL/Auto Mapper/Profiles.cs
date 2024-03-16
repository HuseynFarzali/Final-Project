using AutoMapper;
using SMS.Authentication.Models;
using SMS.BLL.Data_Transfer_Objects;
using SMS.DAL.Data.Entities.Concrete;
using SMS.Tools.Extensions;
using System.Collections.Immutable;

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
            #region Teacher Mappings
            CreateMap<Teacher, TeacherDto>()
                .ForMember(dto => dto.InstructedCourseIds, options =>
                {
                    options.AllowNull();
                    options.MapFrom(entity => entity.CourseTeachers.Select(ct => ct.CourseId));
                })
                .ForMember(dto => dto.InstructedCourses, options =>
                {
                    options.AllowNull();
                    options.MapFrom(entity => entity.CourseTeachers.Select(ct => ct.Course));
                });

            CreateMap<TeacherCreateDto, Teacher>();

            CreateMap<TeacherUpdateDto, Teacher>()
                .ForMember(entity => entity.CourseTeachers, o => o.MapFrom(dto => dto.InstructedCourseIds.Select(courseId => new CourseTeacher { CourseId = courseId })));

            CreateMap<TeacherDto, TeacherUpdateDto>();
            #endregion
            #region Course mappings
            CreateMap<Course, CourseDto>()
                .ForMember(dto => dto.EnrolledStudentIds, options =>
                {
                    options.AllowNull();
                    options.MapFrom(entity => entity.CourseStudents!.Select(cs => cs.StudentId));
                })
                .ForMember(dto => dto.EnrolledStudents, options =>
                {
                    options.AllowNull();
                    options.MapFrom(entity => entity.CourseStudents!.Select(cs => cs.Student));
                })
                .ForMember(dto => dto.InstructedTeacherIds, options =>
                {
                    options.AllowNull();
                    options.MapFrom(entity => entity.CourseTeachers!.Select(ct => ct.TeacherId));
                })
                .ForMember(dto => dto.InstructedTeachers, options =>
                {
                    options.AllowNull();
                    options.MapFrom(entity => entity.CourseTeachers!.Select(ct => ct.Teacher));
                })
                .ForMember(dto => dto.ModuleIds,
                           o => o.MapFrom(entity => entity.CourseModules!.Select(cm => cm.ModuleId)))
                .ForMember(dto => dto.Modules,
                           o => o.MapFrom(entity => entity.CourseModules!.Select(cm => cm.Module)))
                .ForMember(dto => dto.Duration,
                           o => o.MapFrom(
                               entity => entity.CourseModules!.Aggregate(
                                   new TimeSpan(0,0,0),
                                   (totalSpan, cm) => totalSpan + cm.Module!.LessonModules!.Aggregate(
                                       new TimeSpan(0,0,0),
                                       (spanForEachModule, lm) => spanForEachModule + lm.Lesson!.Duration))))
                .ForMember(dto => dto.LessonCount,
                           o => o.MapFrom(entity => entity.CourseModules!.Aggregate(
                               0, (totalCount, cm) => totalCount + cm.Module!.LessonModules!.Count)));


            CreateMap<CourseCreateDto, Course>();

            CreateMap<CourseUpdateDto, Course>()
                .ForMember(entity => entity.CourseStudents, o =>
                {
                    o.MapFrom(dto => dto.EnrolledStudentIds!.Select(studentId => new CourseStudent { StudentId = studentId }));
                })
                .ForMember(entity => entity.CourseTeachers, o =>
                {
                    o.MapFrom(dto => dto.InstructedTeacherIds!.Select(teacherId => new CourseTeacher { TeacherId = teacherId }));
                });

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
            #region Lesson mappings
            CreateMap<Lesson, LessonDto>()
                .ForMember(dto => dto.UsedInModules,
                           o => o.MapFrom(entity => entity.LessonModules!.Select(lm => lm.Module)));

            CreateMap<LessonCreateDto, Lesson>()
                .ForMember(entity => entity.Duration,
                    opt => opt.MapFrom(dto => new TimeSpan(dto.DurationHours, dto.DurationMinutes, 0)));

            CreateMap<LessonUpdateDto, Lesson>()
                .ForMember(entity => entity.Duration,
                    opt => opt.MapFrom(dto => new TimeSpan(dto.DurationHours, dto.DurationMinutes, 0)));

            CreateMap<LessonDto, LessonUpdateDto>()
                .ForMember(updateDto => updateDto.DurationHours, o => o.MapFrom(dto => dto.Duration.Hours))
                .ForMember(updateDto => updateDto.DurationMinutes, o => o.MapFrom(dto => dto.Duration.Minutes))
                .ForMember(updateDto => updateDto.UsedInModuleIds, o => o.MapFrom(dto => dto.UsedInModules.Select(m => m.Id)));
            #endregion
            #region Module mappings
            CreateMap<Module, ModuleDto>()
                .ForMember(dto => dto.Lessons,
                           o => o.MapFrom(entity => entity.LessonModules!.Select(lm => lm.Lesson)))
                .ForMember(dto => dto.UsedInCourses,
                           o => o.MapFrom(entity => entity.CourseModules!.Select(cm => cm.Course)))
                .ForMember(dto => dto.Duration,
                           o => o.MapFrom(entity => entity.LessonModules!.Aggregate(
                                   new TimeSpan(0, 0, 0), (totalSpan, lm) => totalSpan + lm.Lesson!.Duration)))
                .ForMember(dto => dto.Duration,
                           o => o.MapFrom(
                               entity => entity.LessonModules!.Aggregate(
                                   new TimeSpan(0,0,0), (totalSpan, lm) => totalSpan + lm.Lesson!.Duration)))
                .ForMember(dto => dto.LessonCount,
                           o => o.MapFrom(entity => entity.LessonModules!.Count));

            CreateMap<ModuleCreateDto, Module>()
                .ForMember(entity => entity.CourseModules,
                           o => o.MapFrom(dto => dto.UsedInCourseIds!.Select(courseId => new CourseModule { CourseId = courseId })))
                .ForMember(entity => entity.LessonModules,
                           o => o.MapFrom(dto => dto.LessonIds.Select(lessonId => new LessonModule { LessonId = lessonId })));

            CreateMap<ModuleUpdateDto, Module>()
                .ForMember(entity => entity.CourseModules,
                           o => o.MapFrom(dto => dto.UsedInCourseIds!.Select(courseId => new CourseModule { CourseId = courseId })))
                .ForMember(entity => entity.LessonModules,
                           o => o.MapFrom(dto => dto.LessonIds.Select(lessonId => new LessonModule { LessonId = lessonId })));

            CreateMap<ModuleDto, ModuleUpdateDto>()
                .ForMember(updateDto => updateDto.LessonIds,
                           o => o.MapFrom(dto => dto.Lessons!.Select(lesson => lesson.Id)))
                .ForMember(updateDto => updateDto.UsedInCourseIds,
                           o => o.MapFrom(dto => dto.UsedInCourses!.Select(course => course.Id)));
            #endregion
            #region Message mappings
            CreateMap<Message, MessageDto>();
            CreateMap<MessageCreateDto, Message>();
            CreateMap<MessageUpdateDto, Message>();
            #endregion
            #region Chat mappings
            CreateMap<Chat, ChatDto>()
                .ForMember(dto => dto.AppUsers, o => o.MapFrom(entity => entity.ChatUsers!.Select(cu => cu.AppUser)));
            CreateMap<ChatCreateDto, Chat>()
                .ForMember(entity => entity.ChatUsers, o => o.MapFrom(dto => dto.AppUserIds.Select(userId => new ChatUser { AppUserId = userId })));

            CreateMap<ChatUpdateDto, Chat>()
                .ForMember(entity => entity.ChatUsers, o => o.MapFrom(dto => dto.AppUserIds.Select(userId => new ChatUser { AppUserId = userId })));
            #endregion
        }
    }
}
