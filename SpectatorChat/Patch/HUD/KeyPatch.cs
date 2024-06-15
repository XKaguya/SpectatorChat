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
            GlobalVariables.PlayerControllerInstance = __instance;
            
            if (!__instance.IsOwner && !__instance.isTestingPlayer)
            {
                return;
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