export interface ISignInModel {
    login: string;
    token: string;
    applicationType: ApplicationType;
}

export enum ApplicationType {
    None = 0,
    Dream = 1,
    Moto = 2
}
