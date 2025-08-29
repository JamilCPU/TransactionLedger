export interface User {
  id: number;
  username: string;
  email: string;
  firstName: string;
  lastName: string;
  dateOfBirth: string;
  phoneNumber: string;
  address: string;
  createdAt: string;
  updatedAt: string;
}

export interface Account {
  id: number;
  accountNumber: string;
  accountType: 'Savings' | 'Checking' | 'Investment';
  balance: number;
  currency: string;
  status: 'Active' | 'Inactive' | 'Suspended';
  userId: number;
  createdAt: string;
  updatedAt: string;
}

export interface Transaction {
  id: number;
  amount: number;
  type: 'Deposit' | 'Withdrawal' | 'Transfer' | 'Payment';
  description: string;
  status: 'Pending' | 'Completed' | 'Failed' | 'Cancelled';
  accountId: number;
  timestamp: string;
  createdAt: string;
  updatedAt: string;
}

export interface CreateUserRequest {
  username: string;
  email: string;
  firstName: string;
  lastName: string;
  dateOfBirth: string;
  phoneNumber: string;
  address: string;
}

export interface CreateAccountRequest {
  accountType: 'Savings' | 'Checking' | 'Investment';
  userId: number;
}

export interface CreateTransactionRequest {
  amount: number;
  type: 'Deposit' | 'Withdrawal' | 'Transfer' | 'Payment';
  description: string;
  accountId: number;
}

export interface ApiResponse<T> {
  data: T;
  message: string;
  success: boolean;
}

export interface PaginatedResponse<T> {
  data: T[];
  totalCount: number;
  pageNumber: number;
  pageSize: number;
  totalPages: number;
} 