
import { ActionsTypes } from './types';
import { 
  dynamicDataTypeByIdDeleteService,
  dynamicDataTypeByIdGetService,
  dynamicDataTypeByIdPutService,
  dynamicDataTypeGetService,
  dynamicDataTypePostService
} from '../../../core/service/DataTypeService';

const dynamicDataTypeByIdDelete = (id) => ({
  types: ActionsTypes.dataTypeByIdDeleteActionsTypes,
  callService: () => dynamicDataTypeByIdDeleteService(id),
  payload: { id }
});


const dynamicDataTypeByIdGet = (id) => ({
  types: ActionsTypes.dataTypeByIdGetActionsTypes,
  callService: () => dynamicDataTypeByIdGetService(id),
  payload: { id }
});


const dynamicDataTypeByIdPut = (id,Id,Name,UseLength) => ({
  types: ActionsTypes.dataTypeByIdPutActionsTypes,
  callService: () => dynamicDataTypeByIdPutService(id,Id,Name,UseLength),
  payload: { id,Id,Name,UseLength }
});


const dynamicDataTypeGet = () => ({
  types: ActionsTypes.dataTypeGetActionsTypes,
  callService: () => dynamicDataTypeGetService(),
  payload: {  }
});


const dynamicDataTypePost = (item) => ({
  types: ActionsTypes.dataTypePostActionsTypes,
  callService: () => dynamicDataTypePostService(item),
  payload: { item }
});


export {
  dynamicDataTypeByIdDelete,
  dynamicDataTypeByIdGet,
  dynamicDataTypeByIdPut,
  dynamicDataTypeGet,
  dynamicDataTypePost
};
