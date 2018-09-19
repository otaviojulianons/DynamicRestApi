
import { ActionsTypes } from '../../actions/Codegen/types';

const initialState = { 
    listClientTemplates: [],
    listServerTemplates: [],
};

const CodegenReducer = (state = initialState, action) => {
  switch (action.type) {
    case ActionsTypes.CODEGEN_CLIENT_GET_REQUEST:
        return {
        ...state,
        listClientTemplates: []
        };

    case ActionsTypes.CODEGEN_CLIENT_GET_RESPONSE:
        return {
            ...state,
            listClientTemplates: action.response.data,
        };

    case ActionsTypes.CODEGEN_CLIENT_GET_FAILURE:
        return {
            ...state,
        }; 
    case ActionsTypes.CODEGEN_SERVER_GET_REQUEST:
        return {
        ...state,
        listServerTemplates: []
        };

    case ActionsTypes.CODEGEN_SERVER_GET_RESPONSE:
        return {
            ...state,
            listServerTemplates: action.response.data,
        };

    case ActionsTypes.CODEGEN_SERVER_GET_FAILURE:
        return {
            ...state,
        }; 
       
    default:
      return state;
  }
};

export default CodegenReducer;
