using GameNetcodeStuff;
using HarmonyLib;

namespace SpectatorChat
{
    internal static class ReviveDeadPlayersPostfixPatch
    {
        [HarmonyPatch(typeof(StartOfRound), "ReviveDeadPlayers")]
        private static void Postfix(PlayerControllerB __instance)
        {
            if (__instance.IsOwner)
            {
                if (API.Instance.StopPermanentTransparent())
                {
                    Plugin.mls.LogInfo($"Routine stopped successfully. Player {__instance.playerUsername}");
                }
            }
        }
    }
}