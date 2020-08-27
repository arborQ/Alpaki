import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DreamsRoutingModule } from './dreams-routing.module';
import { DreamListComponent } from './dream-list/dream-list.component';
import { DreamsService } from './dreams-service';
import { DreamCardComponent } from './dream-card/dream-card.component';

import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { MatListModule } from '@angular/material/list';
import { DreamDetailsComponent } from './dream-details/dream-details.component';
import { DreamListItemComponent } from './dream-list-item/dream-list-item.component';

@NgModule({
  declarations: [DreamListComponent, DreamCardComponent, DreamDetailsComponent, DreamListItemComponent],
  providers: [DreamsService],
  imports: [
    CommonModule,
    DreamsRoutingModule,
    MatCardModule,
    MatChipsModule,
    MatListModule
  ]
})
export class DreamsModule { }
