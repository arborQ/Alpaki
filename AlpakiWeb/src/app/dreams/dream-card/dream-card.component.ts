import { Component, OnInit, Input } from '@angular/core';
import { IDream } from '../dreams-service';

@Component({
  selector: 'app-dream-card',
  templateUrl: './dream-card.component.html',
  styleUrls: ['./dream-card.component.less']
})
export class DreamCardComponent {

  @Input() dream: IDream;
}
