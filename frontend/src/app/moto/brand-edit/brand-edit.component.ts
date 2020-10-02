import { HttpClient } from '@angular/common/http';
import { Component, Inject, Optional, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { map } from 'rxjs/operators';
import { BrandsService } from '../brands.service';

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

  editForm: FormGroup;

  ngOnInit(): void {
    this.editForm = new FormGroup({
      brandId: new FormControl('', Validators.required),
      brandName: new FormControl('', Validators.required),
    });

    this.activeRoute.params.pipe(map(params => params.brandId))
      .subscribe(brandId => {
        const selectBrandId = brandId || this.data?.brandId;
        this.editForm.disable();
        this.brandsService.details({ brandId: selectBrandId }).toPromise().then(p => {
          const brandName = p.brandName;
          this.editForm.setValue({ brandName, brandId: selectBrandId });
          this.isLoading = false;
          this.editForm.enable();
        });
      });
  }
  cancel() { this.dialogRef?.close(); }
  onLoginFormSubmitted() {
    const newBrand = { brandId: this.editForm.value.brandId, brandName: this.editForm.value.brandName };
    this.http.put('/api/Moto/Brand', newBrand).toPromise().then(() => {
      this.brandsService.update(newBrand);
      this.dialogRef?.close();
    });
  }
}
