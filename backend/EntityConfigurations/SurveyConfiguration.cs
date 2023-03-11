using backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backend.EntityConfigurations;

public class SurveyConfiguration:IEntityTypeConfiguration<Survey>
{
    public void Configure(EntityTypeBuilder<Survey> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Question).HasMaxLength(511).IsRequired();

        builder
            .HasOne(s => s.User)
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasMany(x => x.SurveyOptions)
            .WithOne(x => x.Survey)
            .OnDelete(DeleteBehavior.Cascade);
    }
}