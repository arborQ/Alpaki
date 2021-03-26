import React from 'react';
import { Card } from 'Components/Card'
import algoliasearch from 'algoliasearch/lite';
import { InstantSearch, SearchBox, Hits } from 'react-instantsearch-dom';
import { ProductTile } from './product.tile';
import { SearchItem } from './product.models';

const searchClient = algoliasearch('SR9N5725NZ', '164a52f63b83b46dc1e9a88afc977f93');

searchClient.search()

export function ProductSearch() {
    return (
        <div className="w-full flex justify-center">
            <div className="w-full lg:w-3/4 p-4">
                <Card>
                    <InstantSearch searchClient={searchClient} indexName="party-shop-index">
                        <SearchBox />
                        <Hits hitComponent={hint => (
                            <div className="mt-4">
                                <ProductTile item={hint.hit as any} />
                            </div>
                        )} />
                    </InstantSearch>
                </Card>
            </div>
        </div>
    );
}