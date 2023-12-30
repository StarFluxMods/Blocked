using System.Collections.Generic;
using KitchenLib;
using KitchenLib.Logging;
using KitchenMods;
using System.Reflection;
using Blocked.Menus;
using Kitchen;
using KitchenLib.Event;
using KitchenLib.Preferences;

namespace Blocked
{
    public class Mod : BaseMod, IModSystem
    {
        public const string MOD_GUID = "com.starfluxgames.blocked";
        public const string MOD_NAME = "Blocked";
        public const string MOD_VERSION = "0.1.0";
        public const string MOD_AUTHOR = "StarFluxGames";
        public const string MOD_GAMEVERSION = ">=1.1.8";

        public static KitchenLogger Logger;
        public static PreferenceManager manager;

        public Mod() : base(MOD_GUID, MOD_NAME, MOD_AUTHOR, MOD_VERSION, MOD_GAMEVERSION, Assembly.GetExecutingAssembly()) { }

        protected override void OnInitialise()
        {
            Logger.LogWarning($"{MOD_GUID} v{MOD_VERSION} in use!");
        }

        protected override void OnPostActivate(KitchenMods.Mod mod)
        {
            Logger = InitLogger();

            manager = new PreferenceManager(MOD_GUID);
            manager.RegisterPreference(new PreferenceDictionary<ulong, string>("BLOCKED_USERS", new Dictionary<ulong, string>()));
            manager.Load();
            manager.Save();
            
            ModsPreferencesMenu<PauseMenuAction>.RegisterMenu(MOD_NAME, typeof(PreferenceMenu), typeof(PauseMenuAction));
            
            Events.PlayerPauseView_SetupMenusEvent += (s, args) =>
            {
                args.addMenu.Invoke(args.instance, new object[] { typeof(PreferenceMenu), new PreferenceMenu(args.instance.ButtonContainer, args.module_list) });
                args.addMenu.Invoke(args.instance, new object[] { typeof(CurrentPlayersMenu), new CurrentPlayersMenu(args.instance.ButtonContainer, args.module_list) });
                args.addMenu.Invoke(args.instance, new object[] { typeof(BlockedMenu), new BlockedMenu(args.instance.ButtonContainer, args.module_list) });
                args.addMenu.Invoke(args.instance, new object[] { typeof(ConfirmBlock), new ConfirmBlock(args.instance.ButtonContainer, args.module_list) });
                args.addMenu.Invoke(args.instance, new object[] { typeof(ConfirmUnblock), new ConfirmUnblock(args.instance.ButtonContainer, args.module_list) });
            };
        }
    }
}

