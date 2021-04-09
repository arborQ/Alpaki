import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import App from './App';
import reportWebVitals from './reportWebVitals';
import { AuthorizeContextProvider } from 'Contexts/AuthorizeContext';
import { ConfigurationContextProvider } from 'Contexts/ConfigurationContext';
import './i18n';

ReactDOM.render(
  <React.StrictMode>
    <AuthorizeContextProvider>
      <ConfigurationContextProvider>
        <App />
      </ConfigurationContextProvider>
    </AuthorizeContextProvider>
  </React.StrictMode>,
  document.getElementById('root')
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals(console.log);
