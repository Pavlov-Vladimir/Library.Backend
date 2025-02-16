using Library.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.DataAccess;
public class ApplicationDbContext : DbContext
{
    public DbSet<Book> Books { get; set; } = null!;
    public DbSet<Rating> Ratings { get; set; } = null!;
    public DbSet<Review> Reviews { get; set; } = null!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {  }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Book>(BookConfigure);
        builder.Entity<Review>(ReviewConfigure);
        builder.Entity<Rating>(RatingConfigure);
    }

    private void BookConfigure(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(b => b.Id);
        builder.HasIndex(b => b.Id).IsUnique();
        builder.Property(b => b.Title).IsRequired().HasMaxLength(255);
        builder.Property(b => b.Author).IsRequired().HasMaxLength(100);
        builder.Property(b => b.Genre).HasMaxLength(50);
        builder.HasMany(b => b.Ratings).WithOne(r => r.Book).OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(b => b.Reviews).WithOne(r => r.Book).OnDelete(DeleteBehavior.Cascade);
    }

    private void RatingConfigure(EntityTypeBuilder<Rating> builder)
    {
        builder.HasKey(r => r.Id);
        builder.HasIndex(r => r.Id).IsUnique();
        builder.Property(r => r.Score).IsRequired();
        builder.HasOne(r => r.Book).WithMany(b => b.Ratings)
            .IsRequired().OnDelete(DeleteBehavior.Cascade).HasForeignKey("BookId");
    }

    private void ReviewConfigure(EntityTypeBuilder<Review> builder)
    {
        builder.HasKey(r => r.Id);
        builder.HasIndex(r => r.Id).IsUnique();
        builder.Property(r => r.Reviewer).IsRequired().HasMaxLength(50);
        builder.Property(r => r.Message).IsRequired();
        builder.HasOne(r => r.Book).WithMany(b => b.Reviews)
            .IsRequired().OnDelete(DeleteBehavior.Cascade).HasForeignKey("BookId");
    }
}
