import * as Types from '../../../types';

import gql from 'graphql-tag';
import { Injectable } from '@angular/core';
import * as Apollo from 'apollo-angular';
export type MotoQueryQueryVariables = Types.Exact<{
  page: Types.Scalars['Int'];
  search?: Types.Maybe<Types.Scalars['String']>;
}>;

export type MotoQueryQuery = { __typename?: 'Query' } & {
  moto?: Types.Maybe<
    { __typename?: 'MotoGraphQuery' } & {
      brands?: Types.Maybe<
        { __typename?: 'BrandPagedCollectionType' } & Pick<
          Types.BrandPagedCollectionType,
          'totalCount'
        > & {
            items?: Types.Maybe<
              Array<
                Types.Maybe<
                  { __typename?: 'BrandType' } & Pick<
                    Types.BrandType,
                    'brandId' | 'brandName' | 'modelCount'
                  >
                >
              >
            >;
          }
      >;
    }
  >;
};

export const MotoQueryDocument = gql`
  query MotoQuery($page: Int!, $search: String) {
    moto {
      brands {
        totalCount(search: $search)
        items(page: $page, search: $search) {
          brandId
          brandName
          modelCount
        }
      }
    }
  }
`;

@Injectable({
  providedIn: 'root',
})
export class MotoQueryGQL extends Apollo.Query<
  MotoQueryQuery,
  MotoQueryQueryVariables
> {
  document = MotoQueryDocument;

  constructor(apollo: Apollo.Apollo) {
    super(apollo);
  }
}
