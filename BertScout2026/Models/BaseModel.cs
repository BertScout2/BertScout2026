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
            @"Id INTEGER PRIMARY KEY AUTOINCREMENT
            , Uuid TEXT NOT NULL
            , AirtableId TEXT
            , Changed INTEGER";
    }

    public static string BaseFieldsWithID()
    {
        return
            @$"Id
            , {BaseFields()}";
    }

    public static string BaseFields()
    {
        return
            @"Uuid
            , AirtableId
            , Changed";
    }

    public string BaseAddValues()
    {
        return
            @$"'{Uuid ?? Guid.NewGuid().ToString()}'
            , {"'" + AirtableId + "'" ?? "NULL"}
            , {(Changed ? 1 : 0)}";
    }

    public string BaseUpdateValues()
    {
        return
            @$"AirtableId = {(AirtableId != null ? "'" + AirtableId + "'" : "NULL")}
            , Changed = {(Changed ? 1 : 0)},";
    }
}
