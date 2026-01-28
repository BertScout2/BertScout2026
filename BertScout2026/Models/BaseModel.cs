using SQLite;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace BertScout2026.Models
{
    public class BaseModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Indexed(Unique = true)]
        public string Uuid { get; set; } = "";

        public string AirtableId { get; set; } = "";

        public bool Changed { get; set; }

        protected readonly JsonSerializerOptions WriteOptions = new()
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };
    }
}
