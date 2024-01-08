using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using GameNetcodeStuff;
using HarmonyLib;

namespace SpectatorChat
{
    [HarmonyPatch(typeof(HUDManager), "EnableChat_performed")]
    internal static class EnableChatPatch
    {
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> codes = instructions.ToList();
            
            if (!SpectatorChatBase.IsPatched)
            {
                FieldInfo targetField = typeof(PlayerControllerB).GetField(nameof(PlayerControllerB.isPlayerDead));

                int index = 0;
            
                SpectatorChatBase.mls.LogInfo("Processing EnableChat_performed method...");

                try
                {
                    index = codes.FindLastIndex(instruction => instruction.opcode == OpCodes.Ldfld && instruction.operand is FieldInfo fieldInfo && fieldInfo.Equals(targetField));
                    SpectatorChatBase.mls.LogInfo($"Index: {index} Code: {codes[index]} found.");
                    index -= 2;
                    SpectatorChatBase.mls.LogInfo($"Got previous IL Code: {codes[index]}.");
                }
                catch (Exception ex)
                {
                    SpectatorChatBase.mls.LogError(ex.Message + ex.StackTrace);
                    throw;
                }

                try
                {
                    SpectatorChatBase.mls.LogInfo("Patching following codes...");
                    for (int i = 0; i <= 4; i++)
                    {
                        SpectatorChatBase.mls.LogInfo($"Index: {index + i}, Code: {codes[index + i]}");
                        codes[index + i].opcode = OpCodes.Nop;
                    }
                
                    SpectatorChatBase.mls.LogInfo("Patched codes:");
                    for (int i = 0; i <= 4; i++)
                    {
                        SpectatorChatBase.mls.LogInfo($"Index: {index + i}, Code: {codes[index + i]}");
                    }
                }
                catch (Exception ex)
                {
                    SpectatorChatBase.mls.LogError(ex.Message + ex.StackTrace);
                    throw;
                }
                finally
                {
                    SpectatorChatBase.mls.LogInfo("Patch success. No any fatal error were raised.");
                }
                
                return codes.AsEnumerable();
            }

            return codes.AsEnumerable();
        }
    }
}

