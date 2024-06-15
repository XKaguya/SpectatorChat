using GameNetcodeStuff;
using HarmonyLib;
using SpectatorChat.API;
using SpectatorChat.Other;

namespace SpectatorChat.Patch.HUD
{
    [HarmonyPatch(typeof(StartOfRound), "SetShipReadyToLand")]
    internal static class SetShipReadyToLandPostfix
    {
        static void Postfix()
        {
            HarmonyAPI.LogCallingMethod("StartOfRound.SetShipReadyToLand");
            
            Generic.ClearSpectatorUI(GlobalVariables.HUDManagerInstance!);
        }
    }
}




