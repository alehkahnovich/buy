import React from 'react';
import './FilterLoader.scss';
import ContentLoader from "react-content-loader"
class FilterLoader extends React.Component {
    render() {
        return (
            <ContentLoader 
                    height={160}
                    width={400}
                    speed={2}
                    className="filter-loader"
            >
    <rect x="0" y="0" rx="3" ry="3" width="350" height="17" /> 
    <rect x="0" y="35" rx="3" ry="3" width="20" height="17" /> 
    <rect x="25" y="35" rx="0" ry="0" width="310" height="17" /> 
    <rect x="0" y="70" rx="3" ry="3" width="20" height="17" /> 
    <rect x="25" y="70" rx="0" ry="0" width="310" height="17" /> 
    <rect x="0" y="105" rx="3" ry="3" width="20" height="17" /> 
    <rect x="25" y="105" rx="0" ry="0" width="310" height="17" /> 
            </ContentLoader>
        );
    }
}

export default FilterLoader;