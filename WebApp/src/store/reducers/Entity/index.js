
import { ActionsTypes } from '../../actions/Entity/types';

const initialState = { 
    isFetchingEntitiesGet: false,
    listEntities: [],
    isExecutingEntityByIdDelete: false,
    isExecutingEntityPost: false,
};

const EntityReducer = (state = initialState, action) => {
  switch (action.type) {
    case ActionsTypes.ENTITY_GET_REQUEST:
      return {
        ...state,
        listEntities: [],
        isFetchingEntitiesGet: true,
      };

    case ActionsTypes.ENTITY_GET_RESPONSE:
      return {
        ...state,
        listEntities: action.response.result,
        isFetchingEntitiesGet: false,
      };

    case ActionsTypes.ENTITY_GET_FAILURE:
      return {
        ...state,
        isFetchingEntitiesGet: false,
      }; 


    case ActionsTypes.ENTITY_BY_ID_DELETE_REQUEST:
      return {
        ...state,
        isExecutingEntityByIdDelete: true,
      };

    case ActionsTypes.ENTITY_BY_ID_DELETE_RESPONSE:
      return {
        ...state,
        isExecutingEntityByIdDelete: false,
      };

    case ActionsTypes.ENTITY_BY_ID_DELETE_FAILURE:
      return {
        ...state,
        isExecutingEntityByIdDelete: false,
      }; 

    case ActionsTypes.ENTITY_POST_REQUEST:
      return {
        ...state,
        isExecutingEntityPost: true,
      };

    case ActionsTypes.ENTITY_POST_RESPONSE:
      return {
        ...state,
        isExecutingEntityPost: false,
      };

    case ActionsTypes.ENTITY_POST_FAILURE:
      return {
        ...state,
        isExecutingEntityPost: false,
      }; 
       
    default:
      return state;
  }
};

export default EntityReducer;
