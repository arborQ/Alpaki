import { Injectable } from '@angular/core';
import { Observable, BehaviorSubject } from 'rxjs';
import { CurrentUser } from './models/current-user';
import { map } from 'rxjs/operators';
import { ApplicationType } from './app/authorize/sign-in/sign-in.models';
const userStorageKey = 'currentUser';
const tokenStorageKey = 'authorize';

@Injectable({
  providedIn: 'root'
})
export class CurrentUserService {
  private currentUserSubject: BehaviorSubject<CurrentUser>;
  public currentUser: Observable<CurrentUser>;
  public $isAuthorized: Observable<boolean>;

  constructor() {
    this.currentUserSubject = new BehaviorSubject<CurrentUser>(JSON.parse(localStorage.getItem(userStorageKey)));
    this.currentUser = this.currentUserSubject.asObservable();
    this.$isAuthorized = this.currentUser.pipe(map(u => !!u));
  }

  setCurrentUser(login: string, token: string, applicationType: ApplicationType) {
    const currentUser: CurrentUser = { login, expire: new Date(), applicationType };
    localStorage.setItem(userStorageKey, JSON.stringify(currentUser));
    localStorage.setItem(tokenStorageKey, token);
    this.currentUserSubject.next(currentUser);
  }

  clearCurrentUser() {
    localStorage.removeItem(userStorageKey);
    localStorage.removeItem(tokenStorageKey);
    this.currentUserSubject.next(null);
  }
}
