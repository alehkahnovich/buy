import React, { Component } from 'react';
import * as router from 'react-router-dom';
import { AppSidebarNav2 as AppSidebarNav } from '@coreui/react';
import './Buckets.scss';


class Buckets extends Component {
    convertRoot = (category) => {
        const facet = {
            name: category.name,
            url: `/category/${category.id}`,
            icon: 'icon-speedometer',
            children: (category.siblings || []).map(sibling => this.convertSibling(sibling))
        };

        return facet;            
    }
    convertSibling = (sibling) => {
        return {
            name: sibling.name,
            url: `/category/${sibling.id}`,
            class: 'facet',
            badge: {
                variant: 'light',
                text: sibling.count
            }
        };
    }
    render() {
        const roots = this.props.categories.map(category => this.convertRoot(category));
        return (<AppSidebarNav className='facet_container' navConfig={{items:roots}} {...this.props} router={router}/>)
    }
}

export default Buckets;