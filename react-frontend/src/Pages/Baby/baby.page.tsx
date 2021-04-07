import React, { useState } from 'react';
import { 
    FaRegHandPointLeft as LeftIcon, 
    FaRegHandPointRight as RightIcon,
    FaTrashAlt as RemoveIcon
} from "react-icons/fa";
import { useTranslation } from 'react-i18next';
import { ListComponent } from 'Components/List';

interface BabyItem {
    side: 'left' | 'right';
    date: string;
}

function getStoredData(key: string): BabyItem[] {
    const itemString = localStorage.getItem(`baby-data-${key}`) ?? '[]';

    return JSON.parse(itemString);
}

function addStoredData(key: string, items: BabyItem[]): void {
    const itemString = localStorage.getItem(`baby-data-${key}`) ?? '[]';
    localStorage.setItem(`baby-data-${key}`, JSON.stringify(items));
}

export function BabyPage() {
    const { t } = useTranslation();
    const [history, updateHistory] = useState<Array<BabyItem>>(getStoredData('oliwia'));

    return (
        <div className="w-full flex justify-center flex flex-col">
            <div className="w-full lg:w-3/4 p-4 flex">
                <button onClick={() => {
                    const items = [{ side: 'left', date: new Date().toUTCString() } as BabyItem, ...history];
                    updateHistory(items);
                    addStoredData('oliwia', items);
                }} className="shadow bg-gray-50 dark:bg-gray-500 hover:shadow-xl transition-shadow rounded-md p-4 w-1/2 flex justify-center">
                    <span className="flex flex-col items-center h-full">
                        <LeftIcon />
                        <span>{t('direction.left')}</span>
                    </span>
                </button>
                <button onClick={() => {
                    const items = [{ side: 'right', date: new Date().toUTCString() } as BabyItem, ...history];
                    updateHistory(items);
                    addStoredData('oliwia', items);
                }} className="shadow bg-gray-50 dark:bg-gray-500 hover:shadow-xl transition-shadow rounded-md p-4 ml-2 flex justify-center w-1/2">
                    <span className="flex flex-col items-center h-full">
                        <RightIcon />
                        <span>{t('direction.right')}</span>
                    </span>
                </button>
            </div>
            <div className="w-full flex flex">
                <div className="w-full lg:w-3/4 p-4 flex flex-col">
                    <ListComponent items={history}>
                        {
                            item => <div>
                                <span className="flex flex-row h-full items-center">
                                    {item.side === 'left' ? <LeftIcon className="mr-2" /> : <RightIcon className="mr-2" />}
                                    {item.side === 'left' ? <span>{t('direction.left')}</span> : <span>{t('direction.right')}</span>}
                                    {item.date}
                                    <button className="outline-none" onClick={() => { 
                                        const items = [...history.filter(h => h !== item)];
                                        updateHistory(items);
                                        addStoredData('oliwia', items);
                                     }}><RemoveIcon /></button>
                                </span>
                            </div>
                        }
                    </ListComponent>
                </div></div>
        </div>
    );
}