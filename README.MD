# SpectatorChat
## SpectatorChat is a mod based on [BepInEx](https://github.com/BepInEx/BepInEx). This mod allows dead player use text to chat with other player.Default is only dead. By setting CanLivingPlayerReceiveMessage for true, Dead player can talk with living player.
If you encounter any bug, Like can't send message, have no Chat UI, please tell me and bring your profile code.

## Currently not compatible with `ChatCommands`.

# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

# 1.0.0

First release.

# 1.0.1

Only Chat ui will be display now.
The configure of courtine will be publish soon.

# 1.0.4

## General

### Fixed a problem that will cause all player's UI disappear.

## Feature

### You can now Press HotKey to manually control about the death player boxes.
#### Default key is R. It is configurable in the game settings.

### Spectator players can see the clock if the config `ShowClock` is true.
#### You can set to `false` to disable this feature.

# 1.0.5

## Feature
### You can check log in `%AppData%\r2modmanPlus-local\LethalCompany\profiles\Default\BepInEx` for debug.
### e.g.
```
[Info   :Kaguya.SpectatorChat] Method HUDManager.Awake is patched by:
- Prefix: BiggerLobby
- Postfix: BiggerLobbyA
- Postfix: Kaguya.SpectatorChat

[Info   :Kaguya.SpectatorChat] Method HUDManager.EnableChat_performed is patched by:
- Transpiler: Kaguya.SpectatorChat

[Info   :Kaguya.SpectatorChat] Method HUDManager.SubmitChat_performed is patched by:
- Transpiler: ModAPI
- Transpiler: Kaguya.SpectatorChat

[Info   :Kaguya.SpectatorChat] Method GameNetcodeStuff.PlayerControllerB.KillPlayer is patched by:
- Postfix: Kaguya.SpectatorChat
- Transpiler: ModAPI
- Transpiler: Kaguya.SpectatorChat

[Info   :Kaguya.SpectatorChat] Method GameNetcodeStuff.PlayerControllerB.Update is patched by:
- Postfix: CustomSounds
- Postfix: Kaguya.SpectatorChat

[Info   :Kaguya.SpectatorChat] Method StartOfRound.ReviveDeadPlayers is patched by:
- Postfix: Kaguya.SpectatorChat
```

### If you encountered any bugs, Give me these logs then i'll fix that.

# 1.0.6

## Feature

### You can now set `CanLivingPlayerReceiveMessage` to true to enable the ability to talk with living player.

### Mod will now show who patched the methods mod should patch. By sending this to me can also helps me.

### If you encountered any bugs, Give me these logs then i'll fix that.

## General

## Added support for following mods:

```
LC_API
TooManyEmotes
NiceChat
FlashlightToggle
QuickRestart
More_Emotes
```

# 1.0.7

# Currently not compatible with `ChatCommands`.

## General

### Fixed compatible with mod `EladsHUD`
### Fixed compatible with mod `MirrorDecor`
### Changed all codes into instructions.

# 1.0.8

## General

### Resolved version number issue.

### Resolved `[Warning: Unity Log] The script 'SpectatorChat.Plugin' could not be instantiated!` issue. #2
#### --- This is caused by the mod require a hard dependence mod. That i've forget to written in it.

### Resolved a issue that will cause summary broken.
#### --- This is caused by Zeekers called `ReviveDeadPlayers` by a `AnimatedShipDoor`. _WTF IS THIS ??_
#### --- Changed the patch to `HideHUD` method. Now the summary is working normallly now.

### Removed useless reference and useless codes.

# 1.0.9

## General

### Fixed while player is dead, the hurt UI still there.
#### Like when you killed by Masked, or etc.

### Fixed while player is dead, Switching the spectator boxes, will show last round's dead players boxes.

## Known Issue

### While `CanLivingPlayerReceiveMessage` is true. It's inevitably that chat distance between surviving players is also infinite.
#### Will looking into a way to solve this.

# 1.1.0

## General

### Resolved a problem that will cause dead player "revive".

### Resolved a problem that the spectator boxes not showing correctly.

# 1.1.1

## General

### Resolved a class spell problem.
