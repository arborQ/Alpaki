import React from 'react';

export interface CardProps {
    children: any
}

export function Card({ children }: CardProps) {
    return (
        <div className="shadow bg-gray-50 dark:bg-gray-500 hover:shadow-xl transition-shadow rounded-md p-4">
            { children }
        </div>
    );
}