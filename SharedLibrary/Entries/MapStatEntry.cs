using System.Text.Json.Serialization;

namespace SharedLibrary.Entries
{
    public class MapStatEntry(string name)
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = name;

        [JsonPropertyName("tWin")]
        public int TWin { get; set; }

        [JsonPropertyName("ctWin")]
        public int CTWin { get; set; }

        [JsonPropertyName("lastPlayed")]
        public DateTime LastPlayed { get; set; }
    }
}
