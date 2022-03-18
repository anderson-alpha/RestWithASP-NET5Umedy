import React, {useState, useEffect} from 'react';
import {Link, useHistory} from 'react-router-dom';
import { FiPower, FiEdit, FiTrash2 } from 'react-icons/fi';
import api from '../../services/api';
import './styles.css';
import logoImage from '../../assets/logo.svg';

export default function Books() {

    const [books, setBooks] = useState([]);  
    const [page, setPage] = useState(0);      

    const userName = localStorage.getItem('userName');      
      
    const accessToken = localStorage.getItem('accessToken');

    const history = useHistory();

    const authorization = {
        headers:{
            Authorization: `Bearer ${accessToken}`
        }
    };    

    useEffect(() => {
        fetchMoreBooks();
    },[accessToken]);

    async function fetchMoreBooks() {
        const response = await api.get(`api/Book/v1/asc/4/${page}` , authorization).then(response => {
            setBooks([...books, ...response.data.list]);
            setPage(page + 1);            
        })
    }    

    async function deleteBook(id) {

        try{
            await api.delete(`api/Book/v1/${id}`,authorization);

            setBooks(books.filter(book => book.id !== id))
        }catch(erros){
            alert('Falha ao apagar Livro! Tente novamente.');
        }
    }    

    async function editBook(id) {
        try{
            history.push(`/book/new/${id}`);
        }catch(erros){
            alert('Falha ao apagar Livro! Tente novamente.');
        }
    }     

    async function logout() {
        try{
            await api.get('api/auth/v1/revoke',authorization);

            localStorage.clear();
            history.push('/');
        }catch(erros){
            alert('Logout falhou! Tente novamente.');
        }
    }  

    return(
        <div className="book-container">
            <header>
                <img src={logoImage} alt="Empresa" />
                <span>Bem Vindo, <strong>{userName.toLowerCase()}</strong>!</span>
                <Link className='button' to="book/new/0">Adicionar Novo Livro</Link>
                <button onClick={logout} type='button'>                    
                    <FiPower size={18} color="#251FC5"/>
                </button>
            </header>
            <h1>Registro de Livros</h1>
            <ul>
                {books.map(book => (
                    <li key={book.id}>
                        <strong>Titulo:</strong>
                        <p>{book.title}</p>
                        <strong>Autor:</strong>                
                        <p>{book.author}</p>                
                        <strong>Preco:</strong>                
                        <p>R$ {Intl.NumberFormat('pt-BR', {style: 'currency', currency:'BRL'}).format(book.price)}</p>
                        <strong>Data de Publicação:</strong>                
                        <p>{Intl.DateTimeFormat('pt-BR').format(new Date(book.launchDate))}</p>      
                        <button type='button' onClick={() => editBook(book.id)}>
                            <FiEdit size={20} color="#251FC5"/>
                        </button>                                          
                        <button onClick={() => deleteBook(book.id)} type='button'>
                            <FiTrash2 size={20} color="#251FC5"/>
                        </button>
                    </li>
                ))}                
            </ul>
            <button className="button" onClick={fetchMoreBooks} type='button'>Carregar mais</button>
        </div>
    );
  }