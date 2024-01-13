using HarmonyLib;

namespace SpectatorChat
{
    [HarmonyPatch(typeof(HUDManager), "Awake")]
    [HarmonyPriority(999)]
    internal static class HUDManagerPostfixPatch
    {
        static void Postfix(HUDManager __instance)
        {
            HarmonyAPI.LogCallingMethod("HUDManager.Awake");
            
            Plugin.HUDManagerInstance = __instance;
            
            Plugin.HUDElements = __instance.HUDElements;

            if (Plugin.HUDElements != null)
            {
                foreach (var hudElement in Plugin.HUDElements)
                {
                    Plugin.mls.LogInfo($"HUDElement Content: {hudElement.canvasGroup}");
                }
            }
        }
    }
}