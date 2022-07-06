import React, { Component } from 'react';
import { NavLink } from 'react-router-dom';
import './ContentItem.scss';

class ContentItem extends Component {
    placeholder = (props) => {
      return (
        <svg className="content_item_image bd-placeholder-img bd-placeholder-img-lg card-img" width="100%" height={props.height} xmlns="http://www.w3.org/2000/svg" preserveAspectRatio="xMidYMid slice" focusable="false" role="img" aria-label="Placeholder: Card image">
          <title>Нет Фото</title>
          <rect width="100%" height="100%" fill="#A9A9A9"></rect>
          <text x="37%" y="50%" fill="#dee2e6" dy=".3em">Нет Фото</text>
        </svg>
      );
    }
    buildArtifacts = (props) => {
      if (!this.props.content.artifacts)
        return this.placeholder(props);
      const artifact = this.props.content.artifacts[0];
      return (<img className="content_item_image" style={{width:'100%', height:'100%'}} src={`http://localhost:5400/api/content/artifact/${artifact}`} alt="" />);
    }
    render() {

        const margin = this.props.margin / 2;
        const style = {
          ...this.props.style, 
          margin:margin
        };
        return (
          <div style={style} className="card text-white content_item">
            {this.buildArtifacts({height:style.height})}
            <div className="card-img-overlay">
              <h5 className="card-title">
                <NavLink to={`/module/${this.props.content.id}`} className="content_title">{this.props.content.name}</NavLink>
              </h5>
              <p className="card-text conten_item_main_text">{this.props.content.description}</p>
              <p className="card-text conten_item_date_mark">{this.props.content.created_date}</p>
            </div>
          </div>
        );
    }
}

export default ContentItem;