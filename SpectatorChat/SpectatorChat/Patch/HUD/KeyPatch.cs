using GameNetcodeStuff;
using HarmonyLib;
using SpectatorChat.API;

namespace SpectatorChat.Patch.HUD
{
    [HarmonyPatch(typeof(PlayerControllerB), "Update")]
    internal static class KeyPatch
    {
        internal static void Postfix(PlayerControllerB __instance)
        {
            Plugin.PlayerControllerInstance = __instance;
            
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
                        Generic.ToggleSpectatorBoxUI(Plugin.HUDManagerInstance, false);
                        Plugin.KeyPressed = true;
                        Plugin.mls.LogInfo($"Player {__instance.playerUsername} pressed {Plugin.InputActionInstance.ToggleDeathBoxKey}, UI has now set to invincible.");
                    }
                    else
                    {
                        Generic.ToggleSpectatorBoxUI(Plugin.HUDManagerInstance, true);
                        Plugin.KeyPressed = false;
                        Plugin.mls.LogInfo($"Player {__instance.playerUsername} pressed {Plugin.InputActionInstance.ToggleDeathBoxKey}, UI has now set to visible.");
                    }
                }
            }
        }
    }
}   