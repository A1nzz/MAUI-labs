using PositionManager.Pages;
namespace PositionManager;

public partial class AppShell : Shell
{
	public AppShell()
	{
        Routing.RegisterRoute(nameof(ResponsibilityDetails), typeof(ResponsibilityDetails));
        Routing.RegisterRoute(nameof(AddResponsibilityPage), typeof(AddResponsibilityPage));
        Routing.RegisterRoute(nameof(AddPositionPage), typeof(AddPositionPage));
        Routing.RegisterRoute(nameof(EditResponsibilityPage), typeof(EditResponsibilityPage));

        InitializeComponent();
	}
}
