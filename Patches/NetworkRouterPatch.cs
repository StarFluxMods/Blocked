using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using Kitchen;
using Kitchen.NetworkSupport;
using Kitchen.Transports;
using KitchenLib.Preferences;
using KitchenLib.Utils;

namespace Blocked.Patches
{
    [HarmonyPatch(typeof(NetworkRouter), "OnReceive")]
    public class NetworkRouterPatch
    {
        private static FieldInfo _SeenTargets = ReflectionUtils.GetField<NetworkRouter>("SeenTargets");
        static void Postfix(NetworkRouter __instance, INetworkTarget source)
        {
            if (source is not SteamNetworkTarget target) return;
            if (!Mod.manager.GetPreference<PreferenceDictionary<ulong, string>>("BLOCKED_USERS").Value.ContainsKey(target.ID.Value)) return;
            List<INetworkTarget> SeenTargets = (List<INetworkTarget>)_SeenTargets.GetValue(__instance);
            SeenTargets.Remove(target);
            _SeenTargets.SetValue(__instance, SeenTargets);
            foreach (INetworkTransport networkTransport in __instance.Transports)
            {
                if (networkTransport.SendStatus == TransportSendStatus.Ready)
                {
                    networkTransport.RequestKickUser(target);
                }
            }
        }
    }
}