namespace BertScout2026.Models
{
    internal class Pit : BaseModel
    {
        // Meta data
        public int TeamNumber { get; set; }

        // Robot properties
        public string? DriveTrain { get; set; }
        public string? PreferredStartingPosition { get; set; }
        public bool ClimbInAuto { get; set; }
        public bool ShootInAuto { get; set; }
        public string? BestAuto { get; set; }
        public int MaxBallCapacity { get; set; }
        public int ClimbingLevel { get; set; }
        public int ShootBallsPerSecond { get; set; }
        public string? PreferredRoute { get; set; }
        public int NumberOfTeamMembers { get; set; }
    }
}
