using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace SpectatorChat
{
    public class API : MonoBehaviour
    {
        private static API instance;
        
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

        public static API Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameObject("SpectatorChatAPI").AddComponent<API>();
                }

                return instance;
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
            HarmonyAPI.LogCallingMethod("StartPermanentTransparent");
            
            GetHUDElements();
            
            if (permanentTransparentCoroutine == null)
            {
                permanentTransparentCoroutine = StartCoroutine(PermanentTransparentCoroutine(HUDElements));

                return true;
            }
            else
            {
                return false;
            }
        }
        
        public bool StopPermanentTransparent()
        {
            HarmonyAPI.LogCallingMethod("StopPermanentTransparent");
            
            if (permanentTransparentCoroutine != null)
            {
                StopCoroutine(permanentTransparentCoroutine);
                permanentTransparentCoroutine = null;

                return true;
            }
            else
            {
                return false;
            }
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
    }
}