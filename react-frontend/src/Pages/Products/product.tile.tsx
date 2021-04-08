import { Card } from 'Components/Card';
import { SearchItem } from './product.models';

interface ProductTileProps {
    item: SearchItem;
}

export function ProductTile(props: ProductTileProps) {
    const { item } = props;
    const [photo] = item.photos.split(';');
    return (
        <Card>
            <div className="flex content-around">
                <div className="flex flex-col">
                    <h1 className="font-bold">{item.name}</h1>
                    <h2 className="font-light">{item.description}</h2>
                </div>
                <img src={photo} className="h-10 w-10" alt={item.name} />
            </div>
        </Card>
    );
}