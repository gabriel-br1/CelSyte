using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CelSyte.Models;

namespace CelSyte.Data
{
    public class CelSyteContext(DbContextOptions<CelSyteContext> options) : IdentityDbContext<User>(options)
    {
        public DbSet<Image> Image { get; set; } = default!;

        public DbSet<Canvas> Canvas { get; set; } = default!;

        public DbSet<CompositionElement> CompositionElement { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Image>()
                .HasMany(i => i.CompositionElements)
                .WithOne(i => i.Image)
                .HasForeignKey(c => c.ImageId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Canvas>()
                .HasMany(c => c.CompositionElements)
                .WithOne(c => c.Canvas)
                .HasForeignKey(c => c.CanvasId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }

}
