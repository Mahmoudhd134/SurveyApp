using backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backend.EntityConfigurations;

public class SurveyAnswerConfiguration:IEntityTypeConfiguration<SurveyAnswer>
{
    public void Configure(EntityTypeBuilder<SurveyAnswer> builder)
    {
        builder.HasKey(sa => sa.Id);

        builder
            .HasOne(sa => sa.User)
            .WithMany()
            .HasForeignKey(sa => sa.UserId);

        builder
            .HasOne(sa => sa.Survey)
            .WithMany(s => s.SurveyAnswers)
            .HasForeignKey(sa => sa.SurveyId);

        builder
            .HasOne(sa => sa.SurveyOption)
            .WithMany(so => so.SurveyAnswers)
            .HasForeignKey(sa => sa.SurveyOptionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(sa => new { sa.UserId, sa.SurveyOptionId }).IsUnique();
        builder.HasIndex(sa => new { sa.UserId, sa.SurveyId }).IsUnique();
    }
}