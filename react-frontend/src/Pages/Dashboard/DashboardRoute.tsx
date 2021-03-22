import React from 'react';
import {
    Route,
} from "react-router-dom";
import { Dashboard } from './Dashboard';

export function DashboardRoute({ path }: { path: string }) {
    return (
        <Route path={path} exact>
            <Dashboard />
        </Route>
    );
}