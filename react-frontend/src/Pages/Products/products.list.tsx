import React, { useEffect } from 'react';
import { Card } from 'Components/Card';

const query = {
    "count":false,
    "skip":0,
    "top":50,
    "searchMode":"any",
    "queryType":"simple",
    "facets":["is_available,count:5,sort:count"],
    "search":"kartka"
};

export function ProductList() {
    useEffect(() => {
        fetch('/api/search?index=party-shop-index', { method: 'POST', body: JSON.stringify(query) })
    }, []);
    return (
        <div className="w-full flex justify-center">
            <div className="w-full lg:w-3/4 p-4">
                <Card>
                    Product list
                </Card>
            </div>
        </div>
    );
}