import { Link } from 'react-router-dom';
import { authApi } from '../services/api';
import './Profile.css';

function Profile() {
  const username = authApi.getUsername();
  const email = authApi.getEmail();
  const isAuthenticated = authApi.isAuthenticated();

  const handleLogout = () => {
    authApi.logout();
    window.location.href = '/login';
  };

  return (
    <div className="page-container">
      <div className="container">
        <div className="page-header">
          <h1>User Profile</h1>
          <Link to="/" className="btn btn-secondary">Back</Link>
        </div>

        <div className="card">
          <h2>Profile Information</h2>
          <div className="profile-info">
            <div className="info-item">
              <label>Authentication Status:</label>
              <span className={isAuthenticated ? 'success' : 'error'}>
                {isAuthenticated ? '✓ Authenticated' : '✗ Not Authenticated'}
              </span>
            </div>
            <div className="info-item">
              <label>Username:</label>
              <span>{username || 'Not available'}</span>
            </div>
            <div className="info-item">
              <label>Email:</label>
              <span>{email || 'Not available'}</span>
            </div>
          </div>
          <div className="profile-actions">
            <button onClick={handleLogout} className="btn btn-danger">
              Logout
            </button>
          </div>
        </div>
      </div>
    </div>
  );
}

export default Profile;

