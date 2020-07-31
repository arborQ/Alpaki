import { Component } from '@angular/core';
import { DreamsService } from '../dreams-service';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-dream-details',
  templateUrl: './dream-details.component.html',
  styleUrls: ['./dream-details.component.less']
})
export class DreamDetailsComponent {
  constructor(private dreamService: DreamsService) {}
  dreamResponse$ = this.dreamService.getDream(5);

  dream$ = this.dreamResponse$.pipe(map(response => response.data.dreams[0]));
  isLoading$ = this.dreamResponse$.pipe(map(response => response.loading));
}
