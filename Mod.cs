using System.Collections.Generic;
using KitchenLib;
using KitchenMods;
using System.Reflection;
using Blocked.Menus;
using Kitchen;
using Kitchen.Transports;
using KitchenLib.Preferences;
using KitchenLib.UI.PlateUp.PreferenceMenus;
using KitchenLib.Utils;
using KitchenLogger = KitchenLib.Logging.KitchenLogger;

namespace Blocked
{
    public class Mod : BaseMod, IModSystem
    {
        public const string MOD_GUID = "com.starfluxgames.blocked";
        public const string MOD_NAME = "Blocked";
        public const string MOD_VERSION = "0.1.1";
        public const string MOD_AUTHOR = "StarFluxGames";
        public const string MOD_GAMEVERSION = ">=1.1.8";

        public static KitchenLogger Logger;
        public static PreferenceManager manager;

        public static NetworkRouter networkRouter;

        public Mod() : base(MOD_GUID, MOD_NAME, MOD_AUTHOR, MOD_VERSION, MOD_GAMEVERSION, Assembly.GetExecutingAssembly()) { }

        protected override void OnInitialise()
        {
            Logger.LogWarning($"{MOD_GUID} v{MOD_VERSION} in use!");
        }

        private bool GetNetworkRouter(out NetworkRouter networkRouter)
        {
            networkRouter = null;
            RouterManager routerManager = SystemUtils.GetSystem<RouterManager>();
            if (routerManager == null) return false;
            if (routerManager.Routers == null) return false;
            if (routerManager.Routers.Count < 0) return false;
            foreach (IViewRouter router in routerManager.Routers)
            {
                if (router is not NetworkRouter _networkRouter) continue;
                networkRouter =  _networkRouter;
                return true;
            }

            return false;
        }

        protected override void OnPostActivate(KitchenMods.Mod mod)
        {
            Logger = InitLogger();

            manager = new PreferenceManager(MOD_GUID);
            manager.RegisterPreference(new PreferenceDictionary<ulong, string>("BLOCKED_USERS", new Dictionary<ulong, string>()));
            manager.Load();
            manager.Save();
            
            PauseMenuPreferencesesMenu.RegisterMenu(MOD_NAME, typeof(PreferenceMenu));
            
            PauseMenuPreferencesesMenu.RegisterUsableMenu(typeof(PreferenceMenu));
            PauseMenuPreferencesesMenu.RegisterUsableMenu(typeof(CurrentPlayersMenu));
            PauseMenuPreferencesesMenu.RegisterUsableMenu(typeof(BlockedMenu));
            PauseMenuPreferencesesMenu.RegisterUsableMenu(typeof(ConfirmBlock));
            PauseMenuPreferencesesMenu.RegisterUsableMenu(typeof(ConfirmUnblock));
        }
    }
}

