import { Injectable } from '@angular/core';
import { DailyWeather } from '../models/daily-weather.model';
import { ForecastWeather } from '../models/forecast-weather.model';
import { HttpClient } from '../../core/http/http-client';

@Injectable({
  providedIn: 'root'
})
export class WeatherService extends HttpClient {

  constructor() {
    super('https://localhost:7152/api/v1/');
  }

  async getGetDailyWeatherByCityAsync(name: string): Promise<DailyWeather> {
    return this.get<DailyWeather>(`weather/current-daily?name=${name}`);
  }

  async getGetForecastWeatherByCityAsync(name: string): Promise<ForecastWeather> {
    return this.get<ForecastWeather>(`weather/weather-forecast?name=${name}`);
  }
}
