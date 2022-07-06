import React from 'react';
import './Loader.scss';

class Loader extends React.Component {
    render() {
        return (
            <div className="lds-ellipsis">
                <div></div>
                <div></div>
                <div></div>
                <div></div>
            </div>
        );
    }
}

export default Loader;