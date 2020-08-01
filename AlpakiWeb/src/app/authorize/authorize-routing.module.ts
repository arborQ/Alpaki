import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SignInComponent } from './sign-in/sign-in.component';
import { RegisterComponent } from './register/register.component';
import { IsAnonymousGuard } from '../../is-anonymous.guard';
import { ProfileComponent } from './profile/profile.component';
import { IsAuthorizedGuard } from 'src/is-authorized.guard';

const routes: Routes = [
  { path: '', redirectTo: 'sign-in' },
  { path: 'sign-in', component: SignInComponent, canActivate: [IsAnonymousGuard] },
  { path: 'register', component: RegisterComponent, canActivate: [IsAnonymousGuard] },
  { path: 'profile', component: ProfileComponent, canActivate: [IsAuthorizedGuard] },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuthorizeRoutingModule { }
