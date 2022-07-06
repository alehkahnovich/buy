import axios from 'axios';
import { BUCKET_REQUESTED, BUCKET_RECEIVED, BUCKET_SET } from '../types/actionTypes';
import Configuration from '../../configration';

export function receiveBuckets(bucketId) {
    return dispatch => {
        dispatch({type: BUCKET_REQUESTED});
        axios.get(`${Configuration.content.url}/api/search/buckets/categories`)
        .then(response => dispatch({type: BUCKET_RECEIVED, buckets: response.data, parent: bucketId}));
    };
}

export function setCurrentBucket(bucket) {
    return dispatch => dispatch({type: BUCKET_SET, bucket: bucket});
}