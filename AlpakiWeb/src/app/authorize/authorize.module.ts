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
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { ProfileComponent } from './profile/profile.component';
import { ProfileService } from './profile/profile.service';
import { RemindPasswordComponent } from './remind-password/remind-password.component';

@NgModule({
  declarations: [
    SignInComponent,
    RegisterComponent,
    ProfileComponent,
    RemindPasswordComponent,
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
    MatProgressSpinnerModule
  ],
  providers: [ProfileService]
})
export class AuthorizeModule { }
