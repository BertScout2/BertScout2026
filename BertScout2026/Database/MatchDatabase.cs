using BertScout2026.Models;
using Microsoft.Data.Sqlite;

namespace BertScout2026.Database;

public class MatchDatabase : BaseDatabase
{
    private const string MatchDBFilename = "MatchScout2026.db3";
    private SqliteConnection? Database;
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
            _databasePath = $"Data Source={Path.Combine(DatabasePath, MatchDBFilename)}";
            Database = new SqliteConnection(_databasePath);
            await Database.OpenAsync();
            var createTableCmd = Database.CreateCommand();
            createTableCmd.CommandText = Match.CreateTableCommand();
            await createTableCmd.ExecuteNonQueryAsync();
            Database.Close();
            _created = true;
        }
        catch (Exception ex)
        {
            throw new SystemException($"Error initializing Match database: {_databasePath}\r\n{ex.Message}");
        }
    }

    /*
    public async Task<List<Match>> GetMatchItemsAsync()
    {
        await InitMatchDB();
        return await Database!.Table<Match>()
            .ToListAsync();
    }

    public async Task<List<Match>> GetChangedMatchItemsAsync()
    {
        await InitMatchDB();
        return await Database!.Table<Match>()
            .Where(i => i.Changed)
            .ToListAsync();
    }

    public async Task<List<Match>> GetTeamAllMatches(int team)
    {
        await InitMatchDB();
        return await Database!.Table<Match>()
            .Where(i => i.TeamNumber == team)
            .OrderBy(i => i.MatchNumber)
            .ToListAsync();
    }
    */

    public async Task<Match?> GetMatchAsync(int match, int team)
    {
        await InitMatchDB();
        await Database!.OpenAsync();
        var selectCmd = Database!.CreateCommand();
        selectCmd.CommandText =
            @$"SELECT
            {Match.MatchFields()}
            FROM Match 
            WHERE TeamNumber = @team AND MatchNumber = @match";
        selectCmd.Parameters.AddWithValue("@team", team);
        selectCmd.Parameters.AddWithValue("@match", match);
        await using var reader = await selectCmd.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return Match.FromReader(reader);
        }
        return null;
    }

    public async Task<Match?> GetMatchByIdAsync(int id)
    {
        await InitMatchDB();
        await Database!.OpenAsync();
        var selectCmd = Database!.CreateCommand();
        selectCmd.CommandText =
            @$"SELECT
            {Match.MatchFields()}
            FROM Match 
            WHERE Id = @id";
        selectCmd.Parameters.AddWithValue("@id", id);
        await using var reader = await selectCmd.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return Match.FromReader(reader);
        }
        return null;
    }

    public async Task<int> SaveMatchItemAsync(Match item)
    {
        await InitMatchDB();
        await Database!.OpenAsync();
        var cmd = Database!.CreateCommand();
        if (item.Id != 0)
        {
            cmd.CommandText = item.UpdateCommand();
        }
        else
        {
            var oldItem = await GetMatchAsync(item.MatchNumber, item.TeamNumber);
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

    /*
    public async Task DeleteMatchItemAsync(int match, int team)
    {
        await InitMatchDB();
        var item = await Database!.Table<Match>()
            .Where(i => i.TeamNumber == team && i.MatchNumber == match)
            .FirstOrDefaultAsync();
        if (item != null)
        {
            await Database.DeleteAsync(item);
        }
    }
    */
}
