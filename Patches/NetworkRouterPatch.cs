using System.Reflection;
using HarmonyLib;
using Kitchen;
using Kitchen.Transports;
using KitchenLib.Preferences;
using KitchenLib.Utils;

namespace Blocked.Patches
{
    [HarmonyPatch(typeof(NetworkRouter), "OnReceive")]
    public class NetworkRouterPatch
    {
        static void Postfix(NetworkRouter __instance, INetworkTarget source)
        {
            if (source is not SteamNetworkTarget target) return;
            if (!Mod.manager.GetPreference<PreferenceDictionary<ulong, string>>("BLOCKED_USERS").Value.ContainsKey(target.ID.Value)) return;
            MethodInfo removeClient = ReflectionUtils.GetMethod<NetworkRouter>("RemoveClient");
            removeClient.Invoke(__instance, new object[] { target });
        }
    }
}