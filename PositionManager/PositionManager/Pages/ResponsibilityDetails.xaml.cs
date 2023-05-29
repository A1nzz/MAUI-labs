using PositionManager.ViewModels;

namespace PositionManager.Pages;

public partial class ResponsibilityDetails : ContentPage
{
	private ResponsibilityDetailsViewModel _detailsViewModel;
	public ResponsibilityDetails(ResponsibilityDetailsViewModel vm)
	{
		InitializeComponent();
        _detailsViewModel = vm;
		BindingContext = _detailsViewModel;
    }
}