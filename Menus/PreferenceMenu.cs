using Kitchen;
using Kitchen.Modules;
using KitchenLib;
using UnityEngine;

namespace Blocked.Menus
{
    public class PreferenceMenu : KLMenu<PauseMenuAction>
    {
        public PreferenceMenu(Transform container, ModuleList module_list) : base(container, module_list) { }

        public override void Setup(int player_id)
        {
            AddLabel("Blocked");
            
            AddButton("In-Game Players", delegate (int i)
            {
                RequestSubMenu(typeof(CurrentPlayersMenu));
            }, 0, 1f, 0.2f);

            AddButton("Blocked Players", delegate (int i)
            {
                RequestSubMenu(typeof(BlockedMenu));
            }, 0, 1f, 0.2f);

            New<SpacerElement>(true);
            New<SpacerElement>(true);

            AddButton(Localisation["MENU_BACK_SETTINGS"], delegate (int i)
            {
                RequestPreviousMenu();
            }, 0, 1f, 0.2f);
        }
    }
}