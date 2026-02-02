using System.Text.Json;
using Microsoft.Data.Sqlite;

namespace BertScout2026.Models
{
    public class Match : BaseModel
    {
        // Meta data
        public int TeamNumber { get; set; }
        public int MatchNumber { get; set; }
        public string? ScoutName { get; set; }

        // Autonomous properties
        public int AutoNumberOfCycles { get; set; }
        public int AutoBallsPerCycle { get; set; }
        public int AutoRobotSpeed { get; set; }
        public bool AutoFloorPickup { get; set; }
        public bool AutoHumanPlayerPickup { get; set; }
        public int AutoClimbingLevel { get; set; }

        // Teleop properties
        public int TeleNumberOfCycles { get; set; }
        public int TeleBallsPerCycle { get; set; }
        public int TeleRobotSpeed { get; set; }
        public bool TeleFloorPickup { get; set; }
        public bool TeleHumanPlayerPickup { get; set; }
        public int TeleClimbingLevel { get; set; }

        // End game
        public int Score { get; set; }
        public string? Comments { get; set; }

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
                , ScoutName TEXT
                , AutoNumberOfCycles INTEGER
                , AutoBallsPerCycle INTEGER
                , AutoRobotSpeed INTEGER
                , AutoClimbingLevel INTEGER
                , AutoFloorPickup INTEGER
                , AutoHumanPlayerPickup INTEGER
                , TeleNumberOfCycles INTEGER
                , TeleBallsPerCycle INTEGER
                , TeleRobotSpeed INTEGER
                , TeleClimbingLevel INTEGER
                , TeleFloorPickup INTEGER
                , TeleHumanPlayerPickup INTEGER
                , Score INTEGER
                , Comments TEXT
                )";
        }

        public static string MatchFields()
        {
            return
                @$"{BaseFields()}
                , TeamNumber
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
                {MatchFields()}
                ) VALUES (
                {BaseAddValues()}
                , {TeamNumber}
                , {MatchNumber}
                , {"'" + ScoutName + "'" ?? "NULL"}
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
                , {"'" + Comments + "'" ?? "NULL"}
                )";
        }

        public string UpdateCommand()
        {
            return
                @$"UPDATE Match SET
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
            return new Match
            {
                Id = reader.GetInt32(0),
                Uuid = reader.GetString(1),
                AirtableId = reader.GetString(2),
                Changed = reader.GetInt32(3) == 1,
                TeamNumber = reader.GetInt32(4),
                MatchNumber = reader.GetInt32(5),
                ScoutName = reader.GetString(6),
                AutoNumberOfCycles = reader.GetInt32(7),
                AutoBallsPerCycle = reader.GetInt32(8),
                AutoRobotSpeed = reader.GetInt32(9),
                AutoClimbingLevel = reader.GetInt32(10),
                AutoFloorPickup = reader.GetInt32(11) == 1,
                AutoHumanPlayerPickup = reader.GetInt32(12) == 1,
                TeleNumberOfCycles = reader.GetInt32(13),
                TeleBallsPerCycle = reader.GetInt32(14),
                TeleRobotSpeed = reader.GetInt32(15),
                TeleClimbingLevel = reader.GetInt32(16),
                TeleFloorPickup = reader.GetInt32(17) == 1,
                TeleHumanPlayerPickup = reader.GetInt32(18) == 1,
                Score = reader.GetInt32(19),
                Comments = reader.GetString(20),
            };
        }
    }
}
