using GameNetcodeStuff;
using HarmonyLib;
using SpectatorChat.API;

namespace SpectatorChat.Patch.Other
{
    internal static class KillPlayerPostfixPatch
    {
        [HarmonyPatch(typeof(PlayerControllerB), "KillPlayer")]
        private static void Postfix(PlayerControllerB __instance)
        {
            HarmonyAPI.LogCallingMethod("KillPlayer");
            
            Plugin.mls.LogInfo($"{__instance.playerUsername} Environment: Instance Name: {__instance.name} Instance Object Name: {__instance.playerUsername} Instance Owner: {__instance.IsOwner}, Instance Player Dead: {__instance.isPlayerDead}, API IsCoroutineNull: {Generic.Instance.IsCoroutineNull()}");

            if (__instance.IsOwner)
            {
                if (Generic.Instance.StartPermanentTransparent())
                {
                    Plugin.PlayerControllerInstance.bleedingHeavily = false;
                    Plugin.PlayerControllerInstance.criticallyInjured = false;
                    HUDManager.Instance.HUDAnimator.SetTrigger("revealHud");

                    Plugin.mls.LogInfo($"Routine start successfully. Player {__instance.playerUsername}");
                }
            }
        }
    }
}