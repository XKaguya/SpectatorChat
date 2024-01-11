using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using GameNetcodeStuff;
using HarmonyLib;

namespace SpectatorChat
{
    [HarmonyPatch(typeof(PlayerControllerB), "KillPlayer")]
    internal static class HUDPatch
    {
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> codes = instructions.ToList();
            
            if (!Plugin.IsPatched)
            {
                MethodInfo hideHUDMethod = typeof(HUDManager).GetMethod(nameof(HUDManager.HideHUD));

                int index = 0;
            
                Plugin.mls.LogInfo("Processing KillPlayer method...");

                try
                {
                    index = codes.FindLastIndex(i => i.opcode == OpCodes.Callvirt && i.operand is MethodInfo methodInfo && methodInfo.Equals(hideHUDMethod));
                    Plugin.mls.LogInfo($"Index: {index} Code: {codes[index]} found.");
                    index -= 2;
                }
                catch (Exception ex)
                {
                    Plugin.mls.LogError(ex.Message + ex.StackTrace);
                    throw;
                }

                try
                {
                    Plugin.mls.LogInfo("Patching following codes...");
                    for (int i = 0; i <= 2; i++)
                    {
                        Plugin.mls.LogInfo($"Index: {index + i}, Code: {codes[index + i]}");
                        codes[index + i].opcode = OpCodes.Nop;
                    }
                
                    Plugin.mls.LogInfo("Patched codes:");
                    for (int i = 0; i <= 2; i++)
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

