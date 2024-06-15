#pragma warning disable 1591

using System.Collections.Generic;
using System.Threading;
using GameNetcodeStuff;
using UnityEngine.UI;

namespace SpectatorChat.Other
{
    public class GlobalVariables
    {
        public static CancellationTokenSource? CoroutineCancellationTokenSource = null;

        public static bool Init { get; set; }

        public static bool InitReservedItem { get; set; }
        
        public static List<Image>? ReservedItemSlots { get; set; }
        
        public static HUDElement[]? HUDElements { get; set; }
        
        public static HUDManager? HUDManagerInstance { get; set; }
        
        public static PlayerControllerB? PlayerControllerInstance { get; set; }
    }
}