import { createContext, useState } from 'react';

export interface ConfigurationContextValue {
    
}
const AUTHORIZE_STORE_KEY = 'alpaki-config';

const configurationContext = createContext<ConfigurationContextValue>({  });

export const ConfigurationContext = configurationContext;
export const ConfigurationContextConsumer = configurationContext.Consumer;

export function ConfigurationContextProvider(props: { children: any }) {

    return (
        <ConfigurationContext.Provider value={{
        }}>
            {props.children}
        </ConfigurationContext.Provider>
    );
}
