import React, { Component } from "react";
import { Modal, Form, Input, Button } from 'antd';

const FormItem = Form.Item;

export default class ApikeyModal extends Component {

  constructor(){
    super();
    this.state = { value: ""};
  }

  onSubmit = (e) => {
    e.preventDefault();
    this.props.onSubmit(this.state.value);
  }

  render() {
    const button = 
    <Button key="submit" type="primary" onClick={this.onSubmit}>
        OK
    </Button>;

    return (
      <Modal
        title="Authentication Settings"
        footer={ [ button ]}
        closable={false}
        visible={this.props.open}>
        <div className="col-md-12">
          <Form onSubmit={this.onSubmit}>
            <FormItem label="API Key">
              <Input autoFocus={true} type="password" style={{ width: 150 }} onChange={ e => this.setState({ value: e.target.value}) }/>
            </FormItem>
          </Form>
        </div>
      </Modal>
    );
  }
}



