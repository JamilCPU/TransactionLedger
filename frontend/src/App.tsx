import React from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import Layout from './components/layout/Layout';
import Dashboard from './pages/Dashboard';
import Login from './pages/Login';
import Register from './pages/Register';
import './App.css';
import { ToastContainer } from 'react-toastify';
import { UserProvider } from './contexts/UserContext';

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
      <ToastContainer
        position="bottom-right"
        autoClose={5000}
        hideProgressBar={false}
        newestOnTop={false}
        closeOnClick={false}
        rtl={false}
        pauseOnFocusLoss
        draggable
        pauseOnHover
        theme="dark"
      />
      <Router>
        <Layout>
        <UserProvider>

          <Routes>
            <Route path="/" element={<Navigate to="/login" />} />
            <Route path="/login" element={<Login />} />
            <Route path="/register" element={<Register />} />
            <Route path="/dashboard" element={<Dashboard />} />
            <Route path="/users" element={<div className="text-center py-12"><h2 className="text-2xl font-bold">Users Page - Coming Soon</h2></div>} />
            <Route path="/accounts" element={<div className="text-center py-12"><h2 className="text-2xl font-bold">Accounts Page - Coming Soon</h2></div>} />
            <Route path="/transactions" element={<div className="text-center py-12"><h2 className="text-2xl font-bold">Transactions Page - Coming Soon</h2></div>} />
          </Routes>
          </UserProvider>

        </Layout>
      </Router>
    </QueryClientProvider>
  );
}

export default App;
