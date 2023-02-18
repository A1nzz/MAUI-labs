using CommunityToolkit.Maui.Views;

namespace MauiToolkitPopupSample;

public partial class PopupNull : Popup
{
    private double memory;
    public PopupNull()
	{
		InitializeComponent();
        this.LabelName.Text = "Cannot be divided by zero";

    }

    public PopupNull(double mem)
	{
        InitializeComponent();
        memory = mem;
        this.LabelName.Text = "Memory value: " + memory.ToString();
    }

    public void OnClose(object sender, EventArgs e)
	{
		Close();
	}
}