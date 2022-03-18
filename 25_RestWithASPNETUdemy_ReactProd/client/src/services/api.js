import axios from "axios";

axios.defaults.headers.post['Access-Control-Allow-Origin'] = '*';

const api = axios.create({
    baseURL:'https://localhost:44393/',
})


export default api;