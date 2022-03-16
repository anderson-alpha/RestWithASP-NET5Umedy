import React,{useState} from 'react';
import Header from './Header';

export default function App() {
  //let counter = 0;
  const [counter, setCounter] = useState(0);  

  function increment()
  {
    setCounter(counter +1);
  };

  return(
    //<Header title="Novo Header Udemy2"/> //Uma forma de passar parâmetro
    <div>
      <Header>       
        Novo Header Udemy3<br/>
        Contador:{counter}
      </Header>      
      <button onClick={increment}>Add</button>
    </div>
  );
}