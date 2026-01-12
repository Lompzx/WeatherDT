using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WeatherServices.DatabaseManagement.IoC;
using WeatherServices.Identity.Application.UseCases.AuthenticateUser;
using WeatherServices.Identity.Domain.Interfaces;
using WeatherServices.Identity.Infraestructure.Persistence;
using WeatherServices.Identity.Infraestructure.Security.Hash;
using WeatherServices.Identity.Infraestructure.Security.Jwt;
using WeatherServices.Identity.Ports.In;
using WeatherServices.Identity.Ports.Out;
using WeatherServices.SharedKernel.Core.Patterns.Options;

namespace WeatherServices.Identity.IoC;

public static class IdentityServiceCollectionExtensions
{
    private readonly static string SectionName = "Jwt";
    public static IServiceCollection AddWeatherIdentity(
        this IServiceCollection services,
        IConfiguration configuration)
    {

        services.RegisterOptionsOf<JwtOptions>(SectionName);       

        services.AddScoped<IAuthenticateUser, AuthenticateUserHandler>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();
        services.AddScoped<ITokenService, JwtTokenService>();

        services.AddAuthentication(configuration);

        services.AddDapperFacade(configuration);

        return services;
    }

    private static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            JwtOptions jwt = configuration.GetSection("Jwt").Get<JwtOptions>()!;

            options.RequireHttpsMetadata = false; // dev
            options.SaveToken = true;

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = jwt.Issuer,
                ValidAudience = jwt.Audience,

                IssuerSigningKey = new SymmetricSecurityKey(
               Encoding.UTF8.GetBytes(jwt.Key)
               ),
                ClockSkew = TimeSpan.Zero
            };

        });

        return services;
    }
}
