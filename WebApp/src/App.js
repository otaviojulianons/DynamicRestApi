import React, { Component } from 'react';
import configureStore from './store/store';
import { Provider } from 'react-redux';
import './App.css';
import TabMenu from './components/tab-menu';

class App extends Component {
  

  render() {
    return (
      <Provider store={configureStore()}>
        <div className="App">
          <TabMenu/>
        </div>
      </Provider>
    );
  }
}

export default App;
