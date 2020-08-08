import { Component, OnInit } from '@angular/core';
import { ProfileService } from './profile.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.less']
})
export class ProfileComponent implements OnInit {

  constructor(private profileService: ProfileService) { }

  profile$ = this.profileService.currentUserProfile();

  ngOnInit(): void {
  }

}
