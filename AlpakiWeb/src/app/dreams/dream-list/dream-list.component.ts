import { Component } from '@angular/core';
import { DreamsService } from '../dreams-service';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-dream-list',
  templateUrl: './dream-list.component.html',
  styleUrls: ['./dream-list.component.less']
})
export class DreamListComponent {

  constructor(private dreamsService: DreamsService) { }

  dreamResponse$ = this.dreamsService.getDreams();

  dreams$ = this.dreamResponse$.pipe(map(response => response.data.dreams));
  isLoading$ = this.dreamResponse$.pipe(map(response => response.loading));
}
