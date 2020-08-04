import { Injectable } from '@angular/core';
import { Apollo } from 'apollo-angular';
import { Observable, Subject, ReplaySubject } from 'rxjs';
import { ApolloQueryResult } from 'apollo-client';
import gql from 'graphql-tag';
import { HttpClient } from '@angular/common/http';

export interface ICategoryStep {
  dreamCategoryDefaultStepId: number;
  stepDescription: string;
  isSponsorRelated: boolean;
}

export interface ICategory {
  dreamCategoryId: number;
  categoryName: string;
  dreamCount: number;
  defaultSteps: ICategoryStep[];
}

export interface ICategoriesResponse {
  categories: ICategory[];
}

@Injectable({
  providedIn: 'root'
})
export class CategoriesService {
  categoriesQuery = this.apollo
    .watchQuery<ICategoriesResponse>({
      query: gql`
      query DreamerQuery {
        categories {
          dreamCategoryId
          categoryName
          dreamCount
          defaultSteps {
            dreamCategoryDefaultStepId
            stepDescription
            isSponsorRelated
          }
        }
      }
      `,
    });
  constructor(private apollo: Apollo, private http: HttpClient) { }

  getCategories(): Observable<ApolloQueryResult<ICategoriesResponse>> {
    return this.categoriesQuery.valueChanges;
  }

  updateCategory(category: ICategory) {
    return this.http.patch<{}>('/api/categories', category).toPromise().then(() => {
      this.categoriesQuery.refetch();
    });
  }

  addCategory(category: ICategory) {
    return this.http.post<{ categoryId: number }>('/api/categories', category).toPromise().then(() => {
      this.categoriesQuery.refetch();
    });
  }
}
