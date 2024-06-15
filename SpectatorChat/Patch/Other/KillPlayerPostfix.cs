using GameNetcodeStuff;
using HarmonyLib;
using SpectatorChat.API;
using SpectatorChat.Other;

namespace SpectatorChat.Patch.Other
{
    internal static class KillPlayerPostfixPatch
    {
        [HarmonyPatch(typeof(PlayerControllerB), "KillPlayer")]
        private static void Postfix(PlayerControllerB __instance)
        {
            HarmonyAPI.LogCallingMethod("KillPlayer");

            if (__instance.IsOwner)
            {
                if (Generic.Instance.StartPermanentTransparent())
                {
                    GlobalVariables.PlayerControllerInstance.bleedingHeavily = false;
                    GlobalVariables.PlayerControllerInstance.criticallyInjured = false;
                    HUDManager.Instance.HUDAnimator.SetTrigger("revealHud");

                    Plugin.mls.LogInfo($"Routine start successfully. Player {__instance.playerUsername}");
                }
            }
        }
    }
}