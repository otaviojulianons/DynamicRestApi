import React, { Component } from 'react';
import { Card, Avatar } from 'antd';

const { Meta } = Card;

class SwaggerCard extends Component {
    constructor(props) {
        super(props);
        this.state = {  }
    }
    render() { 
        return ( <div className="col-md-6 col-sm-12" style={{padding: 24}}>
                    <Card
                        cover={<div><img alt="example" src="./images/swagger-logo.png" className="center" style={{ marginTop: 24, height: 64}} /></div>}
                        actions={[<a href={this.props.url} target="_blank"><Avatar size="large" icon="link" /></a>]}>
                            <Meta
                            title={this.props.title}
                            description={this.props.description}
                            />
                    </Card>
                </div>);
    }
}
 
export default SwaggerCard;