import React, { useState } from 'react';

const Login: React.FC = () => {
    const baseUrl = import.meta.env.VITE_API_BASE_URL;
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');

    const [isLoading, setIsLoading] = useState(false);
    const [error, setError] = useState('');

    const disableLogin = () => {

    }

    const tryLogin = async () => {
        setIsLoading(true);
        try {
            const response = await fetch(baseUrl + '/api/auth/login', {
                method: 'POST',
                body: JSON.stringify({ username, password })
            });
            console.log(response);
        } catch (error) {
            console.error('An Unexpected Error has occurred', error);
        } finally {
            setIsLoading(false);
        }
    }

    return (
        <div className="flex flex-col justify-center min-h-[70vh] gap-4">
            <form className="flex flex-col justify-center gap-4" onSubmit={(form) => {
                form.preventDefault();
                tryLogin();
            }}>
                <div className="mx-auto">
                    <h1 className="text-2xl font-bold mb-2">Username</h1>
                    <input type="text" className="border-2 border-gray-300 rounded-md p-2" onChange={(input) => setUsername(input.target.value)} />
                </div>
                <div className="mx-auto">
                    <h1 className="text-2xl font-bold mb-2">Password</h1>
                    <input type="password" className="border-2 border-gray-300 rounded-md p-2" onChange={(input) => setPassword(input.target.value)} />
                </div>
                <button className="bg-blue-500 text-white p-2 rounded-md w-28 mx-auto mt-6" type="submit" disabled={isLoading}>{isLoading ? 'Loading...' : 'Login'}</button>
            </form>
        </div>
    );
};

export default Login;
