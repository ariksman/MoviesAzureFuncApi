import http from "./http-common";
import { IMovie } from "./DataModel/Types";

const getAllComing = () => {
  return http.get<IMovie[]>("/movie/coming");
};

const getAll = () => {
  return http.get<IMovie[]>("/movie");
};

const get = (id: string) => {
  return http.get<IMovie>(`/movie/${id}`);
};

const create = (data: IMovie) => {
  return http.post<IMovie>("/movie", data);
};

const update = (id: string, data: IMovie) => {
  return http.put<IMovie>(`/movie/${id}`, data);
};

const remove = (id: string) => {
  return http.delete<IMovie>(`/movie/${id}`);
};

// const removeAll = () => {
//   return http.delete(`/movie`);
// };

// const findByTitle = (name: string) => {
//   return http.get(`/movie?title=${title}`);
// };

export default {
  getAllComing,
  getAll,
  get,
  create,
  update,
  remove,
  //   removeAll,
  //   findByTitle,
};
