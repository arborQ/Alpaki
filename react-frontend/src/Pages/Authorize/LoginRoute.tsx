import React from 'react';
import {
    Route,
} from "react-router-dom";
import { LoginPage } from './Login';

export function LoginRoute({ path }: { path: string }) {
    return (
        <Route path={path} exact>
            <LoginPage />
        </Route>
    );
}