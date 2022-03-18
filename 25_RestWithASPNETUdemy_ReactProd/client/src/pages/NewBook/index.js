import React, {useEffect, useState} from 'react';
import {Link, useHistory, useParams} from 'react-router-dom';
import { FiArrowLeft } from 'react-icons/fi';

import api from '../../services/api';

import './styles.css';
import logoImage from '../../assets/logo.svg';

export default function NewBook() {
    const [id, setId] = useState(null);        
    const [author, setAuthor] = useState('');    
    const [title, setTitle] = useState('');
    const [launchDate, setLaunchDate] = useState('');
    const [price, setPrice] = useState('');

    const { bookId }= useParams();

    const accessToken = localStorage.getItem('accessToken');

    const authorization = {
        headers:{
            Authorization: `Bearer ${accessToken}`
        }
    };

    const history = useHistory();

    useEffect(() => {
        if(bookId === '0') return;
        else loadBook();        
    },bookId);
    
    async function loadBook(){
        try{
            const response = await api.get(`api/book/v1/${bookId}`, authorization);

            let adjustedDate = response.data.launchDate.split("T",10)[0];

            setId(response.data.id);
            setTitle(response.data.title);
            setAuthor(response.data.author);
            setPrice(response.data.price);
            setLaunchDate(adjustedDate);                                                
        }catch(error){
            alert('Falha ao recuperar o Livro! Tente novamente.');
            history.push('/books');
        }
    }

    // async function createNewBook(e)
    // {
    //     e.preventDefault();
    //     const data = {
    //         author,
    //         title,
    //         launchDate,
    //         price,            
    //     };

    //     try{
    //         await api.post('api/Book/v1/',data,authorization);
    //     }catch(erros){
    //         alert('Falha ao gravar Livro! Tente novamente.');
    //     }

    //     history.push('/books');        
    // }

    async function saveOrUpdate(e)
    {
        e.preventDefault();
        const data = {
            author,
            title,
            launchDate,
            price,            
        };

        try{
            if(bookId==='0'){
                await api.post('api/Book/v1/',data,authorization);
            }else{
                data.id = id;
                await api.put('api/Book/v1/',data,authorization);                
            }            
        }catch(erros){
            alert('Falha ao gravar Livro! Tente novamente.');
        }

        history.push('/books');        
    }


    return(
        <div className="new-book-container">
            <div className="content">
                <section className="form">
                    <img src={logoImage} alt="Aplicacao" />
                    <h1>{bookId==='0'?'Adicionar':'Atualizar'} um Livro</h1>                    
                    <p>Informe detalhes do Livro e clique em {bookId==='0'?`Adicionar`:`Atualizar`}</p>
                    <Link className="back-link" to="/books">
                        <FiArrowLeft size={16} color="#251FC5" />
                        Voltar
                    </Link>
                </section>
                {/* <form onSubmit={createNewBook}> */}
                <form onSubmit={saveOrUpdate}>
                    <input value={title} onChange={e => setTitle(e.target.value)} placeholder= 'Titulo' type="text" />
                    <input value={author} onChange={e => setAuthor(e.target.value)} placeholder= 'Autor' type="text" />
                    <input value={launchDate} onChange={e => setLaunchDate(e.target.value)} placeholder= 'Data de Publicacao' type="date" />
                    <input value={price} onChange={e => setPrice(e.target.value)} placeholder= 'Preço' type="text" />
                    <button className="button" type='submit'>
                        {bookId==='0'?'Adicionar':'Atualizar'}
                    </button>
                </form>
            </div>
        </div>
    );
  }