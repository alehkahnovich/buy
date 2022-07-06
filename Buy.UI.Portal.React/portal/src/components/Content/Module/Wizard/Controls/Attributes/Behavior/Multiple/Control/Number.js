import React from 'react';
import Checkbox from './Checkbox';

class Number extends React.Component {
    render() {
        const options = this.props.attribute.options.map(option => {
            return (
                <div className="form-check col-md-4 mb-3 content_control_check" key={option.id}>
                    <Checkbox id={this.props.attribute.id} optionKey={option.id} name={option.name} {...this.props}/>
                </div>
            );
        });

        return options;
    }
}

export default Number;