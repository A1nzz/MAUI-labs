using Calculator.Pages.SQLiteDemo.Services;
using Calculator.Pages.SQLiteDemo.Entities;
namespace Calculator;

public partial class SQLiteDemoPage : ContentPage
{
    private IDbService DbService { get; init; }
    
    public List<EmployeePosition> PositionList { set; get; }

    public List<Responsibilities> ResponsibilitiesList { set; get; }

    public SQLiteDemoPage(IDbService dbService) 
	{
        DbService = dbService;
		DbService.Init();
        BindingContext = this;
        InitializeComponent();
    }

    public void OnLoaded(object sender, EventArgs e)
    {
        PositionList = DbService.GetAllPositions().ToList();
        picker.ItemsSource = PositionList;
    }

    void OnPickerSelectedIndexChanged(object sender, EventArgs e)
    {

        int selectedIndex = picker.SelectedIndex;

        if (selectedIndex != -1)
        {
            ResponsibilitiesList = (DbService.GetPositionResponsibility((picker.SelectedItem as EmployeePosition).Id)).ToList();
            listView.ItemsSource = ResponsibilitiesList;
        }
    }
}