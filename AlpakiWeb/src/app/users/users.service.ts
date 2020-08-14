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

export interface IGetUsersRequest {
  page: number;
  asc: boolean;
  sortBy: keyof (IUser);
}

export interface IGetUsersResponse {
  users: IUser[];
  totalCount: number;
  loading: boolean;
}

@Injectable({
  providedIn: 'root'
})
export class UsersService {
  private userResponseSubject: BehaviorSubject<IGetUsersResponse>
    = new BehaviorSubject<IGetUsersResponse>({ users: [], totalCount: 0, loading: true });
  constructor(private http: HttpClient) { }


  users({ page = 0, asc = true, sortBy = 'email' }: Partial<IGetUsersRequest>) {
    this.http.get<IGetUsersResponse>(`/api/user?page=${page}&asc=${asc}&orderBy=${sortBy}`)
    .subscribe(response => {
      this.userResponseSubject.next(response);
    });

    return this.userResponseSubject.asObservable();
  }


  updateUser(userId: number): void {
    // const edited = { userId, firstName: 'EDITED' };
    // const users = [...this._users.value.map(u => u.userId !== userId ? u : { ...u, ...edited })];
    // this.http.patch('/api/user', edited);
  }

  deleteUser(userId: number): void {
    // const beforeDeleteUsers = this._users.value;
    // const users = [...beforeDeleteUsers.filter(u => u.userId !== userId)];
    // this._users.next(users);

    // this.http.delete(`/api/user?userId=${userId}`).toPromise().catch(() => {
    //   alert('ojojoj');
    //   this._users.next(beforeDeleteUsers);
    // });
  }
}
