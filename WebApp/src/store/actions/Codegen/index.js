
import { ActionsTypes } from './types';
import { 
  codegenGetClientTypesService,
  codegenGetServerTypesService,

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


export {
  codegenClientGet,
  codegenServerGet,
};
