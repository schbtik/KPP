import { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import { BarChart, Bar, XAxis, YAxis, CartesianGrid, Tooltip, Legend, ResponsiveContainer, PieChart, Pie, Cell } from 'recharts';
import { tendersApi, suppliersApi } from '../services/api';
import './Statistics.css';

function Statistics() {
  const [tenderStatistics, setTenderStatistics] = useState<Record<string, number>>({});
  const [supplierStatistics, setSupplierStatistics] = useState<Record<string, number>>({});
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    loadStatistics();
  }, []);

  const loadStatistics = async () => {
    try {
      setLoading(true);
      const [tenderData, supplierData] = await Promise.all([
        tendersApi.getStatistics(),
        suppliersApi.getStatistics(),
      ]);
      setTenderStatistics(tenderData);
      setSupplierStatistics(supplierData);
    } catch (err) {
      console.error('Failed to load statistics', err);
    } finally {
      setLoading(false);
    }
  };

  const tenderChartData = Object.entries(tenderStatistics).map(([name, value]) => ({
    name,
    value,
  }));

  const supplierChartData = Object.entries(supplierStatistics).map(([name, value]) => ({
    name,
    value,
  }));

  const COLORS = ['#512BD4', '#0088FE', '#00C49F', '#FFBB28', '#FF8042', '#8884d8', '#82ca9d'];

  if (loading) {
    return <div className="loading">Loading statistics...</div>;
  }

  return (
    <div className="page-container">
      <div className="container">
        <div className="page-header">
          <h1>Statistics</h1>
          <Link to="/" className="btn btn-secondary">Back</Link>
        </div>

        <div className="card">
          <h2>Tender Statistics by Category (Total Value)</h2>
          {tenderChartData.length > 0 ? (
            <ResponsiveContainer width="100%" height={400}>
              <BarChart data={tenderChartData}>
                <CartesianGrid strokeDasharray="3 3" />
                <XAxis dataKey="name" />
                <YAxis />
                <Tooltip formatter={(value: number) => value.toLocaleString()} />
                <Legend />
                <Bar dataKey="value" fill="#512BD4" name="Total Value" />
              </BarChart>
            </ResponsiveContainer>
          ) : (
            <p>No statistics available</p>
          )}
        </div>

        <div className="card">
          <h2>Suppliers by Country</h2>
          {supplierChartData.length > 0 ? (
            <ResponsiveContainer width="100%" height={400}>
              <PieChart>
                <Pie
                  data={supplierChartData}
                  cx="50%"
                  cy="50%"
                  labelLine={false}
                  label={({ name, percent }) => `${name}: ${(percent * 100).toFixed(0)}%`}
                  outerRadius={120}
                  fill="#8884d8"
                  dataKey="value"
                >
                  {supplierChartData.map((entry, index) => (
                    <Cell key={`cell-${index}`} fill={COLORS[index % COLORS.length]} />
                  ))}
                </Pie>
                <Tooltip />
                <Legend />
              </PieChart>
            </ResponsiveContainer>
          ) : (
            <p>No statistics available</p>
          )}
        </div>

        <div className="card">
          <h2>Statistics Table</h2>
          <table className="table">
            <thead>
              <tr>
                <th>Category</th>
                <th>Total Value</th>
              </tr>
            </thead>
            <tbody>
              {tenderChartData.map((item) => (
                <tr key={item.name}>
                  <td>{item.name}</td>
                  <td>{item.value.toLocaleString()}</td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </div>
    </div>
  );
}

export default Statistics;

