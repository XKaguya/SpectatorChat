using System.Linq;
using HarmonyLib;
using SpectatorChat.API;
using SpectatorChat.Other;

namespace SpectatorChat.Patch.HUD
{
    [HarmonyPatch(typeof(HUDManager), "Awake")]
    internal static class HUDManagerPostfixPatch
    {
        static void Postfix(HUDManager __instance)
        {
            HarmonyAPI.LogCallingMethod("HUDManager.Awake");
            
            GlobalVariables.HUDManagerInstance = __instance;

            if (GlobalVariables.InitReservedItem)
            {
                ReservedItemUI.InitReservedItemUI();
            }
            
            if (Plugin.ShowClock.Value)
            {
                GlobalVariables.HUDElements = __instance.HUDElements.Where(element => element != __instance.HUDElements[1] && element != __instance.HUDElements[5]).ToArray();
            }
            else
            {
                GlobalVariables.HUDElements = __instance.HUDElements.Where(element => element != __instance.HUDElements[1]).ToArray();
            }
        }
    }
}