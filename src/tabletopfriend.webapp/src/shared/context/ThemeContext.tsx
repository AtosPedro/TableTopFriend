import { Box, ThemeProvider } from "@mui/material";
import { createContext, useCallback, useContext, useMemo, useState } from "react";
import { DarkTheme, LightTheme } from "../themes";

interface IThemeContextData {
    theme: 'light' | 'dark';
    toggleTheme: () => void;
}

type Props = {
    children?: React.ReactNode
};

const ThemeContext = createContext({} as IThemeContextData);

export const useAppThemeContext = () => {
    return useContext(ThemeContext);
}

export const AppThemeProvider: React.FC<Props> = ({ children }) => {

    const [theme, setTheme] = useState<'light' | 'dark'>('light');

    const toggleTheme = useCallback(() => {
        setTheme(theme === 'light' ? 'dark' : 'light');
    }, []);

    const currentTheme = useMemo(() => {
        return theme === 'light' ? LightTheme : DarkTheme;
    }, [theme]);

    return (
        <ThemeContext.Provider value={{ theme, toggleTheme }}>
            <ThemeProvider theme={currentTheme}>
                <Box width="100vw" height="100vh">
                    {children}
                </Box>
            </ThemeProvider>
        </ThemeContext.Provider>
    );
}