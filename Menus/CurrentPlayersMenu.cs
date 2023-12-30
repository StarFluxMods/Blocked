using System.Collections.Generic;
using Kitchen;
using Kitchen.Modules;
using Kitchen.NetworkSupport;
using KitchenLib;
using UnityEngine;

namespace Blocked.Menus
{
    public class CurrentPlayersMenu : KLMenu<PauseMenuAction>
    {
        public CurrentPlayersMenu(Transform container, ModuleList module_list) : base(container, module_list) { }

        public override void Setup(int player_id)
        {
            AddLabel("Select a player to block");
            
            New<SpacerElement>(true);
            
            using (List<PlayerInfo>.Enumerator enumerator = Players.Main.All().GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    PlayerInfo playerInfo = enumerator.Current;
                    if (playerInfo.Connection == ConnectionType.Steam)
                    {
                        AddButton("Block " + playerInfo.Name, delegate (int i)
                        {
                            ConfirmBlock.confirmedPlayer = playerInfo;
                            RequestSubMenu(typeof(ConfirmBlock));
                        }, 0, 1f, 0.2f);
                    }
                }
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