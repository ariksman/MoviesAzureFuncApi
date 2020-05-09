import React from "react";
import MovieItemComponent from "./MovieItemComponent";
import { getMovies } from "./api";

export default class MoviesComponent extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      movies: [],
    };
  }

  componentDidMount() {
    getMovies((movies) => {
      this.setState({ movies: movies });
      console.log(this.state.movies);
    });
  }

  render() {
    const movies = this.state.movies.map((x, i) => {
        return(
            <MovieItemComponent key={x.id} name={x.name} description={x.description} />
        );
    });

    return (
        <div id="app" className="container">
            <div className="row mb-2">
                <input type="text" className="form-control" placeholder="Add movie"></input>
            </div>
            <ul className="list-group">
                {movies}
            </ul>
        </div>
      );
  }
}
