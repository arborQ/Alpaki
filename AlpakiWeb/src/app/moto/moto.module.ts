import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MotoRoutingModule } from './moto-routing.module';
import { BrandsComponent } from './brands/brands.component';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';

@NgModule({
  declarations: [BrandsComponent],
  imports: [
    CommonModule,
    MotoRoutingModule,
    MatTableModule,
    MatPaginatorModule
  ]
})
export class MotoModule { }
