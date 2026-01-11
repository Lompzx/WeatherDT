export interface ForecastWeather {
  city: string;
  days: DailyForecast[];
}

export interface DailyForecast {
  date: Date;
  minTemp: number;
  maxTemp: number;
  condition: string;
  icon: string;
}
