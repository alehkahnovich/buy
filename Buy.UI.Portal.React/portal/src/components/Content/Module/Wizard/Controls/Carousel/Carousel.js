import React from 'react';
import Loader from '../../../../../Common/Loader';
import './Carousel.scss'
class Carousel extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            current: 0,
            previous: -1
        };
    }
    setNextSlideHandler = (e) => {
        e.preventDefault();
        e.stopPropagation();
        const current = this.state.current + 1;
        this.setSlide(current > this.props.items.length - 1 ? 0 : current);
    }
    setPreviousSlideHandler = (e) => {
        e.preventDefault();
        e.stopPropagation();
        const current = this.state.current - 1;
        this.setSlide(current < 0 ? this.props.items.length - 1 : current);
    }
    setSlideHandler = (e) => {
        e.preventDefault();
        e.stopPropagation();
        const current = parseInt(e.target.dataset.target);
        this.setSlide(current);
    }
    setSlide = (current) => {
        let index = current >= 0 && current <= this.props.items.length
        ? current
        : 0;

        this.setState({
            current: index,
            previous: this.state.current
        });
    }
    render() {
        const slides = this.props.items.map((item, index) => {
            const image = item.pending
            ? <div className="slide_image text-center"><Loader /></div>
            : (<img className="img-thumbnail slide_image" 
                src={item.url} 
                alt="no-img" />)

            return (
                <div className="col-md-4 wrapper_slide_image" key={item.id}>
                    {image}
                </div>
            );
        });

        return (
            <div className="container">
                <div className="row">
                    {slides}
                </div>
            </div>
        );
    }
}

export default Carousel;