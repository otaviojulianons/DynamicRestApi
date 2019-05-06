import React, { Component } from 'react';
import configureStore from './store/store';
import ReduxToastr from 'react-redux-toastr'
import { Provider } from 'react-redux';
import './App.css';
import 'react-redux-toastr/lib/css/react-redux-toastr.min.css';
import TabMenu from './components/tab-menu';
import AxiosConfiguration from './core/api/AxiosConfiguration'; 
class App extends Component {
  
  constructor(){
    super();
    AxiosConfiguration.setupInterceptors();
  }

  render() {
    return (
      <Provider store={configureStore()}>
        <div className="App">
          <ReduxToastr timeOut={20000} preventDuplicates progressBar />
          <TabMenu/>
          
        </div>
      </Provider>
    );
  }
}

export default App;
