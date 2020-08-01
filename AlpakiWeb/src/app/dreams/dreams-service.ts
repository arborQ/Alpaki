import { Injectable } from '@angular/core';
import { Apollo } from 'apollo-angular';
import gql from 'graphql-tag';
import { Observable } from 'rxjs';
import { ApolloQueryResult } from 'apollo-client';

export interface IDreamQueryResponse {
  dreams: IDream[];
}
export interface IDreamCategory {
  dreamCategoryId: number;
  categoryName: string;
}

export interface IDream {
  dreamId: number;
  firstName: string;
  age: number;
  lastName: string;
  dreamState: string;
  gender: string;
  dreamCategory: IDreamCategory;
}

@Injectable({
  providedIn: 'root'
})
export class DreamsService {

  constructor(private apollo: Apollo) { }

  private getAllDreamsQuery = gql`
    query DreamerQuery {
      dreams {
        dreamId
        firstName
        gender
        age
        lastName
        dreamState 
        dreamCategory {
          categoryName
        }
      }
    }
    `;
  getDreams(): Observable<ApolloQueryResult<IDreamQueryResponse>> {
    return this.apollo
      .watchQuery<IDreamQueryResponse>({
        query: this.getAllDreamsQuery,
      })
      .valueChanges;
  }

  getDream(dreamId: number) : Observable<ApolloQueryResult<IDreamQueryResponse>> {
    return this.apollo
      .watchQuery<IDreamQueryResponse>({
        query: gql`
        query DreamerQuery {
          dreams(dreamId: ${dreamId}) {
            dreamId
            firstName
            gender
            age
            lastName
            dreamState 
            dreamCategory {
              categoryName
            }
          }
        }
        `,
      })
      .valueChanges;
  }
}
