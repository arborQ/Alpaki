export type Maybe<T> = T | null;
export type Exact<T extends { [key: string]: unknown }> = {
  [K in keyof T]: T[K];
};
/** All built-in and custom scalars, mapped to their actual values */
export type Scalars = {
  ID: string;
  String: string;
  Boolean: boolean;
  Int: number;
  Float: number;
  /** The `Date` scalar type represents a year, month and day in accordance with the [ISO-8601](https://en.wikipedia.org/wiki/ISO_8601) standard. */
  Date: any;
  /** The `DateTime` scalar type represents a date and time. `DateTime` expects timestamps to be formatted in accordance with the [ISO-8601](https://en.wikipedia.org/wiki/ISO_8601) standard. */
  DateTime: any;
  /** The `DateTimeOffset` scalar type represents a date, time and offset from UTC. `DateTimeOffset` expects timestamps to be formatted in accordance with the [ISO-8601](https://en.wikipedia.org/wiki/ISO_8601) standard. */
  DateTimeOffset: any;
  /** The `Seconds` scalar type represents a period of time represented as the total number of seconds. */
  Seconds: any;
  /** The `Milliseconds` scalar type represents a period of time represented as the total number of milliseconds. */
  Milliseconds: any;
  Decimal: any;
};

export type Query = {
  __typename?: 'Query';
  dreams?: Maybe<DreamQuery>;
  moto?: Maybe<MotoGraphQuery>;
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
  status?: Maybe<DreamStateEnum>;
  ageFrom?: Maybe<Scalars['Int']>;
  ageTo?: Maybe<Scalars['Int']>;
  page?: Maybe<Scalars['Int']>;
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

export type UserType = {
  __typename?: 'UserType';
  brand: Scalars['String'];
  email: Scalars['String'];
  firstName: Scalars['String'];
  lastName: Scalars['String'];
  phoneNumber: Scalars['String'];
  userId: Scalars['Int'];
};

export type DreamType = {
  __typename?: 'DreamType';
  age: Scalars['Int'];
  categoryName: Scalars['String'];
  cityName: Scalars['String'];
  displayName: Scalars['String'];
  dreamCategory?: Maybe<DreamCategoryType>;
  dreamCategoryId: Scalars['Int'];
  dreamComeTrueDate: Scalars['DateTimeOffset'];
  dreamId: Scalars['Int'];
  dreamImage?: Maybe<ImageType>;
  dreamImageId?: Maybe<Scalars['ID']>;
  dreamState?: Maybe<DreamStateEnum>;
  finishedStepCount: Scalars['Int'];
  requiredSteps?: Maybe<Array<Maybe<DeamStepType>>>;
  sponsors?: Maybe<AssignedSponsorType>;
  stepCount: Scalars['Int'];
  tags: Scalars['String'];
  title: Scalars['String'];
};

export type ImageType = {
  __typename?: 'ImageType';
  imageData?: Maybe<Scalars['ID']>;
};

export type AssignedSponsorType = {
  __typename?: 'AssignedSponsorType';
  sponsor?: Maybe<SponsorType>;
  sponsorId: Scalars['Int'];
};

export type SponsorType = {
  __typename?: 'SponsorType';
  brand: Scalars['String'];
  contactPersonName: Scalars['String'];
  cooperationType?: Maybe<SponsorTypeEnum>;
  displayName: Scalars['String'];
  email: Scalars['String'];
  sponsorId: Scalars['Int'];
};

export enum SponsorTypeEnum {
  PermanentCooperation = 'PERMANENT_COOPERATION',
  TemporaryCooperation = 'TEMPORARY_COOPERATION',
}

export type DreamCategoryType = {
  __typename?: 'DreamCategoryType';
  categoryName: Scalars['String'];
  defaultSteps?: Maybe<Array<Maybe<DefaultStepType>>>;
  dreamCategoryId: Scalars['Int'];
  dreamCount: Scalars['Int'];
};

export type DefaultStepType = {
  __typename?: 'DefaultStepType';
  dreamCategoryDefaultStepId: Scalars['Int'];
  isSponsorRelated: Scalars['Boolean'];
  stepDescription: Scalars['String'];
};

export enum DreamStateEnum {
  Created = 'CREATED',
  Approved = 'APPROVED',
  InProgress = 'IN_PROGRESS',
  Done = 'DONE',
  Terminated = 'TERMINATED',
}

export type DeamStepType = {
  __typename?: 'DeamStepType';
  dreamStepId: Scalars['Int'];
  stepDescription: Scalars['String'];
  stepState?: Maybe<StepStateEnum>;
};

export enum StepStateEnum {
  Awaiting = 'AWAITING',
  Done = 'DONE',
  Skiped = 'SKIPED',
}

export type InvitationType = {
  __typename?: 'InvitationType';
  code: Scalars['String'];
  email: Scalars['String'];
  invitationId: Scalars['Int'];
  status?: Maybe<InvitationStateEnum>;
};

export enum InvitationStateEnum {
  Pending = 'PENDING',
  Accepted = 'ACCEPTED',
}

export type MotoGraphQuery = {
  __typename?: 'MotoGraphQuery';
  brands?: Maybe<BrandPagedCollectionType>;
};

export type BrandPagedCollectionType = {
  __typename?: 'BrandPagedCollectionType';
  items?: Maybe<Array<Maybe<BrandType>>>;
  totalCount?: Maybe<Scalars['Int']>;
};

export type BrandPagedCollectionTypeItemsArgs = {
  search?: Maybe<Scalars['String']>;
  page?: Maybe<Scalars['Int']>;
};

export type BrandPagedCollectionTypeTotalCountArgs = {
  search?: Maybe<Scalars['String']>;
  page?: Maybe<Scalars['Int']>;
};

export type BrandType = {
  __typename?: 'BrandType';
  brandId: Scalars['Int'];
  brandName: Scalars['String'];
  isActive: Scalars['Boolean'];
  modelCount: Scalars['Int'];
  models?: Maybe<Array<Maybe<ModelType>>>;
};

export type ModelType = {
  __typename?: 'ModelType';
  modelId: Scalars['Int'];
  modelName: Scalars['String'];
};
