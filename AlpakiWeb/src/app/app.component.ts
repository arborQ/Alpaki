import { ChangeDetectorRef, Component, OnInit, OnDestroy } from '@angular/core';
import { of } from 'rxjs';
import { MediaMatcher } from '@angular/cdk/layout';
import { CurrentUserService } from 'src/current-user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.less']
})
export class AppComponent implements OnDestroy {
  constructor(
    private currentUserService: CurrentUserService,
    private router: Router,
    media: MediaMatcher,
    changeDetectorRef: ChangeDetectorRef
  ) {
    this.mobileQuery = media.matchMedia('(max-width: 600px)');
    this.mobileQueryListener = () => changeDetectorRef.detectChanges();
    this.mobileQuery.addEventListener('change', this.mobileQueryListener);
  }
  mobileQuery: MediaQueryList;
  private mobileQueryListener: () => void;

  $isAuthorized = this.currentUserService.$isAuthorized;

  ngOnDestroy(): void {
    this.mobileQuery.removeEventListener('change', this.mobileQueryListener);
  }

  onSignOut() {
    this.currentUserService.clearCurrentUser();
    this.router.navigate(['/authorize/sign-in']);
  }
}
