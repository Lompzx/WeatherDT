import { Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { FavoriteComponent } from './pages/favorite/favorite.component';
import { authGuard } from './core/guards/auth.guard';
import { guestGuard } from './core/guards/guest.guard';

export const routes: Routes = [
 {
    path: 'login',
    canActivate: [guestGuard],
    loadComponent: () =>
      import('./pages/login/login.component')
        .then(m => m.LoginComponent)
  },
  {
    path: 'forecast',
    component: HomeComponent,
    canActivate: [authGuard]
  },
  {
    path: 'favorites',
    component: FavoriteComponent,
    canActivate: [authGuard]
  },
  {
    path: '',
    redirectTo: 'forecast',
    pathMatch: 'full'
  },
  {
    path: '**',
    redirectTo: 'forecast'
  }
];
