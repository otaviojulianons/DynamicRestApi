import React, { Component } from 'react';
import { Collapse } from 'antd';

const Panel = Collapse.Panel;
const text = `
  A dog is a type of domesticated animal.
  Known for its loyalty and faithfulness,
  it can be found as a welcome guest in many households across the world.
`;

class EntityItem extends Component {

    constructor(props) {
        super(props);
        this.state = {  }
    }
    render() { 
        return ( 
            <Collapse defaultActiveKey="1">
                <Panel header={this.props.item.name} key="1" style={customPanelStyle}>
                    <p>{text}</p>
                </Panel>
            </Collapse>
         );
    }
}

const customPanelStyle = {
    background: '#f7f7f7',
    borderRadius: 4,
    marginBottom: 24,
    border: 0,
    overflow: 'hidden',
  };
 
export default EntityItem;