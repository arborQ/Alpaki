import { Component } from '@angular/core';
import { DreamsService } from '../dreams-service';
import { map } from 'rxjs/operators';
import { of } from 'rxjs';

@Component({
  selector: 'app-dream-list',
  templateUrl: './dream-list.component.html',
  styleUrls: ['./dream-list.component.less']
})
export class DreamListComponent {

  constructor(private dreamsService: DreamsService) { }

  dreamResponse$ = this.dreamsService.getDreams();

  dreams$ = this.dreamResponse$.pipe(map(response => response.dreams));
  isLoading$ = of(false);
}
