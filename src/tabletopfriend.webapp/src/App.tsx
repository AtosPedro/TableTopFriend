import { BrowserRouter } from "react-router-dom";
import { AppRoutes } from "./routes";
import { AppThemeProvider } from "./shared/context/ThemeContext";
import { AppBar, Button, Input, Toolbar } from "@mui/material";
import { AppNavigationBar } from "./shared/components/AppNavigationBar";

export const App = () => {
  return (
    <AppThemeProvider>
      <AppNavigationBar />
       <BrowserRouter>
          <AppRoutes />
        </BrowserRouter>
    </AppThemeProvider>
  );
}
