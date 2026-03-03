namespace BertScout2026;

public partial class LoginPage : ContentPage
{
    private GlobalViewModel _global;

    public LoginPage(GlobalViewModel global)
    {
        InitializeComponent();
        _global = global;
        BindingContext = _global;
    }

    private void SaveButton_Clicked(object? sender, EventArgs e)
    {
        _global.ScouterName = ScouterName.Text;
        Message.Text = "Logged In!";
        Routing.RegisterRoute("MainPage", typeof(MainPage));
        Shell.Current.GoToAsync("MainPage");
    }

    private void ClearButton_Clicked(object? sender, EventArgs e)
    {
        ScouterName.Text = "";
        _global.ScouterName = "";
        Message.Text = "Logged Out";
    }
}