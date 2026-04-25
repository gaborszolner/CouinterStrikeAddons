using System.Text.Json.Serialization;

namespace SharedLibrary.Entries
{
    public class PlayerStatEntry(string identity, string name, int kill = 0, int dead = 0, int selfKill = 0, int teamKill = 0, int assister = 0)
    {
        [JsonPropertyName("identity")]
        public string Identity { get; set; } = identity;

        [JsonPropertyName("name")]
        public string Name { get; set; } = name;

        [JsonPropertyName("kill")]
        public int Kill { get; set; } = kill;

        [JsonPropertyName("dead")]
        public int Dead { get; set; } = dead;

        [JsonPropertyName("selfkill")]
        public int SelfKill { get; set; } = selfKill;

        [JsonPropertyName("teamkill")]
        public int TeamKill { get; set; } = teamKill;

        [JsonPropertyName("assister")]
        public int Assister { get; set; } = assister;

        public double Score => (Kill + (0.5 * Assister) - SelfKill - (0.5 * TeamKill)) / (Dead + 1);
    }
}
