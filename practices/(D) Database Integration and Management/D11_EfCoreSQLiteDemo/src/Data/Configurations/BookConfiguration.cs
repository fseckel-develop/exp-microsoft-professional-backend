using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EfCoreSQLiteDemo.Models;

namespace EfCoreSQLiteDemo.Data.Configurations;

public sealed class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> entity)
    {
        entity.Property(b => b.Title).IsRequired().HasMaxLength(120);
        entity.Property(b => b.Author).IsRequired().HasMaxLength(80);
        entity.Property(b => b.AddedAt).IsRequired();

        entity.HasOne(b => b.Category)
              .WithMany(c => c.Books)
              .HasForeignKey(b => b.CategoryId)
              .OnDelete(DeleteBehavior.Restrict);
    }
}