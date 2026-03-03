using BertScout2026.Database;

namespace BertScout2026;

public partial class AirtablePage : ContentPage
{
    private readonly MatchDatabase db = new();

    public AirtablePage()
    {
        InitializeComponent();
    }

    private async void AirtableSend_Clicked(object? sender, EventArgs e)
    {
        var matches = await db.GetMatchItemsAsync();
    }
}