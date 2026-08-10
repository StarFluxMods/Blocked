using System;
using Controllers;
using Kitchen;
using Kitchen.Modules;
using Kitchen.Transports;
using KitchenLib;
using UnityEngine;

namespace Blocked.Menus
{
    public class CurrentPlayersMenu : KLMenu<MenuAction>
    {
        public CurrentPlayersMenu(Transform container, ModuleList module_list) : base(container, module_list) { }

        public override void Setup(int player_id)
        {
            AddLabel("Select a player to block");
            
            New<SpacerElement>(true);

            foreach (PlayerInfo playerInfo in Players.Main.All())
            {
                if (GetSteamTarget(playerInfo, out SteamNetworkTarget target))
                {
                    AddButton($"Block {playerInfo.Name} ({playerInfo.SecondaryName})", delegate
                    {
                        ConfirmBlock.confirmedPlayer = playerInfo;
                        RequestSubMenu(typeof(ConfirmBlock));
                    });
                }
            }

            New<SpacerElement>(true);
            New<SpacerElement>(true);

            AddButton(Localisation["MENU_BACK_SETTINGS"], delegate (int i)
            {
                RequestPreviousMenu();
            }, 0, 1f, 0.2f);
        }

        private bool GetSteamTarget(PlayerInfo playerInfo, out SteamNetworkTarget target)
        {
            target = null;
            foreach (ValueTuple<SourceIdentifier, NetworkPeerInformation> tuple in Session.NetworkPeers)
            {
                if (tuple.Item1 == playerInfo.Identifier && tuple.Item2.Target is SteamNetworkTarget _target)
                {
                    target = _target;
                    return true;
                }
            }
            return false;
        }
    }
}