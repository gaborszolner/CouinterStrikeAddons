using CounterStrikeSharp.API;
using SharedLibrary.Entries;
using System.Text.Json;

namespace SharedLibrary
{
    public class Utils
    {
        public static DateTime GetServerTime()
        {
            TimeZoneInfo tz;

            if (OperatingSystem.IsWindows())
            {
                tz = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
            }
            else
            {
                tz = TimeZoneInfo.FindSystemTimeZoneById("Europe/Budapest");
            }

            DateTime time = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tz);
            return time;
        }

        public static T? ReadStoredStat<T>(string filePath)
        {
            T? returnStat = default;
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                try
                {
                    var deserialized = JsonSerializer.Deserialize<T>(json);

                if (deserialized is not null)
                {
                    returnStat = deserialized;
                }
                }
                catch (Exception)
                {
                    Server.PrintToConsole($"Failed to deserialize file: {json}");
                }
            }
            else
            {
                File.WriteAllText(filePath, "{}");
            }

            return returnStat;
        }

        public static Dictionary<string, T>? LoadDataFromFile<T>(string filePath)
        {
            Dictionary<string, T>? returnStat = default;

            if (File.Exists(filePath))
            {
                string? json = File.ReadAllText(filePath);
                try
                {
                    var deserialized = JsonSerializer.Deserialize<Dictionary<string, T>>
                        (json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    if (deserialized is not null)
                    {
                        returnStat = deserialized;
                    }
                    
                }
                catch (Exception)
                {
                    Server.PrintToConsole($"Failed to deserialize file: {json}");
                    return null;
                }
            }
            else
            {
                File.WriteAllText(filePath, "{}");
            }

            return returnStat;
        }

        public static void WriteToFile<T>(T? entry, string fileName)
        {
            if (entry is null)
            {
                return;
            }

            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                File.WriteAllText(fileName, JsonSerializer.Serialize(entry, options));
            }
            catch (Exception)
            {
                Server.PrintToConsole($"Error writing to file {fileName}");
            }
        }
    }
}
