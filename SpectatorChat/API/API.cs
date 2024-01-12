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
        
        public static HUDElement[] HUDElements { get; set; }
        
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
            else
            {
                return false;
            }
        }
        
        public bool StopPermanentTransparent()
        {
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
                
                yield return 1f;
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
                        Plugin.mls.LogInfo($"{instance.spectatingPlayerBoxes.ElementAt(i).Key.name} has been actived.");
                    }
                }
                else
                {
                    for (int i = 0; i < instance.spectatingPlayerBoxes.Count; i++)
                    {
                        instance.spectatingPlayerBoxes.ElementAt(i).Key.gameObject.SetActive(false);
                        Plugin.mls.LogInfo(
                            $"{instance.spectatingPlayerBoxes.ElementAt(i).Key.name} has been deactived.");
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