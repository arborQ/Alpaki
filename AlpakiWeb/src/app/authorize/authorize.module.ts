import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuthorizeRoutingModule } from './authorize-routing.module';
import { SignInComponent } from './sign-in/sign-in.component';
import { RegisterComponent } from './register/register.component';
import { ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatSelectModule } from '@angular/material/select';
import { MatSnackBarModule, MAT_SNACK_BAR_DEFAULT_OPTIONS } from '@angular/material/snack-bar';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { ProfileComponent } from './profile/profile.component';
import { ProfileService } from './profile/profile.service';
import { RemindPasswordComponent } from './remind-password/remind-password.component';
import { MatChipsModule } from '@angular/material/chips';
import { SharedModule } from '../shared/shared.module';
import { MatIconModule } from '@angular/material/icon';
import { ProfileEditComponent } from './profile/profile-edit/profile-edit.component';
import { SignInService } from './sign-in.service';

@NgModule({
  declarations: [
    SignInComponent,
    RegisterComponent,
    ProfileComponent,
    RemindPasswordComponent,
    ProfileEditComponent,
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule,
    AuthorizeRoutingModule,
    MatCardModule,
    MatProgressSpinnerModule,
    MatSnackBarModule,
    MatChipsModule,
    MatIconModule,
    SharedModule
  ],
  providers: [ProfileService, SignInService, {provide: MAT_SNACK_BAR_DEFAULT_OPTIONS, useValue: {duration: 2500}}]
})
export class AuthorizeModule { }
