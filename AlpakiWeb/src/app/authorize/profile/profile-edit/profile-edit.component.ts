import { Component, OnInit, Input } from '@angular/core';
import { ICurrentUserProfile } from '../profile.service';

@Component({
  selector: 'app-profile-edit',
  templateUrl: './profile-edit.component.html',
  styleUrls: ['./profile-edit.component.less']
})
export class ProfileEditComponent implements OnInit {
  inEditMode = false;
  @Input() profile: ICurrentUserProfile;
  constructor() { }

  ngOnInit(): void {
  }

  toggleEditMode() {
    this.inEditMode = !this.inEditMode;
  }
}
