using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Hosting;
using WeatherServices.SharedKernel.Configurations.Swagger;

namespace WeatherServices.SharedKernel.Configurations.Api;

public static partial class ApiConfiguration
{
    private const string DefaultCorsPolicy = "Total";
    public static IApplicationBuilder UseApiDefaults(this IApplicationBuilder app, IWebHostEnvironment environment, Action<IApplicationBuilder>? configureCustomMiddleware = null)
    {
        // Be aware of middleware load order: https://learn.microsoft.com/en-us/aspnet/core/fundamentals/middleware/?view=aspnetcore-8.0#middleware-order
        app.UseApiDefaultConfiguration(environment);
        app.UseDefaultExceptionHandlers();
        app.WithCors();
        app.WithAuthenticationAndAuthorization();

        if (configureCustomMiddleware is not null)
            configureCustomMiddleware(app);

        app.WithEndpointMiddleware();

        return app;
    }

    private static IApplicationBuilder UseDefaultExceptionHandlers(this IApplicationBuilder app)
    {
        app.UseExceptionHandler("/Error");

        return app;
    }

    private static IApplicationBuilder UseApiDefaultConfiguration(this IApplicationBuilder app, IWebHostEnvironment environment)
    {
        const string defaultRequestCulture = "en-US";

        if (environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseHttpLogging();            
            app.UseSwaggerConfiguration(app.ApplicationServices);
        }

        app.UseForwardedHeaders();
        app.UseHsts();
        app.UseRouting();

        app.UseRequestLocalization(options =>
        {
            options.DefaultRequestCulture = new RequestCulture(defaultRequestCulture);
        });


        return app;
    }

    private static IApplicationBuilder WithAuthenticationAndAuthorization(this IApplicationBuilder app)
    {
        app.UseAuthentication();
        app.UseAuthorization();

        return app;
    }

    private static IApplicationBuilder WithEndpointMiddleware(this IApplicationBuilder app, Action<IEndpointRouteBuilder>? configureEndpointRouteDelegate = null)
    {
        if (configureEndpointRouteDelegate is not null)
        {
            app.UseEndpoints(endpoints => configureEndpointRouteDelegate(endpoints));            
        }
        else
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        return app;
    }
    private static IApplicationBuilder WithCors(this IApplicationBuilder app, string policy = DefaultCorsPolicy)
    {
        app.UseCors(policy);

        return app;
    }
}
