using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PositionManager.Application.Abstractions;
using PositionManager.Domain.Entities;

namespace PositionManager.ViewModels
{
    [QueryProperty(nameof(Position), nameof(Position))]
    public partial class AddResponsibilityViewModel : ObservableObject
    {
        private IResponsibilityService _respService;

        public AddResponsibilityViewModel(IResponsibilityService respService)
        {
            _respService = respService;
        }

        [ObservableProperty]
        EmployeePosition _position;

        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        private string _description;

        [ObservableProperty]
        private string _importance;


        [RelayCommand]
        async void AddResponsibility() => await AddNewResponsibility();

        public async Task AddNewResponsibility()
        {
            int importance;
            if (Position != null && int.TryParse(Importance, out importance))
            {
                Responsibility resp = new Responsibility(Name, Description, importance, Position.Id);

                await _respService.AddAsync(resp);
                await _respService.SaveChangesAsync();

                MessagingCenter.Send(this, "update");
                await Shell.Current.GoToAsync("..");
            } else
            {
                await App.Current.MainPage.DisplayAlert("Responsibility", "Введите число", "Ок");
            }
        }

    }
}
