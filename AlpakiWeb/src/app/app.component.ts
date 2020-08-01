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
  mobileQuery: MediaQueryList;
  private _mobileQueryListener: () => void;
  constructor(private currentUserService: CurrentUserService, private router: Router, media: MediaMatcher, changeDetectorRef: ChangeDetectorRef) {
    this.mobileQuery = media.matchMedia('(max-width: 600px)');
    this._mobileQueryListener = () => changeDetectorRef.detectChanges();
    this.mobileQuery.addListener(this._mobileQueryListener);
  }

  ngOnDestroy(): void {
    this.mobileQuery.removeListener(this._mobileQueryListener);
  }

  $isAuthorized = this.currentUserService.$isAuthorized;

  onSignOut() {
    this.currentUserService.clearCurrentUser();
    this.router.navigate(['/authorize/sign-in']);
  }
}
