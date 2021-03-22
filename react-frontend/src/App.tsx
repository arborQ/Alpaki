import './App.css';
import { LoginRoute } from './Pages/Authorize/LoginRoute';
import {
  BrowserRouter as Router,
  Switch,
  Route,
  Link
} from "react-router-dom";
import './i18n';

function App() {
  return (
    <Router>
      <div className="w-screen h-screen flex">
        <div className="w-1/4">
          <Link to="/authorize/login">Login</Link>
        </div>
        <div className="w-3/4">
          <div className="bg-back md:container md:mx-auto h-screen">
            <Switch>
              <LoginRoute path="/authorize/login" />
            </Switch>
          </div>
        </div>
      </div>
    </Router>
  );
}

export default App;
