﻿namespace SMS.App.Models
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public List<CourseDto>? Courses { get; set; }

        public int? BackgroundImageId { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? LastUpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class CategoryCreateDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
    }

    public class CategoryUpdateDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }

        public List<int>? CoursesIds { get; set; }
    }
}
