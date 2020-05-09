import React from "react";
import logo from "./logo.svg";
import "./App.css";
import MoviesComponent from "./MoviesComponent";
import { Row, Col, Container } from "reactstrap";

function App() {
  return (
    <div>
      <Container>
        <MoviesComponent />
      </Container>
    </div>
  );
}

export default App;
