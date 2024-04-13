using DAL.Entities;
using DAL.Entities.Cloudinary;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class MainContext : DbContext
    {
        public MainContext(DbContextOptions<MainContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<ImagePlaceholder> ImagePlaceholders { get; set; }
        public DbSet<CloudinaryImage> CloudinaryImages { get; set; }
    }
}
