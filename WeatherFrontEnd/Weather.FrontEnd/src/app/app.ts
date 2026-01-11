import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router'; 

@Component({
  selector: 'app-root',
  imports: [CommonModule, RouterModule],
  templateUrl: './app.html',
  styleUrl: './app.css',
  standalone: true
})
export class App {
logout() {
    localStorage.removeItem('access_token'); 
    localStorage.removeItem('userId');
     window.location.href = '/login';
 }  
}