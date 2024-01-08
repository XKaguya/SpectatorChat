using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace SpectatorChat
{
    [BepInPlugin(modGUID, modName, modVersion)]
    [BepInDependency("LC_API", "1.0.0")]
    public class SpectatorChatBase : BaseUnityPlugin
    {
        private const string modGUID = "Kaguya.SpectatorChat";
        private const string modName = "SpectatorChat";
        private const string modVersion = "1.0.0";
        
        public static bool IsPatched { get; set; }

        private readonly Harmony harmony = new(modGUID);

        private static SpectatorChatBase Instance;

        public static ManualLogSource mls;

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
                
                harmony.PatchAll(typeof(SpectatorChatBase));
                harmony.PatchAll(typeof(EnableChatPatch));
                harmony.PatchAll(typeof(SubmitChatPatch));
                harmony.PatchAll(typeof(HUDPatch));
                harmony.PatchAll(typeof(AddPlayerChatMessagePatch));
                
                IsPatched = true;
            }
        }


    }
}