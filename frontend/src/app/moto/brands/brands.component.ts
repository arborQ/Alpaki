import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { map, switchMap } from 'rxjs/operators';
import { MotoQueryGQL } from './brands.list.generated';
import { PageEvent } from '@angular/material/paginator';

@Component({
  selector: 'app-brands',
  templateUrl: './brands.component.html',
  styleUrls: ['./brands.component.less']
})
export class BrandsComponent implements OnInit {

  constructor(private brandsQuery: MotoQueryGQL, private activeRoute: ActivatedRoute, private router: Router) { }

  graphQlResponse = this.activeRoute.queryParamMap
    .pipe(switchMap(queryParams => {
      const page = queryParams.get('page') ?? '0';
      const search = queryParams.get('search') ?? '';
      return this.brandsQuery.fetch({ page: +page, search });
    }));

  // graphQlResponse = this.brandsQuery.fetch({ page: 0, search: '' });
  search = this.activeRoute.queryParamMap.pipe(map(queryParams => queryParams.get('search') || ''));
  dataSource = this.graphQlResponse.pipe(map(r => r.data.moto.brands.items));
  totalCount = this.graphQlResponse.pipe(map(r => r.data.moto.brands.totalCount));
  isLoadingResults = this.graphQlResponse.pipe(map(r => r.loading));

  displayedColumns = ['brandName', 'action'];
  ngOnInit(): void {
  }

  changePage($event: PageEvent) {
    this.router.navigate([], { relativeTo: this.activeRoute, queryParams: { page: $event.pageIndex }, queryParamsHandling: 'merge' });
  }

  triggerSearch(search: string) {
    this.router.navigate([], { relativeTo: this.activeRoute, queryParams: { search }, queryParamsHandling: 'merge' });
  }

  editBrand($event: MouseEvent, brandId: number) {
    $event.preventDefault();
    $event.stopPropagation();

    return false;
  }
}