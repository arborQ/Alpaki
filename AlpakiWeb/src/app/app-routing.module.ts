import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { IsAuthorizedGuard } from 'src/guards/is-authorized/is-authorized.guard';


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
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
