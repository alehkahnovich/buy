import React, { useEffect } from 'react';
import { connect } from 'react-redux';
import { receive, create, update, remove } from '../store/actions/categories';
import Typography from '@material-ui/core/Typography';
import CategoryGrid from './Grid/Categories/CategoryGrid';

const Categories = function (props) {
    const receive = props.receive;
    useEffect(() => {
        receive();
    }, [receive]);

    return (
        <div>
            <Typography variant="h3" gutterBottom>
                Categories
            </Typography>
            <CategoryGrid 
                categories={props.categories}
                onCreate={props.create}
                onUpdate={props.update}
                onDelete={props.remove}
                isLoading={props.loading} />
        </div>
    );
}

const mapping = (state) => {
    return {
        categories: state.category.categories,
        loading: state.category.loading
    }
}

const dispatchers = dispatch => {
    return {
        receive: () => dispatch(receive()),
        create: (payload) => dispatch(create(payload)),
        update: (payload) => dispatch(update(payload)),
        remove: (key) => dispatch(remove(key))
    }
}

export default connect(mapping, dispatchers)(Categories);