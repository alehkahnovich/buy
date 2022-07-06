import axios from 'axios';
import {
    ROOT_BUCKETS_REQUESTED,
    ROOT_BUCKETS_RECIEVED,
    ROOT_BUCKETS_FAILED,
    ROOT_BUCKETS_TOOGLE_MENU
} from './types';

export function toogleBucketsMenu() {
    return dispatch => dispatch({type: ROOT_BUCKETS_TOOGLE_MENU});
}

export function receiveRoots() {
    return dispatch => {
        dispatch({ type: ROOT_BUCKETS_REQUESTED });
        axios.get('http://localhost:5300/api/search/buckets/categories')
        .then(response => dispatch({ type: ROOT_BUCKETS_RECIEVED, payload: response.data }))
        .catch(error => dispatch({ type: ROOT_BUCKETS_FAILED, payload: error }));
    }
}