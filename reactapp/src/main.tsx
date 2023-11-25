// eslint-disable-next-line @typescript-eslint/no-unused-vars
import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './App.jsx'
import { BrowserRouter } from 'react-router-dom'

/**
 * Note: "React.StrictMode" causes problems with external javascript files (all those that have to be dynamically rendered in the 
 * HTML by the component). These problems, since are caused by that element, just appear in dev mode.
 */

ReactDOM.createRoot(document.getElementById('root')!).render(
/*    <React.StrictMode>*/
        <BrowserRouter>
            <App />
        </BrowserRouter>
/*    </React.StrictMode >*/
);