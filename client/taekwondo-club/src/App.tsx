import React from "react";
import "./css/App.css";
import StudentTable from "./components/StudentTable";

const App = () => {
  return (
    <div className="App">
      <header className="App-header"></header>
      <StudentTable  />
      <footer></footer>
    </div>
  );
};

export default App;
