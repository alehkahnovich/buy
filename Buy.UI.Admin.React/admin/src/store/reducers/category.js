import {
    CATEGORIES_REQUESTED,
    CATEGORIES_RECEIVED,
    CATEGORIES_FAILED,
    CATEGORIES_CREATED_REQUESTED,
    CATEGORIES_CREATED,
    CATEGORIES_PATCH,
    CATEGORIES_DELETE_REQUESTED,
    CATEGORIES_DELETE,
    CATEGORIES_DELETE_FAILED
} from '../actions/types';

const initial = { categories: [], loading: false };
export default function reducer(state = initial, action) {
    switch(action.type) {
        case CATEGORIES_RECEIVED:
            return {
                ...state,
                loading: false,
                categories: action.payload
            };   
        case CATEGORIES_REQUESTED: 
            return { ...state, loading: true };
        case CATEGORIES_CREATED:
            return {
                ...state,
                loading: false,
                categories: create(state, action)
            };
        case CATEGORIES_PATCH:
            return {
                ...state,
                categories:patch(state, action)
            };
        case CATEGORIES_CREATED_REQUESTED:
            return { ...state, loading: true };
        case CATEGORIES_DELETE_REQUESTED: 
            return { ...state, loading: true };
        case CATEGORIES_DELETE: 
            return { ...state, loading: false, categories:remove(state, action) };
        case CATEGORIES_FAILED:
            return state;
        case CATEGORIES_DELETE_FAILED:
        default:
            return state;
    }
}


const create = (state, action) => {
    return [...state.categories, action.payload];
}

const patch = (state, action) => {
    const index = state.categories.findIndex(entry => entry.key === action.payload.key);
    if (index === -1) return state;
    const categories = [...state.categories];
    categories[index] = action.payload;
    return categories; 
}

const remove = (state, action) => {
    const index = state.categories.findIndex(entry => entry.key === action.payload);
    if (index === -1) return state;
    const categories = [...state.categories];
    categories.splice(index, 1);
    return categories;
}