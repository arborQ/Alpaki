import { createContext, useEffect, useState } from 'react';
import { useTranslation } from 'react-i18next';

export interface ConfigurationContextValue {
    config: ConfigModel

    changeConfig: (newConfig: Partial<ConfigModel>) => void;
}

interface ConfigModel {
    darkMode: 'on' | 'off' | 'auto';
    language: 'pl' | 'en'
}

const CONFIG_STORE_KEY = 'alpaki-config';
const defaultConfig: ConfigModel = { darkMode: 'auto', language: 'pl' };

const configurationContext = createContext<ConfigurationContextValue>({ config: defaultConfig, changeConfig: () => { } });

export const ConfigurationContext = configurationContext;
export const ConfigurationContextConsumer = configurationContext.Consumer;

export function ConfigurationContextProvider(props: { children: any }) {
    const configString = localStorage.getItem(CONFIG_STORE_KEY);
    const config = !!configString ? JSON.parse(configString) as ConfigModel : defaultConfig;
    const [stateConfig, changeConfig] = useState(config);
    const { i18n } = useTranslation();

    useEffect(() => {
        if (stateConfig.darkMode === 'on') {
            document.documentElement.classList.add('dark');
        } else {
            document.documentElement.classList.remove('dark');
        }
        i18n.changeLanguage(stateConfig.language);

    }, [stateConfig.darkMode, stateConfig.language, i18n]);

    return (
        <ConfigurationContext.Provider value={{
            config: stateConfig,
            changeConfig: newConfig => {
                const updatedConfig = { ...stateConfig, ...newConfig };
                changeConfig(updatedConfig);
                localStorage.setItem(CONFIG_STORE_KEY, JSON.stringify(updatedConfig));
            }
        }}>
            {props.children}
        </ConfigurationContext.Provider>
    );
}
