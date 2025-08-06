import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import Layout from './components/layout/Layout';
import Dashboard from './pages/Dashboard';
import './App.css';

// Create a client
const queryClient = new QueryClient({
  defaultOptions: {
    queries: {
      retry: 1,
      refetchOnWindowFocus: false,
    },
  },
});

function App() {
  return (
    <QueryClientProvider client={queryClient}>
      <Router>
        <Layout>
          <Routes>
            <Route path="/" element={<Dashboard />} />
            {/* Add more routes here as we create more pages */}
            <Route path="/users" element={<div className="text-center py-12"><h2 className="text-2xl font-bold">Users Page - Coming Soon</h2></div>} />
            <Route path="/accounts" element={<div className="text-center py-12"><h2 className="text-2xl font-bold">Accounts Page - Coming Soon</h2></div>} />
            <Route path="/transactions" element={<div className="text-center py-12"><h2 className="text-2xl font-bold">Transactions Page - Coming Soon</h2></div>} />
          </Routes>
        </Layout>
      </Router>
    </QueryClientProvider>
  );
}

export default App;
