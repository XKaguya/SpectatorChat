#pragma warning disable 1591

using System.Collections.Generic;
using UnityEngine.UI;

namespace SpectatorChat.Other
{
    public class ReservedItemUI
    {
        public static void InitReservedItemUI()
        {
            if (API.Generic.IsModLoaded("FlipMods.ReservedItemSlotCore"))
            {
                GlobalVariables.ReservedItemSlots = ReservedItemSlotCore.Patches.HUDPatcher.reservedItemSlots;
            }
            else
            {
                GlobalVariables.ReservedItemSlots = null;
            }
        }

        public static void SwitchReservedItemUI(bool hide)
        {
            if (GlobalVariables.ReservedItemSlots != null)
            {
                if (hide)
                {
                    foreach (Image img in GlobalVariables.ReservedItemSlots)
                    {
                        img.enabled = false;
                    }
                }
        
                foreach (Image img in GlobalVariables.ReservedItemSlots)
                {
                    img.enabled = true;
                }
            }
        }
    }
}