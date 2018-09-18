import axios from 'axios';
const API_URL = process.env.REACT_APP_DYNAMIC_REST_API_URL;

const dynamicEntityByIdDelete = (id) => {

  //####### DELETE #######
  const options = {  
  };  
  return axios.delete(`${ API_URL }/Dynamic/Entity/{id}`.replace(["{","id","}"].join(),id),options);
}

const dynamicEntityGet = () => {

  //####### GET #######
  const options = { 
  };  
  return axios.get(`${ API_URL }/Dynamic/Entity`,options);
}

const dynamicEntityPost = (item) => {

  //####### POST #######
  const options = { 
    headers:{ 
      "Content-Type":"application/json"
    }
  };  
  return axios.post(`${ API_URL }/Dynamic/Entity`,{ item: item },options);
}

export {
  dynamicEntityByIdDelete, 
  dynamicEntityGet, 
  dynamicEntityPost
};