using BertScout2026.Database;
using BertScout2026.Models;
using static BertScout2026.Utilities.PermissionManagement;

namespace BertScout2026
{
    public partial class MainPage : ContentPage
    {
        private readonly MatchDatabase db = new();

        //public string AppVersion => $"Bert Scout 2026 - Version {AppInfo.VersionString}";

        public string ScoutName => "Scott";

        Match match = new();

        public MainPage()
        {
            InitializeComponent();
            var taskPerm = Task.Run(() => CheckAndRequestStoragePermissionsAsync());
            if (!taskPerm.Result)
            {
                ShowError("Storage Permissions have been denied\n" +
                    "Please turn on Storage permission in App Info / Permissions");
            }
        }

        private void StartButtonClicked(object? sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(EntryTeamNumber.Text) || string.IsNullOrWhiteSpace(EntryMatchNumber.Text) ||
                    int.Parse(EntryTeamNumber.Text) <= 0 || int.Parse(EntryMatchNumber.Text) <= 0)
                {
                    ShowError("Match Number and Team Number cannot be empty");
                    return;
                }
                var teamNum = int.Parse(EntryTeamNumber.Text);
                var matchNum = int.Parse(EntryMatchNumber.Text);
                var taskLoad = Task.Run(() => db.GetMatchAsync(teamNum, matchNum));
                var existingMatch = taskLoad.Result;
                if (existingMatch != null)
                {
                    match = existingMatch;
                    FillFields();
                }
                else
                {
                    match = new Match(teamNum, matchNum, ScoutName);
                    ClearFields();
                }
                EntryTeamNumber.IsEnabled = false;
                EntryMatchNumber.IsEnabled = false;
                StartButton.IsVisible = false;
                SaveButton.IsVisible = true;
                ScoutingLayout.IsVisible = true;
            }
            catch (Exception ex)
            {
                ShowError("Error loading data from database\n" + ex.Message);
            }
        }

        private void ClearFields()
        {
            AutoNumberOfCyclesPlus.Text = "0";
            AutoBallsPerCyclePlus.Text = "0";
            SetAutoRobotSpeed(0);
        }

        private void FillFields()
        {
            throw new NotImplementedException();
        }

        private void SaveButtonClicked(object? sender, EventArgs e)
        {
            SaveData();
            EntryTeamNumber.IsEnabled = true;
            EntryMatchNumber.IsEnabled = true;
            StartButton.IsVisible = true;
            SaveButton.IsVisible = false;
            ScoutingLayout.IsVisible = false;
            EntryTeamNumber.Text = string.Empty;
            EntryMatchNumber.Text = (int.Parse(EntryMatchNumber.Text) + 1).ToString();
            EntryTeamNumber.Focus();
        }

        #region AutoNumberOfCycles

        private void AutoNumberOfCyclesPlusClicked(object? sender, EventArgs e)
        {
            SetAutoNumberOfCycles(1);
            SaveData();
        }

        private void AutoNumberOfCyclesMinusClicked(object? sender, EventArgs e)
        {
            SetAutoNumberOfCycles(-1);
            SaveData();
        }
        private void SetAutoNumberOfCycles(int value)
        {
            if (match.AutoNumberOfCycles + value >= 0)
            {
                match.AutoNumberOfCycles = match.AutoNumberOfCycles + value;
                AutoNumberOfCyclesPlus.Text = (match.AutoNumberOfCycles + value).ToString();
            }
        }

        #endregion
        
        #region AutoBallsPerCycle

        private void AutoBallsPerCyclePlusClicked(object? sender, EventArgs e)
        {
            SetAutoBallsPerCycle(1);
            SaveData();
        }
        private void AutoBallsPerCycleMinusClicked(object? sender, EventArgs e)
        {
            SetAutoBallsPerCycle(-1);
            SaveData();
        }


        private void SetAutoBallsPerCycle(int value)
        {
            if (match.AutoBallsPerCycle + value >= 0)
            {
                match.AutoBallsPerCycle = match.AutoBallsPerCycle + value;
                AutoBallsPerCyclePlus.Text = (match.AutoBallsPerCycle + value).ToString();
            }
        }

        #endregion

        #region AutoRobotSpeed

        private void AutoRobotSpeedNoMovementClicked(object? sender, EventArgs e)
        {
            SetAutoRobotSpeed(0);
            SaveData();
        }
        private void AutoRobotSpeedSlowClicked(object? sender, EventArgs e)
        {
            SetAutoRobotSpeed(1);
            SaveData();
        }
        private void AutoRobotSpeedMediumClicked(object? sender, EventArgs e)
        {
            SetAutoRobotSpeed(2);
            SaveData();
        }
        private void AutoRobotSpeedFastClicked(object? sender, EventArgs e)
        {
            SetAutoRobotSpeed(3);
            SaveData();
        }

        private void SetAutoRobotSpeed(int value)
        {
            match.AutoRobotSpeed = value;
            AutoRobotSpeedNoMovement.BackgroundColor = value == 0 ? Colors.Green : Colors.Gray;
            AutoRobotSpeedSlow.BackgroundColor = value == 0 ? Colors.Green : Colors.Gray;
            AutoRobotSpeedMedium.BackgroundColor = value == 0 ? Colors.Green : Colors.Gray;
            AutoRobotSpeedFast.BackgroundColor = value == 0 ? Colors.Green : Colors.Gray;
        }

        #endregion

        #region AutoFloorPickup

        private void AutoFloorPickupFalseClicked(object? sender, EventArgs e)
        {
            SetAutoFloorPickup(false);
            SaveData();
        }
        private void AutoFloorPickupTrueClicked(object? sender, EventArgs e)
        {
            SetAutoFloorPickup(true);
            SaveData();
        }

        private void SetAutoFloorPickup(bool value)
        {
            match.AutoFloorPickup = value;
            AutoFloorPickupFalse.BackgroundColor = value ? Colors.Green : Colors.Gray;
            AutoFloorPickupTrue.BackgroundColor = value ? Colors.Green : Colors.Gray;
        }

        #endregion

        #region AutoHumanPlayerPickup

        private void AutoHumanPlayerPickupFalseClicked(object? sender, EventArgs e)
        {
            SetAutoHumanPlayerPickup(false);
            SaveData();
        }
        private void AutoHumanPlayerPickupTrueClicked(object? sender, EventArgs e)
        {
            SetAutoHumanPlayerPickup(true);
            SaveData();
        }
        private void SetAutoHumanPlayerPickup(bool value)
        {
            match.AutoHumanPlayerPickup = value;
            AutoHumanPlayerPickupFalse.BackgroundColor = value ? Colors.Green : Colors.Gray;
            AutoHumanPlayerPickupTrue.BackgroundColor = value ? Colors.Green : Colors.Gray;
        }

        #endregion

        #region AutoClimbingLevel

        private void AutoClimbingNoClimbClicked(object? sender, EventArgs e)
        {
            SetAutoClimbingLevel(0);
            SaveData();
        }
        private void AutoClimbingLevel1Clicked(object? sender, EventArgs e)
        {
            SetAutoClimbingLevel(1);
            SaveData();
        }
        private void AutoClimbingLevel2Clicked(object? sender, EventArgs e)
        {
            SetAutoClimbingLevel(2);
            SaveData();
        }
        private void AutoClimbingLevel3Clicked(object? sender, EventArgs e)
        {
            SetAutoClimbingLevel(3);
            SaveData();
        }
        private void SetAutoClimbingLevel(int value)
        {
            match.AutoClimbingLevel = value;
            AutoClimbingNoClimb.BackgroundColor = value == 0 ? Colors.Green : Colors.Gray;
            AutoClimbingLevel1.BackgroundColor = value == 1 ? Colors.Green : Colors.Gray;
            AutoClimbingLevel2.BackgroundColor = value == 2 ? Colors.Green : Colors.Gray;
            AutoClimbingLevel3.BackgroundColor = value == 3 ? Colors.Green : Colors.Gray;
        }

        #endregion

        #region TeleNumberOfCycles

        private void TeleNumberOfCyclesPlusClicked(object? sender, EventArgs e)
        {
            SetTeleNumberOfCycles(1);
            SaveData();
        }

        private void TeleNumberOfCyclesMinusClicked(object? sender, EventArgs e)
        {
            SetTeleNumberOfCycles(-1);
            SaveData();
        }
        private void SetTeleNumberOfCycles(int value)
        {
            if (match.TeleNumberOfCycles + value >= 0)
            {
                match.TeleNumberOfCycles = match.TeleNumberOfCycles + value;
                TeleNumberOfCyclesPlus.Text = (match.TeleNumberOfCycles + value).ToString();
            }
        }

        #endregion

        #region TeleBallsPerCycle

        private void TeleBallsPerCyclePlusClicked(object? sender, EventArgs e)
        {
            SetTeleBallsPerCycle(1);
            SaveData();
        }
        private void TeleBallsPerCycleMinusClicked(object? sender, EventArgs e)
        {
            SetTeleBallsPerCycle(-1);
            SaveData();
        }


        private void SetTeleBallsPerCycle(int value)
        {
            if (match.TeleBallsPerCycle + value >= 0)
            {
                match.TeleBallsPerCycle = match.TeleBallsPerCycle + value;
                TeleBallsPerCyclePlus.Text = (match.TeleBallsPerCycle + value).ToString();
            }
        }

        #endregion

        #region TeleRobotSpeed

        private void TeleRobotSpeedNoMovementClicked(object? sender, EventArgs e)
        {
            SetTeleRobotSpeed(0);
            SaveData();
        }
        private void TeleRobotSpeedSlowClicked(object? sender, EventArgs e)
        {
            SetTeleRobotSpeed(1);
            SaveData();
        }
        private void TeleRobotSpeedMediumClicked(object? sender, EventArgs e)
        {
            SetTeleRobotSpeed(2);
            SaveData();
        }
        private void TeleRobotSpeedFastClicked(object? sender, EventArgs e)
        {
            SetTeleRobotSpeed(3);
            SaveData();
        }

        private void SetTeleRobotSpeed(int value)
        {
            match.TeleRobotSpeed = value;
            TeleRobotSpeedNoMovement.BackgroundColor = value == 0 ? Colors.Green : Colors.Gray;
            TeleRobotSpeedSlow.BackgroundColor = value == 0 ? Colors.Green : Colors.Gray;
            TeleRobotSpeedMedium.BackgroundColor = value == 0 ? Colors.Green : Colors.Gray;
            TeleRobotSpeedFast.BackgroundColor = value == 0 ? Colors.Green : Colors.Gray;
        }

        #endregion

        #region TeleFloorPickup

        private void TeleFloorPickupFalseClicked(object? sender, EventArgs e)
        {
            SetTeleFloorPickup(false);
            SaveData();
        }
        private void TeleFloorPickupTrueClicked(object? sender, EventArgs e)
        {
            SetTeleFloorPickup(true);
            SaveData();
        }

        private void SetTeleFloorPickup(bool value)
        {
            match.TeleFloorPickup = value;
            TeleFloorPickupFalse.BackgroundColor = value ? Colors.Green : Colors.Gray;
            TeleFloorPickupTrue.BackgroundColor = value ? Colors.Green : Colors.Gray;
        }

        #endregion

        #region TeleHumanPlayerPickup

        private void TeleHumanPlayerPickupFalseClicked(object? sender, EventArgs e)
        {
            SetTeleHumanPlayerPickup(false);
            SaveData();
        }
        private void TeleHumanPlayerPickupTrueClicked(object? sender, EventArgs e)
        {
            SetTeleHumanPlayerPickup(true);
            SaveData();
        }

        private void SetTeleHumanPlayerPickup(bool value)
        {
            match.TeleHumanPlayerPickup = value;
            TeleHumanPlayerPickupFalse.BackgroundColor = value ? Colors.Green : Colors.Gray;
            TeleHumanPlayerPickupTrue.BackgroundColor = value ? Colors.Green : Colors.Gray;
        }

        #endregion

        #region TeleClimbingLevel

        private void TeleClimbingNoClimbClicked(object? sender, EventArgs e)
        {
            SetTeleClimbingLevel(0);
            SaveData();
        }
        private void TeleClimbingLevel1Clicked(object? sender, EventArgs e)
        {
            SetTeleClimbingLevel(1);
            SaveData();
        }
        private void TeleClimbingLevel2Clicked(object? sender, EventArgs e)
        {
            SetTeleClimbingLevel(2);
            SaveData();
        }
        private void TeleClimbingLevel3Clicked(object? sender, EventArgs e)
        {
            SetTeleClimbingLevel(3);
            SaveData();
        }
        private void SetTeleClimbingLevel(int value)
        {
            match.TeleClimbingLevel = value;
            TeleClimbingNoClimb.BackgroundColor = value == 0 ? Colors.Green : Colors.Gray;
            TeleClimbingLevel1.BackgroundColor = value == 1 ? Colors.Green : Colors.Gray;
            TeleClimbingLevel2.BackgroundColor = value == 2 ? Colors.Green : Colors.Gray;
            TeleClimbingLevel3.BackgroundColor = value == 3 ? Colors.Green : Colors.Gray;
        }

        #endregion

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
            try
            {
                match.Changed = true;
                var taskSave = Task.Run(() => db.SaveMatchItemAsync(match));
                taskSave.Wait();
            }
            catch (Exception ex)
            {
                ShowError("Error saving data to database\n" + ex.Message);
            }
        }

        private void ShowError(string message)
        {
            MainLayout.IsVisible = false;
            ErrorLayout.IsVisible = true;
            ErrorMsg.Text = message;
        }
    }
}
