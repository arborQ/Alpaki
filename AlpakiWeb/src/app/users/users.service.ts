import { Injectable } from '@angular/core';

export interface IUser {
  userId: number;
  firstName: string;
  lastName: string;
  email: string;
}

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  constructor() { }
}
