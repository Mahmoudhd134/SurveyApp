using backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backend.EntityConfigurations;

public class SurveyOptionConfiguration : IEntityTypeConfiguration<SurveyOption>
{
    public void Configure(EntityTypeBuilder<SurveyOption> builder)
    {
        builder.HasKey(so => so.Id);
        builder.Property(so => so.Option).HasMaxLength(255).IsRequired();
        builder
            .HasOne(so => so.Survey)
            .WithMany(s => s.SurveyOptions)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(so => so.SurveyId);
        builder.HasIndex(so => so.Option);
    }
}