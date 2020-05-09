import React, { useState, FunctionComponent, useEffect } from "react";
import { getMovies, deleteMovie, createMovie } from "./Api";
import { IMovie } from "./DataModel/Types";
import MovieInputComponent from "./InTheatersRadioButtons";
import MovieDataService from "./MovieService";
import {
  Grid,
  makeStyles,
  GridList,
  createStyles,
  Theme,
} from "@material-ui/core";
import MovieItemComponent from "./MovieItemComponent";
import InTheatersRadioButtons from "./InTheatersRadioButtons";

interface IMoviesProps {
  initialMovies: IMovie[];
}

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    root: {
      marginTop: 20,
      flexGrow: 1,
    },
    movieItem: {
      padding: 10,
    },
  })
);

export default function MoviesComponent(this: any) {
  const [movies, setMovieData] = useState<IMovie[]>([]);
  const [selectedMovies, setSelectedMovieData] = useState<IMovie[]>([]);

  const classes = useStyles();

  useEffect(() => {
    retrieveMovies();
  }, []);

  const retrieveMovies = () => {
    MovieDataService.getAll()
      .then((response) => {
        setMovieData(response.data);
        setSelectedMovieData(response.data);
        console.log(response.data);
      })
      .catch((e) => {
        console.log(e);
      });
  };

  const onToggleButtonClickHandler = (inTheatersOnly: boolean) => {
    let filtered;
    if (inTheatersOnly) {
      filtered = movies.filter((movie) => movie.inSchedule);
    } else {
      filtered = movies.filter((movie) => movie.inSchedule != true);
    }

    setSelectedMovieData(filtered);
  };

  const allMovies = selectedMovies.map((x, i) => {
    return (
      <Grid className={classes.movieItem}>
        <MovieItemComponent
          key={x.id}
          id={x.id}
          name={x.name}
          imageUrl={x.imageUrl}
          homepageUrl={x.homepageUrl}
          description={x.description}
          dtLocalRelease={x.dtLocalRelease}
          descriptionLong={x.descriptionLong}
          clickHandler={() => {}}
        />
      </Grid>
    );
  });

  return (
    <div className={classes.root}>
      <InTheatersRadioButtons
        onToggleButtonClick={onToggleButtonClickHandler}
      />
      <Grid container className={classes.root} spacing={2}>
        <Grid item xs={12}>
          <Grid container justify="center" spacing={3}>
            {allMovies}
          </Grid>
        </Grid>
      </Grid>
    </div>
  );
}
// export default class MoviesComponent extends React.Component<{}, IMoviesState> {
//   state: IMoviesState = {
//     Movies: [],
//   };

//   componentDidMount() {
//     getMovies(this.updateMovies);
//   }

//   updateMovies = (movies: IMovie[]) => {
//     this.setState((state) => ({
//       Movies: movies,
//     }));
//   };

//   removeMovie = (id: string) => {
//     deleteMovie(id, this.removeLocalMovie);
//   };

//   removeLocalMovie = (id: string) => {
//     this.setState((prevState) => ({
//       Movies: prevState.Movies.filter((movie) => movie.id !== id),
//     }));
//   };

//   addLocalMovie = (movie: IMovie) => {
//     this.setState((prevState) => ({
//       Movies: [...prevState.Movies, movie],
//     }));
//   };

//   addNewMovie = (name: string, description: string) => {
//     const newMovie = {
//       name: name,
//       description: description,
//       duration: 0,
//       imageUrl: "",
//       homepageUrl: "",
//       id: "",
//     };

//     createMovie(newMovie, this.addLocalMovie);
//   };

//   //classes = useStyles();

//   render() {
//     const movies = this.state.Movies.map((x, i) => {
//       return (
//         <MovieItemComponent
//           key={x.id}
//           id={x.id}
//           name={x.name}
//           imageUrl={x.imageUrl}
//           homepageUrl={x.homepageUrl}
//           description={x.description}
//           clickHandler={this.removeMovie}
//         />
//       );
//     });

//     return (
//       <div>
//         <MovieInputComponent onClickHandler={this.addNewMovie} />
//         <Grid>
//           <ul className="list-group">{movies}</ul>
//         </Grid>
//         {/* <GridList cellHeight={160} cols={3}>
//           <ul className="list-group">{movies}</ul>
//         </GridList> */}
//       </div>
//     );
//   }
// }
