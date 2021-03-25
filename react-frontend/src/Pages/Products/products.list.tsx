import React, { useEffect, useState } from 'react';
import { Card } from 'Components/Card';
import { TextInput } from 'Components/TextInput';

const query = {
    "count": false,
    "skip": 0,
    "top": 50,
    "searchMode": "any",
    "queryType": "simple",
    "facets": ["is_available,count:5,sort:count"],
    "search": "kartka"
};

export function ProductList() {
    const [search, changeSearch] = useState('');
    // useEffect(() => {
    //     fetch('/api/search?index=party-shop-index', { method: 'POST', body: JSON.stringify(query) })
    // }, []);
    return (
        <div className="w-full flex justify-center">
            <div className="w-full lg:w-3/4 p-4">
                <Card>
                    <div className="focus-within:bg-gray-50 focus-within:w-screen focus-within:h-screen focus-within:absolute focus-within:p-10 top-0 left-0">
                        <TextInput
                            className="focus:p-4"
                            name="search"
                            autoComplete={'off'}
                            value={search}
                            onChange={e => changeSearch(e.target.value)}
                        />
                    </div>
                    Product list
                </Card>
            </div>
        </div>
    );
}