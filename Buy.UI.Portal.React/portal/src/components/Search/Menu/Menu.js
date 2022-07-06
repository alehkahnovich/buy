import React from 'react';
import className from 'classnames';
import { NavLink } from 'react-router-dom';
import logo from '../../../assets/img/brand/logo.svg'
import './Menu.scss';
class Menu extends React.Component {
    toogle = (e) => {
      e.preventDefault();
      this.props.toogleBucketsMenu();
    }
    convertRoot = (category) => {
      const facet = {
          id: category.id,
          name: category.name,
          url: `/category/${category.id}`,
          icon: 'icon-speedometer',
          children: (category.siblings || []).map(sibling => this.convertSibling(sibling))
      };

      return facet;            
    }
    convertSibling = (sibling) => {
        return {
            id: sibling.id,
            name: sibling.name,
            url: `/category/${sibling.id}`,
            class: 'facet',
            badge: {
                variant: 'light',
                text: sibling.count
            }
        };
    }
    render () {
      const roots = this.props.categories.map(category => this.convertRoot(category));
      const links = roots.map(root => {
        const subs = root.children.map(sibling => {
          return (
            <div key={sibling.id} className="col-md-12">
                <h4>
                  <NavLink className="nav-link" to={sibling.url}>{sibling.name}
                    <span className="badge badge-light bucket-count">
                      {sibling.badge.text}
                    </span>
                  </NavLink>
                </h4>
            </div>
          );
        });
        return (
          <div key={root.id} className="col-md-4 text-left root-category">
            <h1 className="text-left">{root.name}</h1>
            <small className="text-muted">
              {subs}
            </small>
          </div>
        );
      });

      const brand = (
      <a href="/#" onClick={(e) => e.preventDefault()} className="navbar-brand">
        <img src={logo} width="89" height="25" alt="BouyM Logo" className="navbar-brand-full menu-logo" />
      </a>);

      return (
          <div id="bucket-navigation" className={className("overlay", { 'overlay-open': this.props.menuOpen })}>
            <a href="/#" className="closebtn" onClick={this.toogle}>&times;</a>
            {brand}
            <div className={className("overlay-content", 
              { 'overlay-fadein': this.props.menuOpen },
              { 'overlay-fadeout': !this.props.menuOpen },)}>
              <div className="container-fluid">
                <div className="row">
                  {links}
                </div>
              </div>
            </div>
          </div>
      );
    }
}

export default Menu;