import React, { useState } from 'react';
import { Link, useLocation, useNavigate } from 'react-router-dom';
import { toast } from 'react-toastify';
import { useUser } from '../contexts/UserContext';

const Login: React.FC = () => {
    const baseUrl = import.meta.env.VITE_API_BASE_URL;
    const frontendUrl = import.meta.env.VITE_FRONTEND_URL;
    const [username, setUsername] = useState(useLocation().state?.username || '');
    const [password, setPassword] = useState('');
    const [isLoading, setIsLoading] = useState(false);
    const navigate = useNavigate();
    const { setUser } = useUser();

    const tryLogin = async () => {
        setIsLoading(true);
        try {
            const response = await fetch(baseUrl + '/api/auth/login', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'Access-Control-Allow-Origin': frontendUrl
                },
                body: JSON.stringify({ username, password })
            });
            if(response.status !== 200){
                console.log(username);
                console.log(password);
                toast.error(`${response.statusText} error has occurred`)
            }else{
                const loginInfo = await response.json();
                console.log(loginInfo);
                localStorage.setItem('token', loginInfo.jwtToken);
                const userInfo = await fetch(baseUrl + '/api/users/info/' + loginInfo.username, {
                    method: 'GET',
                    headers: {
                        'Content-Type': 'application/json',
                        'Access-Control-Allow-Origin': frontendUrl
                    }
                });
                const user = await userInfo.json();
                setUser(user);
                console.log(user);
                setUser(user);
                navigate('/dashboard');
            }
        } catch (error) {
            console.error('An Unexpected Error has occurred', error);
            toast.error('An Unexpected Error has occurred')
        } finally {
            setIsLoading(false);
        }
    }

    return (
        <div className="flex flex-col justify-center gap-4 mb-0 w-1/2 mx-auto">
            <form className="flex flex-col justify-center gap-4 border" onSubmit={(form) => {
                form.preventDefault();
                tryLogin();
            }}>
                <div className="mx-auto">
                    <h1 className="text-large font-bold mb-2">Username</h1>
                    <input type="text" className="border-2 border-gray-300 rounded-md text-black pl-1" onChange={(input) => setUsername(input.target.value)} value={username} />
                </div>
                <div className="mx-auto">
                    <h1 className="text-large font-bold mb-2">Password</h1>
                    <input type="password" className="border-2 border-gray-300 rounded-md text-black pl-1" onChange={(input) => setPassword(input.target.value)} />
                </div>
                <button className="bg-blue-500 text-white p-2 rounded-md w-28 mx-auto mt-6 mb-5" type="submit" disabled={isLoading}>{isLoading ? 'Loading...' : 'Login'}</button>
            </form>

            <div className="flex flex-col justify-center gap-4 mt-0">
                <h5 className="justify-center mx-auto font-bold underline small-text"><Link to="/register">Don't have an account? Create one here!</Link></h5>            
            </div>
        </div>

    );
};

export default Login;
