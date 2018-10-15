import axios from 'axios';
const API_URL = process.env.REACT_APP_DYNAMIC_REST_API_URL;

const dynamicDataTypeByIdDelete = (id) => {

  //####### DELETE #######
  const options = {  
  };  
  return axios.delete(`${ API_URL }/Dynamic/DataType/{id}`.replace(["{","id","}"].join(),id),options);
}

const dynamicDataTypeByIdGet = (id) => {

  //####### GET #######
  const options = { 
  };  
  return axios.get(`${ API_URL }/Dynamic/DataType/{id}`.replace(["{","id","}"].join(),id),options);
}

const dynamicDataTypeByIdPut = (Id, Name, UseLength) => {

  //####### PUT #######
  const options = {
    params: { 
      Id, 
      Name, 
      UseLength
    }
  };  
  return axios.put(`${ API_URL }/Dynamic/DataType/{id}`.replace(["{","id","}"].join(),Id),options);
}

const dynamicDataTypeGet = () => {

  //####### GET #######
  const options = { 
  };  
  return axios.get(`${ API_URL }/Dynamic/DataType`,options);
}

const dynamicDataTypePost = (item) => {

  //####### POST #######
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