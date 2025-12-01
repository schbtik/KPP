import { Routes, Route, Navigate } from 'react-router-dom';
import { authApi } from './services/api';
import Login from './pages/Login';
import Main from './pages/Main';
import Suppliers from './pages/Suppliers';
import Tenders from './pages/Tenders';
import Categories from './pages/Categories';
import Statistics from './pages/Statistics';
import Profile from './pages/Profile';
import About from './pages/About';
import './App.css';

function PrivateRoute({ children }: { children: React.ReactNode }) {
  return authApi.isAuthenticated() ? <>{children}</> : <Navigate to="/login" />;
}

function App() {
  return (
    <div className="app">
      <Routes>
        <Route path="/login" element={<Login />} />
        <Route
          path="/"
          element={
            <PrivateRoute>
              <Main />
            </PrivateRoute>
          }
        />
        <Route
          path="/suppliers"
          element={
            <PrivateRoute>
              <Suppliers />
            </PrivateRoute>
          }
        />
        <Route
          path="/tenders"
          element={
            <PrivateRoute>
              <Tenders />
            </PrivateRoute>
          }
        />
        <Route
          path="/categories"
          element={
            <PrivateRoute>
              <Categories />
            </PrivateRoute>
          }
        />
        <Route
          path="/statistics"
          element={
            <PrivateRoute>
              <Statistics />
            </PrivateRoute>
          }
        />
        <Route
          path="/profile"
          element={
            <PrivateRoute>
              <Profile />
            </PrivateRoute>
          }
        />
        <Route
          path="/about"
          element={
            <PrivateRoute>
              <About />
            </PrivateRoute>
          }
        />
      </Routes>
    </div>
  );
}

export default App;

