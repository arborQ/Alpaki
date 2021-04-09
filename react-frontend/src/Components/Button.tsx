import React from 'react';

export interface ButtonProps extends React.ButtonHTMLAttributes<HTMLButtonElement> {
}

export function Button(props: ButtonProps) {
    const { className } = props;

    return (
        <button {...props} className={`disabled:opacity-90 disabled:cursor-none bg-primary text-gray-50 transition-shadow rounded-md p-2 w-full hover:shadow focus:shadow-md focus:outline-none dark:bg-black ${className}`}></button>
    );
}