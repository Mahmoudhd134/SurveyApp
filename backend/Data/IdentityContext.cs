using backend.Models;

namespace backend.Data;

using backend.Models.IdentityModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using backend.EntityConfigurations;

public class IdentityContext : IdentityDbContext<User, Role, string>
{
    public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Survey> Surveys { get; set; }
    public DbSet<SurveyCategory> SurveyCategories { get; set; }
    public DbSet<SurveyAnswer> SurveyAnswers { get; set; }
    public DbSet<SurveyOption> SurveyOptions { get; set; }

    public IdentityContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new RoleConfiguration());
        
        builder.ApplyConfiguration(new UserRefreshTokenConfiguration());
        builder.ApplyConfiguration(new SurveyConfiguration());
        builder.ApplyConfiguration(new CategoryConfiguration());
        builder.ApplyConfiguration(new SurveyCategoryConfiguration());
        builder.ApplyConfiguration(new SurveyAnswerConfiguration());
        builder.ApplyConfiguration(new SurveyOptionConfiguration());
        base.OnModelCreating(builder);
    }
}