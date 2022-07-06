import React from 'react';
import { connect } from 'react-redux';
import { receiveBuckets, setCurrentBucket } from '../../store/actions/contentActions';
import CommonLoader from '../Loader/CommonLoader';

class Pull extends React.Component {
    componentDidMount() {
        this.props.receiveBuckets(this.props.match.params.bucket);
    }
    setBucketHandler(bucket) {
        if ((bucket.siblings || []).length > 0) {
            this.props.setCurrentBucket(bucket);
            this.props.history.push(`/pull/${bucket.id}`);
            return;
        }
    }
    render() {
        if (this.props.isLoading)
            return <CommonLoader />

        const options = this.props.buckets.map(bucket => {
            return <li onClick={() => this.setBucketHandler(bucket)} key={bucket.id} className="list-group-item font-weight-bold text-uppercase">{bucket.name}</li>
        });

        return (
        <ul className="list-group list-group-flush">
            {options}
        </ul>);
    }
}

const mapping = (state) => {
    return {
        buckets: state.buckets.buckets,
        isLoading: state.loading
    };
};

const dispatchers = dispatch => {
    return {
        receiveBuckets: (bucketId) => dispatch(receiveBuckets(bucketId)),
        setCurrentBucket: (bucket) => dispatch(setCurrentBucket(bucket))
    }
};

export default connect(mapping, dispatchers)(Pull);