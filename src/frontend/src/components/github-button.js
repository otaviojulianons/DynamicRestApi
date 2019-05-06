import React, { Component } from "react";
import { Avatar } from 'antd';

export default class GithubButton extends Component {
  render() {
    return (
    <div className="header-button">
        <a className="header-link" href="https://github.com/otaviojulianons/DynamicRestApi" target="_blank">
            { !this.props.isMobile && "View on Github" }
            <Avatar 
                alt="View on Github"
                shape="logo-avatar" 
                style={{ float: "right"}} 
                size={24} 
                src="./images/github-logo.png" 
                shape="square"/>
        </a>
    </div>
    );
  }
}

