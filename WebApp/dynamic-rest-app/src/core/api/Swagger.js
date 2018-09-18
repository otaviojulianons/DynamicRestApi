import axios from 'axios';
const API_URL = process.env.REACT_APP_DYNAMIC_REST_API_URL;

const dynamicSwaggerGet = () => {

  //####### GET #######
  const options = { 
  };  
  return axios.get(`${ API_URL }/Dynamic/swagger`,options);
}

export {
  dynamicSwaggerGet
};