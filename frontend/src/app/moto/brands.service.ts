import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { BrandType } from 'src/types';
import { BrandDetailsQueryGQL, BrandDetailsQueryQueryVariables } from './brand-edit/brands.details.generated';
import { MotoQueryGQL, MotoQueryQueryVariables, MotoQueryQuery } from './brands/brands.list.generated';

interface IPagedResponse<T> {
  items: Array<Partial<T>>;
  totalCount: number;
  isLoading: boolean;
}

@Injectable({
  providedIn: 'root'
})
export class BrandsService {
  private responseSubject: BehaviorSubject<IPagedResponse<BrandType>>
    = new BehaviorSubject<IPagedResponse<BrandType>>({ items: [], totalCount: 0, isLoading: true });

  constructor(private brandsQuery: MotoQueryGQL, private brandDetails: BrandDetailsQueryGQL) { }

  searchBrands(query: MotoQueryQueryVariables): Observable<IPagedResponse<BrandType>> {
    this.brandsQuery.fetch(query).subscribe(response => {
      this.responseSubject.next({
        items: response.data.moto.brands.items,
        totalCount: response.data.moto.brands.totalCount,
        isLoading: false
      });
    });

    return this.responseSubject.asObservable();
  }

  details(query: BrandDetailsQueryQueryVariables): Observable<Partial<BrandType>> {
    return this.brandDetails.fetch(query).pipe(map(response => response.data.moto.brands.items[0]));
  }

  update(brand: Partial<BrandType>): void {
    this.responseSubject.next({
      ...this.responseSubject.value,
      items: this.responseSubject.value.items.map(item => {
        if (item.brandId === brand.brandId) {
          return { ...item, brandName: brand.brandName };
        }

        return item;
      })
    });
  }
}
