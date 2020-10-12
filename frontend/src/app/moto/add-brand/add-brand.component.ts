import { Component, OnInit, Optional } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-brand-add-page',
  template: '<div></div>'
})
export class AddBrandPageComponent implements OnInit {
  constructor(
    private dialog: MatDialog,
    private router: Router) { }
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

  constructor(@Optional() public dialogRef: MatDialogRef<AddBrandComponent>) { }
  addForm: FormGroup;

  ngOnInit(): void {
    this.addForm = new FormGroup({
      brandName: new FormControl('', Validators.required),
    });
  }
  cancel() {
    this.dialogRef?.close();
  }

  onSubmited() { }
}
