import React from 'react';
import {
    Route,
} from "react-router-dom";
import { ProductList } from './products.list';

export function ProductsRoute({ path }: { path: string }) {
    return (
        <Route path={path} exact>
            <ProductList />
        </Route>
    );
}