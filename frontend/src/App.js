import './App.css';
import React from 'react';
import logo from './logo.png'

function App() {
  return (
    <div className="App">
      <header className="App-header">
        <img 
          src={logo} 
          style={{ alignSelf: 'justify'}}
          width={250} 
          height={250} 
          alt="Website Logo"/>
        <h1>ComfyMusic</h1>
        <h2>A place to store your favourite songs.</h2>
      </header>
    </div>
  );
}

export default App;
