import { useContext } from 'react';
import { ConfigurationContext, DarkModeOption } from 'Contexts/ConfigurationContext';
import { Button } from 'Components/Button'
import { useTranslation } from 'react-i18next';

const nextText = {
    [DarkModeOption.Off]: 'Off' ,
    [DarkModeOption.On]: 'On',
    [DarkModeOption.Auto]: 'Auto'
};

export function ToggleDarkModeSwitch() {
    const configContext = useContext(ConfigurationContext);
    const { t } = useTranslation();

    return (
        <Button onClick={() => {
            const next = {
                [DarkModeOption.Off]: DarkModeOption.On ,
                [DarkModeOption.On]: DarkModeOption.Auto,
                [DarkModeOption.Auto]: DarkModeOption.Off
            }
            

            const nextValue = next[configContext.config.darkMode] ?? DarkModeOption.On;
            configContext.changeConfig({ darkMode: nextValue })
        }}>
            {t('toggleDarkModeFormat', { mode: nextText[configContext.config.darkMode] })}
        </Button>
    );
}
