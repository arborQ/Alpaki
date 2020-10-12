import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { BrandsComponent } from './brands/brands.component';
import { BrandEditPageComponent } from './brand-edit/brand-edit.component';
import { AddBrandPageComponent } from './add-brand/add-brand.component';
import { AddModelComponent } from './add-model/add-model.component';

const routes: Routes = [
  { path: 'brands', component: BrandsComponent },
  { path: 'brands/edit/:brandId', component: BrandEditPageComponent },
  { path: 'brands/add', component: AddBrandPageComponent },
  { path: 'models/add/:brandId', component: AddModelComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MotoRoutingModule { }
