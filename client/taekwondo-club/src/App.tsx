import routes from "./routes";
import "@modulz/radix-icons";
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
import { MainLinks } from "./components/Navbar";
import Brand from "./components/Brand";
import { useCallback, useState } from "react";
import { NotificationProvider } from "./hooks/context/Notification";

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
          spacing: { xs: 15, sm: 20, md: 25, lg: 30, xl: 40 },
          colorScheme: color,
        }}
      >
        <NotificationProvider>
          <AppShell
            // Add fixed prop if don't want sidebar to shrink
            // But it will make main content to be tear off into right side
            // fixed
            padding="xs"
            navbar={
              <Navbar width={{ base: 250 }} height="calc(100vh - 65px)">
                <Navbar.Section
                  grow
                  component={ScrollArea}
                  ml={-5}
                  mr={-5}
                  sx={{ paddingLeft: 5, paddingRight: 5 }}
                >
                  <MainLinks />
                </Navbar.Section>
                <Navbar.Section>{/* <User /> */}</Navbar.Section>
              </Navbar>
            }
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
        </NotificationProvider>
      </MantineProvider>
    </ColorSchemeProvider>
  );
};

export default App;
