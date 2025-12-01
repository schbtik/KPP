import { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import { suppliersApi } from '../services/api';
import type { Supplier } from '../types';
import './Suppliers.css';

function Suppliers() {
  const [suppliers, setSuppliers] = useState<Supplier[]>([]);
  const [loading, setLoading] = useState(true);
  const [name, setName] = useState('');
  const [country, setCountry] = useState('');
  const [editingId, setEditingId] = useState<number | null>(null);
  const [error, setError] = useState('');

  useEffect(() => {
    loadSuppliers();
  }, []);

  const loadSuppliers = async () => {
    try {
      setLoading(true);
      const data = await suppliersApi.getAll();
      setSuppliers(data);
    } catch (err) {
      setError('Failed to load suppliers');
    } finally {
      setLoading(false);
    }
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError('');

    try {
      if (editingId) {
        await suppliersApi.update(editingId, { name, country });
      } else {
        await suppliersApi.create({ name, country });
      }
      resetForm();
      loadSuppliers();
    } catch (err) {
      setError('Failed to save supplier');
    }
  };

  const handleEdit = (supplier: Supplier) => {
    setEditingId(supplier.id);
    setName(supplier.name);
    setCountry(supplier.country);
  };

  const handleDelete = async (id: number) => {
    if (window.confirm('Are you sure you want to delete this supplier?')) {
      try {
        await suppliersApi.delete(id);
        loadSuppliers();
      } catch (err) {
        setError('Failed to delete supplier');
      }
    }
  };

  const resetForm = () => {
    setEditingId(null);
    setName('');
    setCountry('');
  };

  if (loading) {
    return <div className="loading">Loading suppliers...</div>;
  }

  return (
    <div className="page-container">
      <div className="container">
        <div className="page-header">
          <h1>Suppliers</h1>
          <Link to="/" className="btn btn-secondary">Back</Link>
        </div>

        <div className="card">
          <h2>{editingId ? 'Edit Supplier' : 'Add New Supplier'}</h2>
          <form onSubmit={handleSubmit}>
            <div className="form-group">
              <label>Name</label>
              <input
                type="text"
                value={name}
                onChange={(e) => setName(e.target.value)}
                required
              />
            </div>
            <div className="form-group">
              <label>Country</label>
              <input
                type="text"
                value={country}
                onChange={(e) => setCountry(e.target.value)}
                required
              />
            </div>
            {error && <div className="error">{error}</div>}
            <div className="form-actions">
              <button type="submit" className="btn btn-success">
                {editingId ? 'Update' : 'Create'}
              </button>
              {editingId && (
                <button type="button" onClick={resetForm} className="btn btn-secondary">
                  Cancel
                </button>
              )}
            </div>
          </form>
        </div>

        <div className="card">
          <h2>Suppliers List</h2>
          <table className="table">
            <thead>
              <tr>
                <th>ID</th>
                <th>Name</th>
                <th>Country</th>
                <th>Actions</th>
              </tr>
            </thead>
            <tbody>
              {suppliers.map((supplier) => (
                <tr key={supplier.id}>
                  <td>{supplier.id}</td>
                  <td>{supplier.name}</td>
                  <td>{supplier.country}</td>
                  <td>
                    <button
                      onClick={() => handleEdit(supplier)}
                      className="btn btn-primary"
                      style={{ marginRight: '10px' }}
                    >
                      Edit
                    </button>
                    <button
                      onClick={() => handleDelete(supplier.id)}
                      className="btn btn-danger"
                    >
                      Delete
                    </button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </div>
    </div>
  );
}

export default Suppliers;

