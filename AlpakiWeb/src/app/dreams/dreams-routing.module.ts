import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DreamListComponent } from './dream-list/dream-list.component';
import { DreamDetailsComponent } from './dream-details/dream-details.component';


const routes: Routes = [
  { path: 'list', component: DreamListComponent },
  { path: 'details', component: DreamDetailsComponent, data: { title: 'lol' } },
  { path: '**', redirectTo: 'list?page=1' }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DreamsRoutingModule { }
