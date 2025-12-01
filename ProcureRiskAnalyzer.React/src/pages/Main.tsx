import { Link } from 'react-router-dom';
import { authApi } from '../services/api';
import './Main.css';

function Main() {
  const username = authApi.getUsername();

  return (
    <div className="main-container">
      <div className="container">
        <div className="header">
          <h1>Procure Risk Analyzer</h1>
          {username && (
            <div className="user-info">
              <span>Welcome, <strong>{username}</strong>!</span>
              <Link to="/profile" className="btn btn-secondary">Profile</Link>
            </div>
          )}
        </div>

        <div className="navigation-grid">
          <Link to="/suppliers" className="nav-card">
            <h2>Suppliers</h2>
            <p>Manage suppliers</p>
          </Link>
          <Link to="/tenders" className="nav-card">
            <h2>Tenders</h2>
            <p>Manage tenders</p>
          </Link>
          <Link to="/categories" className="nav-card">
            <h2>Categories</h2>
            <p>Manage categories</p>
          </Link>
          <Link to="/statistics" className="nav-card">
            <h2>Statistics</h2>
            <p>View statistics and charts</p>
          </Link>
          <Link to="/about" className="nav-card">
            <h2>About</h2>
            <p>Application information</p>
          </Link>
        </div>

        <div className="actions">
          <button
            onClick={() => {
              authApi.logout();
              window.location.href = '/login';
            }}
            className="btn btn-secondary"
          >
            Logout
          </button>
        </div>
      </div>
    </div>
  );
}

export default Main;

