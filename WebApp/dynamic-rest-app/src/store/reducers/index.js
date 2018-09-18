import { combineReducers } from 'redux';
import { reducer as toastrReducer } from 'react-redux-toastr';
import EntityReducer from './Entity';
import DataTypeReducer from './DataType';

const rootReducer = combineReducers({
  toastr: toastrReducer,
  entity: EntityReducer,
  datatype: DataTypeReducer
});

export default rootReducer;