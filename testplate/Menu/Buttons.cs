using zedmenu.Classes;
using zedmenu.Mods;
using static zedmenu.Settings;
using static zedmenu.Menu.Main;
using GorillaTag.GuidedRefs;
using System;

namespace zedmenu.Menu
{
    internal class Buttons
    {
        public static ButtonInfo[][] buttons = new ButtonInfo[][]
        {
            new ButtonInfo[] { // Mod Categories
                new ButtonInfo { buttonText = "Settings", method =() => SettingsMods.EnterSettings(), isTogglable = false, toolTip = "Opens the main settings page for the menu."},
                new ButtonInfo { buttonText = "Movement", method =() => SettingsMods.MovementSettings(), isTogglable = false, toolTip = "Opens the movement mods for t he menu."},
                new ButtonInfo { buttonText = "VRRig", method =() => SettingsMods.MenuSettings(), isTogglable = false, toolTip = "Opens the rig mods for the menu."},
                new ButtonInfo { buttonText = "Visuals", method =() => SettingsMods.visual(), isTogglable = false, toolTip = "Opens the visual mods for the menu."},
                new ButtonInfo { buttonText = "Hitsounds", method =() => SettingsMods.HitSettings(), isTogglable = false, toolTip = "Opens the Hitsound mods for the menu."},
                new ButtonInfo { buttonText = "Disguises", method =() => SettingsMods.Disguises(), isTogglable = false, toolTip = "Opens the disguises for the menu."},
                new ButtonInfo { buttonText = "Projectiles", method =() => SettingsMods.bannable(), isTogglable = false, toolTip = "Opens the projectile mods for the menu."},
                new ButtonInfo { buttonText = "Misc", method =() => SettingsMods.ProjectileSettings(), isTogglable = false, toolTip = "Opens the misc mods for the menu."},
                new ButtonInfo { buttonText = "Credits", method =() => SettingsMods.Trolls(), isTogglable = false, toolTip = "Opens the credits for the menu."},
            },

            new ButtonInfo[] { // Settings
                new ButtonInfo { buttonText = "Return to Main", method =() => Global.ReturnHome(), isTogglable = false, toolTip = "Returns to the main page of the menu."},
                //new ButtonInfo { buttonText = "Right Hand", enableMethod =() => SettingsMods.RightHand(), disableMethod =() => SettingsMods.LeftHand(), toolTip = "Puts the menu on your right hand."},
                new ButtonInfo { buttonText = "Notifications", enableMethod =() => SettingsMods.EnableNotifications(), disableMethod =() => SettingsMods.DisableNotifications(), enabled = !disableNotifications, toolTip = "Toggles the notifications."},
                new ButtonInfo { buttonText = "Disconnect Button", enableMethod =() => SettingsMods.EnableDisconnectButton(), disableMethod =() => SettingsMods.DisableDisconnectButton(), enabled = disconnectButton, toolTip = "Toggles the disconnect button."},
                new ButtonInfo { buttonText = "Primary Disconnect "+ytxt, method =() => Global.PrimDisc(), toolTip = "disconnects when primary button on left pressed."},
                new ButtonInfo { buttonText = "Night Mode", method =() => Main.NightTime(), isTogglable = false, toolTip = "Night time."},
                new ButtonInfo { buttonText = "AntiReport", method =() => Safety.AntiReportDisconnect(), toolTip = "Disconnects when reports are attempted."},
                new ButtonInfo { buttonText = "AntiModerator", method =() => Safety.AntiModerator(), toolTip = "Disconnects when a stick joins."},
                new ButtonInfo { buttonText = "Change Platform Mode", overlapText = "Change Platform Mode <color=grey>[</color><color=#96ffb2>Round Classic</color><color=grey>]</color>", method =() => Locomotion.platsize(), isTogglable = false, toolTip = "Changes platform type."},
                new ButtonInfo { buttonText = "Change Draw Size", method =() => Locomotion.drawsize(), isTogglable = false, toolTip = "Changes draw size."},
                new ButtonInfo { buttonText = "Change Speed Boost Amount", overlapText = "Change Speed Boost Amount <color=grey>[</color><color=#96ffb2>Normal</color><color=grey>]</color>", method =() => Locomotion.ChangeSpeedBoostAmount(), isTogglable = false, toolTip = "Changes the speed of the speed boosts."},
                new ButtonInfo { buttonText = "Change Fly Speed", overlapText = "Change Fly Speed <color=grey>[</color><color=#96ffb2>Normal</color><color=grey>]</color>", method =() => Locomotion.changefly(), isTogglable = false, toolTip = "Changes the speed of fly mods."},
                new ButtonInfo { buttonText = "Change Visual Colors", overlapText = "Change Visual Colors <color=grey>[</color><color=#96ffb2>" + "Player Color" + "</color><color=grey>]</color>", method =() => Visuals.ChangeSpeedBoostAmount(), isTogglable = false, toolTip = "Changes draw size."},
                new ButtonInfo { buttonText = "Change RPC Clear Speed", overlapText = "Change RPC Clear Speed <color=grey>[</color><color=#96ffb2>" + "10s" + "</color><color=grey>]</color>", method =() => Global.ChangeRPCSpeed(), isTogglable = false, toolTip = "Changes time until rpcs flush automatically."},
                new ButtonInfo { buttonText = "Auto Flush RPCs", method =() => Global.autorpc(), toolTip = "auto flush rpcs (report flush?)."},
                new ButtonInfo { buttonText = "RPC Flush", method =() => Main.RPCProtection(), isTogglable = false, toolTip = "flush rpcs (report flush?)."},
            },

            new ButtonInfo[] { // Rig Mods
                new ButtonInfo { buttonText = "Return to Main", method =() => Global.ReturnHome(), isTogglable = false, toolTip = "Returns to the main page of the menu."},
                new ButtonInfo { buttonText = "Ghost "+atxt, method =() => Rig.Ghost(), disableMethod =() => Rig.disablecopy(),  toolTip = "Simple Ghost monke."},
                new ButtonInfo { buttonText = "Invis "+btxt, method =() => Rig.Invis(), disableMethod =() => Rig.disablecopy(),  toolTip = "Simple Invis monke."},
                new ButtonInfo { buttonText = "Freeze Rig "+atxt, method =() => Rig.RigFreeze(), disableMethod =() => Rig.disablecopy(),  toolTip = "Ghost but follow the head."},
                new ButtonInfo { buttonText = "Weird Arms "+atxt, method =() => Rig.WeirdArms(), disableMethod =() => Rig.disablecopy(),  toolTip = "makes your arms go downward weirdly."},
                new ButtonInfo { buttonText = "Rig Gun", method =() => Rig.RigGun(), disableMethod =() => Rig.disablecopy(),  toolTip = "moves rig to gun."},
                new ButtonInfo { buttonText = "Point Gun", method =() => Rig.PointGun(), disableMethod =() => Rig.disablecopy(),  toolTip = "points at people."},
                new ButtonInfo { buttonText = "Orbit Gun", method =() => Rig.OrbitGun(), disableMethod =() => Rig.disablecopy(),  toolTip = "Orbits target player."},
                new ButtonInfo { buttonText = "Orbit Random "+atxt, method =() => Rig.Orbit(), disableMethod =() => Rig.disablecopy(), toolTip = "Orbits random player."},
                new ButtonInfo { buttonText = "Lucy Random "+atxt, method =() => Rig.LucyRandom(), disableMethod =() => Rig.disablecopy(), toolTip = "Chases random player."},
                new ButtonInfo { buttonText = "Lucy Gun", method =() => Rig.LucyGun(), disableMethod =() => Rig.disablecopy(), toolTip = "Chases target player."},
                new ButtonInfo { buttonText = "Capuchin", method =() => Rig.Capuchin(), disableMethod =() => Rig.disablecopy(), toolTip = "Replicates the fangame rig movements."},
                new ButtonInfo { buttonText = "Spin Head X", method =() => Rig.SpinHeadX(), disableMethod =() => Rig.disablecopy(), toolTip = "spins your head."},
                new ButtonInfo { buttonText = "Spin Head Y", method =() => Rig.SpinHeadY(), disableMethod =() => Rig.disablecopy(), toolTip = "spins your head."},
                new ButtonInfo { buttonText = "Spin Head Z", method =() => Rig.SpinHeadZ(), disableMethod =() => Rig.disablecopy(), toolTip = "spins your head."},
            },

            new ButtonInfo[] { // Movement Mods
                new ButtonInfo { buttonText = "Return to Main", method =() => Global.ReturnHome(), isTogglable = false, toolTip = "Returns to the main page of the menu."},
                new ButtonInfo { buttonText = "Platforms "+gtxt, method =() => Locomotion.Plats(), toolTip = "Simple platforms."},
                new ButtonInfo { buttonText = "Up and Down "+ttxt, method =() => Locomotion.updown(), toolTip = "Up and down using triggers."},
                new ButtonInfo { buttonText = "Drive", method =() => Locomotion.Drive(),toolTip = "Lets you drive around in your invisible car. Use the <color=green>joystick</color> to move."},
                new ButtonInfo { buttonText = "Iron Man "+atxt, method =() => Locomotion.IronMan(), toolTip = "Turns you into iron man, rotate your hands around to change direction."},
                new ButtonInfo { buttonText = "Fly "+atxt, method =() => Locomotion.Fly(), toolTip = "Flies in the direction of your head."},
                new ButtonInfo { buttonText = "Noclip "+ttxt, method =() => Locomotion.Noclip(), toolTip = "Phase through stuff."},
                new ButtonInfo { buttonText = "Speed Boost", method =() => Locomotion.SpeedBoost(), /*disableMethod =() => Movement.DisableSpeedBoost(),*/ toolTip = "Changes your speed to whatever you set it to."},
                new ButtonInfo { buttonText = "TP Gun", method =() => Locomotion.TPGun(), toolTip = "teleports to gun thing."},
                new ButtonInfo { buttonText = "No Gravity", method =() => Locomotion.ZeroGravity(), toolTip = "float around."},
                new ButtonInfo { buttonText = "WASD Fly", method =() => Locomotion.WASDFly(), toolTip = "move with w, a, s, d."},
            },

            new ButtonInfo[] { // Misc Mods
                new ButtonInfo { buttonText = "Return to Main", method =() => Global.ReturnHome(), isTogglable = false, toolTip = "Returns to the main page of the menu."},
                new ButtonInfo { buttonText = "Draw "+gtxt, method =() => Locomotion.draw(), toolTip = "Draw with spheres."},
                new ButtonInfo { buttonText = "Finger Guns "+gtxt, method =() => Locomotion.shoot(), toolTip = "Shoots pellets."},
                new ButtonInfo { buttonText = "Rapid Slingshot "+atxt, method =() => Global.RapidFireSlingshot(), toolTip = "Rapidly fires slingshot."},
                new ButtonInfo { buttonText = "Destroy Gun", method =() => Global.DestroyGun(), toolTip = "Makes invis to future players."},
                new ButtonInfo { buttonText = "Copy ID Gun", method =() => Global.CopyIDGun(), toolTip = "Copies IDs."},
                new ButtonInfo { buttonText = "Low Quality Microphone", enableMethod =() => Global.LowQualityMicrophone(), disableMethod =() => Global.HighQualityMicrophone(), toolTip = "Makes your microphone bad quality."},
                new ButtonInfo { buttonText = "Funny Mic", enableMethod =() => Global.FunnyMic(), disableMethod =() => Global.UnFunnyMic(), toolTip = "loud = funny."},
                new ButtonInfo { buttonText = "Disable Join Triggers", method =() => Global.DisableNetworkTriggers(), isTogglable = false, toolTip = "No leaving."},
                new ButtonInfo { buttonText = "Enable Join Triggers", method =() => Global.EnableNetworkTriggers(), isTogglable = false, toolTip = "yes leaving."},
            },

            new ButtonInfo[] { // Hitsounds Mods
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
                new ButtonInfo { buttonText = "Set to Presents", method =() => HitSounds.SetHitsounds(240), isTogglable = false, toolTip = "Sets hitsounds to Presents."},
                new ButtonInfo { buttonText = "Set to Candy", method =() => HitSounds.SetHitsounds(249), isTogglable = false, toolTip = "Sets hitsounds to Presents."},
                new ButtonInfo { buttonText = "Set to Fish Food", method =() => HitSounds.SetHitsounds(252), isTogglable = false, toolTip = "Sets hitsounds to food for fishies."},
                new ButtonInfo { buttonText = "Set to Big Crystal", method =() => HitSounds.SetHitsounds(213), isTogglable = false, toolTip = "Sets hitsounds to Big Crystal."},
                new ButtonInfo { buttonText = "Set to Ice", method =() => HitSounds.SetHitsounds(59), isTogglable = false, toolTip = "Sets hitsounds to slippery."},
                new ButtonInfo { buttonText = "Randomize Hitsounds", method =() => HitSounds.SetHitsounds(0,true), isTogglable = false, toolTip = "Randomize hitsounds."},
                new ButtonInfo { buttonText = "Toggle Random Sounds", method =() => HitSounds.randomize(), toolTip = "Constantly randomizes hitsounds."},
            },

            new ButtonInfo[] { // Disguises
                new ButtonInfo { buttonText = "Return to Main", method =() => Global.ReturnHome(), isTogglable = false, toolTip = "Returns to the main page of the menu."},
                new ButtonInfo { buttonText = "Random Disguise", method =() => Safety.ChangeIdentity(), isTogglable = false, toolTip = "disguise."},
                new ButtonInfo { buttonText = "Become PBBV", method =() => Disguise.BecomePBBV(), isTogglable = false, toolTip = "disguise."},
                new ButtonInfo { buttonText = "Become ECHO", method =() => Disguise.BecomeECHO(), isTogglable = false, toolTip = "disguise."},
                new ButtonInfo { buttonText = "Become Daisy", method =() => Disguise.BecomeDAISY09(), isTogglable = false, toolTip = "disguise."},
                new ButtonInfo { buttonText = "Become J3VU", method =() => Disguise.BecomeJ3VU(), isTogglable = false, toolTip = "disguise."},
                new ButtonInfo { buttonText = "Become Void", method =() => Disguise.BecomeVoid(), isTogglable = false, toolTip = "disguise."},
            },

            new ButtonInfo[] { // Trolling Mods
                new ButtonInfo { buttonText = "Return to Main", method =() => Global.ReturnHome(), isTogglable = false, toolTip = "Returns to the main page of the menu."},
                new ButtonInfo { buttonText = "<color=#5cffb6>zedguy</color> - Making half the menu's code", isTogglable = false},
                new ButtonInfo { buttonText = "<color=#ff9f45>iidk</color> - Menu Template", isTogglable = false},
                new ButtonInfo { buttonText = "<color=#e67c7c>Dudeman</color> - Some Ideas", isTogglable = false},
                new ButtonInfo { buttonText = "<color=#c7eeff>You</color> - Actually using the menu", isTogglable = false},
            },

            new ButtonInfo[] { // Visual Mods
                new ButtonInfo { buttonText = "Return to Main", method =() => Global.ReturnHome(), isTogglable = false, toolTip = "Returns to the main page of the menu."},
                new ButtonInfo { buttonText = "Tracers", method =() => Visuals.Tracers(), toolTip = "Traces other players."},
                new ButtonInfo { buttonText = "Chams", method =() => Visuals.CasualChams(), disableMethod =() => Visuals.DisableChams(), toolTip = "Lets you see players through walls."},
                new ButtonInfo { buttonText = "Beacons", method =() => Visuals.Beacons(), toolTip = "Beacons on other players."},
                new ButtonInfo { buttonText = "Box ESP", method =() => Visuals.Box(), toolTip = "Box shaped ESP on other players."},
                new ButtonInfo { buttonText = "Dot ESP", method =() => Visuals.Dot(), toolTip = "Dot shaped ESP on other players."},
            },

            new ButtonInfo[] { // Spam Mods
                new ButtonInfo { buttonText = "Return to Main", method =() => Global.ReturnHome(), isTogglable = false, toolTip = "Returns to the main page of the menu."},
                new ButtonInfo { buttonText = "Spam Snow "+gtxt, method =() => Projectiles.spam()},
                new ButtonInfo { buttonText = "Spam Balloons "+gtxt, method =() => Projectiles.bspam()},
                new ButtonInfo { buttonText = "Spam Presents "+gtxt, method =() => Projectiles.pspam()},
                new ButtonInfo { buttonText = "Spam Mentos "+gtxt, method =() => Projectiles.mspam()},
                new ButtonInfo { buttonText = "Spam Rocks "+gtxt, method =() => Projectiles.lspam()},
                new ButtonInfo { buttonText = "Give Snow Gun", method =() => Projectiles.GiveProj("Snowball"), toolTip = "gives spammer to selected player."},
                new ButtonInfo { buttonText = "Give WaterBalloon Gun", method =() => Projectiles.GiveProj("WaterBalloon"), toolTip = "gives spammer to selected player."},
                new ButtonInfo { buttonText = "Give Present Gun", method =() => Projectiles.GiveProj("ThrowableGift"), toolTip = "gives spammer to selected player."},
                new ButtonInfo { buttonText = "Give Mentos Gun", method =() => Projectiles.GiveProj("ScienceCandy"), toolTip = "gives spammer to selected player."},
                new ButtonInfo { buttonText = "Piss "+gtxt, method =() => Projectiles.pee()},
                new ButtonInfo { buttonText = "Cum "+gtxt, method =() => Projectiles.cumm()},
                new ButtonInfo { buttonText = "Spam impacts "+gtxt, method =() => Projectiles.imspam()},
                new ButtonInfo { buttonText = "Random Sound Spam "+gtxt, method =() => HitSounds.rsp(), toolTip = "Constantly spams random hitsounds."},
            },
        };
        /*public static string[] fullProjectileNames = new string[]
        {
            "SlingshotProjectile",
            "SnowballProjectile",
            "WaterBalloonProjectile",
            "LavaRockProjectile",
            "HornsSlingshotProjectile_PrefabV",
            "CloudSlingshot_Projectile",
            "CupidArrow_Projectile",
            "IceSlingshotProjectile_PrefabV Variant",
            "ElfBow_Projectile",
            "MoltenRockSlingshot_Projectile",
            "SpiderBowProjectile Variant",
            "BucketGift_Cane_Projectile Variant",
            "BucketGift_Coal_Projectile Variant",
            "BucketGift_Roll_Projectile Variant",
            "BucketGift_Round_Projectile Variant",
            "BucketGift_Square_Projectile Variant",
            "ScienceCandyProjectile Variant"
        };*/
    }
}
