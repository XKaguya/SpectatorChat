- [ SpectatorChat](#head1)
- [ Configuration](#head2)
- [ Compatible](#head3)
- [ Changelog](#head4)
	- [ 1.0.0](#head5)
	- [ 1.0.1](#head6)
	- [ 1.0.4](#head7)
		- [ General](#head8)
			- [Fixed a problem that will cause all player's UI disappear.](#head9)
		- [ Feature](#head10)
			- [You can now Press HotKey to manually control about the death player boxes.](#head11)
				- [Default key is R. It is configurable in the game settings.](#head12)
		- [Spectator players can see the clock if the config `ShowClock` is true.](#head13)
			- [You can set to `false` to disable this feature.](#head14)
	- [ 1.0.5](#head15)
		- [ Feature](#head16)
			- [You can check log in `%AppData%\r2modmanPlus-local\LethalCompany\profiles\Default\BepInEx` for debug.](#head17)
			- [ e.g.](#head18)
			- [If you encountered any bugs, Give me these logs then i'll fix that.](#head19)
	- [ 1.0.6](#head20)
		- [ Feature](#head21)
			- [You can now set `CanLivingPlayerReceiveMessage` to true to enable the ability to talk with living player.](#head22)
			- [Mod will now show who patched the methods mod should patch. By sending this to me can also helps me.](#head23)
		- [ General](#head24)
			- [Added support for following mods:](#head25)
			- [If you encountered any bugs, Give me these logs then i'll fix that.](#head26)
	- [ 1.0.7](#head20)
		- [ General](#head26)
			- [Currently not compatible with `ChatCommands`.](#head27)
			- [Fixed compatible with mod `EladsHUD`](#head29)
			- [Fixed compatible with mod `MirrorDecor`](#head30)
			- [Changed all codes into instructions.](#head31)
# <span id="head1"> SpectatorChat</span>

SpectatorChat is a mod based on [BepInEx](https://github.com/BepInEx/BepInEx). This mod allows dead player use text to chat with other player.Default is only dead. By setting CanLivingPlayerReceiveMessage for true, Dead player can talk with living player.

If you encounter any bug, Like can't send message, have no Chat UI, please tell me and bring logs in `%AppData%\r2modmanPlus-local\LethalCompany\profiles\Default\BepInEx`.

# <span id="head2"> Configuration</span>
```
## Show the clock for spectator players.
# Setting type: Boolean
# Default value: true
ShowClock = true

## Can living player receive dead player's message.
# Setting type: Boolean
# Default value: false
CanLivingPlayerReceiveMessage = false
```

# <span id="head3"> Compatible</span>

This mod patched at following method.
If you meet problem about no chat UI or can't input, Find specific patch in the logs and see what else patched the method.

This mod using `SoftDependency` for compatible. It may cause errors.
Please tell me when it happens.
```
HUDManager.Awake
HUDManager.EnableChat_performed
HUDManager.SubmitChat_performed

PlayerControllerB.KillPlayer
PlayerControllerB.Update

StartOfRound.ReviveDeadPlayers
```

# <span id="head4"> Changelog</span>

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## <span id="head5"> 1.0.0</span>

First release.

## <span id="head6"> 1.0.1</span>

Only Chat ui will be display now.
The configure of courtine will be publish soon.

## <span id="head7"> 1.0.4</span>

### <span id="head8"> General</span>

#### <span id="head9">Fixed a problem that will cause all player's UI disappear.</span>

### <span id="head10"> Feature</span>

#### <span id="head11">You can now Press HotKey to manually control about the death player boxes.</span>
##### <span id="head12">Default key is R. It is configurable in the game settings.</span>

### <span id="head13">Spectator players can see the clock if the config `ShowClock` is true.</span>
#### <span id="head14">You can set to `false` to disable this feature.</span>

## <span id="head15"> 1.0.5</span>

### <span id="head16"> Feature</span>
#### <span id="head17">You can check log in `%AppData%\r2modmanPlus-local\LethalCompany\profiles\Default\BepInEx` for debug.</span>
#### <span id="head18"> e.g.</span>
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
#### <span id="head19">If you encountered any bugs, Give me these logs then i'll fix that.</span>

## <span id="head20"> 1.0.6</span>

### <span id="head21"> Feature</span>

#### <span id="head22">You can now set `CanLivingPlayerReceiveMessage` to true to enable the ability to talk with living player.</span>

#### <span id="head23">Mod will now show who patched the methods mod should patch. By sending this to me can also helps me.</span>

### <span id="head24"> General</span>

### <span id="head25">Added support for following mods:</span>

```
LC_API
TooManyEmotes
NiceChat
FlashlightToggle
QuickRestart
More_Emotes
```

## <span id="head26"> 1.0.7</span>

### <span id="head27"> Currently not compatible with `ChatCommands`.

### <span id="head28"> General

#### <span id="head29">Fixed compatible with mod `EladsHUD`
#### <span id="head30">Fixed compatible with mod `MirrorDecor`
#### <span id="head31">Changed all codes into instructions.

### <span id="head26">If you encountered any bugs, Give me these logs then i'll fix that.</span>
