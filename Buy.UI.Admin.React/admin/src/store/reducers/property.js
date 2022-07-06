import {
    PROPERTIES_REQUESTED,
    PROPERTIES_RECEIVED,
    PROPERTIES_FAILED,
    PROPERTIES_CREATED_REQUESTED,
    PROPERTIES_CREATED,
    PROPERTIES_PATCH,
    PROPERTIES_DELETE_REQUESTED,
    PROPERTIES_DELETE,
    PROPERTIES_DELETE_FAILED
} from '../actions/types';

const initial = { properties: [], loading: false };
export default function reducer(state = initial, action) {
    switch(action.type) {
        case PROPERTIES_RECEIVED:
            return {
                ...state,
                loading: false,
                properties: action.payload
            };   
        case PROPERTIES_REQUESTED: 
            return { ...state, loading: true };
        case PROPERTIES_CREATED:
            return {
                ...state,
                loading: false,
                properties: create(state, action)
            };
        case PROPERTIES_PATCH:
            return {
                ...state,
                properties:patch(state, action)
            };
        case PROPERTIES_CREATED_REQUESTED:
            return { ...state, loading: true };
        case PROPERTIES_DELETE_REQUESTED: 
            return { ...state, loading: true };
        case PROPERTIES_DELETE: 
            return { ...state, loading: false, properties:remove(state, action) };
        case PROPERTIES_FAILED:
            return state;
        case PROPERTIES_DELETE_FAILED:
        default:
            return state;
    }
}


const create = (state, action) => {
    return [...state.properties, action.payload];
}

const patch = (state, action) => {
    const index = state.properties.findIndex(entry => entry.key === action.payload.key);
    if (index === -1) return state;
    const properties = [...state.properties];
    properties[index] = action.payload;
    return properties; 
}

const remove = (state, action) => {
    const index = state.properties.findIndex(entry => entry.key === action.payload);
    if (index === -1) return state;
    const properties = [...state.properties];
    properties.splice(index, 1);
    return properties;
}