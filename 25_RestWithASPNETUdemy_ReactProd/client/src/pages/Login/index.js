import React, {useState} from 'react';
import api from '../../services/api';
import { useHistory } from 'react-router-dom';
import './styles.css';

import logoImage from '../../assets/logo.svg';
import padlock from '../../assets/padlock.png';

//export default function Header(props) {
export default function Login() {

    const [userName, setUsername] = useState('');
    const [password, setPassword] = useState('');    
    const history = useHistory();

    async function login(e)
    {
        e.preventDefault();
        const data = {
            userName,
            password,
        };

        try{
            //const response = await api.post('api/Person/v1/4');

            const response = await api.post('api/auth/v1/signin',data,function (res) {
                res.header("Access-Control-Allow-Origin", "*");
            });            

            localStorage.setItem('userName', userName);
            localStorage.setItem('accessToken', response.data.accessToken);
            localStorage.setItem('refreshToken', response.data.refreshToken);                        

            history.push('/books');
        }catch(erros){
            alert('Falha no Login! Tente novamente.');
        }
    }

    return(
    //JSX JavaScript XML
    //div.logint-container
    <div className="login-container">
        <section className="form">
            <img src={logoImage} alt="Logo Cliente" />
            <form onSubmit={login}>
                <h1>Acesse sua conta</h1>
                <input 
                    type="text" placeholder='UserName'
                    value={userName}
                    onChange={e=>setUsername(e.target.value)}
                />

                <input 
                    type="password" placeholder='Password' 
                    value={password}
                    onChange={e=>setPassword(e.target.value)}                    
                />
                <button className='button' type='submit'>Login</button>
            </form>
        </section>
        <img src={padlock} alt="Login" />
    </div>
    );
}