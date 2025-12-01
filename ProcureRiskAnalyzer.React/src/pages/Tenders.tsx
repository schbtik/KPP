import { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import { tendersApi, suppliersApi, categoriesApi } from '../services/api';
import type { Tender, Supplier, Category } from '../types';
import './Tenders.css';

function Tenders() {
  const [tenders, setTenders] = useState<Tender[]>([]);
  const [suppliers, setSuppliers] = useState<Supplier[]>([]);
  const [categories, setCategories] = useState<Category[]>([]);
  const [loading, setLoading] = useState(true);
  const [tenderCode, setTenderCode] = useState('');
  const [buyer, setBuyer] = useState('');
  const [date, setDate] = useState('');
  const [expectedValue, setExpectedValue] = useState('');
  const [supplierId, setSupplierId] = useState('');
  const [categoryId, setCategoryId] = useState('');
  const [editingId, setEditingId] = useState<number | null>(null);
  const [error, setError] = useState('');

  useEffect(() => {
    loadData();
  }, []);

  const loadData = async () => {
    try {
      setLoading(true);
      const [tendersData, suppliersData, categoriesData] = await Promise.all([
        tendersApi.getAll(),
        suppliersApi.getAll(),
        categoriesApi.getAll(),
      ]);
      setTenders(tendersData);
      setSuppliers(suppliersData);
      setCategories(categoriesData);
    } catch (err) {
      setError('Failed to load data');
    } finally {
      setLoading(false);
    }
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError('');

    try {
      const tenderData = {
        tenderCode,
        buyer,
        date,
        expectedValue: parseFloat(expectedValue),
        supplierId: parseInt(supplierId),
        categoryId: parseInt(categoryId),
      };

      if (editingId) {
        await tendersApi.update(editingId, tenderData);
      } else {
        await tendersApi.create(tenderData);
      }
      resetForm();
      loadData();
    } catch (err) {
      setError('Failed to save tender');
    }
  };

  const handleEdit = (tender: Tender) => {
    setEditingId(tender.id);
    setTenderCode(tender.tenderCode);
    setBuyer(tender.buyer);
    setDate(tender.date.split('T')[0]);
    setExpectedValue(tender.expectedValue.toString());
    setSupplierId(tender.supplierId.toString());
    setCategoryId(tender.categoryId.toString());
  };

  const handleDelete = async (id: number) => {
    if (window.confirm('Are you sure you want to delete this tender?')) {
      try {
        await tendersApi.delete(id);
        loadData();
      } catch (err) {
        setError('Failed to delete tender');
      }
    }
  };

  const resetForm = () => {
    setEditingId(null);
    setTenderCode('');
    setBuyer('');
    setDate('');
    setExpectedValue('');
    setSupplierId('');
    setCategoryId('');
  };

  if (loading) {
    return <div className="loading">Loading tenders...</div>;
  }

  return (
    <div className="page-container">
      <div className="container">
        <div className="page-header">
          <h1>Tenders</h1>
          <Link to="/" className="btn btn-secondary">Back</Link>
        </div>

        <div className="card">
          <h2>{editingId ? 'Edit Tender' : 'Add New Tender'}</h2>
          <form onSubmit={handleSubmit}>
            <div className="form-row">
              <div className="form-group">
                <label>Tender Code</label>
                <input
                  type="text"
                  value={tenderCode}
                  onChange={(e) => setTenderCode(e.target.value)}
                  required
                />
              </div>
              <div className="form-group">
                <label>Buyer</label>
                <input
                  type="text"
                  value={buyer}
                  onChange={(e) => setBuyer(e.target.value)}
                  required
                />
              </div>
            </div>
            <div className="form-row">
              <div className="form-group">
                <label>Date</label>
                <input
                  type="date"
                  value={date}
                  onChange={(e) => setDate(e.target.value)}
                  required
                />
              </div>
              <div className="form-group">
                <label>Expected Value</label>
                <input
                  type="number"
                  step="0.01"
                  value={expectedValue}
                  onChange={(e) => setExpectedValue(e.target.value)}
                  required
                />
              </div>
            </div>
            <div className="form-row">
              <div className="form-group">
                <label>Supplier</label>
                <select
                  value={supplierId}
                  onChange={(e) => setSupplierId(e.target.value)}
                  required
                >
                  <option value="">Select supplier</option>
                  {suppliers.map((s) => (
                    <option key={s.id} value={s.id}>
                      {s.name}
                    </option>
                  ))}
                </select>
              </div>
              <div className="form-group">
                <label>Category</label>
                <select
                  value={categoryId}
                  onChange={(e) => setCategoryId(e.target.value)}
                  required
                >
                  <option value="">Select category</option>
                  {categories.map((c) => (
                    <option key={c.id} value={c.id}>
                      {c.name}
                    </option>
                  ))}
                </select>
              </div>
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
          <h2>Tenders List</h2>
          <table className="table">
            <thead>
              <tr>
                <th>Code</th>
                <th>Buyer</th>
                <th>Date</th>
                <th>Value</th>
                <th>Supplier</th>
                <th>Category</th>
                <th>Actions</th>
              </tr>
            </thead>
            <tbody>
              {tenders.map((tender) => (
                <tr key={tender.id}>
                  <td>{tender.tenderCode}</td>
                  <td>{tender.buyer}</td>
                  <td>{new Date(tender.date).toLocaleDateString()}</td>
                  <td>{tender.expectedValue.toLocaleString()}</td>
                  <td>{tender.supplier?.name || 'N/A'}</td>
                  <td>{tender.category?.name || 'N/A'}</td>
                  <td>
                    <button
                      onClick={() => handleEdit(tender)}
                      className="btn btn-primary"
                      style={{ marginRight: '10px' }}
                    >
                      Edit
                    </button>
                    <button
                      onClick={() => handleDelete(tender.id)}
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

export default Tenders;

