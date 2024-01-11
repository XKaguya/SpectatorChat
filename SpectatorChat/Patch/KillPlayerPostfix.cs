using GameNetcodeStuff;
using HarmonyLib;

namespace SpectatorChat
{
    internal static class KillPlayerPostfixPatch
    {
        [HarmonyPatch(typeof(PlayerControllerB), "KillPlayer")]
        private static void Postfix()
        {
            if (API.Instance.StartPermanentTransparent())
            {
                Plugin.mls.LogInfo($"Routine start successfully.");
            }
        }
    }
}