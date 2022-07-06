import React, { Component } from 'react';
import { NavLink } from 'react-router-dom';
import { Nav, NavItem } from 'reactstrap';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { toogleBucketsMenu } from '../../store/actions/buckets';
import { AppAsideToggler, AppNavbarBrand } from '@coreui/react';
import './DefaultHeader.scss';
import logo from '../../assets/img/brand/logo.svg'
import sygnet from '../../assets/img/brand/sygnet.svg'

const propTypes = {
  children: PropTypes.node,
};

const defaultProps = {};

class DefaultHeader extends Component {
  render() {

    // eslint-disable-next-line
    const { children, ...attributes } = this.props;

    return (
      <React.Fragment>
        {/* mobile */}
        <button onClick={this.props.toogleBucketsMenu} type="button" className="d-lg-none navbar-toggler" data-sidebar-toggler="true">
          <span className="navbar-toggler-icon"></span>
        </button>
        <AppNavbarBrand
          full={{ src: logo, width: 89, height: 25, alt: 'BouyM Logo' }}
          minimized={{ src: sygnet, width: 30, height: 30, alt: 'BouyM Logo' }}
        />
        {/* desktop */}
        <button onClick={this.props.toogleBucketsMenu} type="button" className="d-md-down-none navbar-toggler" data-sidebar-toggler="true">
          <span className="navbar-toggler-icon"></span>
        </button>
        <Nav className="d-md-down-none" navbar>
          <NavItem className="px-3">
            <NavLink to="/" className="nav-link text-uppercase" >Объявления</NavLink>
          </NavItem>
          <NavItem className="px-3">
            <NavLink to="/content" className="nav-link text-uppercase">Подать объявление</NavLink>
          </NavItem>
        </Nav>
        <Nav className="ml-auto" navbar>
          <NavItem className="d-md-down-none">
            <NavLink to="#" className="nav-link"><i className="icon-list"></i></NavLink>
          </NavItem>
          <NavItem className="d-md-down-none">
            <NavLink to="#" className="nav-link"><i className="icon-location-pin"></i></NavLink>
          </NavItem>
        </Nav>
        <AppAsideToggler className="d-lg-none" mobile />
      </React.Fragment>
    );
  }
}

DefaultHeader.propTypes = propTypes;
DefaultHeader.defaultProps = defaultProps;

const mapping = (state) => {
  return {
    menuOpen: state.buckets.open
  }
}


const dispatchers = dispatch => {
  return {
    toogleBucketsMenu: () => dispatch(toogleBucketsMenu())
  }
}

export default connect(mapping, dispatchers)(DefaultHeader);
