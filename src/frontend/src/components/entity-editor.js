import React, { Component } from 'react';
import { Modal, Button    } from 'antd';
import AceEditor from 'react-ace';

import 'brace/mode/json';
import 'brace/theme/monokai';

class EntityEditor extends Component {
    constructor(props) {
        super(props);
        this.state = { json: ''};
        this.onChange = this.onChange.bind(this);
        this.onButtonAction = this.onButtonAction.bind(this);
        this.onButtonCancel = this.onButtonCancel.bind(this);
    }

    componentWillReceiveProps(newPropos){
        const { entity } = newPropos;
        if(entity){
            this.setState({ json: JSON.stringify(entity, null, "\t") });
        }
    }

    onChange(newValue) {
        this.setState({ json: newValue });
    }

    onButtonAction() {
        this.props.onButtonAction(this.state.json);
    }

    onButtonCancel() {
        this.props.onButtonCancel();
    }

    render() { 
        const action = 
        <Button key="submit" type="primary" onClick={this.onButtonAction}>
            Insert
        </Button>;

        const cancel = 
        <Button key="cancel" type="default" onClick={this.onButtonCancel} visible={this.props.add}>
            { this.props.add  ?  "Cancel" : "Close"}
        </Button>;

        return ( 
            <Modal
                title="Entity Info"
                closable={false}
                visible={this.props.open}
                footer={ this.props.add ? [ action, cancel] : [cancel]}>
                <AceEditor
                    style={{height: 350, width: 'auto'}}
                    mode="json"
                    theme="monokai"
                    name="blah2"
                    onChange={this.onChange}
                    fontSize={14}
                    showPrintMargin={true}
                    showGutter={true}
                    highlightActiveLine={true}
                    value={this.state.json}
                    setOptions={{
                    enableBasicAutocompletion: false,
                    enableLiveAutocompletion: false,
                    enableSnippets: false,
                    showLineNumbers: true,
                    tabSize: 2,
                    }}/>
            </Modal>
         );
    }
}
 
export default EntityEditor;