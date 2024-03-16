using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using SMS.DAL.Data.Entities.Abstract;
using SMS.DAL.Data.Entities.Concrete;
using SMS.Tools.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.DAL.Data.Database_Context
{
    public class CoreDbContext : DbContext
    {
        public CoreDbContext(DbContextOptions<CoreDbContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseStudent> CourseStudents { get; set; }
        public DbSet<CourseTeacher> CourseTeachers { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<AppUser> AppUsers { get; set; }

        public DbSet<Module> Modules { get; set; }
        public DbSet<Lesson> Lessons { get; set; }

        public DbSet<CourseModule> CourseModules { get; set; }
        public DbSet<LessonModule> LessonModules { get; set; }

        public DbSet<Message> Messages { get; set; }
        public DbSet<Chat> Chats { get; set; }

        public DbSet<ChatUser> ChatUsers { get; set; }

        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChatUser>()
                .HasOne(cu => cu.AppUser)
                .WithMany(user => user.ChatUsers)
                .HasForeignKey(cu => cu.AppUserId);

            modelBuilder.Entity<ChatUser>()
                .HasOne(cu => cu.Chat)
                .WithMany(chat => chat.ChatUsers)
                .HasForeignKey(cu => cu.ChatId);

            modelBuilder.Entity<ChatUser>()
                .HasKey(cu => new { cu.ChatId, cu.AppUserId });

            modelBuilder.Entity<Message>()
                .HasOne(message => message.Chat)
                .WithMany(chat => chat.Messages)
                .HasForeignKey(message => message.ChatId);

            modelBuilder.Entity<Message>()
                .HasOne(message => message.AppUser);

            modelBuilder.Entity<CourseModule>()
                .HasOne(cm => cm.Course)
                .WithMany(course => course.CourseModules)
                .HasForeignKey(cm => cm.CourseId);

            modelBuilder.Entity<CourseModule>()
                .HasOne(cm => cm.Module)
                .WithMany(module => module.CourseModules)
                .HasForeignKey(cm => cm.ModuleId);

            modelBuilder.Entity<CourseModule>()
                .HasKey(cm => new { cm.CourseId, cm.ModuleId });

            modelBuilder.Entity<LessonModule>()
                .HasOne(lm => lm.Lesson)
                .WithMany(lesson => lesson.LessonModules)
                .HasForeignKey(lm => lm.LessonId);

            modelBuilder.Entity<LessonModule>()
                .HasOne(lm => lm.Module)
                .WithMany(module => module.LessonModules)
                .HasForeignKey(lm => lm.ModuleId);

            modelBuilder.Entity<LessonModule>().HasKey(lm => new { lm.LessonId, lm.ModuleId });

            modelBuilder.Entity<CourseStudent>()
                .HasKey(cs => new { cs.CourseId, cs.StudentId });
            modelBuilder.Entity<CourseTeacher>()
                .HasKey(ct => new { ct.CourseId, ct.TeacherId });

            modelBuilder.Entity<CourseStudent>()
                .HasOne(cs => cs.Student)
                .WithMany(s => s.CourseStudents)
                .HasForeignKey(cs => cs.StudentId);

            modelBuilder.Entity<CourseTeacher>()
                .HasOne(ct => ct.Teacher)
                .WithMany(t => t.CourseTeachers)
                .HasForeignKey(ct => ct.TeacherId);

            modelBuilder.Entity<CourseStudent>()
                .HasOne(cs => cs.Course)
                .WithMany(c => c.CourseStudents)
                .HasForeignKey(cs => cs.CourseId);

            modelBuilder.Entity<CourseTeacher>()
                .HasOne(ct => ct.Course)
                .WithMany(c => c.CourseTeachers)
                .HasForeignKey(ct => ct.CourseId);


            modelBuilder.Entity<Student>()
                .HasOne(student => student.AppUser);

            modelBuilder.Entity<Student>()
                .HasOne(student => student.Image);

            modelBuilder.Entity<Teacher>()
                .HasOne(teacher => teacher.AppUser);

            modelBuilder.Entity<Teacher>()
                .HasOne(teacher => teacher.Image);

            modelBuilder.Entity<Course>()
                .HasOne(course => course.Category)
                .WithMany(category => category.Courses);

            modelBuilder.Entity<Course>()
                .HasOne(course => course.BackgroundImage);

            modelBuilder.Entity<Category>()
                .HasOne(category => category.BackgroundImage);

            modelBuilder.Entity<Image>()
                .Property(image => image.Data)
                .HasColumnType("bytea");
        }
    }
}
