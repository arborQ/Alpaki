import React from 'react';

export interface ListProps<T> {
    items: T[];
    children: (item: T) => React.ReactElement;
}

export function ListComponent<T>(props: ListProps<T>) {
    const { items, children } = props;
    return (
        <div className="flex flex-col">
            {
                items.map((item, index) => (
                    <div key={index} className="pb-4 pt-4 pl-2 border-b">
                        {children(item)}
                    </div>
                ))
            }
        </div>
    );
}