import { toastr } from 'react-redux-toastr';

const makeActionCreator = (type, ...argNames) => (...args) => {
  const action = { type };
  argNames.forEach((arg, index) => {
    action[argNames[index]] = args[index];
  });
  return action;
};

const callServiceMiddleware = ({ dispatch, getState }) => next => async (action) => {
  const {
    types, callService, shouldCallService = () => true, payload = {},
  } = action;

  if (!types) {
    // Normal action: pass it on
    return next(action);
  }

  if (
    !Array.isArray(types) ||
    types.length !== 3 ||
    !types.every(type => typeof type === 'string')
  ) {
    throw new Error('Expected an array of three string types.');
  }

  if (typeof callService !== 'function') {
    throw new Error('Expected callService to be a function.');
  }

  if (!shouldCallService(getState())) {
    return;
  }

  const [requestType, successType, failureType] = types;

  dispatch(Object.assign({}, payload, {
    type: requestType,
  }));

  try {
    const response = await callService();
    dispatch(Object.assign({}, payload, {
      response,
      type: successType,
    }));
  } catch (errorResponse) {
    let errorMessage = 'Server error.';

    try {
      errorMessage = errorResponse.data.message;
    } catch (error) {}

    dispatch(Object.assign({}, payload, {
      errorMessage,
      type: failureType,
    }));

    toastr.error(errorMessage);
  }
};

export { callServiceMiddleware, makeActionCreator };
