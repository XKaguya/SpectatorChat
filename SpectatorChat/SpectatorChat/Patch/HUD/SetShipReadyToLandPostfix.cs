using HarmonyLib;
using SpectatorChat.API;

namespace SpectatorChat.Patch.HUD
{
    [HarmonyPatch(typeof(StartOfRound), "SetShipReadyToLand")]
    internal static class SetShipReadyToLandPostfix
    {
        static void Postfix()
        {
            HarmonyAPI.LogCallingMethod("StartOfRound.SetShipReadyToLand");
            
            Generic.ClearSpectatorUI(Plugin.HUDManagerInstance);
        }
    }
}




