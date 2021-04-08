import { useContext, useState } from 'react';
import { AuthorizeContext } from 'Contexts/AuthorizeContext';
import { FaUserCog } from 'react-icons/fa';
import { ToggleLanguageSwitch } from './ToggleLanguageSwitch';
import { ToggleDarkModeSwitch } from './ToggleDarkModeSwitch';

export function UserIcon() {
    const userContext = useContext(AuthorizeContext);
    const isAuthorized = !!userContext.currentUser?.id;
    const [showDetails, toggleDetails] = useState(false);

    if (!isAuthorized) {
        return null;
    }

    return (
        <>
            <button className="flex justify-center p-2 relative w-full focus:outline-none" onClick={() => toggleDetails(true)}>
                <FaUserCog className="text-6xl" />
                <div>{showDetails}</div>
                {
                    showDetails ? (
                        <>
                            <div className="flex absolute left-0 top-0 shadow-lg p-2 w-screen bg-gray-50 dark:bg-gray-500 z-20">
                                <ToggleLanguageSwitch />
                                <ToggleDarkModeSwitch />
                            </div>
                            <div onClick={e => {
                                e.stopPropagation();
                                toggleDetails(false);
                            }} className="w-screen h-screen fixed left-0 top-0 bg-fixed z-10 bg-gray-600 bg-opacity-75"></div>
                        </>
                    ) : null
                }
            </button>
        </>
    );
}