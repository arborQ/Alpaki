import { Component } from '@angular/core';
import { of } from 'rxjs';
import { CurrentUserService } from 'src/current-user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.less']
})
export class AppComponent {
  constructor(private currentUserService: CurrentUserService, private router: Router) { }
  title = 'AlpakiWeb';
  $isAuthorized = this.currentUserService.$isAuthorized;
  onSignOut() {
    this.currentUserService.clearCurrentUser();
    this.router.navigate(['/authorize/sign-in']);
  }
}
