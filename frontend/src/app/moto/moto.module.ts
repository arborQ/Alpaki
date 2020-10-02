import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MotoRoutingModule } from './moto-routing.module';
import { BrandsComponent } from './brands/brands.component';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { BrandEditComponent } from './brand-edit/brand-edit.component';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatCardModule } from '@angular/material/card';
import { ReactiveFormsModule } from '@angular/forms';
import { MatListModule } from '@angular/material/list';
import { ModelListComponent } from './model-list/model-list.component';

@NgModule({
  declarations: [BrandsComponent, BrandEditComponent, ModelListComponent],
  imports: [
    CommonModule,
    MotoRoutingModule,
    MatTableModule,
    MatPaginatorModule,
    MatButtonModule,
    MatProgressSpinnerModule,
    MatInputModule,
    MatIconModule,
    MatDialogModule,
    MatFormFieldModule,
    MatCardModule,
    ReactiveFormsModule,
    MatListModule
  ]
})
export class MotoModule { }
