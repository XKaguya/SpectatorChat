using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using GameNetcodeStuff;
using HarmonyLib;
using LethalCompanyInputUtils.Api;
using UnityEngine.InputSystem;

namespace SpectatorChat
{
    [BepInPlugin(modGUID, modName, modVersion)]
    [BepInDependency("LC_API", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        private const string modGUID = "Kaguya.SpectatorChat";
        private const string modName = "SpectatorChat";
        private const string modVersion = "1.0.4";

        public static ConfigEntry<bool> ShowClock;
        
        public static bool IsPatched { get; set; }

        private readonly Harmony harmony = new(modGUID);

        private static Plugin Instance;

        public static ManualLogSource mls;

        public static HUDElement[] HUDElements { get; set; }
        
        public static HUDManager HUDManagerInstance { get; set; }
        
        public static PlayerControllerB PlayerControllerInstance { get; set; }

        public static bool KeyPressed { get; set; } = false;
        
        public class InputKey : LcInputActions
        {
            [InputAction("<Keyboard>/r", Name = "ToggleDeathBox")]
            public InputAction ToggleDeathBoxKey { get; set; }
        }
        
        internal static InputKey InputActionInstance = new();

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            mls = BepInEx.Logging.Logger.CreateLogSource(modGUID);
            
            mls.LogInfo("SpectatorChat has loaded.");
            
            LoadConfigs();

            PatchAll();
        }

        private void PatchAll()
        {
            if (!IsPatched)
            {
                mls.LogInfo($"IsPatched: {IsPatched}");
                
                harmony.PatchAll(typeof(Plugin));
                harmony.PatchAll(typeof(EnableChatPatch));
                harmony.PatchAll(typeof(SubmitChatPatch));
                harmony.PatchAll(typeof(HUDPatch));
                harmony.PatchAll(typeof(AddPlayerChatMessagePatch));
                
                harmony.PatchAll(typeof(HUDManagerPostfixPatch));
                harmony.PatchAll(typeof(KillPlayerPostfixPatch));
                harmony.PatchAll(typeof(ReviveDeadPlayersPostfixPatch));
                
                harmony.PatchAll(typeof(KeyPatch));
                
                IsPatched = true;
            }
        }

        private void LoadConfigs()
        {
            ShowClock = ((BaseUnityPlugin)this).Config.Bind<bool>("Settings", "ShowClock", true, "Show the clock for spectator players.");
        }
    }
}