import { useState } from "react";
import { Link, Navigate, useNavigate } from "react-router-dom";
import { toast } from "react-toastify";

const Register: React.FC = () => {
    const baseUrl = import.meta.env.VITE_API_BASE_URL;
    const frontendUrl = import.meta.env.VITE_FRONTEND_URL;
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [email, setEmail] = useState('');
    const [phone, setPhone] = useState('');
    const [isLoading, setIsLoading] = useState(false);
    const navigate = useNavigate();


    const validateForm = () => {
        if(username === '' || password === '' || email === '' || phone === ''){
            toast.error('Please fill out all fields');
            return false;
        }
        if(password.length < 8){
            toast.error('Password must be at least 8 characters long');
            return false;
        }
        if(email === '' || !email.includes('@') || !email.includes('.')){
            toast.error('Please enter a valid email');
            return false;
        }
        return true;
    }

    const tryRegister = async () => {
        if(!validateForm()){
            return;
        }
        setIsLoading(true);
        console.log(username, password, email, phone);
        try {
            const response = await fetch(baseUrl + '/api/auth/register', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'Access-Control-Allow-Origin': frontendUrl
                },
                body: JSON.stringify({ username, password, email, phone })
            });
            console.log(response);
            if(response.status === 200){
                toast.success('User has been successfully created!')
                navigate('/login', {state: {username: username}});
            }else{
                const errorText = await response.text();
                toast.error(errorText || `${response.statusText} error has occurred`);
            }
        } catch (error) {
            console.error('An Unexpected Error has occurred', error);
            toast.error('An Unexpected Error has occurred')
        } finally {
            setIsLoading(false);
        }
    }

    return (
        <div className="flex flex-col justify-center gap-4 mb-0">
            <form className="flex flex-col justify-center gap-4 border w-1/2 mx-auto" onSubmit={(form) => {
                form.preventDefault();
                tryRegister();
            }}>
                <div className="mx-auto">
                    <h1 className="text-large font-bold mb-2">Username</h1>
                    <input type="text" className="border-2 border-gray-300 rounded-md text-black pl-1" onChange={(input) => setUsername(input.target.value)} />
                </div>
                <div className="mx-auto">
                    <h1 className="text-large font-bold mb-2">Password</h1>
                    <input type="password" className="border-2 border-gray-300 rounded-md text-black pl-1" onChange={(input) => setPassword(input.target.value)} />
                </div>

                <div className="mx-auto">
                    <h1 className="text-large font-bold mb-2">Email</h1>
                    <input type="email" className="border-2 border-gray-300 rounded-md text-black pl-1" onChange={(input) => setEmail(input.target.value)} />
                </div>

                <div className="mx-auto">
                    <h1 className="text-large font-bold mb-2">Phone</h1>
                    <input type="text" className="border-2 border-gray-300 rounded-md text-black pl-1" onChange={(input) => setPhone(input.target.value)} />
                </div>
                <button className="bg-blue-500 text-white p-2 rounded-md w-28 mx-auto mt-6 mb-6" type="submit" disabled={isLoading}>{isLoading ? 'Registering...' : 'Register'}</button>
            </form>

            <div className="flex flex-col justify-center gap-4 mt-0">
                <h5 className="justify-center mx-auto font-bold underline small-text"><Link to="/login">Have an account already? Login here!</Link></h5>            
            </div>
        </div>
    )
}

export default Register;