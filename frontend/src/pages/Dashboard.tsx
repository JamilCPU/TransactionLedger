import React from 'react';
import { useUser } from '../contexts/UserContext';
import { Grid } from 'lucide-react';

const Dashboard: React.FC = () => {
  const { user } = useUser();
 

  return (
    user ? (
      
      <div className="min-h-screen bg-gradient-to-br from-blue-50 to-indigo-100 dark:from-dark-900 dark:to-dark-800 p-6">
      <div className="max-w-7xl mx-auto">
        {/* Header */}
        <div className="mb-8">
          <h1 className="text-3xl font-bold text-gray-900 dark:text-white mb-2">Dashboard</h1>
          <p className="text-gray-600 dark:text-gray-300">Welcome back, {user.username}!</p>
        </div>
        </div>

      <div className="grid grid-cols-2 gap-4 h-25vh">
        <h2>Welcome, {user?.username}</h2>
        <h2>Email: {user?.email}</h2>
        <h2>Phone: {user?.phone}</h2>
        <h2>Accounts: {user?.accounts?.length}</h2>
      </div>
    </div>
  ) : (
    <div>
      <h1>Dashboard</h1>
      <h2>Please login to view your dashboard</h2>
    </div>
  ))
};

export default Dashboard;


