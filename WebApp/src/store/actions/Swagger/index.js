
import { ActionsTypes } from './types';
import { 
  dynamicSwaggerGetService
} from '../../../core/service/SwaggerService';

const dynamicSwaggerGet = () => ({
  types: ActionsTypes.swaggerGetActionsTypes,
  callService: () => dynamicSwaggerGetService(),
  payload: {  }
});


export {
  dynamicSwaggerGet
};
