import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { map, switchMap } from 'rxjs/operators';
import { MotoQueryGQL } from './brands.list.generated';
import { PageEvent } from '@angular/material/paginator';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { BrandEditComponent } from 'src/app/moto/brand-edit/brand-edit.component';
import { AddBrandComponent } from 'src/app/moto/add-brand/add-brand.component';
import { Location } from '@angular/common';
import { BrandsService } from '../brands.service';

@Component({
  selector: 'app-brands',
  templateUrl: './brands.component.html',
  styleUrls: ['./brands.component.less']
})
export class BrandsComponent implements OnInit {

  constructor(
    private brandsService: BrandsService,
    private activeRoute: ActivatedRoute,
    private router: Router,
    private location: Location,
    private dialog: MatDialog) { }

  graphQlResponse = this.activeRoute.queryParamMap
    .pipe(switchMap(queryParams => {
      const page = queryParams.get('page') ?? '0';
      const search = queryParams.get('search') ?? '';
      return this.brandsService.searchBrands({ page: +page, search });
    }));

  search = this.activeRoute.queryParamMap.pipe(map(queryParams => queryParams.get('search') || ''));
  dataSource = this.graphQlResponse.pipe(map(r => r.items));
  totalCount = this.graphQlResponse.pipe(map(r => r.totalCount));

  displayedColumns = ['brandName', 'modelCount', 'action'];
  ngOnInit(): void {
  }

  changePage($event: PageEvent) {
    this.router.navigate([], { relativeTo: this.activeRoute, queryParams: { page: $event.pageIndex }, queryParamsHandling: 'merge' });
  }

  triggerSearch(search: string) {
    this.router.navigate([], { relativeTo: this.activeRoute, queryParams: { search }, queryParamsHandling: 'merge' });
  }

  addBrand($event: MouseEvent) {
    $event.preventDefault();
    $event.stopPropagation();
    const currentPath = this.location.path();
    this.location.go(`/moto/brands/add`);
    const dialogRef = this.dialog.open(AddBrandComponent);
    dialogRef.afterClosed().subscribe(() => {
      this.location.replaceState(currentPath);
    });
    return false;
  }

  editBrand($event: MouseEvent, brandId: number) {
    $event.preventDefault();
    $event.stopPropagation();
    const currentPath = this.location.path();
    this.location.go(`/moto/brands/edit/${brandId}`);
    const dialogRef = this.dialog.open(BrandEditComponent, { width: '250px', data: { brandId } });
    dialogRef.afterClosed().subscribe(() => {
      this.location.replaceState(currentPath);
    });
    return false;
  }
}
