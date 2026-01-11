import { Component, ChangeDetectorRef, OnInit} from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { WeatherService } from '../../core/services/weather.service';
import { DailyWeather } from '../../core/models/daily-weather.model';
import { AppError } from '../../core/models/app-error.model';
import { ForecastWeather } from '../../core/models/forecast-weather.model';
import { UserService } from '../../core/services/user.service';
import { DailyWeatherState } from '../../core/states/daily-weather.state';
import { ForecastWeatherState } from '../../core/states/forecast-weather.state';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './home.component.html'
})

export class HomeComponent implements OnInit{
  city = '';
  canFavorite = false;
  loading = false;
  dailyWeather?: DailyWeather;
  forecastWeather?: ForecastWeather;
  weatherError?: AppError;  
  favoriteCityError?: AppError;  
  userId? = localStorage.getItem('userId');

  constructor(
    private weatherService: WeatherService,
    private userService :UserService,
    private dailyState : DailyWeatherState,
    private forecastState : ForecastWeatherState,
    private cdr: ChangeDetectorRef) {}

  ngOnInit(): void {
    const dailyForecastCached = this.dailyState.getValue();
    if (dailyForecastCached) {
      this.dailyWeather = dailyForecastCached;
    }

    const weatherForecastCached = this.forecastState.getValue();
    if (weatherForecastCached) {
      this.forecastWeather = weatherForecastCached;
    }       
  }

  async search() : Promise<void> {    

   if (!this.city.trim()) return;  

    this.resetErrors();
    this.loading = true;       

    try {  
       const [currentResult, forecastResult] = await Promise.allSettled([
          this.weatherService.getGetDailyWeatherByCityAsync(this.city),
          this.weatherService.getGetForecastWeatherByCityAsync(this.city)]);

      if (currentResult.status === 'fulfilled') {
           this.dailyWeather = currentResult.value;
           this.canFavorite = true;
           this.dailyState.set(currentResult.value);

      } else {
         this.weatherError = currentResult.reason as AppError;
         return;
       }

     if (forecastResult.status === 'fulfilled') {
        this.forecastWeather = forecastResult.value;
        this.forecastState.set(forecastResult.value);
      } else {
        this.weatherError = forecastResult.reason as AppError;
      }                  
    } finally{
      this.loading = false;       
      this.cdr.detectChanges();   
    }
  }

  async favorite() : Promise<void> {

    if (!this.city.trim())
       return;   
    this.resetErrors();
    this.loading = true;   
  
    try{
      await this.userService.postFavoriteCityAsync(this.userId!, this.city);    
    }
    catch(err){
      this.favoriteCityError = err as AppError;     
    }
    finally{
      this.loading = false;      
      this.cdr.detectChanges();         
    }   
  }

   resetErrors(){
      this.favoriteCityError = undefined;
      this.weatherError = undefined;      
  }
}



