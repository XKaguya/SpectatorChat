﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using GameNetcodeStuff;
using HarmonyLib;   

namespace SpectatorChat
{
    [HarmonyPatch(typeof(HUDManager), "AddPlayerChatMessageClientRpc")]
    internal static class AddPlayerChatMessagePatch
    {
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> codes = instructions.ToList();
            
            if (!Plugin.IsPatched)
            {
                FieldInfo targetField = typeof(PlayerControllerB).GetField(nameof(PlayerControllerB.isPlayerDead));

                int index = 0;
            
                Plugin.mls.LogInfo("Processing AddPlayerChatMessageClientRpc method...");

                try
                {
                    index = codes.FindLastIndex(instruction => instruction.opcode == OpCodes.Ldfld && instruction.operand is FieldInfo fieldInfo && fieldInfo.Equals(targetField));
                    Plugin.mls.LogInfo($"Index: {index} Code: {codes[index]} found.");
                    index -= 9;
                    Plugin.mls.LogInfo($"Got previous IL Code: {codes[index]}.");
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
                        Plugin.mls.LogInfo($"Index: {index + i}, Code: {codes[index + i]}");
                        codes[index + i].opcode = OpCodes.Nop;
                    }
                
                    Plugin.mls.LogInfo("Patched codes:");
                    for (int i = 0; i <= 11; i++)
                    {
                        Plugin.mls.LogInfo($"Index: {index + i}, Code: {codes[index + i]}");
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

                return codes.AsEnumerable();
            }
            
            return codes.AsEnumerable();
        }
    }
}
