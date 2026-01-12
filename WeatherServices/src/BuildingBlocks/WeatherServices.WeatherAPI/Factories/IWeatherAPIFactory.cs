using WeatherServices.WeatherAPI.WeatherAPI.DTOs;

namespace WeatherServices.WeatherAPI.Factories;

public interface IWeatherAPIFactory
{    
    Task<WeatherForecast?> GetDaysForecastAsync(string city,int quantityDay, CancellationToken cancellationToken);
}
