using PositionManager.ViewModels;

namespace PositionManager.Pages;

public partial class AddResponsibilityPage : ContentPage
{

    private AddResponsibilityViewModel _vm;

    public AddResponsibilityPage(AddResponsibilityViewModel vm)
	{
		InitializeComponent();
		_vm = vm;
		BindingContext = _vm;
	}
}