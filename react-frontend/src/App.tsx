import { LoginRoute } from './Pages/Authorize/LoginRoute';
import { DashboardRoute } from './Pages/Dashboard/DashboardRoute';
import {
  BrowserRouter as Router,
  Switch,
  Link, 
  Redirect
} from "react-router-dom";
import './i18n';
import { useTranslation } from 'react-i18next';

import { ToggleDarkModeSwitch } from 'Parts/ToggleDarkModeSwitch';

function App() {
  const { t }  = useTranslation();

  const menuOptions = [
    { path: '/dashboard', text : t('menu.home') },
    { path: '/authorize/login', text : t('menu.login') },
  ];
  return (
    <Router>
      <div className="w-screen h-screen flex">
        <div className="w-1/6 flex flex flex-col">
          {
            menuOptions.map(o => <Link className="w-full block text-center pt-2 pb-2" key={o.path} to={o.path}>{o.text}</Link>)
          }
          <ToggleDarkModeSwitch />
        </div>
        <div className="w-5/6">
          <div className="bg-back dark:bg-gray-700 md:container md:mx-auto h-screen">
            <Switch>
              <DashboardRoute path="/dashboard" />
              <LoginRoute path="/authorize/login" />
              <Redirect to="/dashboard" />
            </Switch>
          </div>
        </div>
      </div>
    </Router>
  );
}

export default App;
