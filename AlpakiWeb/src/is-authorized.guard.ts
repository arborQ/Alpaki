import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { CurrentUserService } from './current-user.service';

@Injectable({
  providedIn: 'root'
})
export class IsAuthorizedGuard implements CanActivate {
  constructor(private currentUserService: CurrentUserService) {}
  canActivate(): Observable<boolean> {
    return this.currentUserService.$isAuthorized;
  }
}
