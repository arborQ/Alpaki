import { Component, OnInit } from '@angular/core';
import { ProfileService } from './profile.service';
import { ImagesService } from 'src/app/images.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.less']
})
export class ProfileComponent implements OnInit {

  constructor(private profileService: ProfileService, private imagesService: ImagesService) { }

  profile$ = this.profileService.currentUserProfile();

  ngOnInit(): void {
  }
  onSelectedFilesChanged($event) {
    console.log($event[0])
    this.imagesService.sendFile($event[0]);
  }
}
