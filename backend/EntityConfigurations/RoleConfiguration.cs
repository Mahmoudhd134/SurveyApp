using backend.Models.IdentityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backend.EntityConfigurations;

public class RoleConfiguration:IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.Property(r => r.Id).ValueGeneratedOnAdd();
    }
}