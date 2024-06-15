using System;
using HarmonyLib;
using SpectatorChat.API;
using SpectatorChat.Other;

namespace SpectatorChat.Patch.Other
{
    internal static class HideHUDPostfixPatch
    {
        [HarmonyPatch(typeof(HUDManager), "HideHUD")]
        private static void Postfix(HUDManager __instance)
        {
            HarmonyAPI.LogCallingMethod("HideHUD");
            
            if (!Generic.Instance.IsCoroutineNull())
            {
                try
                {
                    if (Generic.Instance.StopPermanentTransparent())
                    {
                        Plugin.mls.LogInfo($"Routine stopped successfully. Player {GlobalVariables.PlayerControllerInstance!.playerUsername}");
                    }
                    else
                    {
                        Plugin.mls.LogInfo($"Routine stopped failed. Player {GlobalVariables.PlayerControllerInstance!.playerUsername}");
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