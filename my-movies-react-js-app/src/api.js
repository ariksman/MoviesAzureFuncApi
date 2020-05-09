import axios from "axios";

const baseAddress = "http://localhost:7071";

export function getMovies(callback) {
  axios.get(`${baseAddress}/api/movie`).then((response) => {
    callback(response.data);
  })
  .catch((error) => {
    console.log(error);
  });
}

export function createMovie(movie, callback) {
  axios
    .post(`${baseAddress}/api/movie`, JSON.stringify({ Name: movie.Name, Description: movie.Description} ))
    .then((response) => {
      callback(response.data);
    })
    .catch((error) => {
      console.log(error);
    });
}

export function deleteMovie(movie, callback) {
    axios
    .delete(`${baseAddress}/api/movie/${movie.id}`)
    .then((response) => {
      callback(response.data);
    })
    .catch((error) => {
      console.log(error);
      //const errorMsg = error.response.data.error;
    });
}

export function updateMovie(movie, callback) {
    axios
    .put(`${baseAddress}/api/movie/${movie.id}`, JSON.stringify({ Name: movie.Name, Description: movie.Description} ))
    .then((response) => {
      callback(response.data);
    })
    .catch((error) => {
      console.log(error);
      //const errorMsg = error.response.data.error;
    });
}
