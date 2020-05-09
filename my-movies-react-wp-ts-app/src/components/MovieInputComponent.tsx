import React from "react";
import {
  Button,
  makeStyles,
  Theme,
  createStyles,
  TextField,
  Fab,
} from "@material-ui/core";
import CloudUploadIcon from "@material-ui/icons/CloudUpload";
import NavigationIcon from "@material-ui/icons/Navigation";

interface IMovieInputProps {
  onToggleButtonClick: () => void;
  //onClickHandler: (name: string, description: string) => void;
}

interface IMovieInputState {
  name: string;
  description: string;
}

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    root: {
      "& > *": {
        margin: theme.spacing(1),
      },
    },
    extendedIcon: {
      marginRight: theme.spacing(1),
    },
  })
);

export default function MovieInputComponent(props: IMovieInputProps) {
  // handleNameChange = (e: React.ChangeEvent<HTMLInputElement>) => {
  //   this.setState({ name: e.target.value });
  // };

  // handleDescriptionChange = (e: React.ChangeEvent<HTMLInputElement>) => {
  //   this.setState({ description: e.target.value });
  // };

  const classes = useStyles();

  return (
    <form noValidate autoComplete="off">
      <div>
        <div className={classes.root}>
          <Fab variant="extended" onClick={props.onToggleButtonClick}>
            <NavigationIcon className={classes.extendedIcon} />
            Navigate
          </Fab>
        </div>
        {/* <TextField
            error
            id="outlined-margin-normal"
            label="Name"
            defaultValue=""
            variant="outlined"
            margin="normal"
            onChange={this.handleNameChange}
          />
          <TextField
            error
            id="outlined-margin-normal"
            label="Description"
            defaultValue=""
            variant="outlined"
            margin="normal"
            onChange={this.handleDescriptionChange}
          />
          <Button
            color="primary"
            variant="contained"
            startIcon={<CloudUploadIcon />}
            onClick={() => {
              this.props.onClickHandler(
                this.state.name,
                this.state.description
              );
            }}
          >
            Submit
          </Button> */}
      </div>
    </form>
  );
}
