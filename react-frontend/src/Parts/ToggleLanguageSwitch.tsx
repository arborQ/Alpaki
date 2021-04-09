import { Switch as SwitchButton } from 'Components/Switch';
import { useContext } from 'react';
import { ConfigurationContext } from 'Contexts/ConfigurationContext';
import { Button } from 'Components/Button'
import { useTranslation } from 'react-i18next';

export function ToggleLanguageSwitch() {
    const configContext = useContext(ConfigurationContext);
    const { t } = useTranslation();
    const otherLanguage = configContext.config.language === 'pl' ? 'en': 'pl';
    return (
        <div className="w-auto">
            <Button onClick={() => { configContext.changeConfig({ language: otherLanguage }) }} >{ t('toggleLanguageFormat', { lng: otherLanguage }) }</Button>
        </div>
    );
}
