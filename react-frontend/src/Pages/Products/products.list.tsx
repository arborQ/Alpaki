import React, { useEffect, useState, useCallback, useRef } from 'react';
import { Card } from 'Components/Card';
import { TextInput } from 'Components/TextInput';
import { debounceAsync } from 'Utils/Debounce';
import { SearchResponse, SearchItem } from './product.models';
import { ProductTile } from './product.tile';

const query = {
    "count": false,
    "skip": 0,
    "top": 10,
    "searchMode": "any",
    "queryType": "simple",
    "facets": ["is_available,count:5,sort:count"],
    "search": "kartka"
};

const searchDebounce = debounceAsync(async (search: string): Promise<SearchItem[]> => {
    const response = await fetch('/api/search?index=party-shop-index', { method: 'POST', body: JSON.stringify({ ...query, search }) });
    const json = await response.json() as SearchResponse;
    return json.value;
}, 300);

export function ProductList() {
    const [search, changeSearch] = useState('');
    const [items, updateItems] = useState([] as SearchItem[]);
    var callback = useCallback(searchDebounce, []);
    const searchInput = useRef<HTMLInputElement>();
    useEffect(() => {
        if (search.length > 0) {
            callback(search).then(updateItems);
        }
    }, [search]);

    return (
        <div className="w-full flex justify-center">
            <div className="w-full lg:w-3/4 p-4">
                <Card>
                    <form onSubmit={e => { 
                        e.preventDefault(); 
                        if(searchInput != null) {
                            searchInput?.current?.blur(); 
                        }
                        }}>
                        <div className="transition duration-500 ease-in-out focus-within:bg-gray-50 focus-within:w-screen focus-within:h-screen focus-within:absolute focus-within:p-10 top-0 left-0">
                            <TextInput
                                inputRef={searchInput}
                                className="focus:p-4 focus:leading-10 focus:text-3xl transition duration-500 ease-in-out"
                                name="search"
                                autoComplete={'off'}
                                value={search}
                                onChange={e => changeSearch(e.target.value)}
                            />
                        </div>
                    </form>
                    {
                        items.map(item => <div className="mt-4"><ProductTile key={item.key} item={item} /></div>)
                    }
                </Card>
            </div>
        </div>
    );
}