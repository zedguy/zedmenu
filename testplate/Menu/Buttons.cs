﻿using zedmenu.Classes;
using zedmenu.Mods;
using static zedmenu.Settings;
using static zedmenu.Menu.Main;
namespace zedmenu.Menu
{
    internal class Buttons
    {
        public static ButtonInfo[][] buttons = new ButtonInfo[][]
        {
            new ButtonInfo[] { // Main Mods
                new ButtonInfo { buttonText = "Settings", method =() => SettingsMods.EnterSettings(), isTogglable = false, toolTip = "Opens the main settings page for the menu."},
                new ButtonInfo { buttonText = "Movement Mods", method =() => SettingsMods.MovementSettings(), isTogglable = false, toolTip = "Opens the movement mods for the menu."},
                new ButtonInfo { buttonText = "Rig Mods", method =() => SettingsMods.MenuSettings(), isTogglable = false, toolTip = "Opens the rig mods for the menu."},
                new ButtonInfo { buttonText = "Hitsound Mods", method =() => SettingsMods.HitSettings(), isTogglable = false, toolTip = "Opens the Hitsound mods for the menu."},
                new ButtonInfo { buttonText = "Disguises", method =() => SettingsMods.Disguises(), isTogglable = false, toolTip = "Opens the disguises for the menu."},
                new ButtonInfo { buttonText = "Misc Mods", method =() => SettingsMods.ProjectileSettings(), isTogglable = false, toolTip = "Opens the projectile mods for the menu."},
                new ButtonInfo { buttonText = "Trolling Mods", method =() => SettingsMods.Trolls(), isTogglable = false, toolTip = "Opens the trolling mods for the menu."},
                new ButtonInfo { buttonText = "thank you for using my menu", isTogglable = false},
            },

            new ButtonInfo[] { // Settings
                new ButtonInfo { buttonText = "Return to Main", method =() => Global.ReturnHome(), isTogglable = false, toolTip = "Returns to the main page of the menu."},
                new ButtonInfo { buttonText = "Right Hand", enableMethod =() => SettingsMods.RightHand(), disableMethod =() => SettingsMods.LeftHand(), toolTip = "Puts the menu on your right hand."},
                new ButtonInfo { buttonText = "Notifications", enableMethod =() => SettingsMods.EnableNotifications(), disableMethod =() => SettingsMods.DisableNotifications(), enabled = !disableNotifications, toolTip = "Toggles the notifications."},
                new ButtonInfo { buttonText = "Disconnect Button", enableMethod =() => SettingsMods.EnableDisconnectButton(), disableMethod =() => SettingsMods.DisableDisconnectButton(), enabled = disconnectButton, toolTip = "Toggles the disconnect button."},
                new ButtonInfo { buttonText = "Night Mode", method =() => Main.NightTime(), isTogglable = false, toolTip = "Night time."},
                new ButtonInfo { buttonText = "AntiReport", method =() => Safety.AntiReportDisconnect(), toolTip = "Disconnects when reports are attempted."},
                new ButtonInfo { buttonText = "AntiModerator", method =() => Safety.AntiModerator(), toolTip = "Disconnects when a stick joins."},
                new ButtonInfo { buttonText = "Change Platform Mode", overlapText = "Change Platform Mode <color=grey>[</color><color=#96ffb2>Sphere</color><color=grey>]</color>", method =() => Locomotion.platsize(), isTogglable = false, toolTip = "Changes platform type."},
                new ButtonInfo { buttonText = "Change Draw Size", method =() => Locomotion.drawsize(), isTogglable = false, toolTip = "Changes draw size."},
                new ButtonInfo { buttonText = "Change Speed Boost Amount", overlapText = "Change Speed Boost Amount <color=grey>[</color><color=#96ffb2>Normal</color><color=grey>]</color>", method =() => Locomotion.ChangeSpeedBoostAmount(), isTogglable = false, toolTip = "Changes the speed of the speed boosts."},
                new ButtonInfo { buttonText = "No Spam Delay"},
                new ButtonInfo { buttonText = "RPC Flush", method =() => Main.RPCProtection(), isTogglable = false, toolTip = "flush rpcs (report flush?)."},
            },

            new ButtonInfo[] { // Menu Settings
                new ButtonInfo { buttonText = "Return to Main", method =() => Global.ReturnHome(), isTogglable = false, toolTip = "Returns to the main page of the menu."},
                new ButtonInfo { buttonText = "Ghost "+atxt, method =() => Rig.Ghost(), disableMethod =() => Rig.disablecopy(),  toolTip = "Simple Ghost monke."},
                new ButtonInfo { buttonText = "Freeze Rig "+atxt, method =() => Rig.RigFreeze(), disableMethod =() => Rig.disablecopy(),  toolTip = "Ghost but follow the head."},
                new ButtonInfo { buttonText = "Weird Arms "+atxt, method =() => Rig.WeirdArms(), disableMethod =() => Rig.disablecopy(),  toolTip = "makes your arms go downward weirdly."},
                new ButtonInfo { buttonText = "Rig Gun", method =() => Rig.RigGun(), disableMethod =() => Rig.disablecopy(),  toolTip = "moves rig to gun."},
                new ButtonInfo { buttonText = "Orbit Gun", method =() => Rig.OrbitGun(), disableMethod =() => Rig.disablecopy(),  toolTip = "Orbits target player."},
                new ButtonInfo { buttonText = "Orbit Random "+atxt, method =() => Rig.Orbit(), disableMethod =() => Rig.disablecopy(), toolTip = "Orbits random player."},
            },

            new ButtonInfo[] { // Movement Settings
                new ButtonInfo { buttonText = "Return to Main", method =() => Global.ReturnHome(), isTogglable = false, toolTip = "Returns to the main page of the menu."},
                new ButtonInfo { buttonText = "Platforms "+gtxt, method =() => Locomotion.Plats(), toolTip = "Simple platforms."},
                new ButtonInfo { buttonText = "Up and Down "+ttxt, method =() => Locomotion.updown(), toolTip = "Up and down using triggers."},
                new ButtonInfo { buttonText = "Drive", method =() => Locomotion.Drive(),toolTip = "Lets you drive around in your invisible car. Use the <color=green>joystick</color> to move."},
                new ButtonInfo { buttonText = "Iron Man "+atxt, method =() => Locomotion.IronMan(), toolTip = "Turns you into iron man, rotate your hands around to change direction."},
                new ButtonInfo { buttonText = "Noclip "+ttxt, method =() => Locomotion.Noclip(), toolTip = "Phase through stuff."},
                new ButtonInfo { buttonText = "Speed Boost", method =() => Locomotion.SpeedBoost(), /*disableMethod =() => Movement.DisableSpeedBoost(),*/ toolTip = "Changes your speed to whatever you set it to."},
            },

            new ButtonInfo[] { // Projectile Settings
                new ButtonInfo { buttonText = "Return to Main", method =() => Global.ReturnHome(), isTogglable = false, toolTip = "Returns to the main page of the menu."},
                new ButtonInfo { buttonText = "Draw "+gtxt, method =() => Locomotion.draw(), toolTip = "Draw with spheres."},
                new ButtonInfo { buttonText = "Finger Guns "+gtxt, method =() => Locomotion.shoot(), toolTip = "Shoots pellets."},
                new ButtonInfo { buttonText = "Tracers", method =() => Visuals.Tracers(), toolTip = "Traces other players."},
                new ButtonInfo { buttonText = "Beacons", method =() => Visuals.Beacons(), toolTip = "Beacons on other players."},
                new ButtonInfo { buttonText = "Rapid Slingshot "+atxt, method =() => Global.RapidFireSlingshot(), toolTip = "Rapidly fires slingshot."},
                new ButtonInfo { buttonText = "Destroy Gun", method =() => Global.DestroyGun(), toolTip = "Makes invis to future players."},
                new ButtonInfo { buttonText = "Copy ID Gun", method =() => Global.CopyIDGun(), toolTip = "Copies IDs."},
                new ButtonInfo { buttonText = "Spam snow "+gtxt, method =() => Projectiles.spam()},
                new ButtonInfo { buttonText = "Spam impacts "+gtxt, method =() => Projectiles.imspam()},
            },

            new ButtonInfo[] { // Hitsounds Settings
                new ButtonInfo { buttonText = "Return to Main", method =() => Global.ReturnHome(), isTogglable = false, toolTip = "Returns to the main page of the menu."},
                new ButtonInfo { buttonText = "Set to Rock", method =() => HitSounds.SetHitsounds(0), isTogglable = false, toolTip = "Sets hitsounds to rock."},
                new ButtonInfo { buttonText = "Set to Grass", method =() => HitSounds.SetHitsounds(7), isTogglable = false, toolTip = "Sets hitsounds to grass."},
                new ButtonInfo { buttonText = "Set to Dirt", method =() => HitSounds.SetHitsounds(14), isTogglable = false, toolTip = "Sets hitsounds to dirt."},
                new ButtonInfo { buttonText = "Set to Bark", method =() => HitSounds.SetHitsounds(8), isTogglable = false, toolTip = "Sets hitsounds to bark."},
                new ButtonInfo { buttonText = "Set to Metal", method =() => HitSounds.SetHitsounds(18), isTogglable = false, toolTip = "Sets hitsounds to metal."},
                new ButtonInfo { buttonText = "Set to Glass", method =() => HitSounds.SetHitsounds(28), isTogglable = false, toolTip = "Sets hitsounds to glass."},
                new ButtonInfo { buttonText = "Set to Webs", method =() => HitSounds.SetHitsounds(82), isTogglable = false, toolTip = "Sets hitsounds to webs."},
                new ButtonInfo { buttonText = "Set to Bones", method =() => HitSounds.SetHitsounds(77), isTogglable = false, toolTip = "Sets hitsounds to bones."},
                new ButtonInfo { buttonText = "Set to Pop", method =() => HitSounds.SetHitsounds(84), isTogglable = false, toolTip = "Sets hitsounds to pop."},
                new ButtonInfo { buttonText = "Set to Turkey", method =() => HitSounds.SetHitsounds(83), isTogglable = false, toolTip = "Sets hitsounds to turkey."},
                new ButtonInfo { buttonText = "Set to Frog", method =() => HitSounds.SetHitsounds(91), isTogglable = false, toolTip = "Sets hitsounds to frog."},
                new ButtonInfo { buttonText = "Set to Fruit", method =() => HitSounds.SetHitsounds(96), isTogglable = false, toolTip = "Sets hitsounds to fruit."},
                new ButtonInfo { buttonText = "Set to LavaRock", method =() => HitSounds.SetHitsounds(231), isTogglable = false, toolTip = "Sets hitsounds to lavarock."},
                new ButtonInfo { buttonText = "Set to Snow", method =() => HitSounds.SetHitsounds(32), isTogglable = false, toolTip = "Sets hitsounds to snow."},
                new ButtonInfo { buttonText = "Set to WaterBalloon", method =() => HitSounds.SetHitsounds(204), isTogglable = false, toolTip = "Sets hitsounds to waterballoon."},
                new ButtonInfo { buttonText = "Set to Ice", method =() => HitSounds.SetHitsounds(59), isTogglable = false, toolTip = "Sets hitsounds to slippery."},
                new ButtonInfo { buttonText = "Randomize Hitsounds", method =() => HitSounds.SetHitsounds(0,true), isTogglable = false, toolTip = "Randomize hitsounds."},
                new ButtonInfo { buttonText = "Toggle Random Sounds", method =() => HitSounds.randomize(), toolTip = "Constantly randomizes hitsounds."},
                new ButtonInfo { buttonText = "Random Sound Spam "+gtxt, method =() => HitSounds.rsp(), toolTip = "Constantly spams random hitsounds."},
            },

            new ButtonInfo[] { // Hitsounds Settings
                new ButtonInfo { buttonText = "Return to Main", method =() => Global.ReturnHome(), isTogglable = false, toolTip = "Returns to the main page of the menu."},
                new ButtonInfo { buttonText = "Random Disguise", method =() => Safety.ChangeIdentity(), isTogglable = false, toolTip = "disguise."},
                new ButtonInfo { buttonText = "Become PBBV", method =() => Disguise.BecomePBBV(), isTogglable = false, toolTip = "disguise."},
                new ButtonInfo { buttonText = "Become ECHO", method =() => Disguise.BecomeECHO(), isTogglable = false, toolTip = "disguise."},
                new ButtonInfo { buttonText = "Become Daisy", method =() => Disguise.BecomeDAISY09(), isTogglable = false, toolTip = "disguise."},
                new ButtonInfo { buttonText = "Become J3VU", method =() => Disguise.BecomeJ3VU(), isTogglable = false, toolTip = "disguise."},
                new ButtonInfo { buttonText = "Become Void", method =() => Disguise.BecomeVoid(), isTogglable = false, toolTip = "disguise."},
            },

            new ButtonInfo[] { // Hitsounds Settings
                new ButtonInfo { buttonText = "Return to Main", method =() => Global.ReturnHome(), isTogglable = false, toolTip = "Returns to the main page of the menu."},
                new ButtonInfo { buttonText = "Spaz name", method =() => Rig.CREEPY(), toolTip = "really weird.", disableMethod =() => Rig.disablecopy()},
            },
        };
    }
}
