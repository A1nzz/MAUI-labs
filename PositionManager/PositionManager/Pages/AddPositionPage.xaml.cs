using PositionManager.ViewModels;

namespace PositionManager.Pages;

public partial class AddPositionPage : ContentPage
{
	private AddPositionViewModel _vm;
	public AddPositionPage(AddPositionViewModel vm)
	{	
		InitializeComponent();
        _vm = vm;
        BindingContext = _vm;
    }
}