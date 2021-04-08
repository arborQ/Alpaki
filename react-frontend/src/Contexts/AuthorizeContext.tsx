import { createContext, useState } from 'react';

export enum AuthorizeMode {
    None = 0,
    Shop = 1 << 0,
    Baby = 1 << 1,
    Full = ~(~0 << 2)
}

export interface CurrentUser {
    id?: number;
    login?: string;
    mode: AuthorizeMode;
}

export interface AuthorizeContextValue {
    currentUser: CurrentUser;
    updateUser: (user?: CurrentUser) => void;
}
const AUTHORIZE_STORE_KEY = 'alpaki-auth';
const defaultUser: CurrentUser = { mode: AuthorizeMode.None };

const authorizeContext = createContext<AuthorizeContextValue>({ currentUser: defaultUser, updateUser: () => {  } });

export const AuthorizeContext = authorizeContext;
export const AuthorizeContextConsumer = authorizeContext.Consumer;
export function AuthorizeContextProvider(props: { children: any }) {
    const [currentUser, updateUser] = useState<CurrentUser | null>(CurrentUserMode());

    return (
        <AuthorizeContext.Provider value={{
            currentUser: currentUser ?? defaultUser,
            updateUser: user => {
                if (!!user) {
                    localStorage.setItem(AUTHORIZE_STORE_KEY, JSON.stringify(user));
                    updateUser(user);
                } else {
                    localStorage.removeItem(AUTHORIZE_STORE_KEY);
                    updateUser(defaultUser);
                }
            }
        }}>
            {props.children}
        </AuthorizeContext.Provider>
    );
}

export function CurrentUserMode(): CurrentUser {
    const userString = localStorage.getItem(AUTHORIZE_STORE_KEY);

    if (!!userString) {
        return JSON.parse(userString) as CurrentUser;
    } else {
        return { mode: AuthorizeMode.None };
    }
}
