import React from 'react';
import { useUser } from '../contexts/UserContext';

const Dashboard: React.FC = () => {
  const { user } = useUser();
 

  return (
    user ? (
    <div>
      <h1>Dashboard</h1>
      <h2>Welcome, {user?.username}</h2>
      <h2>Email: {user?.email}</h2>
      <h2>Phone: {user?.phone}</h2>
      <h2>Accounts: {user?.accounts?.length}</h2>
    </div>
  ) : (
    <div>
      <h1>Dashboard</h1>
      <h2>Please login to view your dashboard</h2>
    </div>
  ))
};

export default Dashboard;


