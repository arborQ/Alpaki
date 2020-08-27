import * as Types from '../../types';

import gql from 'graphql-tag';
import { Injectable } from '@angular/core';
import * as Apollo from 'apollo-angular';
export type DreamsQueryVariables = Types.Exact<{
  page: Types.Scalars['Int'];
}>;

export type DreamsQuery = { __typename?: 'DreamQuery' } & {
  dreams?: Types.Maybe<
    Array<
      Types.Maybe<
        { __typename?: 'DreamType' } & Pick<
          Types.DreamType,
          'title' | 'dreamId' | 'displayName' | 'age'
        >
      >
    >
  >;
};

export const DreamsDocument = gql`
  query Dreams($page: Int!) {
    dreams(page: $page) {
      title
      dreamId
      displayName
      age
    }
  }
`;

@Injectable({
  providedIn: 'root',
})
export class DreamsGQL extends Apollo.Query<DreamsQuery, DreamsQueryVariables> {
  document = DreamsDocument;

  constructor(apollo: Apollo.Apollo) {
    super(apollo);
  }
}
