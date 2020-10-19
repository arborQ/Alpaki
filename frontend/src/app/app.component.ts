import { ChangeDetectorRef, Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { combineLatest, of } from 'rxjs';
import { MediaMatcher } from '@angular/cdk/layout';
import { CurrentUserService } from 'src/current-user.service';
import { Router, NavigationStart } from '@angular/router';
import { MatSidenav } from '@angular/material/sidenav';
import { map } from 'rxjs/operators';
import { ApplicationType } from './authorize/sign-in/sign-in.models';

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

  menuPositions$ = this.currentUserService.currentUser.pipe(map(currentUser => {
    let menuOptions = [];

    if (!currentUser) {
      menuOptions = [...menuOptions,
      { label: 'Zaloguj się', path: '/authorize/sign-in' },
      { label: 'Rejestracja', path: '/authorize/register' },
      ];
    }

    if ((currentUser?.applicationType & ApplicationType.Dream) === ApplicationType.Dream) {
      menuOptions = [...menuOptions,
        { label: 'Marzenia', path: '/dreams/list' },
        ];
    }

    if ((currentUser?.applicationType & ApplicationType.Moto) === ApplicationType.Moto) {
      menuOptions = [...menuOptions,
        { label: 'Marki samochodów', path: '/moto/brands' },
        { label: 'Usterki', path: '/moto/car/issues' }
        ];
    }

    return menuOptions;
  }));

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
