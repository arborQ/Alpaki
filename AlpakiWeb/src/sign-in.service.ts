import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SignInService {

  constructor(private http: HttpClient) { }

  authorize(login: string, password: string): Observable<{ isSuccess: boolean }> {
    return this.http.post<{ isSuccess: boolean }>('/api/login', { login, password });
  }
}
