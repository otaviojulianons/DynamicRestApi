import { combineReducers } from 'redux';
import { reducer as toastrReducer } from 'react-redux-toastr';
import EntityReducer from './Entity';
import DataTypeReducer from './DataType';
import CodegenReducer from './Codegen';

const rootReducer = combineReducers({
  toastr: toastrReducer,
  entity: EntityReducer,
  datatype: DataTypeReducer,
  codegen: CodegenReducer,
});

export default rootReducer;