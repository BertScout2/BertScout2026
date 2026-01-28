namespace BertScout2026.Models
{
    public class Match
    {
        // Meta data
        public int TeamNumber { get; set; }
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
    }
}
