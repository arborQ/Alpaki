
export interface SearchItem {
    key: string;
    category_name: string;
    description: string;
    price_gross: string;
    price_net: string;
    stock: string;
    name: string;
    photos: string;
}

export interface SearchResponse {
    value: SearchItem[];
}
