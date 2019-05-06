
import { ActionsTypes } from './types';
import { 
  codegenGetClientTypesService,
  codegenGetServerTypesService,
  codegenPostService

} from '../../../core/service/CodegenService';


const codegenClientGet = () => ({
  types: ActionsTypes.codegenClientGetActionsTypes,
  callService: () => codegenGetClientTypesService(),
  payload: {}
});

const codegenServerGet = () => ({
  types: ActionsTypes.codegenServerGetActionsTypes,
  callService: () => codegenGetServerTypesService(),
  payload: {}
});

const codegenPost= (codeGenType, type) => ({
  types: ActionsTypes.codegenPostActionsTypes,
  callService: () => codegenPostService(codeGenType, type),
  payload: {}
});



export {
  codegenClientGet,
  codegenServerGet,
  codegenPost
};
