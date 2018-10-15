
import { ActionsTypes } from './types';
import { 
  dynamicEntityByIdDeleteService,
  dynamicEntityGetService,
  dynamicEntityPostService
} from '../../../core/service/EntityService';

const dynamicEntityByIdDelete = (id) => ({
  types: ActionsTypes.entityByIdDeleteActionsTypes,
  callService: () => dynamicEntityByIdDeleteService(id),
  payload: { id }
});


const dynamicEntityGet = () => ({
  types: ActionsTypes.entityGetActionsTypes,
  callService: () => dynamicEntityGetService(),
  payload: {  }
});


const dynamicEntityPost = (item) => ({
  types: ActionsTypes.entityPostActionsTypes,
  callService: () => dynamicEntityPostService(item),
  payload: { item }
});


export {
  dynamicEntityByIdDelete,
  dynamicEntityGet,
  dynamicEntityPost
};
