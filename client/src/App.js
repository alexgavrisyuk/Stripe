import React, { Component } from 'react';
import logo from './logo.svg';
import './App.css';
import Axios from 'axios';
import MyStoreCheckout from './MyStoreCheckout';
import { StripeProvider } from 'react-stripe-elements';

class App extends Component {

  state = {
    sessionId: null 
  };

  handleBuy = () => {
    Axios.post('http://localhost:5000/api/Payment/Buy')
    .then(res => {
      console.log(res);
      this.setState({
        sessionId: res.data.id
      })    })
    .catch(err => {
      console.log(err);
    })
  }

  render() {
    return (
      <div className="App">
        <header className="App-header">
          <img src={logo} className="App-logo" alt="logo" />
          <p>
            Edit <code>src/App.js</code> and save to reload.
          </p>
        
            <button onClick={this.handleGetSession}>Get session</button>
            <div>
              Session {this.state.sessionId}
            </div>
            <button onClick={this.handleBuy}>Buy</button>
            <StripeProvider apiKey="pk_test_EZRrwNP9z4BeUYleHMuPhZRL00YdiMadZs">
              <MyStoreCheckout sessionId={this.state.sessionId}/>
            </StripeProvider>
        </header>
      </div>
    );
  }
}

export default App;
