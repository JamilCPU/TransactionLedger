import React from 'react';

const Login: React.FC = () => {
    return (
        <div className="flex flex-col justify-center min-h-[70vh] gap-4">
            <form className="flex flex-col justify-center gap-4">
                <div className="mx-auto">
                    <h1 className="text-2xl font-bold mb-2">Username</h1>
                    <input type="text" className="border-2 border-gray-300 rounded-md p-2" />
                </div>
                <div className="mx-auto">
                    <h1 className="text-2xl font-bold mb-2">Password</h1>
                    <input type="password" className="border-2 border-gray-300 rounded-md p-2" />
                </div>
                <button className="bg-blue-500 text-white p-2 rounded-md w-28 mx-auto mt-6">Login</button>
            </form>
        </div>
    );
};

export default Login;
