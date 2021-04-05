import React, { useContext } from 'react';
import { ThemeContext } from './theme/ThemeProvider';
import PrimaryButton from './components/Button/PrimaryButton';
import SecondaryButton from './components/Button/SecondaryButton';

const App: React.FC = () => {
  const setThemeName = useContext(ThemeContext)

  return (
    <div>
      <PrimaryButton onClick={() => setThemeName("DarkTheme")}>Dark Mode</PrimaryButton>
      <SecondaryButton onClick={() => setThemeName("LightTheme")}>Light Mode</SecondaryButton>
    </div>
  )
}

export default App;
