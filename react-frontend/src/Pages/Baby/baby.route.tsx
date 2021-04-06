import React from 'react';
import {
    Route,
} from "react-router-dom";
import { BabyPage } from './baby.page';

export function BabyRoute({ path }: { path: string }) {
    return (
        <>
            <Route path={path} exact>
                <BabyPage />
            </Route>
        </>
    );
}