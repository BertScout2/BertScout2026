using Microsoft.Data.Sqlite;
using System.Text.Json;

namespace BertScout2026.Models;

public class Match : BaseModel
{
    private const string TableName = "Matches";

    // Meta data
    public int MatchNumber { get; set; }
    public int TeamNumber { get; set; }
    public string ScoutName { get; set; } = "";

    // Autonomous properties
    public int AutoNumberOfCycles { get; set; }
    public int AutoShootingSpeed { get; set; }
    public int AutoBallsPerCycle { get; set; }
    public int AutoAccuracy { get; set; }
    public int AutoRobotSpeed { get; set; }
    public bool AutoFloorPickup { get; set; }
    public bool AutoStationPickup { get; set; }
    public int AutoRoute { get; set; }
    public int AutoClimbingLevel { get; set; }

    // Teleop properties
    public int TeleNumberOfCycles { get; set; }
    public int TeleShootingSpeed { get; set; }
    public int TeleBallsPerCycle { get; set; }
    public int TeleAccuracy { get; set; }
    public int TeleRobotSpeed { get; set; }
    public bool TeleFloorPickup { get; set; }
    public bool TeleStationPickup { get; set; }
    public int TeleRoute { get; set; }
    public int TeleClimbingLevel { get; set; }

    // End game
    public int Score { get; set; }
    public string Comments { get; set; } = "";

    public Match()
    {
    }

    public Match(int matchNumber)
    {
        MatchNumber = matchNumber;
    }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this, WriteOptions);
    }

    public static string CreateTableCommand()
    {
        return
            @$"CREATE TABLE IF NOT EXISTS {TableName} (
            {BaseCreateTableFields()}
            , MatchNumber INTEGER NOT NULL
            , TeamNumber INTEGER NOT NULL
            , ScoutName TEXT NOT NULL
            , AutoNumberOfCycles INTEGER NOT NULL
            , AutoShootingSpeed INTEGER NOT NULL
            , AutoBallsPerCycle INTEGER NOT NULL
            , AutoAccuracy INTEGER NOT NULL
            , AutoRobotSpeed INTEGER NOT NULL
            , AutoFloorPickup INTEGER NOT NULL
            , AutoStationPickup INTEGER NOT NULL
            , AutoRoute INTEGER NOT NULL
            , AutoClimbingLevel INTEGER NOT NULL
            , TeleNumberOfCycles INTEGER NOT NULL
            , TeleShootingSpeed INTEGER NOT NULL
            , TeleBallsPerCycle INTEGER NOT NULL
            , TeleAccuracy INTEGER NOT NULL
            , TeleRobotSpeed INTEGER NOT NULL
            , TeleFloorPickup INTEGER NOT NULL
            , TeleStationPickup INTEGER NOT NULL
            , TeleRoute INTEGER NOT NULL
            , TeleClimbingLevel INTEGER NOT NULL
            , Score INTEGER NOT NULL
            , Comments TEXT NOT NULL
            )";
    }

    public static string CreateTableIndexCommand()
    {
        return
            @$"CREATE UNIQUE INDEX IF NOT EXISTS
            UX_{TableName}_{nameof(MatchNumber)}
            ON {TableName} ({nameof(MatchNumber)})";
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
            @$"MatchNumber
            , TeamNumber
            , ScoutName
            , AutoNumberOfCycles
            , AutoShootingSpeed
            , AutoBallsPerCycle
            , AutoAccuracy
            , AutoRobotSpeed
            , AutoFloorPickup
            , AutoStationPickup
            , AutoRoute
            , AutoClimbingLevel
            , TeleNumberOfCycles
            , TeleShootingSpeed
            , TeleBallsPerCycle
            , TeleAccuracy
            , TeleRobotSpeed
            , TeleFloorPickup
            , TeleStationPickup
            , TeleRoute
            , TeleClimbingLevel
            , Score
            , Comments";
    }

    public string AddCommand()
    {
        return
            @$"INSERT INTO {TableName} (
            {BaseFields()}
            , {MatchFields()}
            ) VALUES (
            {BaseAddValues()}
            , {MatchNumber}
            , {TeamNumber}
            , '{SQLInjectionFix(ScoutName)}'
            , {AutoNumberOfCycles}
            , {AutoShootingSpeed}
            , {AutoBallsPerCycle}
            , {AutoAccuracy}
            , {AutoRobotSpeed}
            , {(AutoFloorPickup ? 1 : 0)}
            , {(AutoStationPickup ? 1 : 0)}
            , {AutoRoute}
            , {AutoClimbingLevel}
            , {TeleNumberOfCycles}
            , {TeleShootingSpeed}
            , {TeleBallsPerCycle}
            , {TeleAccuracy}
            , {TeleRobotSpeed}
            , {(TeleFloorPickup ? 1 : 0)}
            , {(TeleStationPickup ? 1 : 0)}
            , {TeleRoute}
            , {TeleClimbingLevel}
            , {Score}
            , '{SQLInjectionFix(Comments)}'
            )";
    }

    public string UpdateCommand()
    {
        return
            @$"UPDATE {TableName}
            SET
            {BaseUpdateValues()}
            , MatchNumber = {MatchNumber}
            , TeamNumber = {TeamNumber}
            , ScoutName = '{SQLInjectionFix(ScoutName)}'
            , AutoNumberOfCycles = {AutoNumberOfCycles}
            , AutoShootingSpeed = {AutoShootingSpeed}
            , AutoBallsPerCycle = {AutoBallsPerCycle}
            , AutoAccuracy = {AutoAccuracy}
            , AutoRobotSpeed = {AutoRobotSpeed}
            , AutoFloorPickup = {(AutoFloorPickup ? 1 : 0)}
            , AutoStationPickup = {(AutoStationPickup ? 1 : 0)}
            , AutoRoute = {AutoRoute}
            , AutoClimbingLevel = {AutoClimbingLevel}
            , TeleNumberOfCycles = {TeleNumberOfCycles}
            , TeleShootingSpeed = {TeleShootingSpeed}
            , TeleBallsPerCycle = {TeleBallsPerCycle}
            , TeleAccuracy = {TeleAccuracy}
            , TeleRobotSpeed = {TeleRobotSpeed}
            , TeleFloorPickup = {(TeleFloorPickup ? 1 : 0)}
            , TeleStationPickup = {(TeleStationPickup ? 1 : 0)}
            , TeleRoute = {TeleRoute}
            , TeleClimbingLevel = {TeleClimbingLevel}
            , Score = {Score}
            , Comments = '{SQLInjectionFix(Comments)}'
            WHERE Id = {Id}";
    }

    public static Match FromReader(SqliteDataReader reader)
    {
        var match = new Match();
        match.BaseFromReader(reader);
        match.MatchNumber = reader.GetInt32(BaseFieldCount);
        match.TeamNumber = reader.GetInt32(BaseFieldCount + 1);
        match.ScoutName = reader.GetString(BaseFieldCount + 2);
        match.AutoNumberOfCycles = reader.GetInt32(BaseFieldCount + 3);
        match.AutoShootingSpeed = reader.GetInt32(BaseFieldCount + 4);
        match.AutoBallsPerCycle = reader.GetInt32(BaseFieldCount + 5);
        match.AutoAccuracy = reader.GetInt32(BaseFieldCount + 6);
        match.AutoRobotSpeed = reader.GetInt32(BaseFieldCount + 7);
        match.AutoFloorPickup = reader.GetInt32(BaseFieldCount + 8) == 1;
        match.AutoStationPickup = reader.GetInt32(BaseFieldCount + 9) == 1;
        match.AutoRoute = reader.GetInt32(BaseFieldCount + 10);
        match.AutoClimbingLevel = reader.GetInt32(BaseFieldCount + 11);
        match.TeleNumberOfCycles = reader.GetInt32(BaseFieldCount + 12);
        match.TeleShootingSpeed = reader.GetInt32(BaseFieldCount + 13);
        match.TeleBallsPerCycle = reader.GetInt32(BaseFieldCount + 14);
        match.TeleAccuracy = reader.GetInt32(BaseFieldCount + 15);
        match.TeleRobotSpeed = reader.GetInt32(BaseFieldCount + 16);
        match.TeleFloorPickup = reader.GetInt32(BaseFieldCount + 17) == 1;
        match.TeleStationPickup = reader.GetInt32(BaseFieldCount + 18) == 1;
        match.TeleRoute = reader.GetInt32(BaseFieldCount + 19);
        match.TeleClimbingLevel = reader.GetInt32(BaseFieldCount + 20);
        match.Score = reader.GetInt32(BaseFieldCount + 21);
        match.Comments = reader.GetString(BaseFieldCount + 22);
        return match;
    }
}
