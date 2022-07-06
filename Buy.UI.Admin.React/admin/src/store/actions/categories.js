import axios from 'axios';
import config from '../../config';
import {
    CATEGORIES_REQUESTED, 
    CATEGORIES_RECEIVED, 
    CATEGORIES_FAILED,
    CATEGORIES_CREATED,
    CATEGORIES_CREATED_REQUESTED,
    CATEGORIES_CREATED_FAILED,
    CATEGORIES_PATCH_REQUESTED,
    CATEGORIES_PATCH,
    CATEGORIES_PATCH_FAILED,
    CATEGORIES_DELETE_REQUESTED,
    CATEGORIES_DELETE,
    CATEGORIES_DELETE_FAILED
} from './types';

const configuration = config();

export function receive() {
    return (dispatch) => {
        dispatch({ type:CATEGORIES_REQUESTED });
        axios
        .get(`${configuration.content.uri}/api/content/category`)
        .then(response => dispatch({
            type:CATEGORIES_RECEIVED,
            payload:response.data
        }))
        .catch(error => dispatch({
            type:CATEGORIES_FAILED,
            payload:error
        }))
    }
}

export function create(payload) {
    return (dispatch) => {
        dispatch({ type:CATEGORIES_CREATED_REQUESTED });
        axios
        .post(`${configuration.content.uri}/api/content/category`, payload)
        .then(response => dispatch({
            type:CATEGORIES_CREATED,
            payload:response.data
        }))
        .catch(error => dispatch({
            type:CATEGORIES_CREATED_FAILED,
            payload:error
        }))
    }
}

export function update(payload) {
    return (dispatch) => {
        dispatch({ type:CATEGORIES_PATCH_REQUESTED });
        axios
        .put(`${configuration.content.uri}/api/content/category`, payload)
        .then(response => dispatch({
            type:CATEGORIES_PATCH,
            payload:payload
        }))
        .catch(error => dispatch({
            type:CATEGORIES_PATCH_FAILED,
            payload:error
        }))
    }
}

export function remove(key) {
    return dispatch => {
        dispatch({ type: CATEGORIES_DELETE_REQUESTED });
        axios
        .delete(`${configuration.content.uri}/api/content/category/${key}`)
        .then(_ => dispatch({ type: CATEGORIES_DELETE, payload:key }))
        .catch(error => dispatch({ type: CATEGORIES_DELETE_FAILED, payload: error }));
    }
}