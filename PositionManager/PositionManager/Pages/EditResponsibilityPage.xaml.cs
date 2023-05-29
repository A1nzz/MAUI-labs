using PositionManager.ViewModels;

namespace PositionManager.Pages;

public partial class EditResponsibilityPage : ContentPage
{

    private EditResponsibilityViewModel _viewModel;

    public EditResponsibilityPage(EditResponsibilityViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
		BindingContext = _viewModel;
	}
}