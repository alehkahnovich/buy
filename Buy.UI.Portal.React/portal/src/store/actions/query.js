import {
    QUERY_ADD_TERM,
    QUERY_ACCEPTED,
    QUERY_ADD_CAT
} from './types';

export function setTerm(term) {
    return dispatch => {
        dispatch({type: QUERY_ADD_TERM, payload: term});
    };
}

export function setFacet(facet, type) {
    return dispatch => {
        dispatch({type: type, payload: facet})
    }
}

export function acceptQuery() {
    return dispatch => {
        dispatch({type: QUERY_ACCEPTED});
    }
}

export function setCat(category) {
    return dispatch => {
        dispatch({type:QUERY_ADD_CAT, payload:category})
    }
}