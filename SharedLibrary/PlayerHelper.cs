using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Utils;

namespace SharedLibrary
{
    public static class PlayerHelper
    {
        public static bool HasEnoughPlayer => GetAllNonSpecPlayers().Count() >= 4;

        public static IEnumerable<CCSPlayerController> GetAllPlayers()
        {
            return Utilities.GetPlayers().Where(p => p.IsValid && !p.IsBot);
        }

        public static IEnumerable<CCSPlayerController> GetAllNonSpecPlayers()
        {
            return Utilities.GetPlayers().Where(p => p.IsValid && !p.IsBot && p.Team != CsTeam.Spectator && p.Team != CsTeam.None);
        }

        public static IEnumerable<CCSPlayerController> GetAllTerrorist()
        {
            return Utilities.GetPlayers().Where(p => p.IsValid && !p.IsBot && p.Team == CsTeam.Terrorist);
        }

        public static IEnumerable<CCSPlayerController> GetAllCounterTerrorist()
        {
            return Utilities.GetPlayers().Where(p => p.IsValid && !p.IsBot && p.Team == CsTeam.CounterTerrorist);
        }
    }
}
