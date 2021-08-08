import React from 'react';

export interface CardProps {
    children: any
}

const style = {
    boxShadow:  "-18px -18px 34px #bebebe, 18px 18px 34px #ffffff",
    borderRadius: 25,
    background: "#e0e0e0"
};

export function Card({ children }: CardProps) {
    return (
        <div style={style} className="p-4">
            { children }
        </div>
    );
}