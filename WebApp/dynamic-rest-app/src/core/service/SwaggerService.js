
import { 
  dynamicSwaggerGet
   
} from './../api/Swagger';

const dynamicSwaggerGetService = async () => {
  try {
    const response = await dynamicSwaggerGet();
  } catch (error) {
    throw error;
  }
};


//teste
function test(){
  dynamicSwaggerGetService()
    .then( response => console.log('dynamicSwaggerGet',response))
    .catch( err => console.error('dynamicSwaggerGet',err));

}

export {
  dynamicSwaggerGetService,
  test
};
