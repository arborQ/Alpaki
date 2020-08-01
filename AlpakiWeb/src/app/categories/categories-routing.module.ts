import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CategoryListComponent } from './category-list/category-list.component';


const routes: Routes = [
  { path: 'list', component: CategoryListComponent },
  // { path: 'details/:dreamId', component: DreamDetailsComponent, data: { title: 'lol' } },
  { path: '**', redirectTo: 'list' }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CategoriesRoutingModule { }
