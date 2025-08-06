import React from 'react';
import { useQuery } from '@tanstack/react-query';
import { userApi, accountApi, transactionApi } from '../services/api';
import Card from '../components/ui/Card';
import { Users, CreditCard, DollarSign, TrendingUp } from 'lucide-react';

const Dashboard: React.FC = () => {
  const { data: usersData, isLoading: usersLoading } = useQuery({
    queryKey: ['users'],
    queryFn: () => userApi.getAll(),
  });

  const { data: accountsData, isLoading: accountsLoading } = useQuery({
    queryKey: ['accounts'],
    queryFn: () => accountApi.getAll(),
  });

  const { data: transactionsData, isLoading: transactionsLoading } = useQuery({
    queryKey: ['transactions'],
    queryFn: () => transactionApi.getAll(),
  });

  const stats = [
    {
      name: 'Total Users',
      value: usersData?.data?.length || 0,
      icon: Users,
      color: 'text-blue-600',
      bgColor: 'bg-blue-100',
    },
    {
      name: 'Total Accounts',
      value: accountsData?.data?.length || 0,
      icon: CreditCard,
      color: 'text-green-600',
      bgColor: 'bg-green-100',
    },
    {
      name: 'Total Transactions',
      value: transactionsData?.data?.length || 0,
      icon: TrendingUp,
      color: 'text-purple-600',
      bgColor: 'bg-purple-100',
    },
    {
      name: 'Total Balance',
      value: `$${accountsData?.data?.reduce((sum, account) => sum + account.balance, 0).toLocaleString() || 0}`,
      icon: DollarSign,
      color: 'text-orange-600',
      bgColor: 'bg-orange-100',
    },
  ];

  const isLoading = usersLoading || accountsLoading || transactionsLoading;

  if (isLoading) {
    return (
      <div className="flex items-center justify-center h-64">
        <div className="animate-spin rounded-full h-12 w-12 border-b-2 border-primary-600"></div>
      </div>
    );
  }

  return (
    <div className="space-y-6">
      <div>
        <h1 className="text-3xl font-bold text-gray-900">Dashboard</h1>
        <p className="mt-2 text-gray-600">Welcome to your banking management system</p>
      </div>

      {/* Stats Grid */}
      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6">
        {stats.map((stat) => {
          const Icon = stat.icon;
          return (
            <Card key={stat.name} className="p-6">
              <div className="flex items-center">
                <div className={`p-3 rounded-lg ${stat.bgColor}`}>
                  <Icon className={`h-6 w-6 ${stat.color}`} />
                </div>
                <div className="ml-4">
                  <p className="text-sm font-medium text-gray-600">{stat.name}</p>
                  <p className="text-2xl font-bold text-gray-900">{stat.value}</p>
                </div>
              </div>
            </Card>
          );
        })}
      </div>

      {/* Recent Activity */}
      <div className="grid grid-cols-1 lg:grid-cols-2 gap-6">
        <Card
          header={<h3 className="text-lg font-semibold text-gray-900">Recent Transactions</h3>}
        >
          {transactionsData?.data?.slice(0, 5).map((transaction) => (
            <div key={transaction.id} className="flex items-center justify-between py-3 border-b border-gray-100 last:border-b-0">
              <div>
                <p className="font-medium text-gray-900">{transaction.description}</p>
                <p className="text-sm text-gray-500">{transaction.type}</p>
              </div>
              <div className="text-right">
                <p className={`font-medium ${transaction.type === 'Deposit' ? 'text-green-600' : 'text-red-600'}`}>
                  {transaction.type === 'Deposit' ? '+' : '-'}${transaction.amount.toFixed(2)}
                </p>
                <p className="text-sm text-gray-500">{new Date(transaction.timestamp).toLocaleDateString()}</p>
              </div>
            </div>
          ))}
        </Card>

        <Card
          header={<h3 className="text-lg font-semibold text-gray-900">Account Overview</h3>}
        >
          {accountsData?.data?.slice(0, 5).map((account) => (
            <div key={account.id} className="flex items-center justify-between py-3 border-b border-gray-100 last:border-b-0">
              <div>
                <p className="font-medium text-gray-900">{account.accountNumber}</p>
                <p className="text-sm text-gray-500">{account.accountType}</p>
              </div>
              <div className="text-right">
                <p className="font-medium text-gray-900">${account.balance.toFixed(2)}</p>
                <span className={`inline-flex items-center px-2 py-1 rounded-full text-xs font-medium ${
                  account.status === 'Active' ? 'bg-green-100 text-green-800' : 'bg-red-100 text-red-800'
                }`}>
                  {account.status}
                </span>
              </div>
            </div>
          ))}
        </Card>
      </div>
    </div>
  );
};

export default Dashboard;


