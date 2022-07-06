import {
    CONTENT_REQUESTED,
    CONTENT_FAILED,
    CONTENT_RECIEVED
} from '../actions/types';

const initial = {
    searchResult : {
        facets: [],
        results: [],
        total: 0
    },
    loading: false
}

export default function reducer(state = initial, action) {
    switch(action.type) {
        case CONTENT_REQUESTED:
            return {...state, loading: true}
        case CONTENT_FAILED:
            return {...state, loading: false};
        case CONTENT_RECIEVED: 
            return {...state, searchResult: action.payload, loading: false};
        default:
            return state;
    }
}