namespace BertScout2026;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}

    private void SaveButton_Clicked(object sender, EventArgs e)
    {
        Global.ScouterName = ScouterName.Text;
    }

    private void ClearButton_Clicked(object sender, EventArgs e)
    {
        ScouterName.Text = "";
        Global.ScouterName = "";
    }
}