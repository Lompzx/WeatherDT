using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using WeatherServices.WeatherAPI.Weather;
using WeatherServices.WeatherAPI.WeatherAPI.DTOs;
using WeatherServices.WeatherAPI.WeatherAPI.DTOs.Internal;

namespace WeatherServices.WeatherAPI.Factories;

internal sealed class WeatherAPIFactory : IWeatherAPIFactory
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IOptions<WeatherOptions> _options;
    public WeatherAPIFactory(IHttpClientFactory httpClientFactory, IOptions<WeatherOptions> options)
    {
        _httpClientFactory = httpClientFactory;       
        _options = options;

    }  

    public async Task<WeatherForecast?> GetDaysForecastAsync(string city, int quantityDay, CancellationToken cancellationToken)
    {
        string _city = Uri.EscapeDataString($"{city}");

        string url = $"v1/forecast.json?key={_options.Value.ApiKey}&q={_city}&days={quantityDay}&lang=pt";

        HttpClient client = _httpClientFactory.CreateClient(WeatherOptions.SectionName);

        using HttpResponseMessage response = await client.GetAsync(url, cancellationToken);

        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException($"Error fetching weather data: {response.StatusCode}");

        WeatherApiForecastResponse? forecastResponse = await response.Content.ReadFromJsonAsync<WeatherApiForecastResponse>(cancellationToken);
        
        if (forecastResponse is null)
            return null;

        return new WeatherForecast
        {
            City = forecastResponse!.Location.Name,
            Teperature = forecastResponse.Current.Temp_c,
            Feelslike = forecastResponse.Current.FeelsLike_c,
            Humidity = forecastResponse.Current.Humidity,
            Icon = forecastResponse.Current.Condition.Icon,
            Text = forecastResponse.Current.Condition.Text,
            Days = forecastResponse.Forecast.ForecastDay.Select(day => new DailyForecast
            {
                Date = DateOnly.Parse(day.Date),
                MinTemp = day.Day.MinTemp_c,
                MaxTemp = day.Day.MaxTemp_c,
                Condition = day.Day.Condition.Text,
                Icon = day.Day.Condition.Icon
            }).ToList()
        };   
    }
}
