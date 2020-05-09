import React from "react";
import { MovieItemComponent } from "./MovieItemComponent";
import { getMovies, deleteMovie, createMovie } from "./Api";
import { IMovie } from "./DataModel/Types";
import { InputGroupAddon, InputGroupText, Input } from "reactstrap";
import MovieInputComponent from "./MovieInputComponent";

interface IMoviesState {
  Movies: IMovie[];
}

export default class MoviesComponent extends React.Component<{}, IMoviesState> {
  state: IMoviesState = {
    Movies: [],
  };

  componentDidMount() {
    getMovies(this.updateMovies);
  }

  updateMovies = (movies: IMovie[]) => {
    this.setState((state) => ({
      Movies: movies,
    }));
  };

  removeMovie = (id: string) => {
    deleteMovie(id, this.removeLocalMovie);
  };

  removeLocalMovie = (id: string) => {
    this.setState((prevState) => ({
      Movies: prevState.Movies.filter((movie) => movie.id !== id),
    }));
  };

  addLocalMovie = (movie: IMovie) => {
    this.setState((prevState) => ({
      Movies: [...prevState.Movies, movie],
    }));
  };

  addNewMovie = (name: string, description: string) => {
    const newMovie = {
      name: name,
      description: description,
      duration: 0,
      imageUrl: "",
      homepageUrl: "",
      id: "",
    };

    createMovie(newMovie, this.addLocalMovie);
  };

  render() {
    const movies = this.state.Movies.map((x, i) => {
      return (
        <MovieItemComponent
          key={x.id}
          id={x.id}
          name={x.name}
          imageUrl={x.imageUrl}
          homepageUrl={x.homepageUrl}
          description={x.description}
          clickHandler={this.removeMovie}
        />
      );
    });

    return (
      <div>
        <MovieInputComponent onClickHandler={this.addNewMovie} />
        <ul className="list-group">{movies}</ul>
      </div>
    );
  }
}
