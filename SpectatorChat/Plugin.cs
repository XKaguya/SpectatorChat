#pragma warning disable 1591

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using GameNetcodeStuff;
using HarmonyLib;
using LethalCompanyInputUtils.Api;
using OPJosMod.ReviveCompany.CustomRpc;
using SpectatorChat.API;
using SpectatorChat.Other;
using SpectatorChat.Patch.Chat;
using SpectatorChat.Patch.HUD;
using SpectatorChat.Patch.Other;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SpectatorChat
{
    [BepInPlugin(ModGuid, ModName, ModVersion)]
    [BepInDependency("com.rune580.LethalCompanyInputUtils")]
    [BepInDependency("LC_API", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("FlipMods.TooManyEmotes", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("taffyko.NiceChat", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("rr.Flashlight", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("QuickRestart", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("MoreEmotes", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("quackandcheese.mirrordecor", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("me.eladnlg.customhud", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("OpJosMod.ReviveCompany", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("FlipMods.ReservedItemSlotCore", BepInDependency.DependencyFlags.SoftDependency)]
    public class Plugin : BaseUnityPlugin
    {
        private const string ModGuid = "Kaguya.SpectatorChat";
        private const string ModName = "SpectatorChat";
        private const string ModVersion = "1.1.6";

        public static ConfigEntry<bool> ShowClock { get; private set; }
        public static ConfigEntry<bool> CanLivingPlayerReceiveMessage { get; private set; }
        public static ConfigEntry<float> CoroutineDelay { get; private set; }

        private static readonly List<string> ModId = new List<string>();

        private readonly Harmony _harmony = new(ModGuid);

        private static Plugin Instance;

        public static ManualLogSource mls;

        public static bool KeyPressed { get; set; }

        public class InputKey : LcInputActions
        {
            [InputAction("<Keyboard>/r", Name = "ToggleDeathBox")]
            public InputAction ToggleDeathBoxKey { get; set; }
        }

        internal static InputKey InputActionInstance = new();

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            mls = BepInEx.Logging.Logger.CreateLogSource(ModGuid);

            mls.LogInfo("SpectatorChat has loaded.");

            LoadConfigs();
            PatchAll();

            LogPatchInfo();
        }

        private void LoadSpecificPatch()
        {
            ModId.Add("OpJosMod.ReviveCompany");
            ModId.Add("FlipMods.ReservedItemSlotCore");

            StartCoroutine(WaitForDependentMod());
        }
        
        private IEnumerator WaitForDependentMod()
        {
            float startTime = Time.time;
            float timeout = 120f;

            if (!GlobalVariables.Init)
            {
                GlobalVariables.Init = true;
                
                while (Time.time - startTime <= timeout)
                {
                    var loadedMods = BepInEx.Bootstrap.Chainloader.PluginInfos;

                    int num = 0;

                    foreach (var key in loadedMods.Keys)
                    {
                        if (key == "OpJosMod.ReviveCompany")
                        {
                            _harmony.PatchAll(typeof(ReviveCompanyPatch));

                            num++;
                        
                            mls.LogInfo($"Detected {key}, Proceeding PostPatch...");
                        }
                        else if (key == "FlipMods.ReservedItemSlotCore")
                        {
                            GlobalVariables.InitReservedItem = true;
                        
                            num++;
                        
                            mls.LogInfo($"Detected {key}, Proceeding...");
                        }
                    }
                
                    mls.LogInfo($"Mod found. Totally {num} Extra method were executed.");

                    yield return new WaitForSeconds(10f);
                }
            }
            
            mls.LogInfo("Timeout reached or canceled.");
        }

        private void PatchAll()
        {
            _harmony.PatchAll(typeof(EnableChatPatch));
            _harmony.PatchAll(typeof(SubmitChatPatch));
            _harmony.PatchAll(typeof(HUDPatch));
            _harmony.PatchAll(typeof(AddPlayerChatMessagePatch));

            _harmony.PatchAll(typeof(HUDManagerPostfixPatch));
            _harmony.PatchAll(typeof(KillPlayerPostfixPatch));
            _harmony.PatchAll(typeof(HideHUDPostfixPatch));
            _harmony.PatchAll(typeof(SetShipReadyToLandPostfix));

            _harmony.PatchAll(typeof(KeyPatch));
            
            LoadSpecificPatch();
        }

        private void LoadConfigs()
        {
            ShowClock = Config.Bind("Settings", "ShowClock", true, "Show the clock for spectator players.");
            CanLivingPlayerReceiveMessage = Config.Bind("Settings", "CanLivingPlayerReceiveMessage", false, "Can living player receive dead player's message.");
            CoroutineDelay = Config.Bind("Settings", "CoroutineDelay", 1f, "How long will the coroutine delay.");
            
            mls.LogInfo($"Plugin loaded with config: ShowClock: {ShowClock.Value}, CanLivingPlayerReceiveMessage: {CanLivingPlayerReceiveMessage.Value}, CoroutineDelay: {CoroutineDelay.Value}");
        }

        private void LogPatchInfo()
        {
            mls.LogInfo(HarmonyAPI.GetPatchInfoAsString(_harmony, AccessTools.Method(typeof(HUDManager), nameof(HUDManager.Awake))));
            mls.LogInfo(HarmonyAPI.GetPatchInfoAsString(_harmony, AccessTools.Method(typeof(HUDManager), "EnableChat_performed")));
            mls.LogInfo(HarmonyAPI.GetPatchInfoAsString(_harmony, AccessTools.Method(typeof(HUDManager), "SubmitChat_performed")));
            mls.LogInfo(HarmonyAPI.GetPatchInfoAsString(_harmony, AccessTools.Method(typeof(PlayerControllerB), "KillPlayer")));
            mls.LogInfo(HarmonyAPI.GetPatchInfoAsString(_harmony, AccessTools.Method(typeof(PlayerControllerB), "Update")));
            mls.LogInfo(HarmonyAPI.GetPatchInfoAsString(_harmony, AccessTools.Method(typeof(HUDManager), "HideHUD")));
            mls.LogInfo(HarmonyAPI.GetPatchInfoAsString(_harmony, AccessTools.Method(typeof(StartOfRound), "SetShipReadyToLand")));
            mls.LogInfo(HarmonyAPI.GetPatchInfoAsString(_harmony, AccessTools.Method(typeof(CompleteRecievedTasks), "RevivePlayer")));
        }
    }
}
