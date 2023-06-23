import { BrowserRouter } from "react-router-dom";
import { AppRoutes } from "./routes";
import { AppThemeProvider } from "./shared/context/ThemeContext";
import { AppBar, Button, Input, Toolbar } from "@mui/material";

export const App = () => {
  return (
    <AppThemeProvider>
      <AppBar position="sticky">
        <Toolbar>
        </Toolbar>
      </AppBar>
        <BrowserRouter>
          <AppRoutes />
        </BrowserRouter>
    </AppThemeProvider>
  );
}
