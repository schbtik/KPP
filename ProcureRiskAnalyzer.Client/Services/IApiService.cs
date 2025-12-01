using ProcureRiskAnalyzer.Client.Models;

namespace ProcureRiskAnalyzer.Client.Services;

public interface IApiService
{
    Task<List<Supplier>> GetSuppliersAsync();
    Task<Supplier?> GetSupplierAsync(int id);
    Task<bool> CreateSupplierAsync(Supplier supplier);
    Task<bool> UpdateSupplierAsync(Supplier supplier);
    Task<bool> DeleteSupplierAsync(int id);

    Task<List<Tender>> GetTendersAsync();
    Task<Tender?> GetTenderAsync(int id);
    Task<bool> CreateTenderAsync(Tender tender);
    Task<bool> UpdateTenderAsync(Tender tender);
    Task<bool> DeleteTenderAsync(int id);

    Task<List<Category>> GetCategoriesAsync();
    Task<Category?> GetCategoryAsync(int id);
    Task<bool> CreateCategoryAsync(Category category);
    Task<bool> UpdateCategoryAsync(Category category);
    Task<bool> DeleteCategoryAsync(int id);

    Task<Dictionary<string, decimal>> GetTenderStatisticsAsync();
}


