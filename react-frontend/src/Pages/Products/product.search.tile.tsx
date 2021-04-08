import { Card } from 'Components/Card';

interface ProductSearchItem {
    productName: string;
    fromPriceNet: number;
    toPriceNet: number;
    fromPriceGross: number;
    toPriceGross: number;
    imageUrls: string[];
    variants: any[];
}

interface ProductTileProps {
    item: ProductSearchItem;
}

export function ProductSearchTile(props: ProductTileProps) {
    const { item } = props;

    return (
        <Card>
            <div className="flex content-around">
                <div className="flex flex-col">
                    <h1 className="font-bold">{item.productName}</h1>
                    <h2 className="font-light">{item.toPriceNet} ({item.toPriceGross}) zł</h2>
                </div>
            </div>
        </Card>
    );
}