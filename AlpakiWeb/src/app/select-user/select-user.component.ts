import { Component, Input, Output, EventEmitter } from '@angular/core';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-select-user',
  templateUrl: './select-user.component.html',
  styleUrls: ['./select-user.component.less']
})
export class SelectUserComponent {
  @Input() selected: string;
  @Input() options: Observable<any[]>;
  @Output() selectedChange: EventEmitter<string> = new EventEmitter<string>();

  change({ value }) {
    this.selectedChange.emit(value);
  }

  compareOptions(a, b) {
    return a.id === b.id;
  }
}
