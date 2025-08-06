import axios from 'axios';
import type { 
  User, 
  Account, 
  Transaction, 
  CreateUserRequest, 
  CreateAccountRequest, 
  CreateTransactionRequest,
  ApiResponse,
  PaginatedResponse 
} from '../types';

const API_BASE_URL = 'http://localhost:5000/api';

const api = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
});

// Request interceptor for logging
api.interceptors.request.use(
  (config) => {
    console.log(`Making ${config.method?.toUpperCase()} request to ${config.url}`);
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

// Response interceptor for error handling
api.interceptors.response.use(
  (response) => {
    return response;
  },
  (error) => {
    console.error('API Error:', error.response?.data || error.message);
    return Promise.reject(error);
  }
);

// User API
export const userApi = {
  getAll: () => api.get<ApiResponse<User[]>>('/users'),
  getById: (id: number) => api.get<ApiResponse<User>>(`/users/${id}`),
  create: (user: CreateUserRequest) => api.post<ApiResponse<User>>('/users', user),
  update: (id: number, user: Partial<CreateUserRequest>) => 
    api.put<ApiResponse<User>>(`/users/${id}`, user),
  delete: (id: number) => api.delete<ApiResponse<void>>(`/users/${id}`),
};

// Account API
export const accountApi = {
  getAll: () => api.get<ApiResponse<Account[]>>('/accounts'),
  getById: (id: number) => api.get<ApiResponse<Account>>(`/accounts/${id}`),
  getByUserId: (userId: number) => api.get<ApiResponse<Account[]>>(`/accounts/user/${userId}`),
  create: (account: CreateAccountRequest) => api.post<ApiResponse<Account>>('/accounts', account),
  update: (id: number, account: Partial<CreateAccountRequest>) => 
    api.put<ApiResponse<Account>>(`/accounts/${id}`, account),
  delete: (id: number) => api.delete<ApiResponse<void>>(`/accounts/${id}`),
};

// Transaction API
export const transactionApi = {
  getAll: () => api.get<ApiResponse<Transaction[]>>('/transactions'),
  getById: (id: number) => api.get<ApiResponse<Transaction>>(`/transactions/${id}`),
  getByAccountId: (accountId: number) => 
    api.get<ApiResponse<Transaction[]>>(`/transactions/account/${accountId}`),
  create: (transaction: CreateTransactionRequest) => 
    api.post<ApiResponse<Transaction>>('/transactions', transaction),
  update: (id: number, transaction: Partial<CreateTransactionRequest>) => 
    api.put<ApiResponse<Transaction>>(`/transactions/${id}`, transaction),
  delete: (id: number) => api.delete<ApiResponse<void>>(`/transactions/${id}`),
};

export default api; 