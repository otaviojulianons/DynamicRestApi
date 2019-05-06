const ENTITY_BY_ID_DELETE_REQUEST = 'ENTITY_BY_ID_DELETE_REQUEST';
const ENTITY_BY_ID_DELETE_RESPONSE = 'ENTITY_BY_ID_DELETE_RESPONSE';
const ENTITY_BY_ID_DELETE_FAILURE = 'ENTITY_BY_ID_DELETE_FAILURE';

const ENTITY_GET_REQUEST = 'ENTITY_GET_REQUEST';
const ENTITY_GET_RESPONSE = 'ENTITY_GET_RESPONSE';
const ENTITY_GET_FAILURE = 'ENTITY_GET_FAILURE';

const ENTITY_POST_REQUEST = 'ENTITY_POST_REQUEST';
const ENTITY_POST_RESPONSE = 'ENTITY_POST_RESPONSE';
const ENTITY_POST_FAILURE = 'ENTITY_POST_FAILURE';

const entityByIdDeleteActionsTypes = [
  ENTITY_BY_ID_DELETE_REQUEST,
  ENTITY_BY_ID_DELETE_RESPONSE,
  ENTITY_BY_ID_DELETE_FAILURE,
];

const entityGetActionsTypes = [
  ENTITY_GET_REQUEST,
  ENTITY_GET_RESPONSE,
  ENTITY_GET_FAILURE,
];

const entityPostActionsTypes = [
  ENTITY_POST_REQUEST,
  ENTITY_POST_RESPONSE,
  ENTITY_POST_FAILURE,
];


export const ActionsTypes =  {
  ENTITY_BY_ID_DELETE_REQUEST,
  ENTITY_BY_ID_DELETE_RESPONSE,
  ENTITY_BY_ID_DELETE_FAILURE,
  entityByIdDeleteActionsTypes,
  ENTITY_GET_REQUEST,
  ENTITY_GET_RESPONSE,
  ENTITY_GET_FAILURE,
  entityGetActionsTypes,
  ENTITY_POST_REQUEST,
  ENTITY_POST_RESPONSE,
  ENTITY_POST_FAILURE,
  entityPostActionsTypes
};
