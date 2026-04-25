using AdminMenu.Entries;
using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Menu;
using CounterStrikeSharp.API.ValveConstants.Protobuf;
using Microsoft.Extensions.Logging;
using SharedLibrary;
using System.Text.Json;

namespace AdminMenu
{
    public partial class AdminMenu : BasePlugin
    {
        private void BanAction(CCSPlayerController adminPlayer, ChatMenuOption option)
        {
            ShowPlayerListMenu(adminPlayer, false, false, (CCSPlayerController targetPlayer) =>
            {
                ChooseBanTimePlayer(adminPlayer, targetPlayer);
            });
        }

        private void ChooseBanTimePlayer(CCSPlayerController adminPlayer, CCSPlayerController player)
        {
            if (player == null || player.AuthorizedSteamID == null)
            {
                Logger?.LogError("Player or SteamID is invalid.");
                return;
            }

            var banTimeMenu = new CenterHtmlMenu($"Expiration time?", this);
            banTimeMenu.AddMenuOption("1 min", (CCSPlayerController controller, ChatMenuOption option) =>
            {
                BanPlayer(adminPlayer, player, Utils.GetServerTime().AddMinutes(1));
            });
            banTimeMenu.AddMenuOption("10 min", (CCSPlayerController controller, ChatMenuOption option) =>
            {
                BanPlayer(adminPlayer, player, Utils.GetServerTime().AddMinutes(10));
            });
            banTimeMenu.AddMenuOption("1 day", (CCSPlayerController controller, ChatMenuOption option) =>
            {
                BanPlayer(adminPlayer, player, Utils.GetServerTime().AddDays(1));
            });
            banTimeMenu.AddMenuOption("1 week", (CCSPlayerController controller, ChatMenuOption option) =>
            {
                BanPlayer(adminPlayer, player, Utils.GetServerTime().AddDays(7));
            });
            banTimeMenu.AddMenuOption("Permanent", (CCSPlayerController controller, ChatMenuOption option) =>
            {
                BanPlayer(adminPlayer, player, DateTime.MaxValue);
            });

            banTimeMenu.PostSelectAction = PostSelectAction.Close;
            MenuManager.OpenCenterHtmlMenu(this, adminPlayer, banTimeMenu);
        }

        private void BanPlayer(CCSPlayerController adminPlayer, CCSPlayerController player, DateTime banTime)
        {
            try
            {
                string steamId = player.AuthorizedSteamID.SteamId2;
                var bannedList = new Dictionary<string, BannedEntry>();

                if (File.Exists(_bannedFilePath))
                {
                    string json = File.ReadAllText(_bannedFilePath);
                    bannedList = JsonSerializer.Deserialize<Dictionary<string, BannedEntry>>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }) ?? new Dictionary<string, BannedEntry>();
                }

                var newEntry = new BannedEntry
                {
                    Identity = steamId,
                    Name = player.PlayerName,
                    BannedBy = adminPlayer.PlayerName,
                    Expiration = banTime
                };

                _bannedEntry ??= [];
                _bannedEntry.Add(steamId, newEntry);
                bannedList.Add(steamId, newEntry);
                Utils.WriteToFile(bannedList, _bannedFilePath);

                player.Disconnect(NetworkDisconnectionReason.NETWORK_DISCONNECT_KICKBANADDED);
                Server.PrintToChatAll($"{PluginPrefix} {player.PlayerName} has been banned by {adminPlayer.PlayerName} until {banTime}.");
                Logger?.LogInformation($"{PluginPrefix} {player.PlayerName} has been banned by {adminPlayer.PlayerName} until {banTime}.");
            }
            catch (Exception ex)
            {
                Logger?.LogError($"Error banning player: {ex.Message}");
            }
        }
    }
}