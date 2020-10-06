import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-brand-add-page',
  template: '<div></div>'
})
export class AddBrandPageComponent implements OnInit {
  constructor(private activeRoute: ActivatedRoute, private dialog: MatDialog, private router: Router) { }
  ngOnInit(): void {
    this.dialog.open(AddBrandComponent)
      .afterClosed().subscribe(() => {
        this.router.navigate(['/moto/brands']);
      });
  }
}

@Component({
  selector: 'app-add-brand',
  templateUrl: './add-brand.component.html',
  styleUrls: ['./add-brand.component.less']
})
export class AddBrandComponent implements OnInit {

  constructor() { }
  addForm: FormGroup;

  ngOnInit(): void {
    this.addForm = new FormGroup({
      brandName: new FormControl('', Validators.required),
    });
  }
  cancel() {}

  onSubmited() {}
}
