import React, { Component } from 'react';
import { Nav, NavItem, NavLink, TabContent, TabPane, ListGroup, ListGroupItem } from 'reactstrap';
import { FilterLoader } from '../../Common/ContentLoader';
import classNames from 'classnames';
import Filter from './Filter';
import './Filters.scss';
import pallete from './Pallete';

class Filters extends Component {
    constructor(props) {
        super(props);
    
        this.toggle = this.toggle.bind(this);
        this.state = {
          activeTab: 'attributes'
        };
    }
    toggle(tab) {
        if (this.state.activeTab !== tab) {
          this.setState({
            activeTab: tab,
          });
        }
    }
    render() {
        const facets = 
        this.props.loading && this.props.facets.length === 0
        ? (<React.Fragment><FilterLoader /><FilterLoader /></React.Fragment>)
        : this.props.facets.map((facet, idx) => {
            return (<Filter key={`${facet.key}_${idx}`} facet={facet} pallete={pallete[idx]} onFacetSet={this.props.onFacetSet}/>);
        });
        return (
            <React.Fragment>
                <Nav tabs>
                    <NavItem>
                        <NavLink className={classNames({ active: this.state.activeTab === 'attributes' })}
                                onClick={() => {
                                this.toggle('attributes');
                                }}>
                        <i className="icon-list"></i>
                        </NavLink>
                    </NavItem>
                </Nav>
                <TabContent activeTab={this.state.activeTab}>
                    <TabPane tabId="attributes">
                        <ListGroup className="list-group-accent" tag={'div'}>
                            <ListGroupItem className="list-group-item-accent-secondary top-filter-title bg-light text-center font-weight-bold text-muted text-uppercase small">
                                объявлений {this.props.total}
                            </ListGroupItem>
                            {facets}
                        </ListGroup>
                    </TabPane>
                </TabContent>
            </React.Fragment>
        );
    }
}

export default Filters;