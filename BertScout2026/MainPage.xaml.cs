using BertScout2026.Database;
using BertScout2026.Models;
using static BertScout2026.Utilities.PermissionManagement;

namespace BertScout2026
{
    public partial class MainPage : ContentPage
    {
        private readonly MatchDatabase db = new();

        private readonly Color ColorButtonOn = Colors.Green;
        private readonly Color ColorButtonOff = Colors.LightGray;
        private readonly Color ColorButtonError = Colors.Red;

        private readonly GlobalViewModel _global;

        private Match match = new();

        public MainPage(GlobalViewModel global)
        {
            InitializeComponent();
            _global = global;
            BindingContext = _global;
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
                ErrorLabelTop.IsVisible = false;
                if (string.IsNullOrEmpty(_global.ScouterName))
                {
                    SetErrorLabelTop("Missing Scouter Name - Please Login");
                    return;
                }
                if (string.IsNullOrWhiteSpace(EntryMatchNumber.Text))
                {
                    SetErrorLabelTop("Invalid Match Number");
                    return;
                }
                if (string.IsNullOrWhiteSpace(EntryTeamNumber.Text))
                {
                    SetErrorLabelTop("Invalid Team Number");
                    return;
                }
                if (!int.TryParse(EntryMatchNumber.Text, out int matchNum) || matchNum <= 0)
                {
                    SetErrorLabelTop("Invalid Match Number");
                    return;
                }
                if (!int.TryParse(EntryTeamNumber.Text, out int teamNum) || teamNum <= 0)
                {
                    SetErrorLabelTop("Invalid Team Number");
                    return;
                }
                EntryMatchNumber.Text = matchNum.ToString();
                EntryTeamNumber.Text = teamNum.ToString();
                var existingMatch = Task.Run(() => db.GetMatchAsync(matchNum)).Result;
                if (existingMatch != null)
                {
                    match = existingMatch;
                    EntryTeamNumber.Text = match.TeamNumber.ToString();
                    if (string.IsNullOrEmpty(match.ScoutName))
                    {
                        match.ScoutName = _global.ScouterName;
                    }
                    FillFields();
                }
                else
                {
                    match = new Match(matchNum)
                    {
                        TeamNumber = teamNum,
                        ScoutName = _global.ScouterName
                    };
                    ClearFields();
                }
                EntryTeamNumber.IsEnabled = false;
                EntryMatchNumber.IsEnabled = false;
                StartButton.IsVisible = false;
                SaveButton.IsVisible = true;
                ScoutingLayout.IsVisible = true;
            }
            catch (Exception)
            {
                SetErrorLabelTop("Error loading data from database");
            }
        }

        private void SetErrorLabelTop(string message)
        {
            ErrorLabelTop.Text = message;
            ErrorLabelTop.IsVisible = true;
        }

        private void ClearFields()
        {
            SetAutoNumberOfCycles(0);
            SetAutoBallsPerCycle(0);
            SetAutoRobotSpeed(0);
            SetAutoFloorPickup(false);
            SetAutoHumanPlayerPickup(false);
            SetAutoRoute(0);
            SetAutoClimbingLevel(0);
            SetTeleNumberOfCycles(0);
            SetTeleBallsPerCycle(0);
            SetTeleRobotSpeed(0);
            SetTeleFloorPickup(false);
            SetTeleHumanPlayerPickup(false);
            SetTeleRoute(0);
            SetTeleClimbingLevel(0);
            SetScoreStar(0);
            SetComments("");
        }

        private void FillFields()
        {
            SetAutoNumberOfCycles(match.AutoNumberOfCycles);
            SetAutoBallsPerCycle(match.AutoBallsPerCycle);
            SetAutoRobotSpeed(match.AutoRobotSpeed);
            SetAutoFloorPickup(match.AutoFloorPickup);
            SetAutoHumanPlayerPickup(match.AutoHumanPlayerPickup);
            SetAutoRoute(match.AutoRoute);
            SetAutoClimbingLevel(match.AutoClimbingLevel);
            SetTeleNumberOfCycles(match.TeleNumberOfCycles);
            SetTeleBallsPerCycle(match.TeleBallsPerCycle);
            SetTeleRobotSpeed(match.TeleRobotSpeed);
            SetTeleFloorPickup(match.TeleFloorPickup);
            SetTeleHumanPlayerPickup(match.TeleHumanPlayerPickup);
            SetTeleRoute(match.TeleRoute);
            SetTeleClimbingLevel(match.TeleClimbingLevel);
            SetScoreStar(match.Score);
            SetComments(match.Comments);
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
            SetAutoNumberOfCycles(match.AutoNumberOfCycles + 1);
            SaveData();
        }

        private void AutoNumberOfCyclesMinusClicked(object? sender, EventArgs e)
        {
            SetAutoNumberOfCycles(match.AutoNumberOfCycles - 1);
            SaveData();
        }
        private void SetAutoNumberOfCycles(int value)
        {
            if (value >= 0)
            {
                match.AutoNumberOfCycles = value;
                AutoNumberOfCyclesPlus.Text = value.ToString();
            }
        }

        #endregion

        #region AutoBallsPerCycle

        private void AutoBallsPerCyclePlusClicked(object? sender, EventArgs e)
        {
            SetAutoBallsPerCycle(match.AutoBallsPerCycle + 1);
            SaveData();
        }
        private void AutoBallsPerCycleMinusClicked(object? sender, EventArgs e)
        {
            SetAutoBallsPerCycle(match.AutoBallsPerCycle - 1);
            SaveData();
        }

        private void SetAutoBallsPerCycle(int value)
        {
            if (value >= 0)
            {
                match.AutoBallsPerCycle = value;
                AutoBallsPerCyclePlus.Text = value.ToString();
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
            AutoRobotSpeedNoMovement.BackgroundColor = value == 0 ? ColorButtonOn : ColorButtonOff;
            AutoRobotSpeedSlow.BackgroundColor = value == 1 ? ColorButtonOn : ColorButtonOff;
            AutoRobotSpeedMedium.BackgroundColor = value == 2 ? ColorButtonOn : ColorButtonOff;
            AutoRobotSpeedFast.BackgroundColor = value == 3 ? ColorButtonOn : ColorButtonOff;
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
            AutoFloorPickupFalse.BackgroundColor = !value ? ColorButtonOn : ColorButtonOff;
            AutoFloorPickupTrue.BackgroundColor = value ? ColorButtonOn : ColorButtonOff;
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
            AutoHumanPlayerPickupFalse.BackgroundColor = !value ? ColorButtonOn : ColorButtonOff;
            AutoHumanPlayerPickupTrue.BackgroundColor = value ? ColorButtonOn : ColorButtonOff;
        }

        #endregion

        #region AutoRoute

        private void AutoRouteNone_Clicked(object? sender, EventArgs e)
        {
            SetAutoRoute(0);
        }
        private void AutoRouteOver_Clicked(object? sender, EventArgs e)
        {
            SetAutoRoute(1);
        }
        private void AutoRouteUnder_Clicked(object? sender, EventArgs e)
        {
            SetAutoRoute(2);
        }
        private void AutoRouteBoth_Clicked(object? sender, EventArgs e)
        {
            SetAutoRoute(3);
        }

        private void SetAutoRoute(int value)
        {
            match.AutoRoute = value;
            AutoRouteNone.BackgroundColor = value == 0 ? ColorButtonOn : ColorButtonOff;
            AutoRouteOver.BackgroundColor = value == 1 ? ColorButtonOn : ColorButtonOff;
            AutoRouteUnder.BackgroundColor = value == 2 ? ColorButtonOn : ColorButtonOff;
            AutoRouteBoth.BackgroundColor = value == 3 ? ColorButtonOn : ColorButtonOff;
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
            AutoClimbingNoClimb.BackgroundColor = value == 0 ? ColorButtonOn : ColorButtonOff;
            AutoClimbingLevel1.BackgroundColor = value == 1 ? ColorButtonOn : ColorButtonOff;
            AutoClimbingLevel2.BackgroundColor = value == 2 ? ColorButtonOn : ColorButtonOff;
            AutoClimbingLevel3.BackgroundColor = value == 3 ? ColorButtonOn : ColorButtonOff;
        }

        #endregion

        #region TeleNumberOfCycles

        private void TeleNumberOfCyclesPlusClicked(object? sender, EventArgs e)
        {
            SetTeleNumberOfCycles(match.TeleNumberOfCycles + 1);
            SaveData();
        }

        private void TeleNumberOfCyclesMinusClicked(object? sender, EventArgs e)
        {
            SetTeleNumberOfCycles(match.TeleNumberOfCycles - 1);
            SaveData();
        }
        private void SetTeleNumberOfCycles(int value)
        {
            if (value >= 0)
            {
                match.TeleNumberOfCycles = value;
                TeleNumberOfCyclesPlus.Text = value.ToString();
            }
        }

        #endregion

        #region TeleBallsPerCycle

        private void TeleBallsPerCyclePlusClicked(object? sender, EventArgs e)
        {
            SetTeleBallsPerCycle(match.TeleBallsPerCycle + 1);
            SaveData();
        }
        private void TeleBallsPerCycleMinusClicked(object? sender, EventArgs e)
        {
            SetTeleBallsPerCycle(match.TeleBallsPerCycle - 1);
            SaveData();
        }


        private void SetTeleBallsPerCycle(int value)
        {
            if (value >= 0)
            {
                match.TeleBallsPerCycle = value;
                TeleBallsPerCyclePlus.Text = value.ToString();
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
            TeleRobotSpeedNoMovement.BackgroundColor = value == 0 ? ColorButtonOn : ColorButtonOff;
            TeleRobotSpeedSlow.BackgroundColor = value == 1 ? ColorButtonOn : ColorButtonOff;
            TeleRobotSpeedMedium.BackgroundColor = value == 2 ? ColorButtonOn : ColorButtonOff;
            TeleRobotSpeedFast.BackgroundColor = value == 3 ? ColorButtonOn : ColorButtonOff;
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
            TeleFloorPickupFalse.BackgroundColor = !value ? ColorButtonOn : ColorButtonOff;
            TeleFloorPickupTrue.BackgroundColor = value ? ColorButtonOn : ColorButtonOff;
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
            TeleHumanPlayerPickupFalse.BackgroundColor = !value ? ColorButtonOn : ColorButtonOff;
            TeleHumanPlayerPickupTrue.BackgroundColor = value ? ColorButtonOn : ColorButtonOff;
        }

        #endregion

        #region TeleRoute

        private void TeleRouteNone_Clicked(object? sender, EventArgs e)
        {
            SetTeleRoute(0);
        }
        private void TeleRouteOver_Clicked(object? sender, EventArgs e)
        {
            SetTeleRoute(1);
        }
        private void TeleRouteUnder_Clicked(object? sender, EventArgs e)
        {
            SetTeleRoute(2);
        }
        private void TeleRouteBoth_Clicked(object? sender, EventArgs e)
        {
            SetTeleRoute(3);
        }

        private void SetTeleRoute(int value)
        {
            match.TeleRoute = value;
            TeleRouteNone.BackgroundColor = value == 0 ? ColorButtonOn : ColorButtonOff;
            TeleRouteOver.BackgroundColor = value == 1 ? ColorButtonOn : ColorButtonOff;
            TeleRouteUnder.BackgroundColor = value == 2 ? ColorButtonOn : ColorButtonOff;
            TeleRouteBoth.BackgroundColor = value == 3 ? ColorButtonOn : ColorButtonOff;
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
            TeleClimbingNoClimb.BackgroundColor = value == 0 ? ColorButtonOn : ColorButtonOff;
            TeleClimbingLevel1.BackgroundColor = value == 1 ? ColorButtonOn : ColorButtonOff;
            TeleClimbingLevel2.BackgroundColor = value == 2 ? ColorButtonOn : ColorButtonOff;
            TeleClimbingLevel3.BackgroundColor = value == 3 ? ColorButtonOn : ColorButtonOff;
        }

        #endregion

        //private void CommentPicker_SelectedIndexChanged(object? sender, EventArgs e)
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

        #region Comments

        private void Comments_TextChanged(object? sender, TextChangedEventArgs e)
        {
            var value = Comments.Text;
            if (value.Length > 250)
            {
                value = value[0..250];
            }
            if (match.Comments == value)
            {
                return;
            }
            SetComments(value);
            SaveData();
        }

        private void SetComments(string value)
        {
            if (value.Length > 250)
            {
                value = value[0..250];
            }
            match.Comments = value;
            if (Comments.Text == value)
            {
                return;
            }
            Comments.Text = value;
        }

        #endregion

        #region Score

        private void ScoreStar0Clicked(object? sender, EventArgs e)
        {
            SetScoreStar(0);
            SaveData();
        }
        private void ScoreStar1Clicked(object? sender, EventArgs e)
        {
            SetScoreStar(1);
            SaveData();
        }
        private void ScoreStar2Clicked(object? sender, EventArgs e)
        {
            SetScoreStar(2);
            SaveData();
        }
        private void ScoreStar3Clicked(object? sender, EventArgs e)
        {
            SetScoreStar(3);
            SaveData();
        }
        private void ScoreStar4Clicked(object? sender, EventArgs e)
        {
            SetScoreStar(4);
            SaveData();
        }
        private void ScoreStar5Clicked(object? sender, EventArgs e)
        {
            SetScoreStar(5);
            SaveData();
        }
        private void SetScoreStar(int value)
        {
            match.Score = value;
            ScoreStar0.BackgroundColor = value == 0 ? ColorButtonOn : ColorButtonOff;
            ScoreStar1.BackgroundColor = value == 1 ? ColorButtonOn : ColorButtonOff;
            ScoreStar2.BackgroundColor = value == 2 ? ColorButtonOn : ColorButtonOff;
            ScoreStar3.BackgroundColor = value == 3 ? ColorButtonOn : ColorButtonOff;
            ScoreStar4.BackgroundColor = value == 4 ? ColorButtonOn : ColorButtonOff;
            ScoreStar5.BackgroundColor = value == 5 ? ColorButtonOn : ColorButtonOff;
        }

        #endregion

        private void SaveData()
        {
            try
            {
                match.Changed = true;
                var taskSave = Task.Run(() => db.SaveMatchItemAsync(match));
                taskSave.Wait();
            }
            catch (Exception)
            {
                SetErrorLabelTop("Error saving data to database");
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
