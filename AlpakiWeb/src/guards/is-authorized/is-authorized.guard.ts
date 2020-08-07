import { Injectable } from '@angular/core';
import { CanActivate } from '@angular/router';
import { Observable } from 'rxjs';
import { CurrentUserService } from 'src/current-user.service';

@Injectable({
  providedIn: 'root'
})
export class IsAuthorizedGuard implements CanActivate {
  constructor(private currentUserService: CurrentUserService) {}
  canActivate(): Observable<boolean> {
    return this.currentUserService.$isAuthorized;
  }
}
