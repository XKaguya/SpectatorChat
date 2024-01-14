using GameNetcodeStuff;
using HarmonyLib;

namespace SpectatorChat
{
    internal static class KillPlayerPostfixPatch
    {
        [HarmonyPatch(typeof(PlayerControllerB), "KillPlayer")]
        private static void Postfix(PlayerControllerB __instance)
        {
            HarmonyAPI.LogCallingMethod("KillPlayer");
            
            Plugin.mls.LogInfo($"{__instance.playerUsername} Environment: Instance Name: {__instance.name} Instance Object Name: {__instance.playerUsername} Instance Owner: {__instance.IsOwner}, Instance Player Dead: {__instance.isPlayerDead}, API IsCoroutineNull: {API.Instance.IsCoroutineNull()}");
            
            if (__instance.IsOwner)
            {
                if (API.Instance.StartPermanentTransparent())
                {
                    HUDManager.Instance.gameOverAnimator.SetTrigger("revive");
                    
                    Plugin.mls.LogInfo($"Routine start successfully. Player {__instance.playerUsername}");
                }
            }
        }
    }
}