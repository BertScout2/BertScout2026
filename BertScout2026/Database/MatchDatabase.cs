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

    public async Task<Match> GetMatchAsync(int match, int team)
    {
        await InitMatchDB();
        return await Database!.Table<Match>()
            .Where(i => i.MatchNumber == match && i.TeamNumber == team)
            .FirstOrDefaultAsync();
    }

    public async Task<Match> GetMatchItemAsync(int id)
    {
        await InitMatchDB();
        return await Database!.Table<Match>()
            .Where(i => i.Id == id)
            .FirstOrDefaultAsync();
    }
    */

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
            //var oldItem = await GetMatchAsync(item.MatchNumber, item.TeamNumber);
            //if (oldItem != null)
            //{
            //    item.Id = oldItem.Id;
            //    item.Uuid = oldItem.Uuid;
            //    // AirtableId may be updated in item, don't overwrite
            //    if (!string.IsNullOrWhiteSpace(oldItem.AirtableId))
            //        item.AirtableId = oldItem.AirtableId;
            //    return await Database!.UpdateAsync(item);
            //}
            //item.Uuid = Guid.NewGuid().ToString();
            cmd.CommandText = item.AddCommand();
        }
        await cmd.ExecuteNonQueryAsync();
        Database.Close();
        return 0;
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
