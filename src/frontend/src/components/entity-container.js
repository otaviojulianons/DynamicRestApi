import React, { Component } from 'react';
import { connect } from "react-redux";
import { Table, Tooltip, Popconfirm, Icon, Button    } from 'antd';
import { 
    dynamicEntityGet,
    dynamicEntityByIdDelete,
    dynamicEntityPost
} from '../store/actions/Entity';
import EntityEditor from './entity-editor';
import { toastr } from 'react-redux-toastr';

class EntityContainer extends Component {
    constructor(props) {
        super(props);
        this.state = { entitySelect: '', editorOpen: false, readme:'' };
        this.onDelete = this.onDelete.bind(this);
        this.handleButtonAction = this.handleButtonAction.bind(this);
        this.onOpen = this.onOpen.bind(this);
        this.onAdd = this.onAdd.bind(this);
        this.handleButtonCancel = this.handleButtonCancel.bind(this);
    }

    componentDidMount(){
        this.props.actiondynamicEntityGet();
    }

    onDelete(id){
        this.props.actiondynamicEntityByIdDelete(id).then( () => {
            this.props.actiondynamicEntityGet();
        });
    }

    onOpen(entity){
        this.setState({ entitySelect:entity, editorOpen: true, add: false });
    }

    onAdd(){
        var entity = {
            name:'EntityName',
            attributes:[
                {"name":"Id","dataType":"long"},
                {"name":"Name","dataType":"string","length":64},]
        }
        this.setState({ entitySelect: entity, editorOpen: true, add: true });
    }

    handleButtonCancel(json){
        this.setState({editorOpen: false});
    }

    handleButtonAction(json){
        this.setState({editorOpen: false});

        this.props.actiondynamicEntityPost(json).then(() =>{
            this.props.actiondynamicEntityGet();
        });
    }
    
    render() { 
        const columns = [{
            title: 'Entities',
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
                        <Popconfirm title="Delete Entity?" placement="left" onConfirm={() => this.onDelete(record.id)} okText="Yes" cancelText="No">
                            <Icon style={styleIcons} type="delete" className="pointer padding-icon" />
                        </Popconfirm>
                    </Tooltip>                       
                    <Tooltip title="View Entity">
                        <Icon style={styleIcons} type="info-circle" className="pointer padding-icon" onClick={ () => this.onOpen(record) } />
                    </Tooltip>  
                </div>
            ),
          }];

        return ( 
            <div style={{paddingTop: 10}}>
                <div className="col-md-offset-3 col-md-6 col-sm-12">
                 <Button  style={{ zIndex: 10 }} type="primary" onClick={this.onAdd}>Add Entity</Button>
                 <Table
                    size='middle'
                    style={{ height: 200, marginTop: -42 }}
                    rowKey={record => record.id}
                    loading={this.props.isFetchingEntitiesGet || this.props.isExecutingEntityPost || this.props.isExecutingEntityByIdDelete }
                    columns={columns}
                    showHeader={true}
                    pagination={{ position: 'top' }}
                    dataSource={this.props.listEntities}/>

                    <EntityEditor 
                        onButtonAction={this.handleButtonAction}
                        onButtonCancel={this.handleButtonCancel}
                        entity={this.state.entitySelect}
                        open={this.state.editorOpen}
                        add={this.state.add}/>
                </div>
            </div> 
            )
    }
}

const styleIcons = {
    cursor: 'pointer',
    fontSize: 32,
    float: "right"
}

const mapStateToProps = state => ({
    isFetchingEntitiesGet: state.entity.isFetchingEntitiesGet,
    isExecutingEntityPost: state.entity.isExecutingEntityPost,
    isExecutingEntityByIdDelete: state.entity.isExecutingEntityByIdDelete,
    listEntities: state.entity.listEntities,
});
  
const mapDispatchToProps = dispatch => ({
    actiondynamicEntityGet:() => dispatch(dynamicEntityGet()),
    actiondynamicEntityByIdDelete: id => dispatch(dynamicEntityByIdDelete(id)),
    actiondynamicEntityPost: entity => dispatch(dynamicEntityPost(entity))
});
  
 export default connect(
    mapStateToProps,
    mapDispatchToProps,
  )(EntityContainer);