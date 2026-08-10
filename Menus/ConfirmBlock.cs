using System;
using Controllers;
using Kitchen;
using Kitchen.Modules;
using Kitchen.Transports;
using KitchenLib;
using KitchenLib.Preferences;
using UnityEngine;

namespace Blocked.Menus
{
    public class ConfirmBlock : KLMenu<MenuAction>
    {
        public ConfirmBlock(Transform container, ModuleList module_list) : base(container, module_list) { }

        public static PlayerInfo confirmedPlayer;
        
        public override void Setup(int player_id)
        {
            AddLabel("Are you sure you want to block " + confirmedPlayer.SecondaryName + "?");
            
            New<SpacerElement>(true);
            
            AddButton("Yes", delegate (int i)
            {
                foreach (ValueTuple<SourceIdentifier, NetworkPeerInformation> tuple in Session.NetworkPeers)
                {
                    if (tuple.Item1 == confirmedPlayer.Identifier && tuple.Item2.Target is SteamNetworkTarget target)
                    {
                        Mod.manager.GetPreference<PreferenceDictionary<ulong, string>>("BLOCKED_USERS").Value.Add(target.ID.Value, confirmedPlayer.SecondaryName);
                        Mod.manager.Save();
                        Session.KickRequests.Add(confirmedPlayer.Identifier);
                        InputSourceIdentifier.DefaultInputSource.MakeRequest(confirmedPlayer.ID, GameStateRequest.Disconnect);
                    }
                }
                RequestPreviousMenu();
            }, 0, 1f, 0.2f);
            
            AddButton("No", delegate (int i)
            {
                RequestPreviousMenu();
            }, 0, 1f, 0.2f);
        }
    }
}