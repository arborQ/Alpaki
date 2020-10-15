import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-add-model',
  templateUrl: './add-model.component.html',
  styleUrls: ['./add-model.component.less']
})
export class AddModelComponent implements OnInit {

  constructor() { }
  addForm: FormGroup;

  ngOnInit(): void {
    this.addForm = new FormGroup({
      modelName: new FormControl('', Validators.required),
    });
  }
  cancel() {}

  onSubmited() {}
}
