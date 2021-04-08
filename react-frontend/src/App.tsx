import { LoginRoute } from './Pages/Authorize/LoginRoute';
import { DashboardRoute } from './Pages/Dashboard/DashboardRoute';
import { ProductsRoute } from './Pages/Products/products.route';
import { BabyRoute } from './Pages/Baby/baby.route';
import {
  BrowserRouter as Router,
  Switch,
  Link,
  Redirect
} from "react-router-dom";
import { useEffect } from 'react';
import './i18n';
import { useTranslation } from 'react-i18next';

import { ToggleDarkModeSwitch } from 'Parts/ToggleDarkModeSwitch';
import { ToggleLanguageSwitch } from 'Parts/ToggleLanguageSwitch';
import { useContext } from 'react';
import { AuthorizeContext, AuthorizeMode } from './Contexts/AuthorizeContext';


function App() {
  const { t } = useTranslation();
  const { currentUser, updateUser } = useContext(AuthorizeContext);

  const { mode } = currentUser;
  const isAuthorized = !!currentUser?.id;

  useEffect(() => {
    const metaThemeColor = document.querySelector("meta[name=theme-color]");
    const colors = {
      [AuthorizeMode.Baby] : '#ff8882',
      [AuthorizeMode.Shop] : '#9dbeb9',
      [AuthorizeMode.Full] : '#ce1212',
      [AuthorizeMode.None] : '#9ede73',
    }
    if (metaThemeColor) {
      metaThemeColor.setAttribute("content", colors[mode] ?? '#9ede73');
    }
  }, [mode]);

  const menuOptions = [
    { path: '/dashboard', text: t('menu.home') },
    { path: '/authorize/login', text: t('menu.login'), show: !isAuthorized },
    { path: '/shop/products', text: t('menu.shop.products'), mode: AuthorizeMode.Shop },
    { path: '/shop/products/ai', text: t('menu.shop.products.ai'), mode: AuthorizeMode.Shop },
    { path: '/baby/oliwka', text: t('menu.baby.oliwia'), mode: AuthorizeMode.Baby },
  ].filter(m => (m.show === undefined || m.show === true) && (m.mode === undefined || (m.mode & mode) === m.mode));

  return (
    <Router>
      <div className="w-screen h-screen flex">
        <div className="w-1/6 flex flex flex-col bg-gray-100 dark:bg-gray-500 hidden sm:block">
          {
            menuOptions.map(o => <Link className="w-full block text-center text-primary hover:text-secondary dark:text-secondary dark:hover:text-primary pt-2 pb-2" key={o.path} to={o.path}>{o.text}</Link>)
          }
          {!isAuthorized ? null : <button onClick={() => { updateUser(); }}
            className="w-full block text-center text-primary hover:text-secondary dark:text-secondary dark:hover:text-primary pt-2 pb-2">
            {t('log-out')}
          </button>}
          <ToggleDarkModeSwitch />
          <ToggleLanguageSwitch />
        </div>
        <div className="w-full sm:w-5/6 bg-back dark:bg-gray-700 h-screen overflow-hidden overflow-y-auto">
          <Switch>
            <DashboardRoute path="/dashboard" />
            <LoginRoute path="/authorize/login" />
            <ProductsRoute path="/shop/products" />
            <BabyRoute path="/baby/oliwka" />
            <Redirect to="/dashboard" />
          </Switch>
        </div>
      </div>
    </Router>
  );
}

export default App;
