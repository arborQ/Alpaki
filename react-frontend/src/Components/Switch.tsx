import React from 'react';

export interface SwitchProps {
    checked: boolean;
    onChecked: (checked: boolean) => void;
    label?: string;
    name: string;
}

export function Switch(props: SwitchProps) {
    const { checked, onChecked, label, name } = props;
    return (
        <span className="flex">
            <span className="relative inline-block w-10 mr-2 align-middle select-none transition duration-200 ease-in">
                <input onChange={() => { onChecked(!checked); }} type="checkbox" checked={checked} name={name} id={name} className={`${checked ? 'right-0 bg-primary border-primary' : 'bg-white'} absolute block w-6 h-6 rounded-full border-4 appearance-none cursor-pointer`} />
                <label htmlFor="toggle" className="toggle-label block overflow-hidden h-6 rounded-full bg-gray-300 cursor-pointer"></label>
            </span>
            { label && <label htmlFor={name} className="text-xs text-gray-700">{label}</label>}
        </span>
    );
}