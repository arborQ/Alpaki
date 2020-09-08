import { ChangeDetectorRef, Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { of } from 'rxjs';
import { MediaMatcher } from '@angular/cdk/layout';
import { CurrentUserService } from 'src/current-user.service';
import { Router, NavigationStart } from '@angular/router';
import { MatSidenav } from '@angular/material/sidenav';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.less']
})
export class AppComponent implements OnDestroy, OnInit {
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
  @ViewChild(MatSidenav) snav: MatSidenav;

  mobileQuery: MediaQueryList;
  private mobileQueryListener: () => void;

  $isAuthorized = this.currentUserService.$isAuthorized;

  ngOnDestroy(): void {
    this.mobileQuery.removeEventListener('change', this.mobileQueryListener);
  }

  ngOnInit(): void {
    this.router.events.subscribe(e => {
      if (e instanceof NavigationStart && this.mobileQuery.matches && this.snav.opened) {
        this.snav.toggle();
      }
    });
  }

  onSignOut() {
    this.currentUserService.clearCurrentUser();
    this.router.navigate(['/authorize/sign-in']);
  }
}
