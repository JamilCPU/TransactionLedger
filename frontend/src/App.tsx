import { BrowserRouter as Router, Routes, Route, Navigate, useLocation } from 'react-router-dom';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import Layout from './components/layout/Layout';
import Dashboard from './pages/Dashboard';
import Login from './pages/Login';
import Register from './pages/Register';
import './App.css';
import { ToastContainer } from 'react-toastify';
import { UserProvider } from './contexts/UserContext';
import LoggedOutLayout from './components/layout/LoggedOutLayout';
import Accounts from './pages/Accounts';
import Transaction from './pages/Transaction';
// Create a client
const queryClient = new QueryClient({
  defaultOptions: {
    queries: {
      retry: 1,
      refetchOnWindowFocus: false,
    },
  },
});


// Component that uses useLocation inside Router context
function AppContent() {
  const location = useLocation();
  const noLayoutPages = ['/', '/login', '/register'];
  const shouldShowLayout = !noLayoutPages.includes(location.pathname);

  return (
    <UserProvider>
      {shouldShowLayout ? (
        <Layout>
          <Routes>
            <Route path="/dashboard" element={<Dashboard />} />
            <Route path="/accounts" element={<Accounts />} />
            <Route path="/transactions/:accountId" element={<Transaction />} />
          </Routes>
        </Layout>
      ) : (
        <LoggedOutLayout>
        <Routes>
          <Route path="/" element={<Navigate to="/login" />} />
          <Route path="/login" element={<Login />} />
          <Route path="/register" element={<Register />} />
        </Routes>
        </LoggedOutLayout>
      )}
    </UserProvider>
  );
}

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
        <AppContent />
      </Router>
    </QueryClientProvider>
  );
}

export default App;
