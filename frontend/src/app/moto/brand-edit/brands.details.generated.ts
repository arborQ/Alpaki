import * as Types from '../../../types';

import gql from 'graphql-tag';
import { Injectable } from '@angular/core';
import * as Apollo from 'apollo-angular';
export type BrandDetailsQueryQueryVariables = Types.Exact<{
  brandId: Types.Scalars['Int'];
}>;

export type BrandDetailsQueryQuery = { __typename?: 'Query' } & {
  moto?: Types.Maybe<
    { __typename?: 'MotoGraphQuery' } & {
      brands?: Types.Maybe<
        { __typename?: 'BrandPagedCollectionType' } & {
          items?: Types.Maybe<
            Array<
              Types.Maybe<
                { __typename?: 'BrandType' } & Pick<
                  Types.BrandType,
                  'brandId' | 'brandName'
                > & {
                    models?: Types.Maybe<
                      Array<
                        Types.Maybe<
                          { __typename?: 'ModelType' } & Pick<
                            Types.ModelType,
                            'modelId' | 'modelName'
                          >
                        >
                      >
                    >;
                  }
              >
            >
          >;
        }
      >;
    }
  >;
};

export const BrandDetailsQueryDocument = gql`
  query BrandDetailsQuery($brandId: Int!) {
    moto {
      brands {
        items(brandId: $brandId) {
          brandId
          brandName
          models {
            modelId
            modelName
          }
        }
      }
    }
  }
`;

@Injectable({
  providedIn: 'root',
})
export class BrandDetailsQueryGQL extends Apollo.Query<
  BrandDetailsQueryQuery,
  BrandDetailsQueryQueryVariables
> {
  document = BrandDetailsQueryDocument;

  constructor(apollo: Apollo.Apollo) {
    super(apollo);
  }
}
