import React from 'react';
import ReactDom from 'react-dom';
import App from './App';
import { debugContextDevtool } from 'react-context-devtool';
import ThemeProvider from './theme/ThemeProvider';

const container = document.getElementById('root')
ReactDom.render(
    <ThemeProvider>
        <App />
    </ThemeProvider>,
    container
);

debugContextDevtool(container)
