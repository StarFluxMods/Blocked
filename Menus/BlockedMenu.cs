using System.Collections.Generic;
using Kitchen;
using Kitchen.Modules;
using KitchenLib;
using KitchenLib.Preferences;
using UnityEngine;

namespace Blocked.Menus
{
    public class BlockedMenu : KLMenu<PauseMenuAction>
    {
        public BlockedMenu(Transform container, ModuleList module_list) : base(container, module_list) { }

        public override void Setup(int player_id)
        {
            AddLabel("Select a player to unblock");
            
            Dictionary<ulong, string> Blocked = Mod.manager.GetPreference<PreferenceDictionary<ulong, string>>("BLOCKED_USERS").Value;
            
            foreach (ulong steamid in Blocked.Keys)
            {
                AddButton("Unlock " + Blocked[steamid], delegate (int i)
                {
                    ConfirmUnblock.confirmedPlayer = (steamid, Blocked[steamid]);
                    RequestSubMenu(typeof(ConfirmUnblock));
                }, 0, 1f, 0.2f);
            }
            
            New<SpacerElement>(true);
            New<SpacerElement>(true);
            
            AddButton(Localisation["MENU_BACK_SETTINGS"], delegate (int i)
            {
                RequestPreviousMenu();
            }, 0, 1f, 0.2f);
        }
    }
}