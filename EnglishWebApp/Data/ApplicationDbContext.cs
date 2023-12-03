using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EnglishWebApp.Models;

namespace EnglishWebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Theme> Themes { get; set; }
        public DbSet<Word> Words { get; set; }
        public DbSet<FillTheGapTask> FillTheGapTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Word>()
                .HasOne(w => w.Theme)
                .WithMany(t => t.Words)
                .HasForeignKey(w => w.ThemeId);

            builder.Entity<FillTheGapTask>()
                .HasOne(f => f.Theme)
                .WithMany(t => t.FillTasks)
                .HasForeignKey(f => f.ThemeId);

            builder.Entity<Word>()
                .HasMany(w => w.FillTasks)
                .WithMany(f => f.Words);
        }
    }
}