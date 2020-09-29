import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { map, switchMap } from 'rxjs/operators';
import { MotoQueryGQL } from './brands.list.generated';
import { combineLatest, of } from 'rxjs';
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

  dataSource = this.graphQlResponse.pipe(map(r => r.data.moto.brands));
  displayedColumns = ['brandName'];
  ngOnInit(): void {
  }

  changePage($event: PageEvent) {
    this.router.navigate([], { relativeTo: this.activeRoute, queryParams: { page: $event.pageIndex }, queryParamsHandling: 'merge' });
  }
}
