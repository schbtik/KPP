using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProcureRiskAnalyzer.Client.Models;
using ProcureRiskAnalyzer.Client.Services;

namespace ProcureRiskAnalyzer.Client.ViewModels;

public partial class TendersViewModel : BaseViewModel
{
    private readonly IApiService _apiService;

    [ObservableProperty]
    private List<Tender> tenders = new();

    [ObservableProperty]
    private List<Supplier> suppliers = new();

    [ObservableProperty]
    private List<Category> categories = new();

    [ObservableProperty]
    private Tender? selectedTender;

    [ObservableProperty]
    private string tenderCode = string.Empty;

    [ObservableProperty]
    private string buyer = string.Empty;

    [ObservableProperty]
    private DateTime date = DateTime.Now;

    [ObservableProperty]
    private decimal expectedValue;

    [ObservableProperty]
    private Supplier? selectedSupplier;

    [ObservableProperty]
    private Category? selectedCategory;

    [ObservableProperty]
    private bool isEditing;

    public TendersViewModel(IApiService apiService, ILoadingService loadingService) 
        : base(loadingService)
    {
        _apiService = apiService;
    }

    [RelayCommand]
    private async Task LoadDataAsync()
    {
        await ExecuteWithLoadingAsync(async () =>
        {
            Tenders = await _apiService.GetTendersAsync();
            Suppliers = await _apiService.GetSuppliersAsync();
            Categories = await _apiService.GetCategoriesAsync();
        });
    }

    [RelayCommand]
    private async Task SaveTenderAsync()
    {
        if (string.IsNullOrWhiteSpace(TenderCode) || string.IsNullOrWhiteSpace(Buyer))
        {
            return;
        }

        await ExecuteWithLoadingAsync(async () =>
        {
            var tender = new Tender
            {
                Id = SelectedTender?.Id ?? 0,
                TenderCode = TenderCode,
                Buyer = Buyer,
                Date = Date,
                ExpectedValue = ExpectedValue,
                SupplierId = SelectedSupplier?.Id ?? 0,
                CategoryId = SelectedCategory?.Id ?? 0
            };

            bool success;
            if (IsEditing && SelectedTender != null)
            {
                tender.Id = SelectedTender.Id;
                success = await _apiService.UpdateTenderAsync(tender);
            }
            else
            {
                success = await _apiService.CreateTenderAsync(tender);
            }

            if (success)
            {
                await LoadDataAsync();
                ResetForm();
            }
        });
    }

    [RelayCommand]
    private void EditTender(Tender tender)
    {
        SelectedTender = tender;
        TenderCode = tender.TenderCode;
        Buyer = tender.Buyer;
        Date = tender.Date;
        ExpectedValue = tender.ExpectedValue;
        SelectedSupplier = Suppliers.FirstOrDefault(s => s.Id == tender.SupplierId);
        SelectedCategory = Categories.FirstOrDefault(c => c.Id == tender.CategoryId);
        IsEditing = true;
    }

    [RelayCommand]
    private async Task DeleteTenderAsync(Tender tender)
    {
        await ExecuteWithLoadingAsync(async () =>
        {
            var success = await _apiService.DeleteTenderAsync(tender.Id);
            if (success)
            {
                await LoadDataAsync();
            }
        });
    }

    [RelayCommand]
    private void ResetForm()
    {
        SelectedTender = null;
        TenderCode = string.Empty;
        Buyer = string.Empty;
        Date = DateTime.Now;
        ExpectedValue = 0;
        SelectedSupplier = null;
        SelectedCategory = null;
        IsEditing = false;
    }

    [RelayCommand]
    private async Task GoBackAsync()
    {
        await Shell.Current.GoToAsync("//main");
    }

    partial void OnSelectedTenderChanged(Tender? value)
    {
        if (value != null)
        {
            TenderCode = value.TenderCode;
            Buyer = value.Buyer;
            Date = value.Date;
            ExpectedValue = value.ExpectedValue;
            SelectedSupplier = Suppliers.FirstOrDefault(s => s.Id == value.SupplierId);
            SelectedCategory = Categories.FirstOrDefault(c => c.Id == value.CategoryId);
        }
    }
}

