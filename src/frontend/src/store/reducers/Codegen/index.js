
import { ActionsTypes } from '../../actions/Codegen/types';

const initialState = { 
    listClientTemplates: [],
    listServerTemplates: [],
    isExecutingCodegenPost: false
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
    case ActionsTypes.CODEGEN_POST_REQUEST:
        return {
          ...state,
          isExecutingCodegenPost: true,
        };
  
    case ActionsTypes.CODEGEN_POST_RESPONSE:
        return {
            ...state,
            isExecutingCodegenPost: false,
        };

    case ActionsTypes.CODEGEN_POST_FAILURE:
        return {
            ...state,
            isExecutingCodegenPost: false,
        }; 
    default:
      return state;
  }
};

export default CodegenReducer;
