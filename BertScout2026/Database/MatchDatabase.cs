using BertScout2026.Models;
using Microsoft.Data.Sqlite;

namespace BertScout2026.Database;

public class MatchDatabase : BaseDatabase
{
    private const string MatchDBFilename = "MatchScout2026.db3";
    private SqliteConnection Database = new();
    private string? _databasePath;
    private bool _created = false;

    public MatchDatabase()
    {
    }

    private async Task InitMatchDB()
    {
        if (_created)
        {
            return;
        }
        try
        {
            _databasePath = Path.Combine(DatabasePath, MatchDBFilename);
            Database = new SqliteConnection("Data Source=" + _databasePath);
            await Database.OpenAsync();
            var createTableCmd = Database.CreateCommand();
            createTableCmd.CommandText = Match.CreateTableCommand();
            await createTableCmd.ExecuteNonQueryAsync();
            var createIndexCmd = Database.CreateCommand();
            createIndexCmd.CommandText = Match.CreateTableIndexCommand();
            await createIndexCmd.ExecuteNonQueryAsync();
            Database.Close();
            _created = true;
        }
        catch (Exception ex)
        {
            throw new SystemException($"Error initializing Match database: {_databasePath}\r\n{ex.Message}");
        }
    }

    public async Task<List<Match>> GetMatchItemsAsync()
    {
        await InitMatchDB();
        await Database.OpenAsync();
        var selectCmd = Database.CreateCommand();
        selectCmd.CommandText =
            @$"SELECT
            {Match.MatchFieldsWithId()}
            FROM Match 
            ORDER BY MatchNumber";
        await using var reader = await selectCmd.ExecuteReaderAsync();
        var matches = new List<Match>();
        while (await reader.ReadAsync())
        {
            matches.Add(Match.FromReader(reader));
        }
        return matches;
    }

    public async Task<List<Match>> GetChangedMatchItemsAsync()
    {
        await InitMatchDB();
        await Database.OpenAsync();
        var selectCmd = Database.CreateCommand();
        selectCmd.CommandText =
            @$"SELECT
            {Match.MatchFieldsWithId()}
            FROM Match 
            WHERE Changed = 1
            ORDER BY MatchNumber";
        await using var reader = await selectCmd.ExecuteReaderAsync();
        var matches = new List<Match>();
        while (await reader.ReadAsync())
        {
            matches.Add(Match.FromReader(reader));
        }
        return matches;
    }

    public async Task<List<Match>> GetTeamAllMatches(int team)
    {
        await InitMatchDB();
        await Database.OpenAsync();
        var selectCmd = Database.CreateCommand();
        selectCmd.CommandText =
            @$"SELECT
            {Match.MatchFieldsWithId()}
            FROM Match 
            WHERE TeamNumber = @team
            ORDER BY MatchNumber";
        selectCmd.Parameters.AddWithValue("@team", team);
        await using var reader = await selectCmd.ExecuteReaderAsync();
        var matches = new List<Match>();
        while (await reader.ReadAsync())
        {
            matches.Add(Match.FromReader(reader));
        }
        return matches;
    }

    public async Task<Match?> GetMatchAsync(int match)
    {
        await InitMatchDB();
        await Database.OpenAsync();
        var selectCmd = Database.CreateCommand();
        selectCmd.CommandText =
            @$"SELECT
            {Match.MatchFieldsWithId()}
            FROM Match 
            WHERE MatchNumber = @match";
        selectCmd.Parameters.AddWithValue("@match", match);
        await using var reader = await selectCmd.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return Match.FromReader(reader);
        }
        return null;
    }

    //public async Task<Match?> GetMatchByIdAsync(int id)
    //{
    //    await InitMatchDB();
    //    await Database.OpenAsync();
    //    var selectCmd = Database.CreateCommand();
    //    selectCmd.CommandText =
    //        @$"SELECT
    //        {Match.MatchFieldsWithId()}
    //        FROM Match 
    //        WHERE Id = @id";
    //    selectCmd.Parameters.AddWithValue("@id", id);
    //    await using var reader = await selectCmd.ExecuteReaderAsync();
    //    if (await reader.ReadAsync())
    //    {
    //        return Match.FromReader(reader);
    //    }
    //    return null;
    //}

    public async Task<int> SaveMatchItemAsync(Match item)
    {
        await InitMatchDB();
        await Database.OpenAsync();
        var cmd = Database.CreateCommand();
        if (item.Id != 0)
        {
            cmd.CommandText = item.UpdateCommand();
        }
        else
        {
            var oldItem = await GetMatchAsync(item.MatchNumber);
            if (oldItem != null)
            {
                item.Id = oldItem.Id;
                item.Uuid = oldItem.Uuid;
                if (!string.IsNullOrWhiteSpace(oldItem.AirtableId))
                {
                    item.AirtableId = oldItem.AirtableId;
                }
                cmd.CommandText = item.UpdateCommand();
            }
            else
            {
                cmd.CommandText = item.AddCommand();
            }
        }
        var count = await cmd.ExecuteNonQueryAsync();
        Database.Close();
        return count;
    }

    //public async Task<int> DeleteMatchItemAsync(int match)
    //{
    //    await InitMatchDB();
    //    await Database.OpenAsync();
    //    var cmd = Database.CreateCommand();
    //    cmd.CommandText =
    //        @$"DELETE FROM Match 
    //        WHERE MatchNumber = @match";
    //    cmd.Parameters.AddWithValue("@match", match);
    //    var count = await cmd.ExecuteNonQueryAsync();
    //    Database.Close();
    //    return count;
    //}
}
