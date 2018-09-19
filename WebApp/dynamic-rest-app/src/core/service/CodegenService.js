
import { 
    codegenGetClientTypes,
    codegenGetServerTypes,     
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
  
  
  export {
    codegenGetClientTypesService,
    codegenGetServerTypesService,
  };
  