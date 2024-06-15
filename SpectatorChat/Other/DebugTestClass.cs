#pragma warning disable 1591

using System.Text;

namespace SpectatorChat.Other
{
    public static class DebugTestClass
    {
        public static void PrintAllVariables()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Global Variables State:");
            sb.AppendLine($"Init: {GlobalVariables.Init}");
            sb.AppendLine($"InitReservedItem: {GlobalVariables.InitReservedItem}");
            sb.AppendLine($"HUDElements: {(GlobalVariables.HUDElements != null ? "Initialized" : "Null")}");
            sb.AppendLine($"HUDManagerInstance: {(GlobalVariables.HUDManagerInstance != null ? "Initialized" : "Null")}");
            sb.AppendLine($"ReservedItemSlot: {(GlobalVariables.ReservedItemSlots != null ? "Initialized" : "Null")}");
            sb.AppendLine($"PlayerControllerInstance: {(GlobalVariables.PlayerControllerInstance != null ? "Initialized" : "Null")}");
            sb.AppendLine($"CoroutineCancellationTokenSource: {(GlobalVariables.CoroutineCancellationTokenSource != null ? "Initialized" : "Null")}");
                
            Plugin.mls.LogInfo(sb.ToString());
        }
    }
}