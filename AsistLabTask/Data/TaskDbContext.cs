using AsistLabTask.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AsistLabTask.Data
{
    public class TaskDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options) { }

        public DbSet<Document> Documents { get; set; } = default!;
        public DbSet<Comment> Comments { get; set; } = default!;
        public DbSet<DocumentHistoryEvent> DocumentHistoryEvents { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Document>(b =>
            {
                b.HasKey(d => d.Id);
                b.HasOne(d => d.Owner)
                    .WithMany(u => u.Documents)
                    .HasForeignKey(d => d.OwnerId)
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasMany(d => d.Comments)
                    .WithOne(c => c.Document)
                    .HasForeignKey(c => c.DocumentId)
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasMany(d => d.ArchiveEvents)
                    .WithOne(e => e.Document)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Comment>(b =>
            {
                b.HasKey(c => c.Id);
                b.HasOne(c => c.Author)
                    .WithMany()
                    .HasForeignKey(c => c.AuthorId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<DocumentHistoryEvent>(b =>
            {
                b.HasKey(e => e.Id);
                b.HasOne(e => e.Document)
                    .WithMany(d => d.ArchiveEvents)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            //UserSeed.Seed(builder);
        }
    }
}
