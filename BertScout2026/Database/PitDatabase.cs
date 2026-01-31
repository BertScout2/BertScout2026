using BertScout2026.Models;
using SQLite;

namespace BertScout2026.Database;

public class PitDatabase
{
    public string PitDBPath { get; set; } = "";

    public const string PitDBFilename = "PitScout2026.db3";

    private const SQLiteOpenFlags Flags =
        // open the database in read/write mode
        SQLiteOpenFlags.ReadWrite |
        // create the database if it doesn't exist
        SQLiteOpenFlags.Create |
        // enable multi-threaded database access
        SQLiteOpenFlags.SharedCache;

    private bool _created = false;
    private SQLiteAsyncConnection Database = new("");

    public PitDatabase()
    {
#if ANDROID
            if (Directory.Exists("/sdcard/Documents"))
            {
                PitDBPath = "/sdcard/Documents";
            }
#elif WINDOWS
            if (!Directory.Exists("C:\\Temp"))
            {
                Directory.CreateDirectory("C:\\Temp");
            }
            PitDBPath = "C:\\Temp";
#endif
        PitDBPath ??= FileSystem.AppDataDirectory;
    }

    private async Task InitPitDB()
    {
        if (_created)
        {
            return;
        }
        var databasePath = Path.Combine(PitDBPath, PitDBFilename);
        try
        {
            Database = new(databasePath, Flags);
            await Database.CreateTableAsync<Match>();
            await Database.CreateTableAsync<Pit>();
            _created = true;
        }
        catch (Exception ex)
        {
            throw new SystemException($"Error initializing Pit database: {databasePath}\r\n{ex.Message}");
        }
    }

    //public async Task<List<Pit>> GetPitItemsAsync()
    //{
    //    await InitPitDB();
    //    return await Database.Table<Pit>()
    //        .ToListAsync();
    //}

    //public async Task<List<Pit>> GetChangedPitItemsAsync()
    //{
    //    await InitPitDB();
    //    return await Database.Table<Pit>()
    //        .Where(i => i.Changed)
    //        .ToListAsync();
    //}

    //public async Task<List<Pit>> GetTeamAllPites(int team)
    //{
    //    await InitPitDB();
    //    return await Database.Table<Pit>()
    //        .Where(i => i.TeamNumber == team)
    //        .OrderBy(i => i.PitNumber)
    //        .ToListAsync();
    //}

    //public async Task<Pit> GetPitAsync(int team)
    //{
    //    await InitPitDB();
    //    return await Database.Table<Pit>()
    //        .Where(i =>  i.TeamNumber == team)
    //        .FirstOrDefaultAsync();
    //}

    //public async Task<Pit> GetPitItemAsync(int id)
    //{
    //    await InitPitDB();
    //    return await Database.Table<Pit>()
    //        .Where(i => i.Id == id)
    //        .FirstOrDefaultAsync();
    //}

    //public async Task<int> SavePitItemAsync(Pit item)
    //{
    //    await InitPitDB();
    //    if (item.Id != 0)
    //    {
    //        return await Database.UpdateAsync(item);
    //    }
    //    var oldItem = await GetPitAsync(item.TeamNumber);
    //    if (oldItem != null)
    //    {
    //        item.Id = oldItem.Id;
    //        item.Uuid = oldItem.Uuid;
    //        // AirtableId may be updated in item, don't overwrite
    //        if (!string.IsNullOrWhiteSpace(oldItem.AirtableId))
    //            item.AirtableId = oldItem.AirtableId;
    //        return await Database.UpdateAsync(item);
    //    }
    //    item.Uuid = Guid.NewGuid().ToString();
    //    return await Database.InsertAsync(item);
    //}

    //public async Task DeletePitItemAsync(int team)
    //{
    //    await InitPitDB();
    //    var item = await Database.Table<Pit>()
    //        .Where(i => i.TeamNumber == team)
    //        .FirstOrDefaultAsync();
    //    if (item != null)
    //    {
    //        await Database.DeleteAsync(item);
    //    }
    //}
}
