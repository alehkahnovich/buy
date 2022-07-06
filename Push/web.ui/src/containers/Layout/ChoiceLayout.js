import React from 'react';
import { NavLink } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faArrowAltCircleUp, faArrowAltCircleDown } from '@fortawesome/free-solid-svg-icons'
import './ChoiceLayout.scss';

function ChoiceLayout() {
    return (
        <React.Fragment>
        <nav className="navbar navbar-dark bg-dark">
            <span className="navbar-text">
                Navbar text with an inline element
            </span>
        </nav>
        <div className="container choice-layout">
            <div className="row">
                <div className="col-md-6 text-center choice">
                    <NavLink className="nav-link" to='/pull'>
                        <FontAwesomeIcon icon={faArrowAltCircleDown} size="9x"/>
                        <h1 className="display-1">PULL</h1>
                    </NavLink>
                </div>
                <div className="col-md-6 text-center choice">
                    <NavLink className="nav-link" to='/push'>
                        <FontAwesomeIcon icon={faArrowAltCircleUp} size="9x"/>
                        <h1 className="display-1">PUSH</h1>
                    </NavLink>
                </div>
            </div>
        </div>
        </React.Fragment>
    );
}

export default ChoiceLayout;