import { FaAngleDoubleRight as BurgerMenu, FaAngleDoubleLeft as BurgerMenuActive } from 'react-icons/fa';
import { useState } from 'react';

interface MenuProps {
    children: any;
}

export function MenuComponent({ children }: MenuProps) {
    const [showMenu, toggleMenu] = useState(false);
    return (
        <>
            <div className="w-1/6 flex flex flex-col bg-gray-100 dark:bg-gray-500 hidden sm:block">
                {children}
            </div>
            {
                showMenu && (
                    <div className="w-1/6 flex flex flex-col bg-gray-100 dark:bg-gray-500 block sm:hidden absolute left-0 top-0 h-screen">
                        {children}
                    </div>
                )
            }

            <button onBlur={() => { 
                setTimeout(() => { toggleMenu(false); }, 0);
             }} onClick={() => toggleMenu(!showMenu)} className="absolute bottom-0 text-4xl right-0 block sm:hidden mb-1 mr-1 cursor-pointer">
                { showMenu? <BurgerMenuActive />: <BurgerMenu /> }
            </button>
        </>
    );
}