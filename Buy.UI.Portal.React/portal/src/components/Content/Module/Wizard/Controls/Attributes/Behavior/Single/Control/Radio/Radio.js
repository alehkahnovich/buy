import React from 'react';
import './Radio.scss';
class Radio extends React.Component {
    selectOption = (value) => {
        this.props.onChange({
            id: this.props.id,
            behavior: this.props.attribute.behavior,
            option: {
                id: value
            }
        });
    }
    render() {
        const key = `attribute_option_${this.props.optionKey}`;
        return (
            <React.Fragment>
                <input id={key} name={this.props.id} onClick={() => this.selectOption(this.props.optionKey)} className="form-check-input" type="radio" value={this.props.optionKey} />
                <label htmlFor={key} className="form-check-label form_radio_option">
                    {this.props.name}
                </label>
            </React.Fragment>
        );
    }
}

export default Radio;