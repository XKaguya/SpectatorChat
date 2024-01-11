using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace SpectatorChat
{
    [BepInPlugin(modGUID, modName, modVersion)]
    [BepInDependency("LC_API", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        private const string modGUID = "Kaguya.SpectatorChat";
        private const string modName = "SpectatorChat";
        private const string modVersion = "1.0.1";
        
        public static bool IsPatched { get; set; }

        private readonly Harmony harmony = new(modGUID);

        private static Plugin Instance;

        public static ManualLogSource mls;

        public static HUDElement[] HUDElements { get; set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            mls = BepInEx.Logging.Logger.CreateLogSource(modGUID);
            
            mls.LogInfo("SpectatorChat has loaded.");
            
            Harmony.DEBUG = true;

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
                
                IsPatched = true;
            }
        }
    }
}