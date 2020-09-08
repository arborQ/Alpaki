import { Component, OnInit } from '@angular/core';
import { ProfileService } from './profile.service';
import { ImagesService } from 'src/app/images.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.less']
})
export class ProfileComponent implements OnInit {
  constructor(private profileService: ProfileService, private imagesService: ImagesService) { }

  profile$ = this.profileService.currentUserProfile();
  form: FormGroup;
  ngOnInit(): void {
    this.form = new FormGroup({
      firstName: new FormControl('', [Validators.required, Validators.maxLength(200)])
    });

    this.profile$.subscribe(p => {
      this.form.setValue({ firstName: p.firstName });
    });
  }

  onSelectedFilesChanged($event) {
    console.log($event[0]);
    this.imagesService.sendFile($event[0]).then(image => {
      console.log({ image });
    });
  }
}
