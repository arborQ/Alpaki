import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DreamsRoutingModule } from './dreams-routing.module';
import { DreamListComponent } from './dream-list/dream-list.component';
import { DreamsService } from './dreams-service';
import { DreamCardComponent } from './dream-card/dream-card.component';

import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { MatListModule } from '@angular/material/list';
import { DreamDetailsComponent } from './dream-details/dream-details.component';
import { DreamListItemComponent } from './dream-list-item/dream-list-item.component';
import { DreamProgressComponent } from './dream-progress/dream-progress.component';
import { SharedModule } from '../shared/shared.module';

@NgModule({
  declarations: [DreamListComponent, DreamCardComponent, DreamDetailsComponent, DreamListItemComponent, DreamProgressComponent],
  providers: [DreamsService],
  imports: [
    CommonModule,
    DreamsRoutingModule,
    MatCardModule,
    MatChipsModule,
    MatListModule,
    MatProgressSpinnerModule,
    SharedModule
  ]
})
export class DreamsModule { }
