import axios, { AxiosInstance } from 'axios';
import { ApiErrorMapper } from '../mappers/api-error.mapper';

export class HttpClient {

  protected http: AxiosInstance;

  constructor(baseURL: string) {
    this.http = axios.create({
      baseURL
    });
    this.configureInterceptors();    
  }

  private configureInterceptors(): void {

    //interceptor auth
    this.http.interceptors.request.use(
      (config) => {
        const token = localStorage.getItem('access_token');

        if (token) {
          config.headers?.set('Authorization',`Bearer ${token}`);     
        }

        return config;
      },
      (error) => Promise.reject(error)
    );    

    this.http.interceptors.response.use(
    (response) => response, 
    (error) => {
      if (error.response?.status === 401) {        
        
        localStorage.removeItem('access_token');
        
        alert('Sua sessão expirou. Faça login novamente.');
        
        window.location.href = '/login';
      }

      return Promise.reject(error);
    }
  );
  }

  protected async get<T>(url: string): Promise<T> {
    try {
      const response = await this.http.get<T>(url);
      return response.data;
    } catch (err) {
      throw ApiErrorMapper.map(err);
    }
  }

  protected async post<T>(url: string, body: any): Promise<T> {
    try {      
          const response = await this.http.post<T>(url, body);  
           
          return  response.data;
    } catch (err) {
      throw ApiErrorMapper.map(err);
    }
  }

  protected async delete(url: string): Promise<void>{
    try{
      await this.http.delete(url);
    } catch(err) {
      throw ApiErrorMapper.map(err);
    }
  }
}
