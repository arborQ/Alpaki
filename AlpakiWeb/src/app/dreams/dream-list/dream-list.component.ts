import { Component } from '@angular/core';
import { DreamsService } from '../dreams-service';
import { of } from 'rxjs';

@Component({
  selector: 'app-dream-list',
  templateUrl: './dream-list.component.html',
  styleUrls: ['./dream-list.component.less']
})
export class DreamListComponent {

  constructor(private dreamsService: DreamsService) { }

  dreams$ = this.dreamsService.getDreams();
  isLoading$ = of(false);

  removeDream = (dreamId: number) => {
    this.dreamsService.deleteDream(dreamId);
  }
}
