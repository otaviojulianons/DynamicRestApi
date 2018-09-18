
import { 
  dynamicDataTypeByIdDelete,
  dynamicDataTypeByIdGet,
  dynamicDataTypeByIdPut,
  dynamicDataTypeGet,
  dynamicDataTypePost
   
} from './../api/DataType';

const dynamicDataTypeByIdDeleteService = async (id) => {
  try {
    if (id === null || id === undefined) {
        throw new Error('O parâmetro [id] é obrigatório.');
    }
    const response = await dynamicDataTypeByIdDelete(id);
    return response.data;
  } catch (error) {
    throw error;
  }
};

const dynamicDataTypeByIdGetService = async (id) => {
  try {
    if (id === null || id === undefined) {
        throw new Error('O parâmetro [id] é obrigatório.');
    }
    const response = await dynamicDataTypeByIdGet(id);
    return response.data;
  } catch (error) {
    throw error;
  }
};

const dynamicDataTypeByIdPutService = async (id,Id,Name,UseLength) => {
  try {
    if (id === null || id === undefined) {
        throw new Error('O parâmetro [id] é obrigatório.');
    }
    const response = await dynamicDataTypeByIdPut(id, Id, Name, UseLength);
    return response.data;
  } catch (error) {
    throw error;
  }
};

const dynamicDataTypeGetService = async () => {
  try {
    const response = await dynamicDataTypeGet();
    return response.data;
  } catch (error) {
    throw error;
  }
};

const dynamicDataTypePostService = async (item) => {
  try {
    const response = await dynamicDataTypePost(item);
    return response.data;
  } catch (error) {
    throw error;
  }
};


//teste
function test(){
  dynamicDataTypeByIdDeleteService( "parametro" )
    .then( response => console.log('dynamicDataTypeByIdDelete',response))
    .catch( err => console.error('dynamicDataTypeByIdDelete',err));

  dynamicDataTypeByIdGetService( "parametro" )
    .then( response => console.log('dynamicDataTypeByIdGet',response))
    .catch( err => console.error('dynamicDataTypeByIdGet',err));

  dynamicDataTypeByIdPutService( "parametro" , "parametro" , "parametro" , "parametro" )
    .then( response => console.log('dynamicDataTypeByIdPut',response))
    .catch( err => console.error('dynamicDataTypeByIdPut',err));

  dynamicDataTypeGetService()
    .then( response => console.log('dynamicDataTypeGet',response))
    .catch( err => console.error('dynamicDataTypeGet',err));

  dynamicDataTypePostService( { corpo: "corpo" } )
    .then( response => console.log('dynamicDataTypePost',response))
    .catch( err => console.error('dynamicDataTypePost',err));

}

export {
  dynamicDataTypeByIdDeleteService,
  dynamicDataTypeByIdGetService,
  dynamicDataTypeByIdPutService,
  dynamicDataTypeGetService,
  dynamicDataTypePostService,
  test
};
