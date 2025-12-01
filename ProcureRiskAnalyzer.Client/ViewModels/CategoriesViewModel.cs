using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProcureRiskAnalyzer.Client.Models;
using ProcureRiskAnalyzer.Client.Services;

namespace ProcureRiskAnalyzer.Client.ViewModels;

public partial class CategoriesViewModel : BaseViewModel
{
    private readonly IApiService _apiService;

    [ObservableProperty]
    private List<Category> categories = new();

    [ObservableProperty]
    private Category? selectedCategory;

    [ObservableProperty]
    private string name = string.Empty;

    [ObservableProperty]
    private string description = string.Empty;

    [ObservableProperty]
    private bool isEditing;

    public CategoriesViewModel(IApiService apiService, ILoadingService loadingService) 
        : base(loadingService)
    {
        _apiService = apiService;
    }

    [RelayCommand]
    private async Task LoadCategoriesAsync()
    {
        await ExecuteWithLoadingAsync(async () =>
        {
            Categories = await _apiService.GetCategoriesAsync();
        });
    }

    [RelayCommand]
    private async Task SaveCategoryAsync()
    {
        if (string.IsNullOrWhiteSpace(Name))
        {
            return;
        }

        await ExecuteWithLoadingAsync(async () =>
        {
            var category = new Category
            {
                Id = SelectedCategory?.Id ?? 0,
                Name = Name,
                Description = Description
            };

            bool success;
            if (IsEditing && SelectedCategory != null)
            {
                category.Id = SelectedCategory.Id;
                success = await _apiService.UpdateCategoryAsync(category);
            }
            else
            {
                success = await _apiService.CreateCategoryAsync(category);
            }

            if (success)
            {
                await LoadCategoriesAsync();
                ResetForm();
            }
        });
    }

    [RelayCommand]
    private void EditCategory(Category category)
    {
        SelectedCategory = category;
        Name = category.Name;
        Description = category.Description;
        IsEditing = true;
    }

    [RelayCommand]
    private async Task DeleteCategoryAsync(Category category)
    {
        await ExecuteWithLoadingAsync(async () =>
        {
            var success = await _apiService.DeleteCategoryAsync(category.Id);
            if (success)
            {
                await LoadCategoriesAsync();
            }
        });
    }

    [RelayCommand]
    private void ResetForm()
    {
        SelectedCategory = null;
        Name = string.Empty;
        Description = string.Empty;
        IsEditing = false;
    }

    [RelayCommand]
    private async Task GoBackAsync()
    {
        await Shell.Current.GoToAsync("//main");
    }

    partial void OnSelectedCategoryChanged(Category? value)
    {
        if (value != null)
        {
            Name = value.Name;
            Description = value.Description;
        }
    }
}


