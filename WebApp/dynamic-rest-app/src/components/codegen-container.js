import React, { Component } from 'react';
import { Select, Radio, Button, Form } from 'antd';
import { connect } from "react-redux";
import { 
    codegenClientGet,
    codegenServerGet,
} from '../store/actions/Codegen';
const FormItem = Form.Item;
const Option = Select.Option;

class CodegenContainer extends Component {
    constructor(props) {
        super(props);
        this.state = { itens:[], loading: false, type:'client', template:'' }
    }
    
    componentDidMount(){
        this.props.actionCodegenClientGet().then( () =>{
            const itens = this.props.listClientTemplates   
            this.setState({ itens });
        });
        this.props.actionCodegenServerGet();
    }

    enterLoading = () => {
        this.setState({ loading: true });
    }

    handleChangeType = (e) => {
        const type = e.target.value;
        const template = '';
        const itens =  (this.state == 'client') 
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
                                defaultValue="client" 
                                buttonStyle="solid"
                                onChange={this.handleChangeType.bind(this)}
                            >
                                <Radio.Button 
                                    value="client" 
                                    style={{ width: 100 }}
                                >
                                Clients
                                </Radio.Button>
                                <Radio.Button 
                                    value="server" 
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
                                { this.state.itens.map( item =>  <Option value={item}>{item}</Option> )}
                            </Select> 
                        </FormItem>    
                        <Button 
                            type="primary" 
                            loading={this.state.loading} 
                            onClick={this.enterLoading}
                            style={{ width: 200 }}
                        >
                        Generate Code
                        </Button>                                          
                    </Form>
        </div> );
    }
}

const mapStateToProps = state => ({
    listClientTemplates: state.codegen.listClientTemplates,
    listServerTemplates: state.codegen.listServerTemplates,
  });
  
  const mapDispatchToProps = dispatch => ({
    actionCodegenClientGet:() => dispatch(codegenClientGet()),
    actionCodegenServerGet:() => dispatch(codegenServerGet()),
  });
  
 export default connect(
    mapStateToProps,
    mapDispatchToProps,
  )(CodegenContainer);