using SQLite;
using System.Text.Json;

namespace BertScout2026.Models
{
    public class Match : BaseModel
    {
        // Meta data
        [Indexed(Name = "TeamMatchUnique", Order = 1, Unique = true)]
        public int TeamNumber { get; set; }
        [Indexed(Name = "TeamMatchUnique", Order = 2, Unique = true)]
        public int MatchNumber { get; set; }
        public string? ScoutName { get; set; }

        // Autonomous properties
        public int AutoNumberOfCycles { get; set; }
        public int AutoBallsPerCycle { get; set; }
        public int AutoRobotSpeed { get; set; }
        public int AutoClimbingLevel { get; set; }
        public bool AutoFloorPickup { get; set; }
        public bool AutoHumanPlayerPickup { get; set; }

        // Teleop properties
        public int TeleNumberOfCycles { get; set; }
        public int TeleBallsPerCycle { get; set; }
        public int TeleRobotSpeed { get; set; }
        public int TeleClimbingLevel { get; set; }
        public bool TeleFloorPickup { get; set; }
        public bool TeleHumanPlayerPickup { get; set; }

        // End game
        public string? Comments { get; set; }
        public int Score { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this, WriteOptions);
        }
    }
}
