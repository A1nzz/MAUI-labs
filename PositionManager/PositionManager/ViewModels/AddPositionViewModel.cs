using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PositionManager.Application.Abstractions;
using PositionManager.Domain.Entities;
using PositionManager.Domain.Abstractions;
using PositionManager.Persisctence.Repository;
using PositionManager.Pages;


namespace PositionManager.ViewModels
{
    public partial class AddPositionViewModel : ObservableObject
    {
        public AddPositionViewModel(IEmployeePositionService posService)
        {
            _posService = posService;
        }

        [RelayCommand] async Task AddPos() => await Add();
        [ObservableProperty] private string _name;
        [ObservableProperty] private string _salary;
        private readonly IEmployeePositionService _posService;

        private async Task Add()
        {
            if (!int.TryParse(Salary, out int salary))
            {
                await App.Current.MainPage.DisplayAlert("Зарплата", "Введите число", "Ок");
            }
            else
            {
                var pos = new EmployeePosition(Name, salary);
                await _posService.AddAsync(pos);
                await _posService.SaveChangesAsync();
                MessagingCenter.Send(this, "update");
                await Shell.Current.GoToAsync("..");
            }
        }
    }
}
