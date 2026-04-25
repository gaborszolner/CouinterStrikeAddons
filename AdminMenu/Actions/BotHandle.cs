using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Entities;
using CounterStrikeSharp.API.Modules.Menu;

namespace AdminMenu
{
    public partial class AdminMenu : BasePlugin
    {
        private void BotHandleAction(CCSPlayerController adminPlayer, ChatMenuOption option)
        {
            var botMenu = new CenterHtmlMenu($"Choose bot action", this);

            botMenu.AddMenuOption("Kick All", (controller, _) =>
            {
                Server.PrintToChatAll($"{PluginPrefix} All bots have been kicked by {adminPlayer.PlayerName}.");
                Server.ExecuteCommand("bot_kick all");
            });
            botMenu.AddMenuOption("Add T", (controller, _) =>
            {
                Server.PrintToChatAll($"{PluginPrefix} Terrorist bot has been added by {adminPlayer.PlayerName}.");
                Server.ExecuteCommand("bot_add_t");
            });
            botMenu.AddMenuOption("Add CT", (controller, _) =>
            {
                Server.PrintToChatAll($"{PluginPrefix} CounterTerrorist bot has been added by {adminPlayer.PlayerName}.");
                Server.ExecuteCommand("bot_add_ct");
            });

            MenuManager.OpenCenterHtmlMenu(this, adminPlayer, botMenu);
        }
    }
}
