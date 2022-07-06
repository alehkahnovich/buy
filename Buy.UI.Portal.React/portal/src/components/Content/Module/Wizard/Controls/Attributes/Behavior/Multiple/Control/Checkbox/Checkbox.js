import React from 'react';

class Checkbox extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            checked: false
        };
    }
    selectOption = (event) => {
        const contract = {
            id: this.props.id,
            behavior: this.props.attribute.behavior,
            option: { id: this.props.optionKey }
        };

        if (this.state.checked) {
            this.props.onRemove(contract);
        } else {
            this.props.onChange(contract);
        }

        this.setState({checked: !this.state.checked});
    }
    render() {
        const key = `attribute_option_${this.props.optionKey}`;
        return (
            <React.Fragment>
                <input id={key} name={this.props.id} 
                onChange={this.selectOption} 
                className="form-check-input" 
                type="checkbox" 
                checked={this.state.checked} />
                <label htmlFor={key} className="form-check-label form_radio_option">
                    {this.props.name}
                </label>
            </React.Fragment>
        );
    }
}

export default Checkbox;