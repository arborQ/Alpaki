import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { BrandsComponent } from './brands/brands.component';
import { BrandEditPageComponent } from './brand-edit/brand-edit.component';
import { AddBrandPageComponent } from './add-brand/add-brand.component';

const routes: Routes = [
  { path: 'brands', component: BrandsComponent },
  { path: 'brands/edit/:brandId', component: BrandEditPageComponent },
  { path: 'brands/add', component: AddBrandPageComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MotoRoutingModule { }
