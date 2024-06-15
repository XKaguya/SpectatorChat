#pragma warning disable 1591

using System;
using System.Collections;
using System.Linq;
using System.Threading;
using BepInEx.Bootstrap;
using SpectatorChat.Other;
using UnityEngine;

namespace SpectatorChat.API
{
    public class Generic : MonoBehaviour
    {
        private static Generic? instance;
        
        private Coroutine? _permanentTransparentCoroutine;
        
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
                return ulong.Parse(userId) == GlobalVariables.PlayerControllerInstance!.playerClientId;
            }
            catch (Exception ex)
            {
                Plugin.mls.LogError(ex.Message + ex.StackTrace);
                
                return false;
            }
        }

        public bool IsCoroutineNull()
        {
            if (_permanentTransparentCoroutine == null)
            {
                return true;
            }

            return false;
        }

        public bool StartPermanentTransparent()
        {
            if (_permanentTransparentCoroutine == null)
            {
                GlobalVariables.CoroutineCancellationTokenSource = new CancellationTokenSource();
                _permanentTransparentCoroutine = StartCoroutine(PermanentTransparentCoroutine(GlobalVariables.HUDElements!, GlobalVariables.CoroutineCancellationTokenSource));

                return true;
            }

            return false;
        }
        
        public bool StopPermanentTransparent()
        {
            if (_permanentTransparentCoroutine != null && GlobalVariables.CoroutineCancellationTokenSource != null)
            {
                GlobalVariables.CoroutineCancellationTokenSource.Cancel();
                GlobalVariables.CoroutineCancellationTokenSource = null;
                _permanentTransparentCoroutine = null;

                return true;
            }

            return false;
        }

        public static bool IsModLoaded(string guid)
        {
            return Chainloader.PluginInfos.ContainsKey(guid);
        }
        
        private IEnumerator PermanentTransparentCoroutine(HUDElement[] elements, CancellationTokenSource coroutineCancellationTokenSource)
        {
            while (!coroutineCancellationTokenSource.IsCancellationRequested)
            {
                try
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
                        ReservedItemUI.SwitchReservedItemUI(true);
                    }
                }
                catch (Exception ex)
                {
                    Plugin.mls.LogError(ex.Message + ex.StackTrace);
                }
                
                yield return Plugin.CoroutineDelay.Value;
            }
            
            if (IsModLoaded("FlipMods.ReservedItemSlotCore"))
            {
                ReservedItemUI.SwitchReservedItemUI(false);
            }
            
            _permanentTransparentCoroutine = null;
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