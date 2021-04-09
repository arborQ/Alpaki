import React from 'react';
import { Button, ButtonProps } from './Button';

export function SubmitButton(props: ButtonProps) {
    return (
        <Button {...props} type="submit" ></Button>
    );
}