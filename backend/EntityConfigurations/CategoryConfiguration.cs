using backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backend.EntityConfigurations;

public class CategoryConfiguration:IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Name).HasMaxLength(127).IsRequired();
        builder.HasIndex(c => c.Name).IsUnique();
    }
}