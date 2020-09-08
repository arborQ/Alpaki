import { Component, OnInit } from '@angular/core';
import { DreamsService } from '../dreams-service';
import { of } from 'rxjs';
import { map, filter } from 'rxjs/operators';

@Component({
  selector: 'app-dream-list',
  templateUrl: './dream-list.component.html',
  styleUrls: ['./dream-list.component.less']
})
export class DreamListComponent implements OnInit {

  constructor(private dreamsService: DreamsService) { }

  dreamsResponse$ = this.dreamsService.loadedDreams;
  dreams$ = this.dreamsResponse$.pipe(map(response => response?.data?.dreams));
  isLoading$ = this.dreamsResponse$.pipe(map(response => response.loading));

  ngOnInit(): void {
    this.dreamsService.loadDreams({ page: 0 });
  }

  removeDream = (dreamId: number) => {
    this.dreamsService.deleteDream(dreamId);
  }
}
