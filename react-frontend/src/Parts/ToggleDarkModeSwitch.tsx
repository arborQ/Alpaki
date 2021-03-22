import React, { useState, useEffect } from 'react';
import { Switch as SwitchButton } from 'Components/Switch';

export function ToggleDarkModeSwitch() {
    const [isDark, toggleDark] = useState(false);

    useEffect(() => {
        if (localStorage.theme === 'dark' || (!('theme' in localStorage) && window.matchMedia('(prefers-color-scheme: dark)').matches)) {
            document.documentElement.classList.add('dark');
            toggleDark(true);
        } else {
            document.documentElement.classList.remove('dark');
            toggleDark(false);
        }
    }, [1]);

    return (
        <SwitchButton 
        checked={isDark} 
        onChecked={(dark) => {
            localStorage.theme = dark ? 'dark': 'light';
            if (dark) {
                document.documentElement.classList.add('dark');
            } else {
                document.documentElement.classList.remove('dark');
            }
            toggleDark(dark);
        }} />
    );
}
