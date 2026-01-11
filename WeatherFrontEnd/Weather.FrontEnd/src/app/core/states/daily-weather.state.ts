import { Injectable } from "@angular/core";
import { StateService } from "../services/state.service";
import { DailyWeather } from "../models/daily-weather.model";


@Injectable({ providedIn: 'root' })
export class DailyWeatherState extends StateService<DailyWeather> {
  constructor() {
    //Initialize contructor of base class
    super(null);
  }
}