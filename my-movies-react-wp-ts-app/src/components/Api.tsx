import axios from "axios";
import { IMovie } from "./DataModel/Types";

interface ServerResponse {
  Movies: IMovie[];
}

const baseAddress = "http://localhost:7071";

export function getMovies(callback: (event: IMovie[]) => void) {
  axios
    .get<IMovie[]>(`${baseAddress}/api/movie`)
    .then((response: { data: IMovie[] }) => {
      callback(response.data);
    })
    .catch((error: any) => {
      console.log(error);
    });
}

export function createMovie(movie: IMovie, onSuccess: (movie: IMovie) => void) {
  axios
    .post(
      `${baseAddress}/api/movie`,
      JSON.stringify({ Name: movie.name, Description: movie.description })
    )
    .then((response: { data: IMovie }) => {
      onSuccess(response.data);
    })
    .catch((error: any) => {
      console.log(error);
    });
}

export function deleteMovie(id: string, onSuccess: (id: string) => void) {
  axios
    .delete<IMovie>(`${baseAddress}/api/movie/${id}`)
    .then(() => {
      onSuccess(id);
    })
    .catch((error: any) => {
      console.log(error);
    });
}

export function updateMovie(movie: IMovie) {
  axios
    .put<ServerResponse>(
      `${baseAddress}/api/movie/${movie.id}`,
      JSON.stringify({ Name: movie.name, Description: movie.description })
    )
    .catch((error: any) => {
      console.log(error);
    });
}
