import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { IsAuthorizedGuard } from 'src/guards/is-authorized/is-authorized.guard';
import { DreamApplicationGuard, MotoApplicationGuard } from './application.guard';


const routes: Routes = [
  {
    path: 'authorize',
    loadChildren: () => import('./authorize/authorize.module')
      .then(a => a.AuthorizeModule),
  },
  {
    path: 'dreams',
    loadChildren: () => import('./dreams/dreams.module').then(a => a.DreamsModule),
    canActivate: [IsAuthorizedGuard, DreamApplicationGuard]
  },
  {
    path: 'categories',
    loadChildren: () => import('./categories/categories.module').then(a => a.CategoriesModule),
    canActivate: [IsAuthorizedGuard, DreamApplicationGuard]
  },
  {
    path: 'users',
    loadChildren: () => import('./users/users.module').then(a => a.UsersModule),
    canActivate: [IsAuthorizedGuard, DreamApplicationGuard]
  },
  {
    path: 'moto',
    loadChildren: () => import('./moto/moto.module').then(a => a.MotoModule),
    canActivate: [IsAuthorizedGuard, MotoApplicationGuard]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
