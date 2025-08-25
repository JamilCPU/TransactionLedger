import React from 'react';

const Login: React.FC = () => {
    return (
        <div className = "flex flex-col items-center justify-center min-h-[70vh] gap-4">
            <h1 className = "text-2xl font-bold">Username</h1>
            <input type = "text" className = "border-2 border-gray-300 rounded-md p-2" />
            <h1 className = "text-2xl font-bold">Password</h1>
            <input type = "password" className = "border-2 border-gray-300 rounded-md p-2" />
            <button className = "bg-blue-500 text-white p-2 rounded-md">Login</button>
        </div>
    );
};

export default Login;
