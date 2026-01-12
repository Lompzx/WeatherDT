using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace WeatherServices.SharedKernel.Configurations.Api;

public sealed class ApiProvider
{
    private readonly IHttpContextAccessor? _httpContextAccessor;
    private readonly ApiDbSettings _settings;
    public ApiProvider(IHttpContextAccessor? httpContextAccessor, IOptions<ApiDbOptions> settingsOptions)
    {
        _httpContextAccessor = httpContextAccessor;
        _settings = settingsOptions.Value.Setting;
    }

    public string GetConnectionString()
    {
        ApiDbSettings apiSettings = GetSettings();

        return apiSettings.ConnectionString;
    }

    private ApiDbSettings GetSettings()
    {
        ApiDbSettings? settings = _settings; // Simplified for single-tenant scenario

        if (settings is null) throw new NotSupportedException("Unable to find Settings");

        return settings;
    }
}
