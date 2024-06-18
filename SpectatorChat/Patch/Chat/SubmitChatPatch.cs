using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using GameNetcodeStuff;
using HarmonyLib;
using SpectatorChat.API;

namespace SpectatorChat.Patch.Chat
{
    [HarmonyPatch(typeof(HUDManager), "SubmitChat_performed")]
    internal static class SubmitChatPatch
    {
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            HarmonyAPI.LogCallingMethod("SubmitChat_performed");
            
            List<CodeInstruction> newInstructions = new List<CodeInstruction>(instructions);
            
            FieldInfo targetField = typeof(PlayerControllerB).GetField(nameof(PlayerControllerB.isPlayerDead));

            int index = 0;
            
            Plugin.mls.LogInfo("Processing SubmitChat_performed method...");

            try
            {
                index = newInstructions.FindLastIndex(instruction => instruction.opcode == OpCodes.Ldfld && instruction.operand is FieldInfo fieldInfo && fieldInfo.Equals(targetField));
                Plugin.mls.LogInfo($"Index: {index} Code: {newInstructions[index]} found.");
                index -= 2;
                Plugin.mls.LogInfo($"Got previous IL Code: {newInstructions[index]}.");
            }
            catch (Exception ex)
            {
                Plugin.mls.LogError(ex.Message + ex.StackTrace);
                throw;
            }

            try
            {
                Plugin.mls.LogInfo("Patching following codes...");
                for (int i = 0; i <= 4; i++)
                {
                    Plugin.mls.LogInfo($"Index: {index + i}, Code: {newInstructions[index + i]}");
                    newInstructions[index + i].opcode = OpCodes.Nop;
                }
                
                Plugin.mls.LogInfo("Patched codes:");
                for (int i = 0; i <= 4; i++)
                {
                    Plugin.mls.LogInfo($"Index: {index + i}, Code: {newInstructions[index + i]}");
                }
            }
            catch (Exception ex)
            {
                Plugin.mls.LogError(ex.Message + ex.StackTrace);
                throw;
            }
            finally
            {
                Plugin.mls.LogInfo("Patch success. No any fatal error were raised.");
            }

            foreach (CodeInstruction instruction in newInstructions)
            {
                yield return instruction;
            }
        }
    }
}

