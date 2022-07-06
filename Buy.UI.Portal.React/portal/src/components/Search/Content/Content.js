import React, { Component } from 'react';
import { Col, ListGroup } from 'reactstrap';
import { List, WindowScroller, AutoSizer } from 'react-virtualized';
import ContentItem from './ContentItem';
import Loader from '../../Common/Loader';
import './Content.scss';

const columnSize = 300;
const marginSize = 10;

class Content extends Component {
    renderer = (options) => {
        const perRow = this.getPerRow({
            columnWidth:options.parent.props.columnWidth, 
            estimatedColumnSize:options.parent.props.estimatedColumnSize
        });

        const estimatedWidth = (options.parent.props.columnWidth / perRow) - marginSize;
        const estimatedHeight = options.parent.props.rowHeight - marginSize;
        

        const components = [];
        for (let index = perRow * options.index; index < perRow * (options.index + 1); index++) {
            if (index > this.props.content.results.length - 1) break;
            const current = this.props.content.results[index];
            components.push(
                <ContentItem 
                key={`${options.key}_${index}`}
                margin={marginSize}
                style={{width:estimatedWidth, height:estimatedHeight}} 
                content={current}/>
            );
        }
        
        return (
            <div key={options.key} style={options.style}>
                {components}
            </div>
        );
    }
    getPerRow = ({columnWidth, estimatedColumnSize}) => {
        return Math.floor(columnWidth / (estimatedColumnSize));
    }
    getRowCount = (options) => {
        const perRow = this.getPerRow({columnWidth:options.width, estimatedColumnSize:columnSize});
        if (this.props.content.results.length === 0 || perRow === 0)
            return 0;

        const count =  Math.ceil(this.props.content.results.length / perRow);
        return count;
    }
    render() {
        if (this.props.loading)
            return <div className="text-center"><Loader /></div>
        return (
            <div>
                <Col md={{ span: 8, offset: 4 }} style={{ width: '100%', margin: 0 }}>
                    <WindowScroller>
                    {({ height, isScrolling, scrollTop }) => (
                        <AutoSizer disableHeight>
                            {({ width, registerChild }) => (
                                <ListGroup>
                                    <List
                                        style={{outline: 'none'}}
                                        ref={registerChild}
                                        autoHeight
                                        height={height}
                                        width={width}
                                        isScrolling={isScrolling}
                                        rowCount={this.getRowCount({width})}
                                        rowHeight={210}
                                        estimatedColumnSize={columnSize}
                                        rowRenderer={this.renderer}
                                        scrollTop={scrollTop}
                                    />
                                </ListGroup>
                            )}
                        </AutoSizer>
                    )}
                    </WindowScroller>
                </Col>
            </div>
        );
    }
}

export default Content;
