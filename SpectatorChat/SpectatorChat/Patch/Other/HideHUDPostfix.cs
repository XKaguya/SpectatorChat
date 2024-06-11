using System;
using HarmonyLib;
using SpectatorChat.API;

namespace SpectatorChat.Patch.Other
{
    internal static class HideHUDPostfixPatch
    {
        [HarmonyPatch(typeof(HUDManager), "HideHUD")]
        private static void Postfix(HUDManager __instance)
        {
            HarmonyAPI.LogCallingMethod("HideHUD");
            
            Plugin.mls.LogInfo($"{Plugin.PlayerControllerInstance.playerUsername} Environment: Instance Name: {__instance.name} Instance Object Name: {Plugin.PlayerControllerInstance.playerUsername} Instance Owner: {__instance.IsOwner}, Instance Player Dead: {Plugin.PlayerControllerInstance.isPlayerDead}, API IsCoroutineNull: {Generic.Instance.IsCoroutineNull()}");
            
            if (!Generic.Instance.IsCoroutineNull())
            {
                try
                {
                    if (Generic.Instance.StopPermanentTransparent())
                    {
                        Plugin.mls.LogInfo($"Routine stopped successfully. Player {Plugin.PlayerControllerInstance.playerUsername}");
                    }
                    else
                    {
                        Plugin.mls.LogInfo($"Routine stopped failed. Player {Plugin.PlayerControllerInstance.playerUsername}");
                    }
                }
                catch (Exception ex)
                {
                    Plugin.mls.LogError(ex.StackTrace + ex.Message);
                    throw;
                }
            }
        }
    }
}