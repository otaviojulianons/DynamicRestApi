
import { 
  dynamicEntityByIdDelete,
  dynamicEntityGet,
  dynamicEntityPost
   
} from './../api/Entity';

const dynamicEntityByIdDeleteService = async (id) => {
  try {
    if (id === null || id === undefined) {
        throw new Error('O parâmetro [id] é obrigatório.');
    }
    const response = await dynamicEntityByIdDelete(id);
    return response.data;
  } catch (error) {
    throw error;
  }
};

const dynamicEntityGetService = async () => {
  try {
    const response = await dynamicEntityGet();
    return response.data;
  } catch (error) {
    throw error;
  }
};

const dynamicEntityPostService = async (item) => {
  try {
    const response = await dynamicEntityPost(item);
    return response.data;
  } catch (error) {
    throw error;
  }
};


//teste
function test(){
  dynamicEntityByIdDeleteService( "parametro" )
    .then( response => console.log('dynamicEntityByIdDelete',response))
    .catch( err => console.error('dynamicEntityByIdDelete',err));

  dynamicEntityGetService()
    .then( response => console.log('dynamicEntityGet',response))
    .catch( err => console.error('dynamicEntityGet',err));

  dynamicEntityPostService( { corpo: "corpo" } )
    .then( response => console.log('dynamicEntityPost',response))
    .catch( err => console.error('dynamicEntityPost',err));

}

export {
  dynamicEntityByIdDeleteService,
  dynamicEntityGetService,
  dynamicEntityPostService,
  test
};
