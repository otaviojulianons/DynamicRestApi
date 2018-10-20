import axios from 'axios';
const API_URL = process.env.REACT_APP_DYNAMIC_REST_API_URL;

const dynamicDataTypeByIdDelete = (id) => {

  return axios.delete(`${ API_URL }/Dynamic/DataType/${id}`);
}

const dynamicDataTypeByIdGet = (id) => {
  return axios.get(`${ API_URL }/Dynamic/DataType/${id}`);
}

const dynamicDataTypeByIdPut = (Id, Name, UseLength) => {
  const options = {
    params: { 
      Id, 
      Name, 
      UseLength
    }
  };  
  return axios.put(`${ API_URL }/Dynamic/DataType/${Id}`,options);
}

const dynamicDataTypeGet = () => {
  return axios.get(`${ API_URL }/Dynamic/DataType`);
}

const dynamicDataTypePost = (item) => {

  const options = { 
    headers:{ 
      "Content-Type":"application/json"
    }
  };  
  return axios.post(`${ API_URL }/Dynamic/DataType`,{ item: item },options);
}

export {
  dynamicDataTypeByIdDelete, 
  dynamicDataTypeByIdGet, 
  dynamicDataTypeByIdPut, 
  dynamicDataTypeGet, 
  dynamicDataTypePost
};