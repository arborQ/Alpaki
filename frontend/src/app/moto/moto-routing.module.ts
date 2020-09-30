import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { BrandsComponent } from './brands/brands.component';
import { BrandEditComponent } from '../brand-edit/brand-edit.component';

const routes: Routes = [
  { path: 'brands', component: BrandsComponent },
  { path: 'brands/edit/:id', component: BrandEditComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MotoRoutingModule { }
