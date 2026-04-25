using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary
{
    public static class WeaponHelper
    {
        //3 missing weapons from api: "weapon_m4a1_silencer" == weapon_m4a1, "weapon_hkp2000" == "weapon_usp_silencer", "weapon_deagle" == "weapon_revolver"
        public static string[] AllWeapon = { "weapon_g3sg1", "weapon_scar20", "weapon_ssg08", "weapon_awp", "weapon_famas", "weapon_galilar", "weapon_ak47", "weapon_m4a1", "weapon_aug", "weapon_sg556", "weapon_bizon", "weapon_mac10", "weapon_mp5sd", "weapon_mp7", "weapon_mp9", "weapon_p90", "weapon_ump45", "weapon_m249", "weapon_negev", "weapon_mag7", "weapon_nova", "weapon_sawedoff", "weapon_xm1014", "weapon_cz75a", "weapon_deagle", "weapon_fiveseven", "weapon_elite", "weapon_glock", "weapon_hkp2000", "weapon_p250", "weapon_tec9" };

        // weapon?.DesignerName => ami epp nalad van, hiaba vettel fel barmit
        // EventItemPickup @event.Item: {@event.Item}") => weapon_
        enum Grenade
        {
            weapon_molotov,
            weapon_incgrenade,
            weapon_decoy,
            weapon_flashbang,
            weapon_hegrenade,
            weapon_smokegrenade
        }

        enum Knife
        {
            weapon_knife
        }

        enum Pistol
        {
            weapon_glock,
            weapon_hkp2000,
            weapon_usp_silencer,
            weapon_elite,
            weapon_p250,
            weapon_tec9,
            weapon_cz75a,
            weapon_fiveseven,
            weapon_deagle,
            weapon_revolver
        }

        enum Zeus
        {
            weapon_taser
        }

        enum SubMachineGun
        {
            weapon_mac10,
            weapon_mp9,
            weapon_mp7,
            weapon_mp5sd,
            weapon_ump45,
            weapon_bizon,
            weapon_p90
        }

        enum Rifle
        {
            weapon_famas,
            weapon_galilar,
            weapon_ak47,
            weapon_m4a1,
            weapon_aug,
            weapon_sg556
        }

        enum Sniper
        {
            weapon_g3sg1,
            weapon_scar20,
            weapon_ssg08,
            weapon_awp
        }

        enum MachineGun
        {
            weapon_bizon,
            weapon_mac10,
            weapon_mp5sd,
            weapon_mp7,
            weapon_mp9,
            weapon_p90,
            weapon_ump45,
            weapon_m249,
            weapon_negev
        }

        enum Shotgun
        {
            weapon_mag7,
            weapon_nova,
            weapon_sawedoff,
            weapon_xm1014
        }

        public enum WeaponSlot
        {
            Primary,
            Secondary,
            Knife,
            Grenade,
            Unknown
        }

        public static WeaponSlot GetWeaponSlot(string weapon)
        {
            if (Enum.TryParse<SubMachineGun>(weapon, out _)
                || Enum.TryParse<Rifle>(weapon, out _)
                || Enum.TryParse<Sniper>(weapon, out _)
                || Enum.TryParse<MachineGun>(weapon, out _)
                || Enum.TryParse<Shotgun>(weapon, out _))
                return WeaponSlot.Primary;

            if (Enum.TryParse<Pistol>(weapon, out _))
                return WeaponSlot.Secondary;

            if (Enum.TryParse<Knife>(weapon, out _)
                || Enum.TryParse<Zeus>(weapon, out _))
                return WeaponSlot.Knife;

            if (Enum.TryParse<Grenade>(weapon, out _))
                return WeaponSlot.Grenade;

            return WeaponSlot.Unknown;
        }
    }
}
