import React, { useState } from 'react';
import { FaRegHandPointLeft as LeftIcon, FaRegHandPointRight as RightIcon } from "react-icons/fa";
import { useTranslation } from 'react-i18next';

export function BabyPage() {
    const { t } = useTranslation();
    const [history, updateHistory] = useState<Array<{ side: string }>>([]);
    return (
        <div className="w-full flex justify-center flex flex-col">
            <div className="w-full lg:w-3/4 p-4 flex">
                <button onClick={() => { updateHistory([{ side : 'left' }, ...history]); }} className="shadow bg-gray-50 dark:bg-gray-500 hover:shadow-xl transition-shadow rounded-md p-4 w-1/2 flex justify-center">
                    <span className="flex flex-col items-center h-full">
                        <LeftIcon />
                        <span>{t('direction.left')}</span>
                    </span>
                </button>
                <button onClick={() => { updateHistory([{ side : 'right' }, ...history]); }} className="shadow bg-gray-50 dark:bg-gray-500 hover:shadow-xl transition-shadow rounded-md p-4 ml-2 flex justify-center w-1/2">
                    <span className="flex flex-col items-center h-full">
                        <RightIcon />
                        <span>{t('direction.right')}</span>
                    </span>
                </button>
            </div>
            {
                history.map((h, i) => (<div key={i}>{h.side}</div>))
            }
        </div>
    );
}