using GameNetcodeStuff;
using HarmonyLib;

namespace SpectatorChat
{
    internal static class KillPlayerPostfixPatch
    {
        [HarmonyPatch(typeof(PlayerControllerB), "KillPlayer")]
        private static void Postfix(PlayerControllerB __instance)
        {
            if (__instance.IsOwner)
            {
                if (API.Instance.StartPermanentTransparent())
                {
                    Plugin.mls.LogInfo($"Routine start successfully. Player {__instance.playerUsername}");
                }
            }
        }
    }
}