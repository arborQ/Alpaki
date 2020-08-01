import { Injectable } from '@angular/core';
import { Apollo } from 'apollo-angular';
import { Observable } from 'rxjs';
import { ApolloQueryResult } from 'apollo-client';
import gql from 'graphql-tag';

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

  constructor(private apollo: Apollo) { }

  getCategories(): Observable<ApolloQueryResult<ICategoriesResponse>> {
    return this.apollo
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
      })
      .valueChanges;
  }
}
