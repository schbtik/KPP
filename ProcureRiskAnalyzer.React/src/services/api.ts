import axios from 'axios';
import type { Supplier, Category, Tender, LoginRequest, LoginResponse } from '../types';

const API_BASE_URL = 'http://localhost:5019/api';

const api = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
});

// Add token to requests
api.interceptors.request.use((config) => {
  const token = localStorage.getItem('token');
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

export const authApi = {
  login: async (credentials: LoginRequest): Promise<LoginResponse> => {
    const response = await api.post<LoginResponse>('/authapi/login', credentials);
    if (response.data.token) {
      localStorage.setItem('token', response.data.token);
      localStorage.setItem('username', response.data.username);
      localStorage.setItem('email', response.data.email);
    }
    return response.data;
  },
  
  logout: () => {
    localStorage.removeItem('token');
    localStorage.removeItem('username');
    localStorage.removeItem('email');
  },
  
  isAuthenticated: (): boolean => {
    return !!localStorage.getItem('token');
  },
  
  getUsername: (): string | null => {
    return localStorage.getItem('username');
  },
  
  getEmail: (): string | null => {
    return localStorage.getItem('email');
  },
};

export const suppliersApi = {
  getAll: async (): Promise<Supplier[]> => {
    const response = await api.get<Supplier[]>('/suppliers');
    return response.data;
  },
  
  getById: async (id: number): Promise<Supplier> => {
    const response = await api.get<Supplier>(`/suppliers/${id}`);
    return response.data;
  },
  
  create: async (supplier: Omit<Supplier, 'id'>): Promise<Supplier> => {
    const response = await api.post<Supplier>('/suppliers', supplier);
    return response.data;
  },
  
  update: async (id: number, supplier: Omit<Supplier, 'id'>): Promise<Supplier> => {
    const response = await api.put<Supplier>(`/suppliers/${id}`, supplier);
    return response.data;
  },
  
  delete: async (id: number): Promise<void> => {
    await api.delete(`/suppliers/${id}`);
  },
  
  getStatistics: async (): Promise<Record<string, number>> => {
    const response = await api.get<Record<string, number>>('/suppliers/statistics');
    return response.data;
  },
};

export const categoriesApi = {
  getAll: async (): Promise<Category[]> => {
    const response = await api.get<Category[]>('/categories');
    return response.data;
  },
  
  getById: async (id: number): Promise<Category> => {
    const response = await api.get<Category>(`/categories/${id}`);
    return response.data;
  },
  
  create: async (category: Omit<Category, 'id'>): Promise<Category> => {
    const response = await api.post<Category>('/categories', category);
    return response.data;
  },
  
  update: async (id: number, category: Omit<Category, 'id'>): Promise<Category> => {
    const response = await api.put<Category>(`/categories/${id}`, category);
    return response.data;
  },
  
  delete: async (id: number): Promise<void> => {
    await api.delete(`/categories/${id}`);
  },
};

export const tendersApi = {
  getAll: async (): Promise<Tender[]> => {
    const response = await api.get<Tender[]>('/tenders');
    return response.data;
  },
  
  getById: async (id: number): Promise<Tender> => {
    const response = await api.get<Tender>(`/tenders/${id}`);
    return response.data;
  },
  
  create: async (tender: Omit<Tender, 'id'>): Promise<Tender> => {
    const response = await api.post<Tender>('/tenders', tender);
    return response.data;
  },
  
  update: async (id: number, tender: Omit<Tender, 'id'>): Promise<Tender> => {
    const response = await api.put<Tender>(`/tenders/${id}`, tender);
    return response.data;
  },
  
  delete: async (id: number): Promise<void> => {
    await api.delete(`/tenders/${id}`);
  },
  
  getStatistics: async (): Promise<Record<string, number>> => {
    const response = await api.get<Record<string, number>>('/tenders/statistics');
    return response.data;
  },
};

