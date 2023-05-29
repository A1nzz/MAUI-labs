using CommunityToolkit.Mvvm.ComponentModel;
using PositionManager.Application.Abstractions;
using System.Collections.ObjectModel;
using PositionManager.Domain.Entities;
using CommunityToolkit.Mvvm.Input;
using PositionManager.Pages;



namespace PositionManager.ViewModels
{
    public partial class PositionsViewModel : ObservableObject
    {
        private readonly IEmployeePositionService _posService;
        private readonly IResponsibilityService _responsibilityService;
        public PositionsViewModel(IEmployeePositionService posService, IResponsibilityService responsibilityService)
        {
            _posService = posService;
            _responsibilityService = responsibilityService;
            MessagingCenter.Subscribe<AddPositionViewModel>(this, "update", (sender) => UpdateGroupList());
            MessagingCenter.Subscribe<AddResponsibilityViewModel>(this, "update", (sender) => UpdateMembersList());
            MessagingCenter.Subscribe<EditResponsibilityViewModel>(this, "update", (sender) => UpdateMembersList());
        }

        public ObservableCollection<EmployeePosition> Positions { get; set; } = new();
        public ObservableCollection<Responsibility> Responsibilities { get; set; } = new();

        [ObservableProperty]
        EmployeePosition _selectedPosition;


        [RelayCommand]
        async void UpdateGroupList() => await GetPositions();
        [RelayCommand]
        async void UpdateMembersList() => await GetResponsibilities();

        

        public async Task GetPositions()
        {
            var poses = await _posService.GetAllAsync();
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                Positions.Clear();
                foreach (var pos in poses)
                    Positions.Add(pos);
            });
        }

        public async Task GetResponsibilities()
        {
            var resps = await _responsibilityService.ListAsync(posResp => posResp.EmployeePositionId == _selectedPosition.Id);
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                Responsibilities.Clear();
                foreach (var resp in resps)
                    Responsibilities.Add(resp);
            });
        }

        [RelayCommand]
        async void ShowDetails(Responsibility responsibility) => await GotoDetailsPage(responsibility);
        private async Task GotoDetailsPage(Responsibility responsibility)
        {
            IDictionary<string, object> parameters = new Dictionary<string, object>()
            {
                {"Responsibility", responsibility}
            };
            await Shell.Current.GoToAsync(nameof(ResponsibilityDetails), parameters);
        }

        [RelayCommand] async Task AddPosition() => await GoToAddPositionPage();
        [RelayCommand] async Task AddResponsibility() => await GoToAddResponsibilityPage();
        async Task GoToAddPositionPage()
        {
            await Shell.Current.GoToAsync(nameof(AddPositionPage));
        }

        async Task GoToAddResponsibilityPage()
        {
            if (_selectedPosition != null)
            {
                IDictionary<string, object> parameters = new Dictionary<string, object>()
                {
                    {"Position", _selectedPosition}
                };
                await Shell.Current.GoToAsync(nameof(AddResponsibilityPage), parameters);
            }
               
            
        }
    }
}

