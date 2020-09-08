import * as Types from '../../types';

import gql from 'graphql-tag';
import { Injectable } from '@angular/core';
import * as Apollo from 'apollo-angular';
export type DreamerQueryQueryVariables = Types.Exact<{ [key: string]: never }>;

export type DreamerQueryQuery = { __typename?: 'DreamQuery' } & {
  categories?: Types.Maybe<
    Array<
      Types.Maybe<
        { __typename?: 'DreamCategoryType' } & Pick<
          Types.DreamCategoryType,
          'dreamCategoryId' | 'categoryName'
        > & {
            defaultSteps?: Types.Maybe<
              Array<
                Types.Maybe<
                  { __typename?: 'DefaultStepType' } & Pick<
                    Types.DefaultStepType,
                    'stepDescription' | 'isSponsorRelated'
                  >
                >
              >
            >;
          }
      >
    >
  >;
};

export const DreamerQueryDocument = gql`
  query DreamerQuery {
    categories {
      dreamCategoryId
      categoryName
      defaultSteps {
        stepDescription
        isSponsorRelated
      }
    }
  }
`;

@Injectable({
  providedIn: 'root',
})
export class DreamerQueryGQL extends Apollo.Query<
  DreamerQueryQuery,
  DreamerQueryQueryVariables
> {
  document = DreamerQueryDocument;

  constructor(apollo: Apollo.Apollo) {
    super(apollo);
  }
}
