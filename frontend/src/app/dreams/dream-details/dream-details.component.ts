import { Component } from '@angular/core';
import { DreamsService } from '../dreams-service';
import { map, switchMap } from 'rxjs/operators';
import { ActivatedRoute } from '@angular/router';
import { of } from 'rxjs';

@Component({
  selector: 'app-dream-details',
  templateUrl: './dream-details.component.html',
  styleUrls: ['./dream-details.component.less']
})
export class DreamDetailsComponent {
  constructor(private dreamService: DreamsService, private activatedRoute: ActivatedRoute) { }
  dreamId: number | null = null;
  dreamResponse$ = this.activatedRoute.params.pipe(
    switchMap(params => {
      console.log({ params });
      this.dreamId = +params.dreamId;

      return this.dreamService.getDream(this.dreamId);
    })
  );

  dream$ = this.dreamResponse$.pipe(map(response => response.data.dreams[0]));
  isLoading$ = this.dreamResponse$.pipe(map(response => response.loading));
  steps$ = this.dream$.pipe(map(dream => dream.requiredSteps));
}
