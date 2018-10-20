import axios from 'axios';
import FileSaver from 'file-saver';
import { 
    codegenGetClientTypes,
    codegenGetServerTypes,    
    codegenPost 
  } from './../api/Codegen';
  
  const codegenGetClientTypesService = async () => {
    try {
      return await codegenGetClientTypes();
    } catch (error) {
      throw error;
    }
  };

  const codegenGetServerTypesService = async () => {
    try {
      return await codegenGetServerTypes();
    } catch (error) {
      throw error;
    }
  };
  
  const codegenPostService = async (codeGenType, type) => {
    try {
      var result = await codegenPost(codeGenType, type);

      if(result.data.link){
        axios({
          url: result.data.link,
          method: 'GET',
          responseType: 'blob',
        }).then((response) => {
          console.log(response);
          FileSaver.saveAs(new Blob([response.data]), type + ".zip");
        });
      }
      return result;
    } catch (error) {
      throw error;
    }
  };
  
  
  export {
    codegenGetClientTypesService,
    codegenGetServerTypesService,
    codegenPostService
  };
  