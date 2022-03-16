import React from 'react';

//export default function Header(props) {
export default function Header({children}) {
  return(
    //JSX JavaScript XML
    <header>
        <h1>{children}</h1>
    </header>
  );
}