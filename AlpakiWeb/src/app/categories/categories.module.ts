import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CategoriesRoutingModule } from './categories-routing.module';
import { CategoryListComponent } from '../category-list/category-list.component';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatIconModule } from '@angular/material/icon';

@NgModule({
  declarations: [CategoryListComponent],
  imports: [
    CommonModule,
    CategoriesRoutingModule,
    MatExpansionModule,
    MatIconModule
  ]
})
export class CategoriesModule { }
