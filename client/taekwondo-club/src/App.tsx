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
} from "@mantine/core";
import PrivateRoute from "./routes/PrivateRoute";
import NavbarSimple from "./components/Navbar";
import Brand from "./components/Brand";
import { useCallback, useState } from "react";
import { NotificationsProvider } from '@mantine/notifications';
import "./localization";

const App = () => {
  const [color, setColor] = useState<ColorScheme>("light");

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
          fontFamily: "Open Sans, sans serif",
          colors: {
            'ocean-blue': ['#7AD1DD', '#5FCCDB', '#44CADC', '#2AC9DE', '#1AC2D9', '#11B7CD', '#09ADC3', '#0E99AC', '#128797', '#147885'],
            'bright-pink': ['#F0BBDD', '#ED9BCF', '#EC7CC3', '#ED5DB8', '#F13EAF', '#F71FA7', '#FF00A1', '#E00890', '#C50E82', '#AD1374'],
          },
          spacing: { xs: 15, sm: 20, md: 25, lg: 30, xl: 40 },
          colorScheme: color,
        }}
      >
        <NotificationsProvider position="top-right" zIndex={2077}>
          <AppShell
            // Add fixed prop if don't want sidebar to shrink
            // But it will make main content to be tear off into right side
            // fixed
            padding="xs"
            navbar={<NavbarSimple />}
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
