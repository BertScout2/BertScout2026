using System.Text.Json;
using Microsoft.Data.Sqlite;

namespace BertScout2026.Models
{
    public class Match : BaseModel
    {
        // Meta data
        public int TeamNumber { get; set; }
        public int MatchNumber { get; set; }
        public string ScoutName { get; set; } = "";

        // Autonomous properties
        public int AutoNumberOfCycles { get; set; }
        public int AutoBallsPerCycle { get; set; }
        public int AutoRobotSpeed { get; set; }
        public bool AutoFloorPickup { get; set; }
        public bool AutoHumanPlayerPickup { get; set; }
        public int AutoClimbingLevel { get; set; }
        public int AutoRoute { get; set; }

        // Teleop properties
        public int TeleNumberOfCycles { get; set; }
        public int TeleBallsPerCycle { get; set; }
        public int TeleRobotSpeed { get; set; }
        public bool TeleFloorPickup { get; set; }
        public bool TeleHumanPlayerPickup { get; set; }
        public int TeleClimbingLevel { get; set; }
        public int TeleRoute { get; set; }

        // End game
        public int Score { get; set; }
        public string Comments { get; set; } = "";

        public Match()
        {
        }

        public Match(int teamNumber, int matchNumber, string scoutName)
        {
            TeamNumber = teamNumber;
            MatchNumber = matchNumber;
            ScoutName = scoutName;
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this, WriteOptions);
        }

        public static string CreateTableCommand()
        {
            return
                @$"CREATE TABLE IF NOT EXISTS Match (
                {BaseCreateTableFields()}
                , TeamNumber INTEGER NOT NULL
                , MatchNumber INTEGER NOT NULL
                , ScoutName TEXT NOT NULL
                , AutoNumberOfCycles INTEGER NOT NULL
                , AutoBallsPerCycle INTEGER NOT NULL
                , AutoRobotSpeed INTEGER NOT NULL
                , AutoClimbingLevel INTEGER NOT NULL
                , AutoFloorPickup INTEGER NOT NULL
                , AutoHumanPlayerPickup INTEGER NOT NULL
                , TeleNumberOfCycles INTEGER NOT NULL
                , TeleBallsPerCycle INTEGER NOT NULL
                , TeleRobotSpeed INTEGER NOT NULL
                , TeleClimbingLevel INTEGER NOT NULL
                , TeleFloorPickup INTEGER NOT NULL
                , TeleHumanPlayerPickup INTEGER NOT NULL
                , Score INTEGER NOT NULL
                , Comments TEXT NOT NULL
                )";
        }

        public static string CreateTableIndexCommand()
        {
            return
                @"CREATE UNIQUE INDEX IF NOT EXISTS UX_team_match ON Match (TeamNumber, MatchNumber)";
        }

        public static string MatchFieldsWithId()
        {
            return
                @$"{BaseFieldsWithID()}
                , {MatchFields()}";
        }

        public static string MatchFields()
        {
            return
                @$"TeamNumber
                , MatchNumber
                , ScoutName
                , AutoNumberOfCycles
                , AutoBallsPerCycle
                , AutoRobotSpeed
                , AutoClimbingLevel
                , AutoFloorPickup
                , AutoHumanPlayerPickup
                , TeleNumberOfCycles
                , TeleBallsPerCycle
                , TeleRobotSpeed
                , TeleClimbingLevel
                , TeleFloorPickup
                , TeleHumanPlayerPickup
                , Score
                , Comments";
        }

        public string AddCommand()
        {
            return
                @$"INSERT INTO Match (
                {BaseFields()}
                , {MatchFields()}
                ) VALUES (
                {BaseAddValues()}
                , {TeamNumber}
                , {MatchNumber}
                , '{ScoutName}'
                , {AutoNumberOfCycles}
                , {AutoBallsPerCycle}
                , {AutoRobotSpeed}
                , {AutoClimbingLevel}
                , {(AutoFloorPickup ? 1 : 0)}
                , {(AutoHumanPlayerPickup ? 1 : 0)}
                , {TeleNumberOfCycles}
                , {TeleBallsPerCycle}
                , {TeleRobotSpeed}
                , {TeleClimbingLevel}
                , {(TeleFloorPickup ? 1 : 0)}
                , {(TeleHumanPlayerPickup ? 1 : 0)}
                , {Score}
                , '{Comments}'
                )";
        }

        public string UpdateCommand()
        {
            return
                @$"UPDATE Match
                SET
                {BaseUpdateValues()}
                , TeamNumber = {TeamNumber}
                , MatchNumber = {MatchNumber}
                , ScoutName = '{ScoutName}'
                , AutoNumberOfCycles = {AutoNumberOfCycles}
                , AutoBallsPerCycle = {AutoBallsPerCycle}
                , AutoRobotSpeed = {AutoRobotSpeed}
                , AutoClimbingLevel = {AutoClimbingLevel}
                , AutoFloorPickup = {(AutoFloorPickup ? 1 : 0)}
                , AutoHumanPlayerPickup = {(AutoHumanPlayerPickup ? 1 : 0)}
                , TeleNumberOfCycles = {TeleNumberOfCycles}
                , TeleBallsPerCycle = {TeleBallsPerCycle}
                , TeleRobotSpeed = {TeleRobotSpeed}
                , TeleClimbingLevel = {TeleClimbingLevel}
                , TeleFloorPickup = {(TeleFloorPickup ? 1 : 0)}
                , TeleHumanPlayerPickup = {(TeleHumanPlayerPickup ? 1 : 0)}
                , Score = {Score}
                , Comments = '{Comments}'
                WHERE Id = {Id}";
        }

        public static Match FromReader(SqliteDataReader reader)
        {
            var match = new Match();
            match.BaseFromReader(reader);
            match.TeamNumber = reader.GetInt32(BaseFieldCount);
            match.MatchNumber = reader.GetInt32(BaseFieldCount + 1);
            match.ScoutName = reader.GetString(BaseFieldCount + 2);
            match.AutoNumberOfCycles = reader.GetInt32(BaseFieldCount + 3);
            match.AutoBallsPerCycle = reader.GetInt32(BaseFieldCount + 4);
            match.AutoRobotSpeed = reader.GetInt32(BaseFieldCount + 5);
            match.AutoClimbingLevel = reader.GetInt32(BaseFieldCount + 6);
            match.AutoFloorPickup = reader.GetInt32(BaseFieldCount + 7) == 1;
            match.AutoHumanPlayerPickup = reader.GetInt32(BaseFieldCount + 8) == 1;
            match.TeleNumberOfCycles = reader.GetInt32(BaseFieldCount + 9);
            match.TeleBallsPerCycle = reader.GetInt32(BaseFieldCount + 10);
            match.TeleRobotSpeed = reader.GetInt32(BaseFieldCount + 11);
            match.TeleClimbingLevel = reader.GetInt32(BaseFieldCount + 12);
            match.TeleFloorPickup = reader.GetInt32(BaseFieldCount + 13) == 1;
            match.TeleHumanPlayerPickup = reader.GetInt32(BaseFieldCount + 14) == 1;
            match.Score = reader.GetInt32(BaseFieldCount + 15);
            match.Comments = reader.GetString(BaseFieldCount + 16);
            return match;
        }
    }
}
