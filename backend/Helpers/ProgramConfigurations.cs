using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace backend.Helpers;

using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models.IdentityModels;
using Microsoft.AspNetCore.Identity;

public static class ProgramConfigurations
{
    public static void AddCustomConfiguration(this IServiceCollection services, ConfigurationManager configuration)
    {
        //add connections string
        var conStr = configuration.GetSection("connectionStrings")["default"];
        services.AddDbContext<IdentityContext>(opt => opt.UseSqlServer(conStr));

        //add Microsoft Identity
        services.AddDefaultIdentity<User>(opt => opt.SignIn.RequireConfirmedAccount = true)
            .AddRoles<Role>()
            .AddEntityFrameworkStores<IdentityContext>();

        //add autommaper
        services.AddAutoMapper(typeof(Program).Assembly);

        //add MediatR
        services.AddMediatR(opt => { opt.RegisterServicesFromAssemblies(typeof(Program).Assembly); });

        //add cors
        services.AddCors(opt => opt.AddPolicy("allowAll", builder =>
        {
            builder
                .WithOrigins("http://localhost:5173")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
                .Build();
        }));

        //add helper classes configurations
        services.Configure<JWT>(configuration.GetSection("Jwt"));
        services.Configure<Expiry>(configuration.GetSection("Expiry"));

        //add token configuration
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:key"]!)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

        //allow only authenticated users
        services.AddAuthorization(opt =>
            opt.FallbackPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build());
    }
}