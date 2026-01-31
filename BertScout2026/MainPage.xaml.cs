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
            SaveData();
        }

        private void AutoNumberOfCyclesMinusClicked(object? sender, EventArgs e)
        {
            if (match.AutoNumberOfCycles > 0)
            {
                match.AutoNumberOfCycles--;
                AutoNumberOfCyclesPlus.Text = match.AutoNumberOfCycles.ToString();
                SemanticScreenReader.Announce("Auto Number Of Cycles" + AutoNumberOfCyclesPlus.Text);
                SaveData();
            }
        }
        private void AutoBallsPerCyclePlusClicked(object? sender, EventArgs e)
        {
            match.AutoBallsPerCycle++;
            AutoBallsPerCyclePlus.Text = match.AutoBallsPerCycle.ToString();
            SemanticScreenReader.Announce("Auto Balls Per Cycle" + AutoBallsPerCyclePlus.Text);
            SaveData();
        }
        private void AutoBallsPerCycleMinusClicked(object? sender, EventArgs e)
        {
            if (match.AutoBallsPerCycle > 0)
            {
                match.AutoBallsPerCycle--;
                AutoBallsPerCyclePlus.Text = match.AutoBallsPerCycle.ToString();
                SemanticScreenReader.Announce("Auto Balls Per Cycle" + AutoBallsPerCyclePlus.Text);
                SaveData();
            }
        }

        private void AutoRobotSpeedNoMovementClicked(object? sender, EventArgs e)
        {
            match.AutoRobotSpeed = 0;
            AutoRobotSpeedNoMovement.BackgroundColor = Colors.Green;
            AutoRobotSpeedSlow.BackgroundColor = Colors.Gray;
            AutoRobotSpeedMedium.BackgroundColor = Colors.Gray;
            AutoRobotSpeedFast.BackgroundColor = Colors.Gray;
            SaveData();
        }
        private void AutoRobotSpeedSlowClicked(object? sender, EventArgs e)
        {
            match.AutoRobotSpeed = 1;
            AutoRobotSpeedNoMovement.BackgroundColor = Colors.Gray;
            AutoRobotSpeedSlow.BackgroundColor = Colors.Green;
            AutoRobotSpeedMedium.BackgroundColor = Colors.Gray;
            AutoRobotSpeedFast.BackgroundColor = Colors.Gray;
            SaveData();
        }
        private void AutoRobotSpeedMediumClicked(object? sender, EventArgs e)
        {
            match.AutoRobotSpeed = 2;
            AutoRobotSpeedNoMovement.BackgroundColor = Colors.Gray;
            AutoRobotSpeedSlow.BackgroundColor = Colors.Gray;
            AutoRobotSpeedMedium.BackgroundColor = Colors.Green;
            AutoRobotSpeedFast.BackgroundColor = Colors.Gray;
            SaveData();
        }
        private void AutoRobotSpeedFastClicked(object? sender, EventArgs e)
        {
            match.AutoRobotSpeed = 3;
            AutoRobotSpeedNoMovement.BackgroundColor = Colors.Gray;
            AutoRobotSpeedSlow.BackgroundColor = Colors.Gray;
            AutoRobotSpeedMedium.BackgroundColor = Colors.Gray;
            AutoRobotSpeedFast.BackgroundColor = Colors.Green;
            SaveData();
        }

        private void SaveData()
        {
            match.Changed = true;
            var taskSave = Task.Run(() => db.SaveMatchItemAsync(match));
            taskSave.Wait();
        }
    }
}
