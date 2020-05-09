import React from "react";
import logo from "./logo.svg";
import "./App.css";
import MoviesComponent from "./components/MoviesComponent";
import {
  CssBaseline,
  Container,
  AppBar,
  Toolbar,
  Typography,
  Theme,
  makeStyles,
  createStyles,
  Grid,
  Button,
} from "@material-ui/core";
import CameraIcon from "@material-ui/icons/PhotoCamera";

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    heroContent: {
      backgroundColor: theme.palette.background.paper,
      padding: theme.spacing(8, 0, 6),
    },
  })
);

function App() {
  const classes = useStyles();

  return (
    <React.Fragment>
      <div className="App">
        <CssBaseline />
        <div className={classes.heroContent}>
          <Container maxWidth="sm">
            <Typography
              component="h1"
              variant="h2"
              align="center"
              color="textPrimary"
              gutterBottom
            >
              Movies
            </Typography>
            <Typography
              variant="h5"
              align="center"
              color="textSecondary"
              paragraph
            >
              Currently available movies in Finnkino theaters
            </Typography>
          </Container>
        </div>
        <Container>
          <MoviesComponent />
        </Container>
      </div>
    </React.Fragment>
  );
}

export default App;
