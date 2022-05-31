import routes from "./routes";
import "./css/app.css";
import { Routes, Route } from "react-router-dom";
import {
  MantineProvider,
  ColorSchemeProvider,
  AppShell,
  Header,
  MediaQuery,
  Burger,
  Text,
  Navbar,
  ColorScheme,
  ScrollArea,
  GlobalStyles,
  Global,
} from "@mantine/core";
import PrivateRoute from "./routes/PrivateRoute";
import Brand from "./components/Brand";
import { NavbarMinimal } from "./components/Navbar";
import { useCallback, useState } from "react";
import { NotificationsProvider } from "@mantine/notifications";
import "./localization";
import theme, { styles, globalStyles } from "./theme";

const App = () => {
  const [color, setColor] = useState<ColorScheme>("dark");

  const toggleColorScheme = useCallback(() => {
    setColor((prev) => (prev === "light" ? "dark" : "light"));
  }, []);

  return (
    <ColorSchemeProvider
      toggleColorScheme={toggleColorScheme}
      colorScheme={color}
    >
      <MantineProvider
        theme={{
          // Override any other properties from default theme
          fontFamily: "Montserrat",
          colors: {},
          spacing: { xs: 15, sm: 20, md: 25, lg: 30, xl: 40 },
          colorScheme: color,
        }}
      >
        <Global styles={globalStyles} />
        <NotificationsProvider position="top-right" zIndex={2077}>
          <AppShell
            // Add fixed prop if don't want sidebar to shrink
            // But it will make main content to be tear off into right side
            // fixed
            padding="xs"
            navbar={<NavbarMinimal />}
            header={
              <Header height={60} p="xs">
                <Brand />
              </Header>
            }
            styles={(theme) => ({
              main: {
                backgroundColor:
                  theme.colorScheme === "dark"
                    ? theme.colors.dark[8]
                    : theme.colors.gray[0],
              },
            })}
          >
            <Routes>
              {routes.map((route) => {
                const Element = route.element;
                return (
                  <Route
                    key={route.path}
                    path={route.path}
                    element={
                      route?.private ? (
                        <PrivateRoute>
                          <Element />
                        </PrivateRoute>
                      ) : (
                        <Element />
                      )
                    }
                  />
                );
              })}
            </Routes>
          </AppShell>
        </NotificationsProvider>
      </MantineProvider>
    </ColorSchemeProvider>
  );
};

export default App;
