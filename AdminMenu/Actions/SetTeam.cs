using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Menu;
using CounterStrikeSharp.API.Modules.Utils;

namespace AdminMenu
{
    public partial class AdminMenu : BasePlugin
    {
        private void SetTeamAction(CCSPlayerController adminPlayer, ChatMenuOption option)
        {
            ShowPlayerListMenu(adminPlayer, false, false, (CCSPlayerController targetPlayer) =>
            {
                ShowTeamMenu(adminPlayer, targetPlayer);
            });
        }

        private void ShowTeamMenu(CCSPlayerController adminPlayer, CCSPlayerController targetPlayer)
        {
            var teamsMenu = new CenterHtmlMenu($"Choose team", this);
            int adminLevel = GetAdminLevel(adminPlayer);

            teamsMenu.AddMenuOption("Terrorist",
                (CCSPlayerController controller, ChatMenuOption option) =>
                {
                    targetPlayer.SwitchTeam(CsTeam.Terrorist);
                    Server.PrintToChatAll($"{PluginPrefix} {targetPlayer.PlayerName} assigned to {ChatColors.Red}Terrorist {ChatColors.Default}team by {adminPlayer.PlayerName}.");
                });

            if (adminLevel > 2)
            {
                teamsMenu.AddMenuOption("Terrorist + Respawn",
                (CCSPlayerController controller, ChatMenuOption option) =>
                {
                    targetPlayer.SwitchTeam(CsTeam.Terrorist); targetPlayer.Respawn();
                    Server.PrintToChatAll($"{PluginPrefix} {targetPlayer.PlayerName} assigned to {ChatColors.Red}Terrorist {ChatColors.Default}team and Respawned by {adminPlayer.PlayerName}.");
                });
            }

            teamsMenu.AddMenuOption("CounterTerrorist",
                (CCSPlayerController controller, ChatMenuOption option) =>
                {
                    targetPlayer.SwitchTeam(CsTeam.CounterTerrorist);
                    Server.PrintToChatAll($"{PluginPrefix} {targetPlayer.PlayerName} assigned to {ChatColors.Blue}CounterTerrorist {ChatColors.Default}team by {adminPlayer.PlayerName}.");
                });

            if (adminLevel > 2)
            {
                teamsMenu.AddMenuOption("CounterTerrorist + Respawn",
                (CCSPlayerController controller, ChatMenuOption option) =>
                {
                    targetPlayer.SwitchTeam(CsTeam.CounterTerrorist); targetPlayer.Respawn();
                    Server.PrintToChatAll($"{PluginPrefix} {targetPlayer.PlayerName} assigned to {ChatColors.Blue}CounterTerrorist {ChatColors.Default}team and Respawned by {adminPlayer.PlayerName}.");
                });
            }

            teamsMenu.AddMenuOption("Spectator",
                (CCSPlayerController controller, ChatMenuOption option) =>
                {
                    targetPlayer.ChangeTeam(CsTeam.Spectator);
                    Server.PrintToChatAll($"{PluginPrefix} {targetPlayer.PlayerName} assigned to {ChatColors.Grey}Spectator {ChatColors.Default}by {adminPlayer.PlayerName}.");
                });

            teamsMenu.PostSelectAction = PostSelectAction.Close;
            MenuManager.OpenCenterHtmlMenu(this, adminPlayer, teamsMenu);
        }
    }
}




