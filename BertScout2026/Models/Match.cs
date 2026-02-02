using System.Text.Json;

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
            return @$"
            CREATE TABLE IF NOT EXISTS Match (
                {BaseCreateTableFields()}
                TeamNumber INTEGER NOT NULL,
                MatchNumber INTEGER NOT NULL,
                ScoutName TEXT,
                AutoNumberOfCycles INTEGER,
                AutoBallsPerCycle INTEGER,
                AutoRobotSpeed INTEGER,
                AutoClimbingLevel INTEGER,
                AutoFloorPickup INTEGER,
                AutoHumanPlayerPickup INTEGER,
                TeleNumberOfCycles INTEGER,
                TeleBallsPerCycle INTEGER,
                TeleRobotSpeed INTEGER,
                TeleClimbingLevel INTEGER,
                TeleFloorPickup INTEGER,
                TeleHumanPlayerPickup INTEGER,
                Score INTEGER,
                Comments TEXT
            )";
        }

        public string AddCommand()
        {
            return @$"
            INSERT INTO Match (
                {BaseFields()}
                TeamNumber,
                MatchNumber,
                ScoutName,
                AutoNumberOfCycles,
                AutoBallsPerCycle,
                AutoRobotSpeed,
                AutoClimbingLevel,
                AutoFloorPickup,
                AutoHumanPlayerPickup,
                TeleNumberOfCycles,
                TeleBallsPerCycle,
                TeleRobotSpeed,
                TeleClimbingLevel,
                TeleFloorPickup,
                TeleHumanPlayerPickup,
                Score,
                Comments,
                Changed
            ) VALUES (
                {BaseFieldValues()}
                {TeamNumber},
                {MatchNumber},
                {"'" + ScoutName + "'" ?? "NULL"},
                {AutoNumberOfCycles},
                {AutoBallsPerCycle},
                {AutoRobotSpeed},
                {AutoClimbingLevel},
                {(AutoFloorPickup ? 1 : 0)},
                {(AutoHumanPlayerPickup ? 1 : 0)},
                {TeleNumberOfCycles},
                {TeleBallsPerCycle},
                {TeleRobotSpeed},
                {TeleClimbingLevel},
                {(TeleFloorPickup ? 1 : 0)},
                {(TeleHumanPlayerPickup ? 1 : 0)},
                {Score},
                {"'" + Comments + "'" ?? "NULL"},
                {(Changed ? 1 : 0)}
            )";
        }

        public string UpdateCommand()
        {
            return @$"
            UPDATE Match SET
                TeamNumber = {TeamNumber},
                MatchNumber = {MatchNumber},
                ScoutName = '{ScoutName}',
                AutoNumberOfCycles = {AutoNumberOfCycles},
                AutoBallsPerCycle = {AutoBallsPerCycle},
                AutoRobotSpeed = {AutoRobotSpeed},
                AutoClimbingLevel = {AutoClimbingLevel},
                AutoFloorPickup = {(AutoFloorPickup ? 1 : 0)},
                AutoHumanPlayerPickup = {(AutoHumanPlayerPickup ? 1 : 0)},
                TeleNumberOfCycles = {TeleNumberOfCycles},
                TeleBallsPerCycle = {TeleBallsPerCycle},
                TeleRobotSpeed = {TeleRobotSpeed},
                TeleClimbingLevel = {TeleClimbingLevel},
                TeleFloorPickup = {(TeleFloorPickup ? 1 : 0)},
                TeleHumanPlayerPickup = {(TeleHumanPlayerPickup ? 1 : 0)},
                Score = {Score},
                Comments = '{Comments}',
                Changed = {(Changed ? 1 : 0)}
            WHERE Id = {Id}";
        }
    }
}
