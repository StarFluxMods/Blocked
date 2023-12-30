using Kitchen;
using Kitchen.Modules;
using KitchenLib;
using KitchenLib.Preferences;
using UnityEngine;

namespace Blocked.Menus
{
    public class ConfirmUnblock : KLMenu<PauseMenuAction>
    {
        public ConfirmUnblock(Transform container, ModuleList module_list) : base(container, module_list) { }

        public static (ulong, string) confirmedPlayer;
        
        public override void Setup(int player_id)
        {
            AddLabel("Are you sure you want to unblock " + confirmedPlayer.Item2 + "?");
            
            New<SpacerElement>(true);
            
            AddButton("Yes", delegate (int i)
            {
                Mod.manager.GetPreference<PreferenceDictionary<ulong, string>>("BLOCKED_USERS").Value.Remove(confirmedPlayer.Item1);
                Mod.manager.Save();
                RequestPreviousMenu();
            }, 0, 1f, 0.2f);
            
            AddButton("No", delegate (int i)
            {
                RequestPreviousMenu();
            }, 0, 1f, 0.2f);
        }
    }
}