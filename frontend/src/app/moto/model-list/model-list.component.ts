import { Component, Input, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { MatDialog } from '@angular/material/dialog';
import { AddModelComponent } from '../add-model/add-model.component';

@Component({
  selector: 'app-model-list',
  templateUrl: './model-list.component.html',
  styleUrls: ['./model-list.component.less']
})
export class ModelListComponent implements OnInit {
  displayedColumns = ['modelName', 'action'];

  constructor(
    private location: Location,
    private dialog: MatDialog
  ) { }
  @Input() brandId: number;
  @Input() models: any[];
  ngOnInit(): void {
  }
  addModel($event: MouseEvent) {
    $event.preventDefault();
    $event.stopPropagation();
    const currentPath = this.location.path();
    this.location.go(`/moto/models/add/${this.brandId}`);
    const dialogRef = this.dialog.open(AddModelComponent);
    dialogRef.afterClosed().subscribe(() => {
      this.location.replaceState(currentPath);
    });
    return false;
  }
  editModel($event: MouseEvent, modelId: number) { 
    $event.preventDefault();
    $event.stopPropagation();
    const currentPath = this.location.path();
    this.location.go(`/moto/models/edit/${this.brandId}`);
    const dialogRef = this.dialog.open(AddModelComponent);
    dialogRef.afterClosed().subscribe(() => {
      this.location.replaceState(currentPath);
    });
    return false;
  }
}
