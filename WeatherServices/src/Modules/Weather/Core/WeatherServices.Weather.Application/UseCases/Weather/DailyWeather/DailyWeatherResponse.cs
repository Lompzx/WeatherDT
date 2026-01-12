namespace WeatherServices.Weather.Application.UseCases.DailyWeather;

public sealed class DailyWeatherResponse
{
    public required string City { get; init; }    
    
    public required decimal Temperature { get; init; }
    public required decimal FeelsLike { get; init; }
    public int Humidity { get; init; }
    public required string Condition { get; init; }
    public required string Icon { get; init; }
    public decimal MinTemperature { get; init; }
    public decimal MaxTemperature { get; set; }
}
