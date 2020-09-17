import { ApplicationType } from 'src/app/authorize/sign-in/sign-in.models';

export interface CurrentUser {
    login: string;
    expire: Date;
    applicationType: ApplicationType;
}
