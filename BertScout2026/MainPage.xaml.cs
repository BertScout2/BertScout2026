using BertScout2026.Database;
using BertScout2026.Models;

namespace BertScout2026
{
    public partial class MainPage : ContentPage
    {
        private readonly MatchDatabase db = new();

        int count = 0;
        Match match = new Match();

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object? sender, EventArgs e)
        {
            match.AutoNumberOfCycles++;

            if (match.AutoNumberOfCycles == 1)
                CounterBtn.Text = $"Robot went {match.AutoNumberOfCycles} cycle";
            else
                CounterBtn.Text = $"Robot went {match.AutoNumberOfCycles} cycles";

            SemanticScreenReader.Announce(CounterBtn.Text);

            var taskSave = Task.Run(() => db.SaveItemAsync(match));
            taskSave.Wait();
        }
    }
}
