using System.Collections.Generic;
using UnityEngine.UI;

namespace SpectatorChat.Other;

public class ReservedItemUI
{
    private static List<Image> ReservedItemSlots { get; set; }
    
    public static void InitReservedItemUI()
    {
        ReservedItemSlots = ReservedItemSlotCore.Patches.HUDPatcher.reservedItemSlots;
    }

    public static void SwitchReservedItemUI(bool hide)
    {
        if (hide)
        {
            foreach (Image img in ReservedItemSlots)
            {
                img.enabled = false;
            }
        }
        
        foreach (Image img in ReservedItemSlots)
        {
            img.enabled = true;
        }
    }
}