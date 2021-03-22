import React from 'react';

export interface ButtonProps extends React.ButtonHTMLAttributes<HTMLButtonElement> {
}

export function SubmitButton(props: ButtonProps) {
    return (
        <button {...props} type="submit" className="bg-primary text-gray-50 transition-shadow rounded-md p-2 mt-4 w-full hover:shadow focus:shadow-md focus:outline-none dark:bg-black"></button>
    );
}