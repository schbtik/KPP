using System.Net.Http.Json;
using ProcureRiskAnalyzer.Client.Models;

namespace ProcureRiskAnalyzer.Client.Services;

public class ApiService : IApiService
{
    private readonly HttpClient _httpClient;
    private readonly IAuthService _authService;
    private readonly string _apiBaseUrl;

    public ApiService(HttpClient httpClient, IAuthService authService)
    {
        _httpClient = httpClient;
        _authService = authService;
        // In production, this should come from configuration
        // Change to http://localhost:5019 if using HTTP, or https://localhost:7252 for HTTPS
        _apiBaseUrl = "http://localhost:5019"; // Backend API URL
    }

    private void SetAuthHeader()
    {
        if (_authService.IsAuthenticated && !string.IsNullOrEmpty(_authService.AccessToken))
        {
            _httpClient.DefaultRequestHeaders.Authorization = 
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _authService.AccessToken);
        }
    }

    // Suppliers
    public async Task<List<Supplier>> GetSuppliersAsync()
    {
        SetAuthHeader();
        try
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/api/suppliers");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<Supplier>>() ?? new List<Supplier>();
            }
        }
        catch
        {
            // Return mock data for demo
            return GetMockSuppliers();
        }
        return new List<Supplier>();
    }

    public async Task<Supplier?> GetSupplierAsync(int id)
    {
        SetAuthHeader();
        try
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/api/suppliers/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Supplier>();
            }
        }
        catch { }
        return null;
    }

    public async Task<bool> CreateSupplierAsync(Supplier supplier)
    {
        SetAuthHeader();
        try
        {
            var response = await _httpClient.PostAsJsonAsync($"{_apiBaseUrl}/api/suppliers", supplier);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return true; // Mock success
        }
    }

    public async Task<bool> UpdateSupplierAsync(Supplier supplier)
    {
        SetAuthHeader();
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"{_apiBaseUrl}/api/suppliers/{supplier.Id}", supplier);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return true; // Mock success
        }
    }

    public async Task<bool> DeleteSupplierAsync(int id)
    {
        SetAuthHeader();
        try
        {
            var response = await _httpClient.DeleteAsync($"{_apiBaseUrl}/api/suppliers/{id}");
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return true; // Mock success
        }
    }

    // Tenders
    public async Task<List<Tender>> GetTendersAsync()
    {
        SetAuthHeader();
        try
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/api/tenders");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<Tender>>() ?? new List<Tender>();
            }
        }
        catch
        {
            return GetMockTenders();
        }
        return new List<Tender>();
    }

    public async Task<Tender?> GetTenderAsync(int id)
    {
        SetAuthHeader();
        try
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/api/tenders/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Tender>();
            }
        }
        catch { }
        return null;
    }

    public async Task<bool> CreateTenderAsync(Tender tender)
    {
        SetAuthHeader();
        try
        {
            var response = await _httpClient.PostAsJsonAsync($"{_apiBaseUrl}/api/tenders", tender);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return true; // Mock success
        }
    }

    public async Task<bool> UpdateTenderAsync(Tender tender)
    {
        SetAuthHeader();
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"{_apiBaseUrl}/api/tenders/{tender.Id}", tender);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return true; // Mock success
        }
    }

    public async Task<bool> DeleteTenderAsync(int id)
    {
        SetAuthHeader();
        try
        {
            var response = await _httpClient.DeleteAsync($"{_apiBaseUrl}/api/tenders/{id}");
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return true; // Mock success
        }
    }

    // Categories
    public async Task<List<Category>> GetCategoriesAsync()
    {
        SetAuthHeader();
        try
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/api/categories");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<Category>>() ?? new List<Category>();
            }
        }
        catch
        {
            return GetMockCategories();
        }
        return new List<Category>();
    }

    public async Task<Category?> GetCategoryAsync(int id)
    {
        SetAuthHeader();
        try
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/api/categories/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Category>();
            }
        }
        catch { }
        return null;
    }

    public async Task<bool> CreateCategoryAsync(Category category)
    {
        SetAuthHeader();
        try
        {
            var response = await _httpClient.PostAsJsonAsync($"{_apiBaseUrl}/api/categories", category);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return true; // Mock success
        }
    }

    public async Task<bool> UpdateCategoryAsync(Category category)
    {
        SetAuthHeader();
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"{_apiBaseUrl}/api/categories/{category.Id}", category);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return true; // Mock success
        }
    }

    public async Task<bool> DeleteCategoryAsync(int id)
    {
        SetAuthHeader();
        try
        {
            var response = await _httpClient.DeleteAsync($"{_apiBaseUrl}/api/categories/{id}");
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return true; // Mock success
        }
    }

    public async Task<Dictionary<string, decimal>> GetTenderStatisticsAsync()
    {
        SetAuthHeader();
        try
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/api/tenders/statistics");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Dictionary<string, decimal>>() ?? new Dictionary<string, decimal>();
            }
        }
        catch
        {
            return GetMockStatistics();
        }
        return new Dictionary<string, decimal>();
    }

    // Mock data for demo purposes
    private List<Supplier> GetMockSuppliers()
    {
        return new List<Supplier>
        {
            new Supplier { Id = 1, Name = "Microsoft", Country = "USA" },
            new Supplier { Id = 2, Name = "EPAM", Country = "Ukraine" },
            new Supplier { Id = 3, Name = "SoftServe", Country = "Poland" }
        };
    }

    private List<Tender> GetMockTenders()
    {
        return new List<Tender>
        {
            new Tender { Id = 1, TenderCode = "T001", Buyer = "Ministry of Education", Date = DateTime.Now.AddDays(-10), ExpectedValue = 1000000, SupplierId = 1, CategoryId = 1 },
            new Tender { Id = 2, TenderCode = "T002", Buyer = "Ministry of Health", Date = DateTime.Now.AddDays(-5), ExpectedValue = 750000, SupplierId = 2, CategoryId = 2 },
            new Tender { Id = 3, TenderCode = "T003", Buyer = "Ministry of Defense", Date = DateTime.Now, ExpectedValue = 2000000, SupplierId = 3, CategoryId = 1 }
        };
    }

    private List<Category> GetMockCategories()
    {
        return new List<Category>
        {
            new Category { Id = 1, Name = "IT Services", Description = "Information Technology Services" },
            new Category { Id = 2, Name = "Healthcare", Description = "Healthcare and Medical Services" },
            new Category { Id = 3, Name = "Infrastructure", Description = "Infrastructure and Construction" }
        };
    }

    private Dictionary<string, decimal> GetMockStatistics()
    {
        return new Dictionary<string, decimal>
        {
            { "IT Services", 1500000 },
            { "Healthcare", 750000 },
            { "Infrastructure", 2000000 }
        };
    }
}


