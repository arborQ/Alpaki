import { Injectable } from '@angular/core';
import { CanActivate, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { CurrentUserService } from 'src/current-user.service';
import { ApplicationType } from './authorize/sign-in/sign-in.models';

abstract class ApplicationGuard implements CanActivate {
  constructor(private applicationType: ApplicationType, private currentUserService: CurrentUserService) { }
  canActivate(): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
    return this.currentUserService.currentUser
      .pipe(map(currentUser => (currentUser.applicationType & this.applicationType) === this.applicationType));
  }
}

@Injectable({
  providedIn: 'root'
})
export class DreamApplicationGuard extends ApplicationGuard {
  constructor(currentUserService: CurrentUserService) {
    super(ApplicationType.Dream, currentUserService);
  }
}

@Injectable({
  providedIn: 'root'
})
export class MotoApplicationGuard extends ApplicationGuard {
  constructor(currentUserService: CurrentUserService) {
    super(ApplicationType.Moto, currentUserService);
  }
}