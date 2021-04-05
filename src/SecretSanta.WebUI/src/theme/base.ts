import { Theme } from '@material-ui/core';
import LightTheme from './LightTheme';
import DarkTheme from './DarkTheme';

const themeMap: {[key: string]: Theme} = {
    LightTheme,
    DarkTheme
}

const getThemeByName = (theme: string) => {
    return themeMap[theme];
};

export default getThemeByName;
