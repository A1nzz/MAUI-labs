using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PositionManager.Application.Abstractions;
using PositionManager.Application.Services;
using PositionManager.Domain.Entities;
using System.Collections.ObjectModel;


namespace PositionManager.ViewModels
{
    [QueryProperty(nameof(Resp), nameof(Resp))]
    public partial class EditResponsibilityViewModel : ObservableObject
    {
        IResponsibilityService _respService;
        IEmployeePositionService _posService;

        public EditResponsibilityViewModel(IResponsibilityService respService, IEmployeePositionService posService)
        {
            this._respService = respService;
            this._posService = posService;
        }

        public ObservableCollection<EmployeePosition> AllPoses { get; set; } = new();

        [ObservableProperty]
        Responsibility resp;

        [ObservableProperty]
        EmployeePosition curPos;


        [RelayCommand]
        public async void EditResponsibility() => await Edit();

        [RelayCommand]
        public async void UpdatePostitionsList() => await GetAllPositions();

        [RelayCommand]
        async void ChooseImage() => await LoadImage();

        public async Task Edit()
        {
            if (CurPos != null)
            {
                Resp.EmployeePositionId = CurPos.Id;
            }
            await _respService.UpdateAsync(Resp);
            await _respService.SaveChangesAsync();
            MessagingCenter.Send(this, "update");
            await Shell.Current.GoToAsync("../..");
        }

        public async Task GetAllPositions()
        {
            var poses = await _posService.GetAllAsync();
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                AllPoses.Clear();
                foreach (var pos in poses)
                {
                    AllPoses.Add(pos);
                }
            });
        }

        public async Task LoadImage()
        {
            var options = new PickOptions
            {
                PickerTitle = "Pick Image Please",
                FileTypes = FilePickerFileType.Images
            };

            var result = await FilePicker.Default.PickAsync(options);
            if (result != null && Resp != null)
            {
                // Generate FileName
                string targetFileName = Resp.Id.ToString();
                int dotIndex = result.FileName.LastIndexOf('.');
                string fileFormat = result.FileName.Substring(dotIndex);
                targetFileName += fileFormat;

                // Generate path
                string targetFile = Path.Combine(FileSystem.Current.AppDataDirectory, targetFileName);

                // Copy image to appdata
                using var stream = await result.OpenReadAsync();
                using FileStream outputStream = File.OpenWrite(targetFile);
                await stream.CopyToAsync(outputStream);
            }

            MessagingCenter.Send(this, "update");

            // await Shell.Current.GoToAsync("../..");
        }

    }
}
