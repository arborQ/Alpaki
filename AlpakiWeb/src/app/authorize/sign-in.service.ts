import { logging } from 'protractor';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { CurrentUserService } from '../../current-user.service';
import { ISignInModel } from './sign-in/sign-in.models';

@Injectable({
  providedIn: 'root'
})
export class SignInService {

  constructor(private http: HttpClient, private currentUserService: CurrentUserService) { }

  signIn(login: string, password: string): Observable<boolean> {
    return this.http
      .post('/api/authorize', { login, password })
      .pipe(map((response: ISignInModel) => {
        this.currentUserService.setCurrentUser(response.login, response.token);
        return true;
      }));
  }
}
