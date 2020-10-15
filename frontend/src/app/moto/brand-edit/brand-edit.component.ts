import { HttpClient } from '@angular/common/http';
import { Component, Inject, Optional, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { BrandsService } from '../brands.service';

export function OpenInDialog(dialog: MatDialog, brandId: number): Observable<any> {
  const dialogRef = dialog
  .open(BrandEditComponent, { width: '550px', disableClose: true, data: { brandId } })
  .afterClosed();

  return dialogRef;
}

@Component({
  selector: 'app-brand-edit-page',
  template: '<div></div>'
})
export class BrandEditPageComponent implements OnInit {
  constructor(private activeRoute: ActivatedRoute, private dialog: MatDialog, private router: Router) { }
  ngOnInit(): void {
    this.activeRoute.params.pipe(map(params => params.brandId))
      .subscribe(brandId => {
        OpenInDialog(this.dialog, brandId);
      });
  }
}

@Component({
  selector: 'app-brand-edit',
  templateUrl: './brand-edit.component.html',
  styleUrls: ['./brand-edit.component.less']
})
export class BrandEditComponent implements OnInit {

  constructor(
    private brandsService: BrandsService,
    private activeRoute: ActivatedRoute,
    private http: HttpClient,
    @Optional() public dialogRef: MatDialogRef<BrandEditComponent>,
    @Optional() @Inject(MAT_DIALOG_DATA) public data: { brandId: number }
  ) { }
  isLoading = true;
  models: any[] = [];
  editForm: FormGroup;

  brandId = this.activeRoute.params.pipe(map(params => {
    const selectBrandId = params.brandId || this.data?.brandId;
    return selectBrandId;
  }));

  ngOnInit(): void {
    this.editForm = new FormGroup({
      brandId: new FormControl('', Validators.required),
      brandName: new FormControl('', Validators.required),
    });

    this.brandId
      .subscribe(brandId => {
        const selectBrandId = brandId || this.data?.brandId;
        this.editForm.disable();
        this.brandsService.details({ brandId: selectBrandId }).toPromise().then(p => {
          const brandName = p.brandName;
          this.models = p.models;

          this.editForm.setValue({ brandName, brandId: selectBrandId });
          this.isLoading = false;
          this.editForm.enable();
        });
      });
  }
  cancel() { this.dialogRef?.close(); }
  onLoginFormSubmitted() {
    const newBrand = { brandId: +this.editForm.value.brandId, brandName: this.editForm.value.brandName };
    this.http.put('/api/Moto/Brand', newBrand).toPromise().then(() => {
      this.brandsService.update(newBrand);
      this.dialogRef?.close();
    });
  }
}
