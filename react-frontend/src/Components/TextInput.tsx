import React from 'react';

export interface TextInputProps extends React.InputHTMLAttributes<HTMLInputElement> {
    label?: string;
    name: string;
}

export function TextInput(props: TextInputProps) {
    const {
        label = '',
        className = '',
        name,
        ...otherProps
    } = props;

    const itemClassName = `w-full p-2 mb-2 shadow focus:shadow-md focus:outline-none dark:bg-gray-200 ${className}`;

    return (
        <div className="w-full">
            <label className="font-bold text-xs" htmlFor={name}>{label}</label>
            <input {...otherProps} id={name} className={itemClassName} />
        </div>
    );
}