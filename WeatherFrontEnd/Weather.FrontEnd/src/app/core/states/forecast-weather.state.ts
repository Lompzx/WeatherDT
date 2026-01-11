import { Injectable } from "@angular/core";
import { StateService } from "../services/state.service";
import { ForecastWeather } from "../models/forecast-weather.model";


@Injectable({ providedIn: 'root' })
export class ForecastWeatherState extends StateService<ForecastWeather> {
  constructor() {
    super(null);
  }
}