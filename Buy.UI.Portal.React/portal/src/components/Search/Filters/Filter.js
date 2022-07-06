import React, { Component } from 'react';
import { ListGroupItem, Row, Collapse } from 'reactstrap';
import classNames from 'classnames';
import FilterValue from './FilterValue';
import './Filter.scss';
class Filter extends Component {
    constructor(props) {
        super(props);
        this.state = {
            open: false,
            isReset: false,
            isDirty: false
        };
    }
    toggle = (e) => {
        e.preventDefault();
        this.setState({ open: !this.state.open });
    }
    setDirty = (flag) => {
        this.setState({isDirty: flag});
    }
    resetFilter = (e) => {
        e.preventDefault();
        e.stopPropagation();
        this.setState({isReset: !this.state.isReset})
    }
    render() {
        const values = this.props.facet.facets.map((value, iidx) => {
            return (<FilterValue 
                key={`${this.props.facet.key}_${iidx}`} 
                facetKey={this.props.facet.key}
                attribute={value}
                isReset={this.state.isReset}
                setDirty={this.setDirty}
                onFacetSet={this.props.onFacetSet}/>);
        });

        const reset = this.state.isDirty 
            ? <i onClick={this.resetFilter} className="fa fa-close fa-lg facet_reset_value"></i>
            : null;

        return (
            <React.Fragment>
                <ListGroupItem onClick={this.toggle} action tag="a" href="#" className={classNames("facet_filter_border","list-group-item-divider", "facet_filter_title")}>
                    <div className="font-weight-bold text-muted text-uppercase small">
                        {this.props.facet.name}
                        {reset}
                    </div>
                </ListGroupItem>
                <Collapse isOpen={this.state.open} >
                    <Row className="facet_attributes">
                        {values}
                    </Row>
                </Collapse>
            </React.Fragment>
        );
    }
}

export default Filter;
