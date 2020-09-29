import { CurrentUserService } from '../../current-user.service';
import { Injectable } from '@angular/core';
import { CanActivate } from '@angular/router';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class IsAnonymousGuard implements CanActivate {
  constructor(private currentUserService: CurrentUserService) {}

  canActivate(): Observable<boolean> {
    return this.currentUserService.$isAuthorized.pipe(map(isAuthorized => !isAuthorized));
  }
}
