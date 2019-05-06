import React, { Component } from 'react';
import ReactMarkdown  from "react-markdown";
import Axios from 'axios';
import { Spin, Button } from 'antd';
import { toastr } from 'react-redux-toastr';

class Home extends Component {
    constructor(props) {
        super(props);
        this.state = { readme: '', loading:true };
    }

    componentDidMount(){
        Axios.get("https://raw.githubusercontent.com/otaviojulianons/DynamicRestApi/master/README.md")
            .then(response => {
                this.setState( {readme: response.data, loading: false} );
            })
            .catch( err => {
                toastr.error('Error','Failed to load data');
            });
    }

    render() { 
        return ( 
        <div>
            <ReactMarkdown source={ this.state.readme } />
            <div style={{ textAlign: "center"}}><Spin spinning={this.state.loading} /></div>
        </div> );
    }
}

const styleContainer = {
    height: 200,
    margin: -24 -16
}
 
export default Home;