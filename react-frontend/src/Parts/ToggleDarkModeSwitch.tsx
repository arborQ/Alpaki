import { useContext } from 'react';
import { ConfigurationContext } from 'Contexts/ConfigurationContext';
import { Switch as SwitchButton } from 'Components/Switch';

export function ToggleDarkModeSwitch() {
    const configContext = useContext(ConfigurationContext);

    return (
        <SwitchButton 
        name="dark_mode"
        label="Toggle dark mode"
        checked={configContext.config.darkMode === 'on'} 
        onChecked={(dark) => {
            configContext.changeConfig({ darkMode: dark ? 'on': 'off' });
        }} />
    );
}
