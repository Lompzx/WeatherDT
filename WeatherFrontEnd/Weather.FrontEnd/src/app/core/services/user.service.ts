import { Injectable } from '@angular/core';
import { HttpClient } from '../../core/http/http-client';
import { User } from '../models/user-favorites';

@Injectable({
  providedIn: 'root'
})
export class UserService extends HttpClient
{    
    constructor(){
        super('https://localhost:7152/api/v1/');
    }

    async getListUserFavoriteCitiesAsync(uuid: string) : Promise<User>{
        return this.get<User>(`user/${uuid}/list`);
    }

    async postFavoriteCityAsync(uuid: string, city: string) : Promise<void>{
        const body =  {
            city: {
                name: city
            }
        };
        return this.post(`user/${uuid}/favorite-city`, body);
    }

   async deleteFavoriteCitieAsync(userUuid: string, cityUuid: string) {
      return this.delete(`user/${userUuid}/favorite-city/${cityUuid}`);
    }
}