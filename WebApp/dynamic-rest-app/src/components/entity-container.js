import React, { Component } from 'react';
import { connect } from "react-redux";
import EntityItem from "./entity-item";
import { Collapse, Table, Modal, Button, Tooltip, Popconfirm, Icon   } from 'antd';
import brace from 'brace';
import AceEditor from 'react-ace';

import 'brace/mode/json';
import 'brace/theme/monokai';
import { 
    dynamicEntityGet 
} from '../store/actions/Entity';

class EntityContainer extends Component {
    constructor(props) {
        super(props);
        this.state = { objectSelect: '' }
    }

    componentDidMount(){
        this.props.actiondynamicEntityGet();
    }

    onDelete(id){
        fetch(`http://localhost:5000/${this.props.controller}/Delete?id=${id}`,{
            method: "DELETE",
        })
    }

    onEdit(entity){
        this.setState({ objectSelect:entity,editing: true });
    }

    handleUpdate = (e) => {
        console.log(e);
        this.setState({
            editing: false,
        });
    }
    
    handleCancel = (e) => {
        console.log(e);
        this.setState({
            editing: false,
        });
    }

    render() { 
        const columns = [{
            title: 'Entity',
            dataIndex: 'name',
            key: 'name',
            render: (text, record) => (
                <label>{text}</label>
            ),
          }, {
            key: 'action',
            render: (text, record) => (
                <div>
                    <Tooltip title="Delete Entity">
                        <Popconfirm title="Delete Entity?" placement="left" onConfirm={() => this.onDelete(record.Id)} okText="Yes" cancelText="No">
                            <Icon style={styleIcons} type="delete" className="pointer" />
                        </Popconfirm>
                    </Tooltip>                       
                    <Tooltip title="Edit Entity">
                        <Icon style={styleIcons} type="edit" className="pointer" onClick={ () => this.onEdit(record) } />
                    </Tooltip>  
                </div>
            ),
          }];

        return ( 
            <div className="container" style={styleContainer}>
                <div className="col-md-6 col-lg-6 col-sm-10">

                 <Table
                    style={{ height: 200 }}
                    rowKey={record => record.id}
                    loading={this.props.isFetchingEntitiesGet}
                    columns={columns}
                    showHeader={true}
                    pagination={{ position: 'top' }}
                    dataSource={this.props.listEntities}/>
                <Modal
                    title="Edit Entity"
                    visible={this.state.editing}
                    okButtonProps={{  text: "Update" }}
                    onOk={this.handleUpdate}
                    onCancel={this.handleCancel}
                    >
                    <AceEditor
                        style={{height: 350, width: 'auto'}}
                        mode="json"
                        theme="monokai"
                        name="blah2"
                        onLoad={this.onLoad}
                        onChange={this.onChange}
                        fontSize={14}
                        showPrintMargin={true}
                        showGutter={true}
                        highlightActiveLine={true}
                        value={JSON.stringify(this.state.objectSelect, null, "\t")}
                        setOptions={{
                        enableBasicAutocompletion: false,
                        enableLiveAutocompletion: false,
                        enableSnippets: false,
                        showLineNumbers: true,
                        tabSize: 2,
                        }}/>
                </Modal>
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

const styleIcons = {
    cursor: 'pointer',
    fontSize: 32,
    padding: 10,
    float: "right"
}

const mapStateToProps = state => ({
    isFetchingEntitiesGet: state.entity.isFetchingEntitiesGet,
    listEntities: state.entity.listEntities,
  });
  
  const mapDispatchToProps = dispatch => ({
    actiondynamicEntityGet:() => dispatch(dynamicEntityGet()),
  });
  
 export default connect(
    mapStateToProps,
    mapDispatchToProps,
  )(EntityContainer);