﻿using System;
using System.Collections;
using System.Linq;
using BepInEx.Bootstrap;
using UnityEngine;

namespace SpectatorChat.API
{
    public class Generic : MonoBehaviour
    {
        private static Generic instance;
        
        private Coroutine permanentTransparentCoroutine;
        
        private static HUDElement[] HUDElements { get; set; }
        
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                instance = null;
            }
        }

        public static Generic Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameObject("SpectatorChatGeneric").AddComponent<Generic>();
                }

                return instance;
            }
        }

        public static bool IsInstanceOwner(string userId)
        {
            try
            {
                return ulong.Parse(userId) == Plugin.PlayerControllerInstance.playerClientId;
            }
            catch (Exception ex)
            {
                Plugin.mls.LogError(ex.Message + ex.StackTrace);
                
                return false;
            }
        }

        public bool IsCoroutineNull()
        {
            if (permanentTransparentCoroutine == null)
            {
                return true;
            }

            return false;
        }

        public void GetHUDElements()
        {
            if (Plugin.ShowClock.Value)
            {
                HUDElements = Plugin.HUDElements.Where(element => element != Plugin.HUDElements[1] && element != Plugin.HUDElements[5]).ToArray();
            }
            else
            {
                HUDElements = Plugin.HUDElements.Where(element => element != Plugin.HUDElements[1]).ToArray();
            }
        }

        public bool StartPermanentTransparent()
        {
            GetHUDElements();
            
            if (permanentTransparentCoroutine == null)
            {
                permanentTransparentCoroutine = StartCoroutine(PermanentTransparentCoroutine(HUDElements));

                return true;
            }
            
            return false;
        }
        
        public bool StopPermanentTransparent()
        {
            if (permanentTransparentCoroutine != null)
            {
                StopCoroutine(permanentTransparentCoroutine);
                permanentTransparentCoroutine = null;

                if (IsModLoaded("FlipMods.ReservedItemSlotCore"))
                {
                    Other.ReservedItemUI.SwitchReservedItemUI(false);
                }

                return true;
            }

            return false;
        }

        public static bool IsModLoaded(string guid)
        {
            return Chainloader.PluginInfos.ContainsKey(guid);
        }
        
        private IEnumerator PermanentTransparentCoroutine(HUDElement[] elements)
        {
            while (true)
            {
                foreach (HUDElement element in elements)
                {
                    if (element.canvasGroup.alpha != 0)
                    {
                        element.canvasGroup.alpha = 0f;
                    }
                }

                if (IsModLoaded("FlipMods.ReservedItemSlotCore"))
                {
                    Other.ReservedItemUI.SwitchReservedItemUI(true);
                }
                
                yield return Plugin.CoroutineDelay.Value;
            }
        }

        public static void ToggleSpectatorBoxUI(HUDManager instance, bool isVisable)
        {
            try
            {
                if (isVisable)
                {
                    for (int i = 0; i < instance.spectatingPlayerBoxes.Count; i++)
                    {
                        instance.spectatingPlayerBoxes.ElementAt(i).Key.gameObject.SetActive(true);
                    }
                }
                else
                {
                    for (int i = 0; i < instance.spectatingPlayerBoxes.Count; i++)
                    {
                        instance.spectatingPlayerBoxes.ElementAt(i).Key.gameObject.SetActive(false);
                    }
                }
            }
            catch (Exception ex)
            {
                Plugin.mls.LogError(ex.Message + ex.StackTrace);
                throw;
            }
        }
        
        public static void ClearSpectatorUI(HUDManager instance)
        {
            Plugin.mls.LogInfo($"Method ClearSpectatorUI called.");
            
            for (int i = 0; i < instance.spectatingPlayerBoxes.Count; i++)
            {
                instance.spectatingPlayerBoxes.ElementAt(i).Key.gameObject.SetActive(false);
                instance.boxesAdded--;
            }
            
            instance.yOffsetAmount = 0f;
            instance.hasGottenPlayerSteamProfilePictures = false;
            instance.hasLoadedSpectateUI = false;
            
            instance.spectatingPlayerBoxes.Clear();

            if (instance.spectatingPlayerBoxes.Count != 0)
            {
                Plugin.mls.LogInfo("Error. spectatingPlayerBoxes not empty.");
            }
        }
    }
}