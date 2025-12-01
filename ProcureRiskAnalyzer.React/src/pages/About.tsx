import { Link } from 'react-router-dom';
import './About.css';

function About() {
  return (
    <div className="page-container">
      <div className="container">
        <div className="page-header">
          <h1>About</h1>
          <Link to="/" className="btn btn-secondary">Back</Link>
        </div>

        <div className="card">
          <h2>Procure Risk Analyzer</h2>
          <div className="about-content">
            <div className="about-item">
              <h3>Version</h3>
              <p>1.0.0</p>
            </div>
            <div className="about-item">
              <h3>Description</h3>
              <p>
                A cross-platform application for analyzing procurement risks. 
                Built with React.js, .NET MAUI, using MVVM pattern and Okta authentication.
              </p>
            </div>
            <div className="about-item">
              <h3>Technologies</h3>
              <p>React.js, .NET MAUI, .NET 9, Entity Framework Core, SQLite, Okta, MVVM Pattern</p>
            </div>
            <div className="about-item">
              <h3>Author</h3>
              <p>Development Team</p>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

export default About;

