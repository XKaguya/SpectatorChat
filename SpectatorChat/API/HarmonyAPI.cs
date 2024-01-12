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
    }
}