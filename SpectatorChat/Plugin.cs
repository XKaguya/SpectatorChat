using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using GameNetcodeStuff;
using HarmonyLib;
using HarmonyLib.Tools;
using LethalCompanyInputUtils.Api;
using UnityEngine.InputSystem;

namespace SpectatorChat
{
    [BepInPlugin(modGUID, modName, modVersion)]
    [BepInDependency("LC_API", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("FlipMods.TooManyEmotes", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("taffyko.NiceChat", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("rr.Flashlight", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("QuickRestart", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("MoreEmotes", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("quackandcheese.mirrordecor", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("me.eladnlg.customhud", BepInDependency.DependencyFlags.SoftDependency)]
    public class Plugin : BaseUnityPlugin
    {
        private const string modGUID = "Kaguya.SpectatorChat";
        private const string modName = "SpectatorChat";
        private const string modVersion = "1.0.6";

        public static ConfigEntry<bool> ShowClock;
        
        public static ConfigEntry<bool> CanLivingPlayerReceiveMessage;

        public static bool CanReceive { get; set; }

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

            mls.LogInfo(HarmonyAPI.GetPatchInfoAsString(harmony, AccessTools.Method(typeof(HUDManager), nameof(HUDManager.Awake))));
            mls.LogInfo(HarmonyAPI.GetPatchInfoAsString(harmony, AccessTools.Method(typeof(HUDManager), "EnableChat_performed")));
            mls.LogInfo(HarmonyAPI.GetPatchInfoAsString(harmony, AccessTools.Method(typeof(HUDManager), "SubmitChat_performed")));
            mls.LogInfo(HarmonyAPI.GetPatchInfoAsString(harmony, AccessTools.Method(typeof(PlayerControllerB), "KillPlayer")));
            mls.LogInfo(HarmonyAPI.GetPatchInfoAsString(harmony, AccessTools.Method(typeof(PlayerControllerB), "Update")));
            mls.LogInfo(HarmonyAPI.GetPatchInfoAsString(harmony, AccessTools.Method(typeof(StartOfRound), "ReviveDeadPlayers")));
        }

        private void PatchAll()
        {
            harmony.PatchAll(typeof(Plugin));
            harmony.PatchAll(typeof(EnableChatPatch));
            harmony.PatchAll(typeof(SubmitChatPatch));
            harmony.PatchAll(typeof(HUDPatch));
            harmony.PatchAll(typeof(AddPlayerChatMessagePatch));
            
            harmony.PatchAll(typeof(HUDManagerPostfixPatch));
            harmony.PatchAll(typeof(KillPlayerPostfixPatch));
            harmony.PatchAll(typeof(ReviveDeadPlayersPostfixPatch));
            
            harmony.PatchAll(typeof(KeyPatch));
        }

        private void LoadConfigs()
        {
            ShowClock = ((BaseUnityPlugin)this).Config.Bind<bool>("Settings", "ShowClock", true, "Show the clock for spectator players.");
            CanLivingPlayerReceiveMessage = ((BaseUnityPlugin)this).Config.Bind<bool>("Settings", "CanLivingPlayerReceiveMessage", false, "Can living player receive dead player's message.");
            CanReceive = CanLivingPlayerReceiveMessage.Value;
        }
    }
}