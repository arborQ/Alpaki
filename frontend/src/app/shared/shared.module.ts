import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserRolesComponent } from './user-roles/user-roles.component';
import { MatChipsModule } from '@angular/material/chips';
import { DisplayImageComponent } from './display-image/display-image.component';
import { MatFileUploadModule } from 'mat-file-upload';
import { DisplayImageDirective } from './display-image.directive';

@NgModule({
  declarations: [UserRolesComponent, DisplayImageComponent, DisplayImageDirective],
  imports: [
    CommonModule,
    MatChipsModule,
    MatFileUploadModule
  ], exports: [UserRolesComponent, DisplayImageComponent]
})
export class SharedModule { }
