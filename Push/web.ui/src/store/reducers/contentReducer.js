import { BUCKET_REQUESTED, BUCKET_RECEIVED, BUCKET_SET } from '../types/actionTypes';

const initial = {
    buckets: [],
    raw: [],
    loading: false
};

export default function contentReducer(state = initial, action) {
    switch(action.type) {
        case BUCKET_SET:
            return {...state, buckets: action.bucket.siblings};
        case BUCKET_REQUESTED:
            return {...state, loading: true}
        case BUCKET_RECEIVED:
            let buckets = action.buckets;
            if (action.parent)
                buckets = (buckets.find(bucket => bucket.id === action.parent) || { siblings: []}).siblings;
            return {...state, buckets:buckets, raw:action.buckets, loading: false};
        default: return state;
    }
}