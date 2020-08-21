export type Maybe<T> = T | null;
export type Exact<T extends { [key: string]: unknown }> = { [K in keyof T]: T[K] };
/** All built-in and custom scalars, mapped to their actual values */
export type Scalars = {
  ID: string;
  String: string;
  Boolean: boolean;
  Int: number;
  Float: number;
  /** The `DateTimeOffset` scalar type represents a date, time and offset from UTC. `DateTimeOffset` expects timestamps to be formatted in accordance with the [ISO-8601](https://en.wikipedia.org/wiki/ISO_8601) standard. */
  DateTimeOffset: any;
  /** The `Date` scalar type represents a year, month and day in accordance with the [ISO-8601](https://en.wikipedia.org/wiki/ISO_8601) standard. */
  Date: any;
  /** The `DateTime` scalar type represents a date and time. `DateTime` expects timestamps to be formatted in accordance with the [ISO-8601](https://en.wikipedia.org/wiki/ISO_8601) standard. */
  DateTime: any;
  /** The `Seconds` scalar type represents a period of time represented as the total number of seconds. */
  Seconds: any;
  /** The `Milliseconds` scalar type represents a period of time represented as the total number of milliseconds. */
  Milliseconds: any;
  Decimal: any;
};




export type DeamStepType = {
  __typename?: 'DeamStepType';
  dreamStepId: Scalars['Int'];
  stepDescription: Scalars['String'];
  stepState?: Maybe<StepStateEnum>;
};


export type DefaultStepType = {
  __typename?: 'DefaultStepType';
  dreamCategoryDefaultStepId: Scalars['Int'];
  isSponsorRelated: Scalars['Boolean'];
  stepDescription: Scalars['String'];
};

export type DreamCategoryType = {
  __typename?: 'DreamCategoryType';
  categoryName: Scalars['String'];
  defaultSteps?: Maybe<Array<Maybe<DefaultStepType>>>;
  dreamCategoryId: Scalars['Int'];
  dreamCount: Scalars['Int'];
};

export type DreamQuery = {
  __typename?: 'DreamQuery';
  categories?: Maybe<Array<Maybe<DreamCategoryType>>>;
  dreams?: Maybe<Array<Maybe<DreamType>>>;
  invitations?: Maybe<Array<Maybe<InvitationType>>>;
  users?: Maybe<Array<Maybe<UserType>>>;
};


export type DreamQueryDreamsArgs = {
  dreamId?: Maybe<Scalars['ID']>;
  searchName?: Maybe<Scalars['String']>;
  gender?: Maybe<GenderEnum>;
  status?: Maybe<DreamStateEnum>;
  ageFrom?: Maybe<Scalars['Int']>;
  ageTo?: Maybe<Scalars['Int']>;
  categories?: Maybe<Array<Maybe<Scalars['Int']>>>;
  orderBy?: Maybe<Scalars['String']>;
  orderAsc?: Maybe<Scalars['Boolean']>;
};


export type DreamQueryUsersArgs = {
  userId?: Maybe<Scalars['ID']>;
  dreamId?: Maybe<Scalars['ID']>;
  orderBy?: Maybe<Scalars['String']>;
  orderAsc?: Maybe<Scalars['Boolean']>;
};

export enum DreamStateEnum {
  Created = 'CREATED',
  Approved = 'APPROVED',
  InProgress = 'IN_PROGRESS',
  Done = 'DONE',
  Terminated = 'TERMINATED'
}

export type DreamType = {
  __typename?: 'DreamType';
  age: Scalars['Int'];
  dreamCategory?: Maybe<DreamCategoryType>;
  dreamComeTrueDate: Scalars['DateTimeOffset'];
  dreamId: Scalars['Int'];
  dreamState?: Maybe<DreamStateEnum>;
  firstName: Scalars['String'];
  gender?: Maybe<GenderEnum>;
  lastName: Scalars['String'];
  requiredSteps?: Maybe<Array<Maybe<DeamStepType>>>;
  tags: Scalars['String'];
};

export enum GenderEnum {
  NotSpecified = 'NOT_SPECIFIED',
  Male = 'MALE',
  Female = 'FEMALE'
}

export enum InvitationStateEnum {
  Pending = 'PENDING',
  Accepted = 'ACCEPTED'
}

export type InvitationType = {
  __typename?: 'InvitationType';
  code: Scalars['String'];
  email: Scalars['String'];
  invitationId: Scalars['Int'];
  status?: Maybe<InvitationStateEnum>;
};



export enum StepStateEnum {
  Awaiting = 'AWAITING',
  Done = 'DONE',
  Skiped = 'SKIPED'
}

export type UserType = {
  __typename?: 'UserType';
  brand: Scalars['String'];
  email: Scalars['String'];
  firstName: Scalars['String'];
  lastName: Scalars['String'];
  phoneNumber: Scalars['String'];
  userId: Scalars['Int'];
};
