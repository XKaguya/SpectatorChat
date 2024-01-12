using HarmonyLib;

namespace SpectatorChat
{
    [HarmonyPatch(typeof(HUDManager), "Awake")]
    internal static class HUDManagerPostfixPatch
    {
        static void Postfix(HUDManager __instance)
        {
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