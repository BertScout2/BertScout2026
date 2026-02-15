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
            //SemanticScreenReader.Announce("Auto Number Of Cycles" + AutoNumberOfCyclesPlus.Text);
            SaveData();
        }

        private void AutoNumberOfCyclesMinusClicked(object? sender, EventArgs e)
        {
            if (match.AutoNumberOfCycles > 0)
            {
                match.AutoNumberOfCycles--;
                AutoNumberOfCyclesPlus.Text = match.AutoNumberOfCycles.ToString();
                //SemanticScreenReader.Announce("Auto Number Of Cycles" + AutoNumberOfCyclesPlus.Text);
                SaveData();
            }
        }
        private void AutoBallsPerCyclePlusClicked(object? sender, EventArgs e)
        {
            match.AutoBallsPerCycle++;
            AutoBallsPerCyclePlus.Text = match.AutoBallsPerCycle.ToString();
            //SemanticScreenReader.Announce("Auto Balls Per Cycle" + AutoBallsPerCyclePlus.Text);
            SaveData();
        }
        private void AutoBallsPerCycleMinusClicked(object? sender, EventArgs e)
        {
            if (match.AutoBallsPerCycle > 0)
            {
                match.AutoBallsPerCycle--;
                AutoBallsPerCyclePlus.Text = match.AutoBallsPerCycle.ToString();
                //SemanticScreenReader.Announce("Auto Balls Per Cycle" + AutoBallsPerCyclePlus.Text);
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

        private void AutoFloorPickupFalseClicked(object? sender, EventArgs e)
        {
            match.AutoFloorPickup = false;
            AutoFloorPickupFalse.BackgroundColor = Colors.Green;
            AutoFloorPickupTrue.BackgroundColor = Colors.Gray;
            SaveData();
        }
        private void AutoFloorPickupTrueClicked(object? sender, EventArgs e)
        {
            match.AutoFloorPickup = true;
            AutoFloorPickupFalse.BackgroundColor = Colors.Gray;
            AutoFloorPickupTrue.BackgroundColor = Colors.Green;
            SaveData();
        }

        private void AutoHumanPlayerPickupFalseClicked(object? sender, EventArgs e)
        {
            match.AutoHumanPlayerPickup = false;
            AutoHumanPlayerPickupFalse.BackgroundColor = Colors.Green;
            AutoHumanPlayerPickupTrue.BackgroundColor = Colors.Gray;
            SaveData();
        }
        private void AutoHumanPlayerPickupTrueClicked(object? sender, EventArgs e)
        {
            match.AutoHumanPlayerPickup = true;
            AutoHumanPlayerPickupFalse.BackgroundColor = Colors.Gray;
            AutoHumanPlayerPickupTrue.BackgroundColor = Colors.Green;
            SaveData();
        }
        private void AutoClimbingNoClimbClicked(object? sender, EventArgs e)
        {
            match.AutoClimbingLevel = 0;
            AutoClimbingNoClimb.BackgroundColor = Colors.Green;
            AutoClimbingLevel1.BackgroundColor = Colors.Gray;
            AutoClimbingLevel2.BackgroundColor = Colors.Gray;
            AutoClimbingLevel3.BackgroundColor = Colors.Gray;
            SaveData();
        }
        private void AutoClimbingLevel1Clicked(object? sender, EventArgs e)
        {
            match.AutoClimbingLevel = 1;
            AutoClimbingNoClimb.BackgroundColor = Colors.Gray;
            AutoClimbingLevel1.BackgroundColor = Colors.Green;
            AutoClimbingLevel2.BackgroundColor = Colors.Gray;
            AutoClimbingLevel3.BackgroundColor = Colors.Gray;
            SaveData();
        }
        private void AutoClimbingLevel2Clicked(object? sender, EventArgs e)
        {
            match.AutoClimbingLevel = 2;
            AutoClimbingNoClimb.BackgroundColor = Colors.Gray;
            AutoClimbingLevel1.BackgroundColor = Colors.Gray;
            AutoClimbingLevel2.BackgroundColor = Colors.Green;
            AutoClimbingLevel3.BackgroundColor = Colors.Gray;
            SaveData();
        }
        private void AutoClimbingLevel3Clicked(object? sender, EventArgs e)
        {
            match.AutoClimbingLevel = 3;
            AutoClimbingNoClimb.BackgroundColor = Colors.Gray;
            AutoClimbingLevel1.BackgroundColor = Colors.Gray;
            AutoClimbingLevel2.BackgroundColor = Colors.Gray;
            AutoClimbingLevel3.BackgroundColor = Colors.Green;
            SaveData();
        }

        private void TeleNumberOfCyclesPlusClicked(object? sender, EventArgs e)
        {
            match.TeleNumberOfCycles++;
            TeleNumberOfCyclesPlus.Text = match.TeleNumberOfCycles.ToString();
            //SemanticScreenReader.Announce("Tele Number Of Cycles" + TeleNumberOfCyclesPlus.Text);
            SaveData();
        }

        private void TeleNumberOfCyclesMinusClicked(object? sender, EventArgs e)
        {
            if (match.TeleNumberOfCycles > 0)
            {
                match.TeleNumberOfCycles--;
                TeleNumberOfCyclesPlus.Text = match.TeleNumberOfCycles.ToString();
                //SemanticScreenReader.Announce("Tele Number Of Cycles" + TeleNumberOfCyclesPlus.Text);
                SaveData();
            }
        }
        private void TeleBallsPerCyclePlusClicked(object? sender, EventArgs e)
        {
            match.TeleBallsPerCycle++;
            TeleBallsPerCyclePlus.Text = match.TeleBallsPerCycle.ToString();
            //SemanticScreenReader.Announce("Teleop Balls Per Cycle" + TeleBallsPerCyclePlus.Text);
            SaveData();
        }
        private void TeleBallsPerCycleMinusClicked(object? sender, EventArgs e)
        {
            if (match.TeleBallsPerCycle > 0)
            {
                match.TeleBallsPerCycle--;
                TeleBallsPerCyclePlus.Text = match.TeleBallsPerCycle.ToString();
                //SemanticScreenReader.Announce("Teleop Balls Per Cycle" + TeleBallsPerCyclePlus.Text);
                SaveData();
            }
        }
        private void TeleRobotSpeedNoMovementClicked(object? sender, EventArgs e)
        {
            match.TeleRobotSpeed = 0;
            TeleRobotSpeedNoMovement.BackgroundColor = Colors.Green;
            TeleRobotSpeedSlow.BackgroundColor = Colors.Gray;
            TeleRobotSpeedMedium.BackgroundColor = Colors.Gray;
            TeleRobotSpeedFast.BackgroundColor = Colors.Gray;
            SaveData();
        }
        private void TeleRobotSpeedSlowClicked(object? sender, EventArgs e)
        {
            match.TeleRobotSpeed = 1;
            TeleRobotSpeedNoMovement.BackgroundColor = Colors.Gray;
            TeleRobotSpeedSlow.BackgroundColor = Colors.Green;
            TeleRobotSpeedMedium.BackgroundColor = Colors.Gray;
            TeleRobotSpeedFast.BackgroundColor = Colors.Gray;
            SaveData();
        }
        private void TeleRobotSpeedMediumClicked(object? sender, EventArgs e)
        {
            match.TeleRobotSpeed = 2;
            TeleRobotSpeedNoMovement.BackgroundColor = Colors.Gray;
            TeleRobotSpeedSlow.BackgroundColor = Colors.Gray;
            TeleRobotSpeedMedium.BackgroundColor = Colors.Green;
            TeleRobotSpeedFast.BackgroundColor = Colors.Gray;
            SaveData();
        }
        private void TeleRobotSpeedFastClicked(object? sender, EventArgs e)
        {
            match.TeleRobotSpeed = 3;
            TeleRobotSpeedNoMovement.BackgroundColor = Colors.Gray;
            TeleRobotSpeedSlow.BackgroundColor = Colors.Gray;
            TeleRobotSpeedMedium.BackgroundColor = Colors.Gray;
            TeleRobotSpeedFast.BackgroundColor = Colors.Green;
            SaveData();
        }
        private void TeleFloorPickupFalseClicked(object? sender, EventArgs e)
        {
            match.TeleFloorPickup = false;
            TeleFloorPickupFalse.BackgroundColor = Colors.Green;
            TeleFloorPickupTrue.BackgroundColor = Colors.Gray;
            SaveData();
        }
        private void TeleFloorPickupTrueClicked(object? sender, EventArgs e)
        {
            match.TeleFloorPickup = true;
            TeleFloorPickupFalse.BackgroundColor = Colors.Gray;
            TeleFloorPickupTrue.BackgroundColor = Colors.Green;
            SaveData();
        }

        private void TeleHumanPlayerPickupFalseClicked(object? sender, EventArgs e)
        {
            match.TeleHumanPlayerPickup = false;
            TeleHumanPlayerPickupFalse.BackgroundColor = Colors.Green;
            TeleHumanPlayerPickupTrue.BackgroundColor = Colors.Gray;
            SaveData();
        }
        private void TeleHumanPlayerPickupTrueClicked(object? sender, EventArgs e)
        {
            match.TeleHumanPlayerPickup = true;
            TeleHumanPlayerPickupFalse.BackgroundColor = Colors.Gray;
            TeleHumanPlayerPickupTrue.BackgroundColor = Colors.Green;
            SaveData();
        }

        private void TeleClimbingNoClimbClicked(object? sender, EventArgs e)
        {
            match.TeleClimbingLevel = 0;
            TeleClimbingNoClimb.BackgroundColor = Colors.Green;
            TeleClimbingLevel1.BackgroundColor = Colors.Gray;
            TeleClimbingLevel2.BackgroundColor = Colors.Gray;
            TeleClimbingLevel3.BackgroundColor = Colors.Gray;
            SaveData();
        }
        private void TeleClimbingLevel1Clicked(object? sender, EventArgs e)
        {
            match.TeleClimbingLevel = 1;
            TeleClimbingNoClimb.BackgroundColor = Colors.Gray;
            TeleClimbingLevel1.BackgroundColor = Colors.Green;
            TeleClimbingLevel2.BackgroundColor = Colors.Gray;
            TeleClimbingLevel3.BackgroundColor = Colors.Gray;
            SaveData();
        }
        private void TeleClimbingLevel2Clicked(object? sender, EventArgs e)
        {
            match.TeleClimbingLevel = 2;
            TeleClimbingNoClimb.BackgroundColor = Colors.Gray;
            TeleClimbingLevel1.BackgroundColor = Colors.Gray;
            TeleClimbingLevel2.BackgroundColor = Colors.Green;
            TeleClimbingLevel3.BackgroundColor = Colors.Gray;
            SaveData();
        }
        private void TeleClimbingLevel3Clicked(object? sender, EventArgs e)
        {
            match.TeleClimbingLevel = 3;
            TeleClimbingNoClimb.BackgroundColor = Colors.Gray;
            TeleClimbingLevel1.BackgroundColor = Colors.Gray;
            TeleClimbingLevel2.BackgroundColor = Colors.Gray;
            TeleClimbingLevel3.BackgroundColor = Colors.Green;
            SaveData();
        }

        //private void CommentPicker_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (CommentPicker.SelectedIndex < 0)
        //        return;
        //    if (Comments.Text == null)
        //        Comments.Text = "";
        //    else if (Comments.Text.Length > 0 && !Comments.Text.EndsWith(' '))
        //        Comments.Text += " ";
        //    Comments.Text += CommentPicker.SelectedItem.ToString() + " ";
        //    CommentPicker.SelectedIndex = -1;
        //    SaveFields();
        //}

        //private void Comments_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    var temp = Comments?.Text ?? "";
        //    if (temp.Length > 250)
        //    {
        //        temp = temp[0..250];
        //        Comments!.Text = temp;
        //    }
        //    item.Comments = temp;
        //}
         
        private void ScoreStar1Clicked(object? sender, EventArgs e)
        {
            match.TeleClimbingLevel = 1;
            ScoreStar1.BackgroundColor = Colors.Green;
            ScoreStar2.BackgroundColor = Colors.Gray;
            ScoreStar3.BackgroundColor = Colors.Gray;
            ScoreStar4.BackgroundColor = Colors.Gray;
            ScoreStar5.BackgroundColor = Colors.Gray;
            SaveData();
        }
        private void ScoreStar2Clicked(object? sender, EventArgs e)
        {
            match.TeleClimbingLevel = 2;
            ScoreStar1.BackgroundColor = Colors.Gray;
            ScoreStar2.BackgroundColor = Colors.Green;
            ScoreStar3.BackgroundColor = Colors.Gray;
            ScoreStar4.BackgroundColor = Colors.Gray;
            ScoreStar5.BackgroundColor = Colors.Gray;
            SaveData();
        }
        private void ScoreStar3Clicked(object? sender, EventArgs e)
        {
            match.TeleClimbingLevel = 3;
            ScoreStar1.BackgroundColor = Colors.Gray;
            ScoreStar2.BackgroundColor = Colors.Gray;
            ScoreStar3.BackgroundColor = Colors.Green;
            ScoreStar4.BackgroundColor = Colors.Gray;
            ScoreStar5.BackgroundColor = Colors.Gray;
            SaveData();
        }
        private void ScoreStar4Clicked(object? sender, EventArgs e)
        {
            match.TeleClimbingLevel = 4;
            ScoreStar1.BackgroundColor = Colors.Gray;
            ScoreStar2.BackgroundColor = Colors.Gray;
            ScoreStar3.BackgroundColor = Colors.Gray;
            ScoreStar4.BackgroundColor = Colors.Green;
            ScoreStar5.BackgroundColor = Colors.Gray;
            SaveData();
        }
        private void ScoreStar5Clicked(object? sender, EventArgs e)
        {
            match.TeleClimbingLevel = 5;
            ScoreStar1.BackgroundColor = Colors.Gray;
            ScoreStar2.BackgroundColor = Colors.Gray;
            ScoreStar3.BackgroundColor = Colors.Gray;
            ScoreStar4.BackgroundColor = Colors.Gray;
            ScoreStar5.BackgroundColor = Colors.Green;
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
