using BertScout2026.Database;
using BertScout2026.Models;

namespace BertScout2026
{
    public partial class MainPage : ContentPage
    {
        private readonly MatchDatabase db = new();

        Match match = new();

        public MainPage()
        {
            InitializeComponent();
        }

        private void AutoNumberOfCyclesPlusClicked(object? sender, EventArgs e)
        {
            match.AutoNumberOfCycles++;
            AutoNumberOfCyclesPlus.Text = match.AutoNumberOfCycles.ToString();
            SemanticScreenReader.Announce("Auto Number Of Cycles" + AutoNumberOfCyclesPlus.Text);
            var taskSave = Task.Run(() => db.SaveMatchItemAsync(match));
            taskSave.Wait();
        }
    }
}
