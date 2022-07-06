import React from 'react';
import ContentLoader from "react-content-loader"
import './SearchLoader.scss';

class SearchLoader extends React.Component {
    render() {
        const count = !this.props.count ? 1 : this.props.count;
        const loaders = [];
        for (let index = 0; index < count; index++) {
            loaders.push(
                <div key={index} className="search-loader" style={this.props.style}>
                    <ContentLoader 
                        speed={2}
                        {...this.props}
                    >
                    
                    </ContentLoader>
                </div>
            );
        }
        return loaders;
    }
}

export default SearchLoader;