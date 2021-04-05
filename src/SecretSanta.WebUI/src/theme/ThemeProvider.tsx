import React, { Context, PropsWithChildren, ReactElement, useState } from 'react';
import getThemeByName from './base';
import { MuiThemeProvider } from '@material-ui/core';

const ThemeContext: Context<any> = React.createContext((themeName: string): void => {});

const ThemeProvider: React.FC<PropsWithChildren<{}>> = (props) : ReactElement<typeof MuiThemeProvider> => {
    const storedThemeName = localStorage.getItem("appTheme") || "LightTheme";
    const [themeName, _setThemeName] = useState(storedThemeName);

    const setThemeName = (themeName: string): void => {
        localStorage.setItem("appTheme", themeName);
        _setThemeName(themeName);
    }

    const theme = getThemeByName(themeName);

    return (
        <ThemeContext.Provider value={setThemeName}>
            <MuiThemeProvider theme={theme}>{props.children}</MuiThemeProvider>
        </ThemeContext.Provider>
    );
}

export { ThemeContext };
export default ThemeProvider