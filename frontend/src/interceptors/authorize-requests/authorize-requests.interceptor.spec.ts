import { TestBed } from '@angular/core/testing';

import { AuthorizeRequestsInterceptor } from './authorize-requests.interceptor';

describe('AuthorizeRequestsInterceptor', () => {
  beforeEach(() => TestBed.configureTestingModule({
    providers: [
      AuthorizeRequestsInterceptor
      ]
  }));

  it('should be created', () => {
    const interceptor: AuthorizeRequestsInterceptor = TestBed.inject(AuthorizeRequestsInterceptor);
    expect(interceptor).toBeTruthy();
  });
});
