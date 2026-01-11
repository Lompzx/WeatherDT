import { Injectable } from '@angular/core';
import { HttpClient } from '../../core/http/http-client';
import { LoginRequest } from '../../core/models/auth.model';
import { LoginResponse } from '../models/auth-response.model';

@Injectable({
  providedIn: 'root'
}
)export class AuthService extends HttpClient
{    
    constructor(){
        super('https://localhost:7152/api/v1/');
    }    

    async postLogin(loginRequest: LoginRequest) : Promise<LoginResponse>{
        const body =  loginRequest ;
        const response = await this.post<LoginResponse>('user/login', body);
         localStorage.setItem('access_token', response.accessToken);
         localStorage.setItem('userId', response.userId);
        return response
    }

    isAuthenticated(): boolean {
        return !!localStorage.getItem('access_token');
    }
}