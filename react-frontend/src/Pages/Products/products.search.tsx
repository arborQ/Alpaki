import React from 'react';
import { Card } from 'Components/Card'
import algoliasearch from 'algoliasearch/lite';
import { InstantSearch, SearchBox, Hits, RefinementList, ClearRefinements, Configure, Pagination } from 'react-instantsearch-dom';
import { ProductSearchTile } from './product.search.tile';
import { SearchItem } from './product.models';

const searchClient = algoliasearch('SR9N5725NZ', '164a52f63b83b46dc1e9a88afc977f93');

export function ProductSearch() {
    return (
        <div className="w-full flex justify-center">
            <div className="w-full lg:w-3/4 p-4">
                <Card>
                    <InstantSearch searchClient={searchClient} indexName="party-shop-proces-index">
                        <SearchBox  />
                        <ClearRefinements />
                        <RefinementList attribute="categoryPath" />
                        <Configure hitsPerPage={10} />
                        <Pagination />
                        <Hits hitComponent={hint => (
                            <div className="mt-4">
                                <ProductSearchTile item={hint.hit as any}  />
                            </div>
                        )} />
                        <Pagination />
                    </InstantSearch>
                </Card>
            </div>
        </div>
    );
}