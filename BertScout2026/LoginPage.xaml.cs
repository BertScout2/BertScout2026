using BertScout2026.Models;

namespace BertScout2026;

public partial class LoginPage : ContentPage
{
    private IGlobalModel _global;

    public LoginPage(IGlobalModel global)
    {
        InitializeComponent();
        _global = global;
        ScouterName.Text = _global.ScouterName;
    }

    private void SaveButton_Clicked(object? sender, EventArgs e)
    {
        _global.ScouterName = ScouterName.Text;
        //Task.Run(() => Shell.Current.GoToAsync(nameof(MainPage)));
    }

    private void ClearButton_Clicked(object? sender, EventArgs e)
    {
        ScouterName.Text = "";
        _global.ScouterName = "";
    }
}