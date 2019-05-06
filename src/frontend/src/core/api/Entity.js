import axios from 'axios';
const API_URL = process.env.REACT_APP_DYNAMIC_REST_API_URL;

const dynamicEntityByIdDelete = (id) => {
  return axios.delete(`${ API_URL }/Dynamic/Entity/${id}`);
}

const dynamicEntityGet = () => { 
  return axios.get(`${ API_URL }/Dynamic/Entity`);
}

const dynamicEntityPost = (item) => {
  const options = { 
    headers:{ 
      "Content-Type":"application/json"
    }
  };  
  return axios.post(`${ API_URL }/Dynamic/Entity`,item ,options);
}

export {
  dynamicEntityByIdDelete, 
  dynamicEntityGet, 
  dynamicEntityPost
};