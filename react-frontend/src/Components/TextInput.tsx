import React, { LegacyRef, MutableRefObject } from 'react';

export interface TextInputProps extends React.InputHTMLAttributes<HTMLInputElement> {
    label?: string;
    name: string;
    error?: string | null;
    inputRef?: MutableRefObject<HTMLInputElement | undefined>;
}

export function TextInput(props: TextInputProps) {
    const {
        label = '',
        className = '',
        error = null,
        name,
        inputRef,
        ...otherProps
    } = props;

    const itemClassName = `w-full p-2 shadow focus:shadow-md focus:outline-none dark:bg-gray-200 ${className}`;

    return (
        <div className="w-full pb-4">
            <label className="font-bold text-xs" htmlFor={name}>{label}</label>
            <input {...otherProps} ref={inputRef as LegacyRef<HTMLInputElement>} id={name} className={itemClassName} />
            {error && <div className="text-xs text-red-500 absolute">{error}</div>}
        </div>
    );
}