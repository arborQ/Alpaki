import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-model-list',
  templateUrl: './model-list.component.html',
  styleUrls: ['./model-list.component.less']
})
export class ModelListComponent implements OnInit {
  displayedColumns = ['modelName', 'action'];

  constructor() { }
  @Input() models: any[];
  ngOnInit(): void {
  }

}
