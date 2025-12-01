export interface Supplier {
  id: number;
  name: string;
  country: string;
}

export interface Category {
  id: number;
  name: string;
  description: string;
}

export interface Tender {
  id: number;
  tenderCode: string;
  buyer: string;
  date: string;
  expectedValue: number;
  supplierId: number;
  categoryId: number;
  supplier?: Supplier;
  category?: Category;
}

export interface LoginRequest {
  username: string;
  password: string;
}

export interface LoginResponse {
  token: string;
  username: string;
  email: string;
  expiresAt: string;
}

export interface User {
  username: string;
  email: string;
  isAuthenticated: boolean;
}

