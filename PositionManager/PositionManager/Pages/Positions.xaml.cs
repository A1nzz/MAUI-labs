using PositionManager.ViewModels;

namespace PositionManager.Pages;

public partial class Positions : ContentPage
{
    private PositionsViewModel _viewModel;
    public Positions(PositionsViewModel positionsViewModel)
	{
      
		InitializeComponent();
        _viewModel = positionsViewModel;
        BindingContext = _viewModel;
    }
}