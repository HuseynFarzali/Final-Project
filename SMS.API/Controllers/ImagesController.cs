using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMS.DAL.Data.Database_Context;
using SMS.DAL.Data.Entities.Concrete;
using SMS.DAL.Repositories.Contracts;
using SMS.Tools.Enums;

namespace SMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize("Binded Users Only")]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;
        private readonly CoreDbContext _coreDbContext;

        public ImagesController(IImageRepository imageRepository, CoreDbContext coreDbContext)
        {
            _imageRepository = imageRepository;
            _coreDbContext = coreDbContext;
        }

        private static string GenerateRandomImageIdentifier()
        {
            var random = new Random();
            int imageIdentifier = random.Next(10000, 99999);
            return "Img" + imageIdentifier;
        }

        [HttpPost]
        public async Task<IActionResult> PostImage(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                return BadRequest("No image uploaded");
            }

            int? imageId;

            using (var memoryStream = new MemoryStream())
            {
                await imageFile.CopyToAsync(memoryStream);

                var newImage = new Image
                {
                    ImageName = (imageFile.Name ?? "Unnamed image") + "—" +GenerateRandomImageIdentifier(),
                    ContentType = imageFile.ContentType,
                    Data = memoryStream.ToArray()
                };

                imageId = await _imageRepository.Add(newImage);
            }

            return Ok(new { ImageId = imageId.Value });
        }

        [HttpGet("{imageId:int}")]
        public async Task<IActionResult> RetrieveImage([FromRoute] int imageId, [FromQuery] bool loadImage = true)
        {
            var (image, result) = await _imageRepository.TryGet(imageId);

            if (image == null)
            {
                return BadRequest("No image found with given image-id.");
            }

            if (loadImage)
                return File(image.Data, image.ContentType, image.ImageName);
            else
                return Ok(new Image { Id = imageId, ContentType = image.ContentType });
        }

        [HttpPut("set-image/{imageId:int}/to-{userTypeName}/{entityId:int}")]
        public async Task<IActionResult> SetUserImage(
            [FromRoute] int entityId, [FromRoute] int imageId, [FromRoute] string userTypeName)
        {
            var (image, result) = await _imageRepository.TryGet(imageId);

            if (image is null)
            {
                return NotFound("Image not found with the given id.");
            }

            if (userTypeName is nameof(UserType.Student))
            {
                var student = await _coreDbContext.Students.FindAsync(entityId);

                if (student is null)
                {
                    return NotFound("Student not found with the given id.");
                }

                student.ImageId = imageId;
            }
            else if (userTypeName is nameof(UserType.Teacher))
            {
                var teacher = await _coreDbContext.Teachers.FindAsync(entityId);

                if (teacher is null)
                {
                    return NotFound("Teacher not found with the given id.");
                }

                teacher.ImageId = imageId;
            }
            else
            {
                return BadRequest($"Image set is not supported for userType: {userTypeName}");
            }

            await _coreDbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("set-background-image/{imageId:int}/to-{entityTypeName}/{entityId:int}")]
        public async Task<IActionResult> SetEntityBackgroundImage(
            [FromRoute] int entityId, [FromRoute] int imageId, [FromRoute] string entityTypeName)
        {
            var (image, result) = await _imageRepository.TryGet(imageId);

            if (image is null)
            {
                return NotFound("Image not found with the given id.");
            }

            if (entityTypeName == typeof(Course).Name)
            {
                var course = _coreDbContext.Courses.Find(entityId);

                if (course is null)
                {
                    return NotFound("Course not found with the given id.");
                }

                course.BackgroundImageId = imageId;
            }
            else if (entityTypeName == typeof(Category).Name)
            {
                var category = _coreDbContext.Categories.Find(entityId);

                if (category is null)
                {
                    return NotFound("Category not found with the given id.");
                }

                category.BackgroundImageId = imageId;
            }
            else
            {
                return BadRequest($"Image set is not supported for entityType: {entityTypeName}");
            }

            await _coreDbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
