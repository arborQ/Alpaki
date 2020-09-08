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
  dreamResponse$ = this.activatedRoute.paramMap.pipe(
    switchMap(params => {
      this.dreamId = +params.get('dreamId');

      return this.dreamService.getDream(this.dreamId);
    })
  );
  steps$ = of([ 'Zdobyć kasę', 'Wydać kasę' ]);
  dream$ = this.dreamResponse$.pipe(map(response => response));
  isLoading$ = of(false);
}
