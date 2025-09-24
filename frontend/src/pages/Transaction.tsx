import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";

const Transaction = () => {
    const { accountId } = useParams();
    const baseUrl = import.meta.env.VITE_API_BASE_URL;
    const frontendUrl = import.meta.env.VITE_FRONTEND_URL;
    let transactions = useState<[]>([]);
    const getTransactions = async () => {
        try {
            const response = await fetch(baseUrl + '/api/transactions/account/' + accountId + '/transactions', {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json',
                    'Access-Control-Allow-Origin': frontendUrl
                },
            });
            const data = await response.json();
            console.log(data);
            transactions = data;
        } catch (error) {
            console.error('Error fetching transactions:', error);
        }
    }

    useEffect(() => {
        getTransactions();
    }, []);
    return (
        <div>
            <div className="flex flex-col gap-4 justify-center items-center">
            <h1 className = "text-2xl font-bold">Transactions</h1>
            </div>
            <div className="flex flex-col gap-4">
                {transactions.map((transaction: any) => (
                    <div key={transaction.id}>
                        <h1>{transaction.amount}</h1>
                    </div>
                ))}
            </div>

        </div>
    )
}

export default Transaction;