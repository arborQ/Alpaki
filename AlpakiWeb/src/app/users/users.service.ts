import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { map, switchMap } from 'rxjs/operators';

export interface IUser {
  userId: number;
  firstName: string;
  lastName: string;
  email: string;
}

export interface IGetUsersResponse {
  users: IUser[];
}

@Injectable({
  providedIn: 'root'
})
export class UsersService {
  private _users: BehaviorSubject<IUser[]> = new BehaviorSubject<IUser[]>([]);
  constructor(private http: HttpClient) { }


  users() {
    this.http.get<IGetUsersResponse>('/api/users').subscribe(users => {
      this._users.next(users.users);
    });

    return this._users;
  }


  updateUser(userId: number): void {
    this.http.patch('/api/users', {});
  }

  deleteUser(userId: number): void {
    const beforeDeleteUsers = this._users.value;
    const users = [...beforeDeleteUsers.filter(u => u.userId !== userId)];
    this._users.next(users);

    this.http.delete(`/api/users?userId=${userId}`).toPromise().catch(() => {
      alert('ojojoj');
      this._users.next(beforeDeleteUsers);
    });
  }
}
