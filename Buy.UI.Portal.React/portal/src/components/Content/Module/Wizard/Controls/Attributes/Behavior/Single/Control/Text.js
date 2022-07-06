import React from 'react';
import Radio from './Radio';
import Select from './Select';
const threshold = 10;
class Text extends React.Component {
    render() {
        if (this.props.attribute.options.length > threshold)
            return <Select {...this.props} />;
            
        const options = this.props.attribute.options.map(option => {
            return (
                <div className="form-check col-md-4 mb-3 content_control_check" key={option.id}>
                    <Radio id={this.props.attribute.id} optionKey={option.id} name={option.name} {...this.props}/>
                </div>
            );
        });

        return options;
    }
}

export default Text;