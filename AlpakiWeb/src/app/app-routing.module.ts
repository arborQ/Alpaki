import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { IsAuthorizedGuard } from 'src/is-authorized.guard';


const routes: Routes = [
  {
    path: 'authorize',
    loadChildren: () => import('./authorize/authorize.module')
      .then(a => a.AuthorizeModule),
  },
  {
    path: 'dreams',
    loadChildren: () => import('./dreams/dreams.module').then(a => a.DreamsModule),
    canActivate: [IsAuthorizedGuard]
  },
  {
    path: 'categories',
    loadChildren: () => import('./categories/categories.module').then(a => a.CategoriesModule),
    canActivate: [IsAuthorizedGuard]
  },
  {
    path: 'users',
    loadChildren: () => import('./users/users-routing.module').then(a => a.UsersRoutingModule),
    canActivate: [IsAuthorizedGuard]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
