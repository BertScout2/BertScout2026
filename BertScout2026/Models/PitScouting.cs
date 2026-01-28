namespace BertScout2026.Models
{
    internal class PitScouting
    {
        // Meta data
        public int TeamNumber { get; set; }

        // Robot properties
        public int ClimbingLevel { get; set; }
        public string? PreferredStartingPosition { get; set; }
        public string? DriveType { get; set; }
        public int BallCapacity { get; set; }
        public int LaunchingSpeed { get; set; }
        public int RobotSpeed { get; set; }
        public int NumberOfAutonomousPrograms { get; set; }
        public int NumberOfTeamMembers { get; set; }
    }
}
