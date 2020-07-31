import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';


const routes: Routes = [
  {
    path: 'authorize', loadChildren: () => import('./authorize/authorize.module').then(a => a.AuthorizeModule),
  }, 
  {
    path: 'dreams', loadChildren: () => import('./dreams/dreams.module').then(a => a.DreamsModule)
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
