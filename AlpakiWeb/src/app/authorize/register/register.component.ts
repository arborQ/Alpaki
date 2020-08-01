import { Component, OnInit, EventEmitter } from '@angular/core';
import { of } from 'rxjs';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.less']
})
export class RegisterComponent implements OnInit {

  constructor() { }
  options = of([{ id: 1, name: 'option 1' }, { id: 2, name: 'option 2' }, { id: 3, name: 'option 3' }]);
  selected = { id: 2, name: 'option 2' };

  ngOnInit(): void {
  }
}
