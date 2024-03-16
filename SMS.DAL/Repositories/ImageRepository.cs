
using SMS.DAL.Data.Database_Context;
using SMS.DAL.Data.Entities.Abstract;
using SMS.DAL.Data.Entities.Concrete;
using SMS.DAL.Data.Entities.Contracts;
using SMS.DAL.Repositories.Contracts;
using SMS.DAL.Repositories.Main;
using SMS.WebTools.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.DAL.Repositories
{
    [Injectible(
    Lifetime = Microsoft.Extensions.DependencyInjection.ServiceLifetime.Scoped,
    Implements = typeof(IImageRepository))]
    public class ImageRepository : CoreRepository<Image, CoreDbContext>, IImageRepository
    {
        public ImageRepository(CoreDbContext context) : base(context)
        {
        }

        public new async Task<int> Add(Image image)
        {
            if (image is null)
                throw new ArgumentNullException(nameof(image));

            if (typeof(Image).GetInterface(nameof(IDateStample)) is not null)
            {
                var dateStampled = (IDateStample)image;
                dateStampled.CreatedAt ??= DateTime.UtcNow;
                dateStampled.UpdatedAt ??= DateTime.UtcNow;
            }

            if (typeof(Image).GetInterface(nameof(IAuthorStample)) is not null)
            {
                var authorStampled = (IAuthorStample)image;
                authorStampled.CreatedBy ??= "super_admin_will_be_changed_to_user_entity";
                authorStampled.LastUpdatedBy ??= "super_admin_will_be_changed_to_user_entity";
            }

            await _entities.AddAsync(image);
            await _context.SaveChangesAsync();

            return image.Id;
        }
    }
}
