import React from 'react';
import { useQuery } from '@tanstack/react-query';
import { userApi, accountApi, transactionApi } from '../services/api';
import Card from '../components/ui/Card';
import { Users, CreditCard, DollarSign, TrendingUp } from 'lucide-react';
import { useUser } from '../contexts/UserContext';

const Dashboard: React.FC = () => {
  const { user } = useUser();
 

  return (
    <div>
      <h1>Dashboard</h1>
    </div>
  );
};

export default Dashboard;


