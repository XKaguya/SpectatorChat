using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using GameNetcodeStuff;
using HarmonyLib;

namespace SpectatorChat
{
    [HarmonyPatch(typeof(HUDManager), "AddPlayerChatMessageClientRpc")]
    internal static class AddPlayerChatMessagePatch
    {
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            HarmonyAPI.LogCallingMethod("AddPlayerChatMessageClientRpc");

            List<CodeInstruction> newInstructions = new List<CodeInstruction>(instructions);

            Label retLabel = generator.DefineLabel();

            Label continueLabel = generator.DefineLabel();

            FieldInfo targetField = typeof(PlayerControllerB).GetField(nameof(PlayerControllerB.isPlayerDead));

            int index = 0;

            Plugin.mls.LogInfo("Processing AddPlayerChatMessageClientRpc method...");

            try
            {
                index = newInstructions.FindLastIndex(instruction => instruction.opcode == OpCodes.Ldfld && instruction.operand is FieldInfo fieldInfo && fieldInfo.Equals(targetField));
                Plugin.mls.LogInfo($"Index: {index} Code: {newInstructions[index]} found.");
                index -= 9;
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
                for (int i = 0; i <= 11; i++)
                {
                    Plugin.mls.LogInfo($"Index: {index + i}, Code: {newInstructions[index + i]}");
                    newInstructions[index + i].opcode = OpCodes.Nop;
                }

                Plugin.mls.LogInfo("Patched codes:");
                for (int i = 0; i <= 11; i++)
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

            Plugin.mls.LogInfo("Now processing patch 2...");

            try
            {
                index = newInstructions.FindLastIndex(instruction => instruction.opcode == OpCodes.Ldloc_0);
                Plugin.mls.LogInfo($"Index: {index} Code: {newInstructions[index]} found.");
                index += 1;
                Plugin.mls.LogInfo($"Got next IL Code: {newInstructions[index]}.");

                newInstructions[index + 1].opcode = OpCodes.Nop;

                index += 1;
                
                newInstructions.InsertRange(index, new []
                {
                    new(OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(Plugin), nameof(Plugin.CanReceive))),
                    new(OpCodes.Brfalse_S, retLabel),
                    
                    new(OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(Plugin), nameof(Plugin.PlayerControllerInstance.isPlayerDead))),
                    new(OpCodes.Brtrue_S, continueLabel),
                    new(OpCodes.Ret),
                    new CodeInstruction(OpCodes.Nop).WithLabels(continueLabel),
                });
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

            newInstructions[newInstructions.Count - 1].WithLabels(retLabel);

            foreach (CodeInstruction instruction in newInstructions)
            {
                yield return instruction;
            }

        }       
    }
}

