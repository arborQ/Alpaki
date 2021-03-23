import React from 'react';
import { Switch as SwitchButton } from 'Components/Switch';
import { useTranslation } from 'react-i18next';
export function ToggleLanguageSwitch() {
    const { i18n } = useTranslation();

    return (
        <SwitchButton  
        name="language"
        label={i18n.language}
        checked={i18n.language === 'pl'} 
        onChecked={(isPl) => {
            i18n.changeLanguage(isPl ? 'pl': 'en');
        }} />
    );
}
