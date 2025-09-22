import { useState } from "react";
import Card from "../components/ui/Card";
import { useUser } from "../contexts/UserContext";
import { toast } from "react-toastify";


const Accounts = () => {
    const { user } = useUser();
    const [createAccount, setCreateAccount] = useState(false);
    const baseUrl = import.meta.env.VITE_API_BASE_URL;
    const [accountType, setAccountType] = useState('Checking');
    const [isSubmitting, setIsSubmitting] = useState(false);
    const createNewAccount = async (e: React.FormEvent) => {
        e.preventDefault();
        setIsSubmitting(true);
        try {
            const accountTypeMapping: { [key: string]: string } = {
                'Checking': 'CHECKING',
                'Savings': 'SAVINGS'
            };
            
            const mappedAccountType = accountTypeMapping[accountType] || accountType.toUpperCase();

            console.log(user);
            const requestBody = { 
                Amount: 0, 
                UserId: user?.id , 
                AccountType: mappedAccountType
            };
            
            const response = await fetch(baseUrl + '/api/accounts/new', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${localStorage.getItem('token')}`
                },
                body: JSON.stringify(requestBody)
            });
            
            if (response.ok) {
                const result = await response.json();
                console.log('Success response:', result);
                toast.success('Account created successfully!');
                setCreateAccount(false);
            } else {
                const errorData = await response.json();
                console.error('Error response:', errorData);
                toast.error(`Failed to create account: ${errorData.title || 'Unknown error'}`);
            }
        }catch(error){
            console.error('An Unexpected Error has occurred', error);
            toast.error('An Unexpected Error has occurred')
        } finally {
            setIsSubmitting(false);
        }
    }
    
    return (
        user?.accounts ? (
        <div className="min-h-screen bg-gradient-to-br from-blue-50 to-indigo-100 dark:from-gray-900 dark:to-gray-800 p-6">
            <div className="grid grid-cols-1">
                {user.accounts.map((account) => (
                    <Card className="bg-white dark:bg-gray-800 shadow-lg">
                        <div className="p-6">
                            <h2 className="text-xl font-semibold text-gray-900 dark:text-white">{account.accountType}</h2>
                            <p className="text-gray-600 dark:text-gray-400">{account.accountNumber}</p>
                        </div>
                    </Card>
                ))}
            </div>
        </div>
    ) : (
        createAccount ? (
            <div className="min-h-screen bg-gradient-to-br from-blue-50 to-indigo-100 dark:from-gray-900 dark:to-gray-800 p-6">
                <div className="grid grid-cols-1 max-w-md mx-auto">
                    <Card className="bg-white dark:bg-gray-800 shadow-lg">
                        <div className="p-6">
                            <h2 className="text-xl font-semibold text-gray-900 dark:text-white mb-4">Create New Account</h2>
                            <form onSubmit={createNewAccount} className="space-y-4">
                                <div>
                                    <label htmlFor="accountType" className="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-2">
                                        Account Type
                                    </label>
                                    <select
                                        id="accountType"
                                        value={accountType}
                                        onChange={(e) => setAccountType(e.target.value)}
                                        className="w-full px-3 py-2 border border-gray-300 dark:border-gray-600 rounded-md shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500 dark:bg-gray-700 dark:text-white"
                                        required
                                    >
                                        <option value="Checking">Checking</option>
                                        <option value="Savings">Savings</option>
                                    </select>
                                </div>
                                
                                <div className="flex space-x-3 pt-4">
                                    <button
                                        type="submit"
                                        disabled={isSubmitting}
                                        className="flex-1 bg-blue-500 hover:bg-blue-600 disabled:bg-blue-300 text-white px-4 py-2 rounded-md transition-colors duration-200"
                                    >
                                        {isSubmitting ? 'Creating...' : 'Create Account'}
                                    </button>
                                    <button
                                        type="button"
                                        onClick={() => setCreateAccount(false)}
                                        className="flex-1 bg-gray-500 hover:bg-gray-600 text-white px-4 py-2 rounded-md transition-colors duration-200"
                                    >
                                        Cancel
                                    </button>
                                </div>
                            </form>
                        </div>
                    </Card>
                </div>
            </div>
        ) : (
            <div className="grid grid-cols-1">
                <Card className="bg-white dark:bg-gray-800 shadow-lg">
                    <div className="flex items-center justify-center">
                        <div className="p-6 mr-10">
                            <h2 className="text-xl font-semibold text-gray-900 dark:text-white">Accounts</h2>
                            <p className="text-gray-600 dark:text-gray-400">No accounts found</p>
                        </div>
                        <div className="p-6 ml-10">
                            <button className="bg-blue-500 text-white px-4 py-2 rounded-md" onClick={() => setCreateAccount(true)}>Add Account</button>
                        </div>
                    </div>
                </Card>
            </div>
        )
    )
    )
}   

export default Accounts;