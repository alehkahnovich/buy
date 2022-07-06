import React from 'react';
import Pull from '../../components/Pull';

function PullLayout(props) {
    return (
        <React.Fragment>
            <nav className="navbar navbar-dark bg-dark">
                <span className="navbar-text">
                    Navbar text with an inline element
                </span>
            </nav>
            <div className="container">
                <Pull {...props}/>
            </div>
        </React.Fragment>
    );
}

export default PullLayout;