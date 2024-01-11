using HarmonyLib;

namespace SpectatorChat
{
    internal static class ReviveDeadPlayersPostfixPatch
    {
        [HarmonyPatch(typeof(StartOfRound), "ReviveDeadPlayers")]
        private static void Postfix()
        {
            if (API.Instance.StopPermanentTransparent())
            {
                Plugin.mls.LogInfo($"Routine stopped successfully.");
            }
        }
    }
}
