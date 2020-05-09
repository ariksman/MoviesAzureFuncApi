import * as React from "react";
import { Button, InputGroupAddon, InputGroupText, Input } from "reactstrap";

interface IMovieInputProps {
  onClickHandler: (name: string, description: string) => void;
}

interface IMovieInputState {
  name: string;
  description: string;
}

export default class MovieInputComponent extends React.Component<
  IMovieInputProps,
  IMovieInputState
> {
  state: IMovieInputState = {
    name: "",
    description: "",
  };

  handleNameChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    this.setState({ name: e.target.value });
  };

  handleDescriptionChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    this.setState({ description: e.target.value });
  };

  render() {
    const name = this.state.name;
    const description = this.state.description;

    return (
      <div>
        <InputGroupAddon addonType="prepend">
          <InputGroupText>Movie</InputGroupText>
        </InputGroupAddon>
        <Input
          placeholder="name"
          value={name}
          onChange={this.handleNameChange}
        />
        <Input
          placeholder="description"
          value={description}
          onChange={this.handleDescriptionChange}
        />
        <Button
          onClick={() => {
            this.props.onClickHandler(this.state.name, this.state.description);
          }}
        >
          Submit
        </Button>
      </div>
    );
  }
}
