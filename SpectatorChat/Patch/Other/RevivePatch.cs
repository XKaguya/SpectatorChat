using HarmonyLib;
using OPJosMod.ReviveCompany;
using OPJosMod.ReviveCompany.CustomRpc;
using SpectatorChat.API;

namespace SpectatorChat.Patch.Other
{
    internal static class ReviveCompanyPatch
    {
        [HarmonyPatch(typeof(CompleteRecievedTasks), "RevivePlayer")]
        private static bool Prefix(ref string playerIdString)
        {
            if (Generic.IsInstanceOwner(playerIdString))
            {
                HarmonyAPI.LogCallingMethod("RevivePlayer");

                bool flag = int.TryParse(playerIdString, out int playerId);
                if (flag)
                {
                    GeneralUtil.RevivePlayer(playerId);
                
                    Generic.ClearSpectatorUI(Plugin.HUDManagerInstance);
                
                    if (Generic.Instance.StopPermanentTransparent())
                    {
                        Plugin.mls.LogInfo($"Routine stopped successfully. Player {Plugin.PlayerControllerInstance.playerUsername}");
                    }
                    else
                    {
                        Plugin.mls.LogInfo($"Routine stopped failed. Player {Plugin.PlayerControllerInstance.playerUsername}");
                    }
                }
                else
                {
                    Plugin.mls.LogError("Error: Invalid player ID '" + playerIdString + "' did not revive");
                }
            }
            
            return false;
        }
    }
}