import { Component, Inject, Optional, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { map } from 'rxjs/operators';
import { BrandDetailsQueryGQL } from './brands.details.generated';

@Component({
  selector: 'app-brand-edit',
  templateUrl: './brand-edit.component.html',
  styleUrls: ['./brand-edit.component.less']
})
export class BrandEditComponent implements OnInit {

  constructor(
    private gq: BrandDetailsQueryGQL,
    private activeRoute: ActivatedRoute,
    @Optional() @Inject(MAT_DIALOG_DATA) public data: { brandId: number }
  ) { }
  isLoading = true;

  editForm: FormGroup;

  ngOnInit(): void {
    this.editForm = new FormGroup({
      brandName: new FormControl('', Validators.required),
    });

    this.activeRoute.params.pipe(map(params => params.brandId))
      .subscribe(brandId => {
        const selectBrandId = brandId || this.data?.brandId;
        this.editForm.disable();
        this.gq.fetch({ brandId: selectBrandId }).toPromise().then(p => {
          const brandName = p.data.moto.brands.items[0].brandName;
          this.editForm.setValue({ brandName });
          this.isLoading = false;
          this.editForm.enable();
        });
      });
  }

  onLoginFormSubmitted() { }
}
