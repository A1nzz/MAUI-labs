using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using PositionManager.Domain.Entities;
using PositionManager.Pages;

namespace PositionManager.ViewModels
{
    [QueryProperty(nameof(Responsibility), nameof(Responsibility))]
    public partial class ResponsibilityDetailsViewModel : ObservableObject
    {

        [ObservableProperty]
        Responsibility responsibility;

        [RelayCommand]
        async void Edit() => await GotoEditObjectPage();

        public async Task GotoEditObjectPage()
        {
            IDictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "Resp",  Responsibility}
            };
            await Shell.Current.GoToAsync(nameof(EditResponsibilityPage), parameters);
        }
    }
}
