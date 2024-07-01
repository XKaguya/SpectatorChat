using GameNetcodeStuff;
using HarmonyLib;
using SpectatorChat.API;
using SpectatorChat.Other;

namespace SpectatorChat.Patch.HUD
{
    [HarmonyPatch(typeof(PlayerControllerB), "Update")]
    internal static class KeyPatch
    {
        internal static void Postfix(PlayerControllerB __instance)
        {
            if (!((__instance.IsOwner && (__instance.isPlayerControlled || __instance.isPlayerDead) && (!__instance.IsServer || __instance.isHostPlayerObject)) || __instance.isTestingPlayer))
            {
                return;
            }

            if (GlobalVariables.PlayerControllerInstance != __instance || GlobalVariables.PlayerControllerInstance == null)
            {
                Plugin.mls.LogInfo($"Player instance has changed due to {GlobalVariables.PlayerControllerInstance != __instance} or {GlobalVariables.PlayerControllerInstance == null}");
                GlobalVariables.PlayerControllerInstance = __instance;
            }
            
            if (__instance.isPlayerDead)
            {
                if (Plugin.InputActionInstance.ToggleDeathBoxKey.WasPressedThisFrame())
                {
                    if (!Plugin.KeyPressed)
                    {
                        Generic.ToggleSpectatorBoxUI(GlobalVariables.HUDManagerInstance!, false);
                        Plugin.KeyPressed = true;
                    }
                    else
                    {
                        Generic.ToggleSpectatorBoxUI(GlobalVariables.HUDManagerInstance!, true);
                        Plugin.KeyPressed = false;
                    }
                }
            }
        }
    }
}   