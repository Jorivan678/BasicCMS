import React from 'react';
import Routing from './Routing';
import { Provider } from 'react-redux';
import { store } from './redux';

export default function App(): React.JSX.Element
{
    return (
        <Provider store={store}>
            <Routing />
        </Provider>
    );
}