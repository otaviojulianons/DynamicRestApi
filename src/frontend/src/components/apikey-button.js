import React, { Component } from "react";
import { Avatar } from 'antd';

export default class ApikeyButton extends Component {
  render() {
    return (
    <div className="header-button">
        <a className="header-link" onClick={this.props.onAPIKeyClick}>
            { !this.props.isMobile && "API key" }
            <Avatar 
            alt="API key"
            shape="logo-avatar" 
            style={{ float: "right"}} 
            size={24}  
            shape="square" 
            src={ this.props.locked ? "./images/locked.png" : "./images/unlocked.png"}/>
        </a>
    </div>
    );
  }
}



