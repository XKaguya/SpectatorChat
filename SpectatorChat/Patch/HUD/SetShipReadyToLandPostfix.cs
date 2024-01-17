using HarmonyLib;

namespace SpectatorChat
{
    [HarmonyPatch(typeof(StartOfRound), "SetShipReadyToLand")]
    internal static class SetShipReadyToLandPostfix
    {
        static void Postfix()
        {
            HarmonyAPI.LogCallingMethod("StartOfRound.SetShipReadyToLand");
            
            API.ClearSpectatorUI(Plugin.HUDManagerInstance);
        }
    }
}




