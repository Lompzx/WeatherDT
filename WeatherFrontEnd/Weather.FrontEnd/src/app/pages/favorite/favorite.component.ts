import { Component, ChangeDetectorRef, OnInit} from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AppError } from '../../core/models/app-error.model';
import { User } from '../../core/models/user-favorites';
import { UserService } from '../../core/services/user.service';

@Component({
  selector: 'app-favorite',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './favorite.component.html'
})

export class FavoriteComponent implements OnInit {
  favoriteError?: AppError;
  loading = false;
  user?: User;
  removingId?: string;
  userId? = localStorage.getItem('userId');

constructor(private userService: UserService, private cdr: ChangeDetectorRef){}
  ngOnInit(): void {
    this.search();
  }

async search() : Promise<void> {
   
    this.loading = true;   
    this.favoriteError = undefined;

    try {      
      const result  = await this.userService.getListUserFavoriteCitiesAsync(this.userId!);
      this.user = result;          
    } catch(err){
      this.favoriteError = err as AppError;
    }
    finally{
      this.loading = false;       
      this.cdr.detectChanges();   
    }
  }

  async removeFavorite(uuid: string): Promise<void>{
    this.loading = true;
    this.favoriteError = undefined;
    this.removingId = uuid;

    try{
      await this.userService.deleteFavoriteCitieAsync(this.user!.uuid, uuid);

      if(this.user && this.user.favoriteCities){
        this.user.favoriteCities = this.user.favoriteCities.filter(favorite => favorite.uuid !== uuid);
      }
       
    } catch(err){
      this.favoriteError = err as AppError;
    }
    finally{
      this.loading= false;
      this.removingId = undefined;
      this.cdr.detectChanges();      
    }
  }

  get isEmptyFavorites(): boolean {
  return this.user?.favoriteCities?.length === 0;
 }
}
