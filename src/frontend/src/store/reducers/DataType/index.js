
import { ActionsTypes } from '../../actions/DataType/types';

const initialState = { 
    listDataTypes: [],
    isFetchingDataTypesGet: false,
    isExecutingDataTypeByIdDelete: false,
    isExecutingDataTypeByIdPut: false,
    isExecutingDataTypePost: false,
};

const DataTypeReducer = (state = initialState, action) => {
  switch (action.type) {
    case ActionsTypes.DATA_TYPE_GET_REQUEST:
    return {
      ...state,
      listDataTypes: [],
      isFetchingDataTypesGet: true,
    };

    case ActionsTypes.DATA_TYPE_GET_RESPONSE:
      return {
        ...state,
        listDataTypes: action.response.result,
        isFetchingDataTypesGet: false,
      };

    case ActionsTypes.DATA_TYPE_GET_FAILURE:
      return {
        ...state,
        isFetchingDataTypesGet: false,
      }; 
    case ActionsTypes.DATA_TYPE_BY_ID_DELETE_REQUEST:
      return {
        ...state,
        isExecutingDataTypeByIdDelete: true,
      };

    case ActionsTypes.DATA_TYPE_BY_ID_DELETE_RESPONSE:
      return {
        ...state,
        isExecutingDataTypeByIdDelete: false,
      };

    case ActionsTypes.DATA_TYPE_BY_ID_DELETE_FAILURE:
      return {
        ...state,
        isExecutingDataTypeByIdDelete: false,
      }; 

    case ActionsTypes.DATA_TYPE_BY_ID_PUT_REQUEST:
      return {
        ...state,
        isExecutingDataTypeByIdPut: true,
      };

    case ActionsTypes.DATA_TYPE_BY_ID_PUT_RESPONSE:
      return {
        ...state,
        isExecutingDataTypeByIdPut: false,
      };

    case ActionsTypes.DATA_TYPE_BY_ID_PUT_FAILURE:
      return {
        ...state,
        isExecutingDataTypeByIdPut: false,
      }; 

    case ActionsTypes.DATA_TYPE_POST_REQUEST:
      return {
        ...state,
        isExecutingDataTypePost: true,
      };

    case ActionsTypes.DATA_TYPE_POST_RESPONSE:
      return {
        ...state,
        isExecutingDataTypePost: false,
      };

    case ActionsTypes.DATA_TYPE_POST_FAILURE:
      return {
        ...state,
        isExecutingDataTypePost: false,
      }; 
       
    default:
      return state;
  }
};

export default DataTypeReducer;
