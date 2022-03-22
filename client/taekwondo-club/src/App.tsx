import React from "react";
import "./css/App.css";
import StudentTable from "./components/StudentTable";
import { MantineProvider } from "@mantine/core";

const App = () => {
  return (
    <MantineProvider theme={{ colorScheme: "dark" }} withGlobalStyles>
        <StudentTable />
    </MantineProvider>
  );
};

export default App;
