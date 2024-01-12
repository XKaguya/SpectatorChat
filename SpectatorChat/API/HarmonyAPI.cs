using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using HarmonyLib;

namespace SpectatorChat
{
    public static class HarmonyAPI
    {
        public static string GetPatchInfoAsString(Harmony harmony, MethodInfo methodInfo)
        {
            var resultBuilder = new StringBuilder();
            
            var patchedMethods = harmony.GetPatchedMethods();
            var patchesForTargetMethod = patchedMethods.Where(method => method == methodInfo);

            foreach (var patchedMethod in patchesForTargetMethod)
            {
                resultBuilder.AppendLine($"Method {patchedMethod.DeclaringType.FullName}.{patchedMethod.Name} is patched by:");

                var patchInfo = Harmony.GetPatchInfo(patchedMethod);

                foreach (var patch in patchInfo.Prefixes)
                {
                    resultBuilder.AppendLine($"- Prefix: {patch.owner}");
                }

                foreach (var patch in patchInfo.Postfixes)
                {
                    resultBuilder.AppendLine($"- Postfix: {patch.owner}");
                }

                foreach (var patch in patchInfo.Transpilers)
                {
                    resultBuilder.AppendLine($"- Transpiler: {patch.owner}");
                }
            }

            return resultBuilder.ToString();
        }
        
        public static void LogCallingMethod(string patchName)
        {
            try
            {
                StackTrace stackTrace = new StackTrace();
                StackFrame[] stackFrames = stackTrace.GetFrames();
        
                if (stackFrames != null && stackFrames.Length > 1)
                {
                    StackFrame callingFrame = stackFrames[1];
                    
                    MethodBase callingMethod = callingFrame.GetMethod();
                    
                    Type declaringType = callingMethod.DeclaringType;
                    
                    string callingNamespace = declaringType?.Namespace;
                    
                    string callingClassName = declaringType?.Name;
                    
                    Plugin.mls.LogInfo($"Patch {patchName} called by: {callingNamespace}.{callingClassName}.{callingMethod.Name}");
                }
                else
                {
                    Plugin.mls.LogWarning($"Unable to determine calling method for patch: {patchName}");
                }
            }
            catch (Exception ex)
            {
                Plugin.mls.LogError($"An error occurred while logging calling method for patch {patchName}: {ex.Message}");
            }
        }
    }
}