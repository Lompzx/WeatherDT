export interface User {
  uuid: string;
  name: string;
  favoriteCities: UserFavoriteCity[];
}

export interface UserFavoriteCity {
  name:string;
  uuid:string;
}