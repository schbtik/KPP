using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProcureRiskAnalyzer.Client.Models;
using ProcureRiskAnalyzer.Client.Services;

namespace ProcureRiskAnalyzer.Client.ViewModels;

public partial class SuppliersViewModel : BaseViewModel
{
    private readonly IApiService _apiService;

    [ObservableProperty]
    private List<Supplier> suppliers = new();

    [ObservableProperty]
    private Supplier? selectedSupplier;

    [ObservableProperty]
    private string name = string.Empty;

    [ObservableProperty]
    private string country = string.Empty;

    [ObservableProperty]
    private bool isEditing;

    public SuppliersViewModel(IApiService apiService, ILoadingService loadingService) 
        : base(loadingService)
    {
        _apiService = apiService;
    }

    [RelayCommand]
    private async Task LoadSuppliersAsync()
    {
        await ExecuteWithLoadingAsync(async () =>
        {
            Suppliers = await _apiService.GetSuppliersAsync();
        });
    }

    [RelayCommand]
    private async Task SaveSupplierAsync()
    {
        if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Country))
        {
            return;
        }

        await ExecuteWithLoadingAsync(async () =>
        {
            var supplier = new Supplier
            {
                Id = SelectedSupplier?.Id ?? 0,
                Name = Name,
                Country = Country
            };

            bool success;
            if (IsEditing && SelectedSupplier != null)
            {
                supplier.Id = SelectedSupplier.Id;
                success = await _apiService.UpdateSupplierAsync(supplier);
            }
            else
            {
                success = await _apiService.CreateSupplierAsync(supplier);
            }

            if (success)
            {
                await LoadSuppliersAsync();
                ResetForm();
            }
        });
    }

    [RelayCommand]
    private void EditSupplier(Supplier supplier)
    {
        SelectedSupplier = supplier;
        Name = supplier.Name;
        Country = supplier.Country;
        IsEditing = true;
    }

    [RelayCommand]
    private async Task DeleteSupplierAsync(Supplier supplier)
    {
        await ExecuteWithLoadingAsync(async () =>
        {
            var success = await _apiService.DeleteSupplierAsync(supplier.Id);
            if (success)
            {
                await LoadSuppliersAsync();
            }
        });
    }

    [RelayCommand]
    private void ResetForm()
    {
        SelectedSupplier = null;
        Name = string.Empty;
        Country = string.Empty;
        IsEditing = false;
    }

    [RelayCommand]
    private async Task GoBackAsync()
    {
        await Shell.Current.GoToAsync("//main");
    }

    partial void OnSelectedSupplierChanged(Supplier? value)
    {
        if (value != null)
        {
            Name = value.Name;
            Country = value.Country;
        }
    }
}


