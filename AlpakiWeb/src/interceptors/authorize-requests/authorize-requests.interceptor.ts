import { CurrentUserService } from '../../current-user.service';
import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class AuthorizeRequestsInterceptor implements HttpInterceptor {
  constructor() {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    const authorizeToken = localStorage.getItem('authorize');

    if (authorizeToken) {
      request = request.clone({ headers: request.headers.set('authorization', authorizeToken) });
    }

    return next.handle(request);
  }
}
