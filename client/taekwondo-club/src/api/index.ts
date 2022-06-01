import axios from "axios";

const client = axios.create({
  baseURL: process.env.REACT_APP_API_URL,
});

client.interceptors.request.use(
  async (request) => {
    request.headers = {
      Authorization: `Bearer 123456789`,
    };
    return request;
  },
  (error) => Promise.reject(error)
);

export default client;
