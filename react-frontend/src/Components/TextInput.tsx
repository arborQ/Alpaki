import React from 'react';

export interface TextInputProps extends React.InputHTMLAttributes<HTMLInputElement> {
    label: string;
    name: string;
}

export function TextInput(props: TextInputProps) {
    const { label, name, ...otherProps } = props;

    return (
        <div className="w-full">
            <label className="font-bold text-xs" htmlFor={name}>{label}</label>
            <input {...otherProps} id={name} className="w-full p-2 mb-2 shadow focus:shadow-md focus:outline-none" />
        </div>
    );
}