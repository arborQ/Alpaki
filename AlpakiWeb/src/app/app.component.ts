import { Component } from '@angular/core';
import { of } from 'rxjs';
import { CurrentUserService } from 'src/current-user.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.less']
})
export class AppComponent {
  constructor(private currentUserService: CurrentUserService) {}
  title = 'AlpakiWeb';
  $isAuthorized = this.currentUserService.$isAuthorized;
  onSignOut() {
    this.currentUserService.clearCurrentUser();
  }
}
