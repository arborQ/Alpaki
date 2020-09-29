import * as Types from '../../types';

import gql from 'graphql-tag';
import { Injectable } from '@angular/core';
import * as Apollo from 'apollo-angular';
export type DreamDetailsQueryVariables = Types.Exact<{
  dreamId: Types.Scalars['ID'];
}>;

export type DreamDetailsQuery = { __typename?: 'Query' } & {
  dreams?: Types.Maybe<
    { __typename?: 'DreamQuery' } & {
      dreams?: Types.Maybe<
        Array<
          Types.Maybe<
            { __typename?: 'DreamType' } & Pick<
              Types.DreamType,
              | 'dreamId'
              | 'title'
              | 'displayName'
              | 'age'
              | 'cityName'
              | 'dreamComeTrueDate'
              | 'dreamCategoryId'
              | 'categoryName'
              | 'dreamImageId'
            > & {
                requiredSteps?: Types.Maybe<
                  Array<
                    Types.Maybe<
                      { __typename?: 'DeamStepType' } & Pick<
                        Types.DeamStepType,
                        'dreamStepId' | 'stepState' | 'stepDescription'
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
};

export const DreamDetailsDocument = gql`
  query DreamDetails($dreamId: ID!) {
    dreams {
      dreams(dreamId: $dreamId) {
        dreamId
        title
        displayName
        age
        cityName
        dreamComeTrueDate
        dreamCategoryId
        categoryName
        dreamImageId
        requiredSteps {
          dreamStepId
          stepState
          stepDescription
        }
      }
    }
  }
`;

@Injectable({
  providedIn: 'root',
})
export class DreamDetailsGQL extends Apollo.Query<
  DreamDetailsQuery,
  DreamDetailsQueryVariables
> {
  document = DreamDetailsDocument;

  constructor(apollo: Apollo.Apollo) {
    super(apollo);
  }
}
