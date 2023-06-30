import { BrowserRouter } from "react-router-dom";
import { AppRoutes } from "./routes";
import { AppThemeProvider } from "./shared/context/ThemeContext";
import { AppNavigationBar } from "./shared/components/navigation/AppNavigationBar";

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
