using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EfCoreSQLiteDemo.Models;

namespace EfCoreSQLiteDemo.Data.Configurations;

public sealed class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> entity)
    {
        entity.Property(c => c.Name).IsRequired().HasMaxLength(50);
    }
}