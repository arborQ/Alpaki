import { Component, OnInit, Input } from '@angular/core';
import { IDream } from '../dreams-service';

@Component({
  selector: 'app-dream-list-item',
  templateUrl: './dream-list-item.component.html',
  styleUrls: ['./dream-list-item.component.less']
})
export class DreamListItemComponent implements OnInit {
  @Input() dream: IDream;

  constructor() { }

  ngOnInit(): void {
  }

  get subTitle(): string {
    return [ this.dream.displayName, `${this.dream.age} lat`, this.dream.city ].filter(d => !!d).join(' / ' );
  }
}
