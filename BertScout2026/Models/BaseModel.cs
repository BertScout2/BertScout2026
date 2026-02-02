using System.Text.Encodings.Web;
using System.Text.Json;

namespace BertScout2026.Models;

public class BaseModel
{
    public int Id { get; set; }

    public string? Uuid { get; set; }

    public string? AirtableId { get; set; }

    public bool Changed { get; set; }

    protected readonly JsonSerializerOptions WriteOptions = new()
    {
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    public static string BaseCreateTableFields()
    {
        return
            @"Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Uuid TEXT NOT NULL,
                AirtableId TEXT,
                Changed INTEGER,";
    }

    public static string BaseFields()
    {
        return
                @"Uuid,
                AirtableId,
                Changed,";
    }

    public string BaseFieldValues()
    {
        return
                @$"'{Uuid ?? Guid.NewGuid().ToString()}',
                {"'" + AirtableId + "'" ?? "NULL"},
                {(Changed ? 1 : 0)},";
    }
}
