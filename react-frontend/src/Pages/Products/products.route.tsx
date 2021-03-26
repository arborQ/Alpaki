import React from 'react';
import {
    Route,
} from "react-router-dom";
import { ProductList } from './products.list';
import { ProductSearch } from './products.search';

export function ProductsRoute({ path }: { path: string }) {
    return (
        <>
            <Route path={path} exact>
                <ProductList />
            </Route>
            <Route path={`${path}/ai`} exact>
                <ProductSearch />
            </Route>
        </>
    );
}