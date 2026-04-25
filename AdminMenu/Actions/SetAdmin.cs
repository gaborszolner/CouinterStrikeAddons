using AdminMenu.Entries;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Menu;
using SharedLibrary;

namespace AdminMenu
{
    public partial class AdminMenu : BasePlugin
    {
        private void SetAdminAction(CCSPlayerController adminPlayer, ChatMenuOption option)
        {
            ShowPlayerListMenu(adminPlayer, false, false, (CCSPlayerController targetPlayer) =>
            {
                var setAdminMenu = new CenterHtmlMenu($"Set admin level for {targetPlayer.PlayerName}", this);
                setAdminMenu.AddMenuOption("Level 1 (Lowest)", (controller, _) =>
                {
                    SetAdminLevel(adminPlayer, targetPlayer, 1);
                });
                setAdminMenu.AddMenuOption("Level 2", (controller, _) =>
                {
                    SetAdminLevel(adminPlayer, targetPlayer, 2);
                });
                setAdminMenu.AddMenuOption("Level 3 (Highest)", (controller, _) =>
                {
                    SetAdminLevel(adminPlayer, targetPlayer, 3);
                });
                setAdminMenu.AddMenuOption("Delete admin", (controller, _) =>
                {
                    SetAdminLevel(adminPlayer, targetPlayer, 0);
                });
                setAdminMenu.PostSelectAction = PostSelectAction.Close;
                MenuManager.OpenCenterHtmlMenu(this, adminPlayer, setAdminMenu);
            });
        }

        private static void SetAdminLevel(CCSPlayerController adminPlayer, CCSPlayerController targetPlayer, int adminLevel)
        {
            if (targetPlayer == null || targetPlayer.AuthorizedSteamID == null)
            {
                return;
            }

            UpdateAdminConfig(targetPlayer, adminLevel);

            targetPlayer.PrintToChat($"Your admin level has been set to {adminLevel}.");
            MenuManager.GetActiveMenu(adminPlayer)?.Close();
        }

        private static void UpdateAdminConfig(CCSPlayerController targetPlayer, int adminLevel)
        {
            if (targetPlayer == null || targetPlayer.AuthorizedSteamID == null)
            {
                return;
            }

            var newEntry = new AdminEntry
            {
                Name = targetPlayer.PlayerName,
                Identity = targetPlayer.AuthorizedSteamID.SteamId2,
                Level = adminLevel
            };

            string steamId = targetPlayer.AuthorizedSteamID.SteamId2;
            _adminEntry ??= [];
            _adminEntry[steamId] = newEntry;

            Utils.WriteToFile(_adminEntry, _adminsFilePath);
        }
    }
}
