import { Switch as SwitchButton } from 'Components/Switch';
import { useContext } from 'react';
import { ConfigurationContext } from 'Contexts/ConfigurationContext';

export function ToggleLanguageSwitch() {
    const configContext = useContext(ConfigurationContext);

    return (
        <SwitchButton  
        name="language"
        label={configContext.config.language}
        checked={configContext.config.language === 'pl'} 
        onChecked={(isPl) => {
            configContext.changeConfig({ language: isPl ? 'pl': 'en' })
        }} />
    );
}
