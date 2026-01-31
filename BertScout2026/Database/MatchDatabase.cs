using BertScout2026.Models;
using SQLite;

namespace BertScout2026.Database;

public class MatchDatabase
{
    public string MatchDBPath { get; set; } = "";

    private const string MatchDBFilename = "MatchScout2026.db3";

    private const SQLiteOpenFlags Flags =
        // open the database in read/write mode
        SQLiteOpenFlags.ReadWrite |
        // create the database if it doesn't exist
        SQLiteOpenFlags.Create |
        // enable multi-threaded database access
        SQLiteOpenFlags.SharedCache;

    private bool _created = false;
    private SQLiteAsyncConnection Database = new("");

    public MatchDatabase()
    {
#if ANDROID
        if (Directory.Exists("/sdcard/Documents"))
        {
            MatchDBPath = "/sdcard/Documents";
        }
#elif WINDOWS
        if (!Directory.Exists("C:\\Temp"))
        {
            Directory.CreateDirectory("C:\\Temp");
        }
        MatchDBPath = "C:\\Temp";
#endif
        MatchDBPath ??= FileSystem.AppDataDirectory;
    }

    private async Task InitMatchDB()
    {
        if (_created)
        {
            return;
        }
        var databasePath = Path.Combine(MatchDBPath, MatchDBFilename);
        try
        {
            Database = new(databasePath, Flags);
            await Database.CreateTableAsync<Match>();
            await Database.CreateTableAsync<Pit>();
            _created = true;
        }
        catch (Exception ex)
        {
            throw new SystemException($"Error initializing Match database: {databasePath}\r\n{ex.Message}");
        }
    }

    public async Task<List<Match>> GetMatchItemsAsync()
    {
        await InitMatchDB();
        return await Database.Table<Match>()
            .ToListAsync();
    }

    public async Task<List<Match>> GetChangedMatchItemsAsync()
    {
        await InitMatchDB();
        return await Database.Table<Match>()
            .Where(i => i.Changed)
            .ToListAsync();
    }

    public async Task<List<Match>> GetTeamAllMatches(int team)
    {
        await InitMatchDB();
        return await Database.Table<Match>()
            .Where(i => i.TeamNumber == team)
            .OrderBy(i => i.MatchNumber)
            .ToListAsync();
    }

    public async Task<Match> GetMatchAsync(int match, int team)
    {
        await InitMatchDB();
        return await Database.Table<Match>()
            .Where(i => i.MatchNumber == match && i.TeamNumber == team)
            .FirstOrDefaultAsync();
    }

    public async Task<Match> GetMatchItemAsync(int id)
    {
        await InitMatchDB();
        return await Database.Table<Match>()
            .Where(i => i.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<int> SaveMatchItemAsync(Match item)
    {
        await InitMatchDB();
        if (item.Id != 0)
        {
            return await Database.UpdateAsync(item);
        }
        var oldItem = await GetMatchAsync(item.MatchNumber, item.TeamNumber);
        if (oldItem != null)
        {
            item.Id = oldItem.Id;
            item.Uuid = oldItem.Uuid;
            // AirtableId may be updated in item, don't overwrite
            if (!string.IsNullOrWhiteSpace(oldItem.AirtableId))
                item.AirtableId = oldItem.AirtableId;
            return await Database.UpdateAsync(item);
        }
        item.Uuid = Guid.NewGuid().ToString();
        return await Database.InsertAsync(item);
    }

    public async Task DeleteMatchItemAsync(int match, int team)
    {
        await InitMatchDB();
        var item = await Database.Table<Match>()
            .Where(i => i.TeamNumber == team && i.MatchNumber == match)
            .FirstOrDefaultAsync();
        if (item != null)
        {
            await Database.DeleteAsync(item);
        }
    }
}
