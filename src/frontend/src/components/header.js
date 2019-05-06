import React, { Component } from "react";
import ApikeyButton from './apikey-button';
import GithubButton from './github-button';

export default class Header extends Component {
  render() {
    return (
    <div style={layout}>
        <ApikeyButton {...this.props}/>
        <GithubButton {...this.props}/>
    </div>
    );
  }
}

const layout = {
    marginTop: 8,
    marginBottom: -16
}

