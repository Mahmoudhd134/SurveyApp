using backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backend.EntityConfigurations;

public class SurveyCategoryConfiguration:IEntityTypeConfiguration<SurveyCategory>
{
    public void Configure(EntityTypeBuilder<SurveyCategory> builder)
    {
        builder.HasKey(sc => sc.Id);

        builder
            .HasOne(sc => sc.Survey)
            .WithMany(s => s.SurveyCategories);

        builder
            .HasOne(sc => sc.Category)
            .WithMany(c => c.SurveyCategories);

        builder.HasIndex(sc => new { sc.CategoryId, sc.SurveyId }).IsUnique();
    }
}