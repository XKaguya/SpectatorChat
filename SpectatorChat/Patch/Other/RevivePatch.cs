using HarmonyLib;
using OPJosMod.ReviveCompany;
using OPJosMod.ReviveCompany.CustomRpc;
using SpectatorChat.API;
using GlobalVariables = SpectatorChat.Other.GlobalVariables;

namespace SpectatorChat.Patch.Other
{
    internal static class ReviveCompanyPatch
    {
        [HarmonyPatch(typeof(CompleteRecievedTasks), "RevivePlayer")]
        private static bool Prefix(ref string playerIdString)
        {
            HarmonyAPI.LogCallingMethod("RevivePlayer");

            bool flag = int.TryParse(playerIdString, out int playerId);
            if (flag)
            {
                GeneralUtil.RevivePlayer(playerId);
                
                if (Generic.IsInstanceOwner(playerIdString))
                {
                    Generic.ClearSpectatorUI(GlobalVariables.HUDManagerInstance!);
                    
                    if (Generic.Instance.StopPermanentTransparent())
                    {
                        Plugin.mls.LogInfo($"Routine stopped successfully. Player {GlobalVariables.PlayerControllerInstance!.playerUsername}");
                    }
                    else
                    {
                        Plugin.mls.LogInfo($"Routine stopped failed. Player {GlobalVariables.PlayerControllerInstance!.playerUsername}");
                    }
                }
            }
            else
            {
                Plugin.mls.LogError("Error: Invalid player ID '" + playerIdString + "' did not revive");
            }
            
            return false;
        }
    }
}