import React, { Component } from 'react';
import { Select, Radio, Button, Form } from 'antd';
import { connect } from "react-redux";
import { 
    codegenClientGet,
    codegenServerGet,
    codegenPost
} from '../store/actions/Codegen';
const FormItem = Form.Item;
const Option = Select.Option;

class CodegenContainer extends Component {
    constructor(props) {
        super(props);
        this.state = { itens:[], loading: false, type:'clients', template:'' }


    }
    
    componentDidMount(){
        this.props.actionCodegenClientGet().then( () =>{
            const itens = this.props.listClientTemplates   
            this.setState({ itens });
        });
        this.props.actionCodegenServerGet();
    }

    handleCodegen = () => {
        this.props.actionCodegenPost(this.state.type,this.state.template);
    }

    handleChangeType = (e) => {
        const type = e.target.value;
        const template = '';
        const itens =  (this.state.type === 'clients') 
                        ? this.props.listClientTemplates 
                        : this.props.listServerTemplates;   
        this.setState({ type, itens, template });
    }

    handleTemplate = (template) => {
        this.setState({ template });
    }

    render() { 
        return ( <div style={{ justifyContent: 'center',display: 'flex', marginTop: 24 }}>
                    <Form layout="vertical">
                        <FormItem label="Template type">
                            <Radio.Group 
                                style={{ width: 200 }}
                                defaultValue="clients" 
                                buttonStyle="solid"
                                onChange={this.handleChangeType.bind(this)}
                            >
                                <Radio.Button 
                                    value="clients" 
                                    style={{ width: 100 }}
                                >
                                Clients
                                </Radio.Button>
                                <Radio.Button 
                                    value="servers" 
                                    style={{ width: 100 }}
                                >
                                Servers
                                </Radio.Button>
                            </Radio.Group>   
                        </FormItem>
                        <FormItem label="Template">
                            <Select
                                showSearch
                                value={this.state.template}
                                style={{ width: 200 }}
                                placeholder="Select a template"
                                onChange={this.handleTemplate.bind(this)}
                                filterOption={(input, option) => option.props.children.toLowerCase().indexOf(input.toLowerCase()) >= 0}
                            >   
                                { this.state.itens.map( item =>  <Option value={item} key={item}>{item}</Option> )}
                            </Select> 
                        </FormItem>    
                        <Button 
                            type="primary" 
                            loading={this.props.isExecutingCodegenPost} 
                            onClick={this.handleCodegen.bind(this)}
                            style={{ width: 200 }}
                        >
                        Code Generate
                        </Button>                                          
                    </Form>
        </div> );
    }
}

const mapStateToProps = state => ({
    listClientTemplates: state.codegen.listClientTemplates,
    listServerTemplates: state.codegen.listServerTemplates,
    isExecutingCodegenPost: state.codegen.isExecutingCodegenPost,
  });
  
  const mapDispatchToProps = dispatch => ({
    actionCodegenClientGet:() => dispatch(codegenClientGet()),
    actionCodegenServerGet:() => dispatch(codegenServerGet()),
    actionCodegenPost:(codeGenType, type) => dispatch(codegenPost(codeGenType, type)),
  });
  
 export default connect(
    mapStateToProps,
    mapDispatchToProps,
  )(CodegenContainer);