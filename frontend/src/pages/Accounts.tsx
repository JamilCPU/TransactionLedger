import { useState } from "react";
import Card from "../components/ui/Card";
import { useUser } from "../contexts/UserContext";

const Accounts = () => {
    const { user } = useUser();
    const [createAccount, setCreateAccount] = useState(false);
    
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
            <div className="grid grid-cols-1">
                <Card className="bg-white dark:bg-gray-800 shadow-lg">
                    <div className="p-6">
                        <h2 className="text-xl font-semibold text-gray-900 dark:text-white">Create Account</h2>
                        <button 
                            className="bg-gray-500 text-white px-4 py-2 rounded-md mr-2"
                            onClick={() => setCreateAccount(false)}
                        >
                            Cancel
                        </button>
                    </div>
                </Card>
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