import React, { Component } from 'react';
import { Col, Badge } from 'reactstrap';
import { QUERY_ADD_ATTRIBUTE, QUERY_REMOVE_ATTRIBUTE } from '../../../../store/actions/types';
import './Keyword.scss';

class Keyword extends Component {
    constructor(props) {
        super(props);
        this.state = { checked: false };
    }
    setFilter = () => {
        this.setState({
            checked: !this.state.checked
        }, () => {
            const action = { type: this.state.checked === true ? QUERY_ADD_ATTRIBUTE : QUERY_REMOVE_ATTRIBUTE };
            this.props.onFacetSet({
                id: this.props.facetKey,
                type: this.props.attribute.type,
                value: [this.props.attribute.id]
            }, action);
        });
    }
    render() {
        return (
            <Col className="facet_value text-muted" xs="12" sm="12" md="12">
                <input checked={this.state.checked} onChange={this.setFilter} type="checkbox" />
                <span onClick={this.setFilter} className="facet_filter_value">
                    {this.props.attribute.value[0]}
                </span>
                <Badge className="mr-3 facet_filter_doc_count" color="light">{this.props.attribute.count}</Badge>
            </Col> 
        );
    }
}

export default Keyword;