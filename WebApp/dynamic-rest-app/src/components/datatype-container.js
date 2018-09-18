import React, { Component } from 'react';
import { connect } from "react-redux";
import { Table  } from 'antd';

import { 
    dynamicDataTypeGet
} from '../store/actions/DataType';

class DataTypeContainer extends Component {
    constructor(props) {
        super(props);
        this.state = { objectSelect: '' }
    }

    componentDidMount(){
        this.props.actiondynamicDataTypeGet();
    }

    render() { 
        const columns = [{
            title: 'DataType',
            dataIndex: 'name',
            key: 'name',
            render: (text, record) => (
                <label>{text}</label>
            ),
          }];

        return ( 
            <div className="container" style={styleContainer}>
                <div className="col-md-6 col-lg-6 col-sm-10">
                 <Table
                    rowKey={record => record.id}
                    loading={this.props.isFetchingDataTypesGet}
                    columns={columns}
                    showHeader={true}
                    pagination={{ position: 'top' }}
                    dataSource={this.props.listDataTypes}/>
                </div>
            </div> 
            )
    }
}

const styleContainer = {
    display: 'flex',
    justifyContent: 'center',
    paddingTop: 10
}


const mapStateToProps = state => ({
    isFetchingDataTypesGet: state.datatype.isFetchingDataTypesGet,
    listDataTypes: state.datatype.listDataTypes,
  });
  
  const mapDispatchToProps = dispatch => ({
    actiondynamicDataTypeGet:() => dispatch(dynamicDataTypeGet()),
  });
  
 export default connect(
    mapStateToProps,
    mapDispatchToProps,
  )(DataTypeContainer);