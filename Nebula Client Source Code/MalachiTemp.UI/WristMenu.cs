using System.Collections.Generic;
using System.IO;
using System.Linq;
using GorillaLocomotion;
using MalachiTemp.Backend;
using MalachiTemp.Utilities;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MalachiTemp.UI;

internal class WristMenu : MonoBehaviour
{
	public static string MenuTitle = "Nebula Client - V1.1.8";

	public static Font MenuFont = Resources.GetBuiltinResource<Font>("Arial.ttf");

	public static List<ButtonInfo> buttons = new List<ButtonInfo>
	{
		new ButtonInfo
		{
			buttonText = "Save Buttons",
			method = delegate
			{
				Mods.Save1();
			},
			enabled = false,
			nontoggleable = true,
			toolTip = "Saves All Currently Enabled Buttons!"
		},
		new ButtonInfo
		{
			buttonText = "Load Buttons",
			method = delegate
			{
				Mods.Load1();
			},
			enabled = false,
			nontoggleable = true,
			toolTip = "Load The Buttons You Saved!"
		},
		new ButtonInfo
		{
			buttonText = "Settings",
			method = delegate
			{
				Mods.Settings();
			},
			enabled = false,
			toolTip = "Go To Settings!"
		},
		new ButtonInfo
		{
			buttonText = "OP Mods",
			method = delegate
			{
				Mods.Cat1();
			},
			enabled = false,
			toolTip = "Go To OP Mods!"
		},
		new ButtonInfo
		{
			buttonText = "Movement Mods",
			method = delegate
			{
				Mods.Cat2();
			},
			enabled = false,
			toolTip = "Go To Movement Mods!"
		},
		new ButtonInfo
		{
			buttonText = "Safety Mods",
			method = delegate
			{
				Mods.Cat3();
			},
			enabled = false,
			toolTip = "Go To Safety Mods!"
		},
		new ButtonInfo
		{
			buttonText = "Fun Mods",
			method = delegate
			{
				Mods.Cat4();
			},
			enabled = false,
			toolTip = "Go To Fun Mods!"
		},
		new ButtonInfo
		{
			buttonText = "Visual Mods",
			method = delegate
			{
				Mods.Cat5();
			},
			enabled = false,
			toolTip = "Go To Visual Mods!"
		},
		new ButtonInfo
		{
			buttonText = "Utility Mods",
			method = delegate
			{
				Mods.Cat6();
			},
			enabled = false,
			toolTip = "Go To Utility Mods!"
		}
	};

	public static List<ButtonInfo> settingsbuttons = new List<ButtonInfo>
	{
		new ButtonInfo
		{
			buttonText = "Exit Settings",
			method = delegate
			{
				Mods.Settings();
			},
			enabled = false,
			toolTip = "Go To Main!"
		},
		new ButtonInfo
		{
			buttonText = "Save Settings",
			method = delegate
			{
				Mods.Save();
			},
			enabled = false,
			nontoggleable = true,
			toolTip = "Save Your Settings!"
		},
		new ButtonInfo
		{
			buttonText = "Load Settings",
			method = delegate
			{
				Mods.Load();
			},
			enabled = false,
			nontoggleable = true,
			toolTip = "Load Your Settings!"
		},
		new ButtonInfo
		{
			buttonText = "FPS Boost",
			method = delegate
			{
				Mods.FPSboost();
			},
			disableMethod = delegate
			{
				Mods.fixFPS();
			},
			enabled = false,
			toolTip = "Boost Your FPS!"
		},
		new ButtonInfo
		{
			buttonText = "No Notis",
			method = delegate
			{
				Mods.Changenoti();
			},
			enabled = false,
			nontoggleable = true,
			toolTip = "Turn off Notis!"
		},
		new ButtonInfo
		{
			buttonText = "Toggle FPS & Page Counter",
			method = delegate
			{
				Mods.ChangeFPS();
			},
			enabled = false,
			nontoggleable = true,
			toolTip = "Turn off or on the FPS & Page counter!"
		},
		new ButtonInfo
		{
			buttonText = "Change Gun & Hand Orb Color",
			method = delegate
			{
				Mods.ChangeOrbColor();
			},
			enabled = false,
			nontoggleable = true,
			toolTip = "Change the color of the gun and hand orbs!"
		},
		new ButtonInfo
		{
			buttonText = "Change ESP Color",
			method = delegate
			{
				Mods.ChangeVisualColor();
			},
			enabled = false,
			nontoggleable = true,
			toolTip = "Change the color of the ESP mods!"
		},
		new ButtonInfo
		{
			buttonText = "Change Platform Enable",
			method = delegate
			{
				Mods.Changeplat();
			},
			enabled = false,
			nontoggleable = true,
			toolTip = "Change how you use platform mods!"
		},
		new ButtonInfo
		{
			buttonText = "Change Disconnect Location",
			method = delegate
			{
				Mods.Changedisconnect();
			},
			enabled = false,
			nontoggleable = true,
			toolTip = "Change the location of the disconnect button!"
		},
		new ButtonInfo
		{
			buttonText = "Change Menu Location",
			method = delegate
			{
				Mods.Changemenu();
			},
			enabled = false,
			nontoggleable = true,
			toolTip = "Change the location of the menu!"
		},
		new ButtonInfo
		{
			buttonText = "Change Next & Prev",
			method = delegate
			{
				Mods.Changepagebutton();
			},
			enabled = false,
			nontoggleable = true,
			toolTip = "Change the location of the next and prev page buttons!"
		},
		new ButtonInfo
		{
			buttonText = "My Personal Preset",
			method = delegate
			{
				Mods.MyPreset();
			},
			enabled = false,
			nontoggleable = true,
			toolTip = "Makes you have the menu preset that the owner uses!"
		},
		new ButtonInfo
		{
			buttonText = "Theme Changer 1",
			method = delegate
			{
				Mods.ThemeChangerV1();
			},
			enabled = false,
			nontoggleable = true,
			toolTip = "Change the first color the menu fades to!"
		},
		new ButtonInfo
		{
			buttonText = "Theme Changer 2",
			method = delegate
			{
				Mods.ThemeChangerV2();
			},
			enabled = false,
			nontoggleable = true,
			toolTip = "Change the second color the menu fades to!"
		},
		new ButtonInfo
		{
			buttonText = "Theme Changer 3",
			method = delegate
			{
				Mods.ThemeChangerV3();
			},
			enabled = false,
			nontoggleable = true,
			toolTip = "Change the color of buttons when they are disabled!"
		},
		new ButtonInfo
		{
			buttonText = "Theme Changer 4",
			method = delegate
			{
				Mods.ThemeChangerV4();
			},
			enabled = false,
			nontoggleable = true,
			toolTip = "Change the color of buttons when they are enabled!"
		},
		new ButtonInfo
		{
			buttonText = "Theme Changer 5",
			method = delegate
			{
				Mods.ThemeChangerV5();
			},
			enabled = false,
			nontoggleable = true,
			toolTip = "Change the color of the text on buttons when they are enabled!"
		},
		new ButtonInfo
		{
			buttonText = "Theme Changer 6",
			method = delegate
			{
				Mods.ThemeChangerV6();
			},
			enabled = false,
			nontoggleable = true,
			toolTip = "Change the color of the text on buttons when they are disabled!"
		},
		new ButtonInfo
		{
			buttonText = "Theme Changer 7",
			method = delegate
			{
				Mods.ThemeChangerV7();
			},
			enabled = false,
			nontoggleable = true,
			toolTip = "Change the sound of buttons!"
		}
	};

	public static List<ButtonInfo> CatButtons1 = new List<ButtonInfo>
	{
		new ButtonInfo
		{
			buttonText = "Exit OP Mods",
			method = delegate
			{
				Mods.Cat1();
			},
			enabled = false,
			toolTip = "Go to Main!"
		},
		new ButtonInfo
		{
			buttonText = "Disable Network Triggers",
			method = delegate
			{
				Mods.DisableNetworkTriggers();
			},
			enabled = false,
			toolTip = "Makes it so you can walk into every map and not be disconnected from the room you are in!"
		},
		new ButtonInfo
		{
			buttonText = "Vibrate Gun (M)",
			method = delegate
			{
				Mods.VibrateGun();
			},
			enabled = false,
			toolTip = "Vibrates Someones Controllers (Requires Master Client)!"
		},
		new ButtonInfo
		{
			buttonText = "Slow Gun (M)",
			method = delegate
			{
				Mods.SlowGun();
			},
			enabled = false,
			toolTip = "Applies tag freeze to whoever your hand desires (Requires Master Client)!"
		},
		new ButtonInfo
		{
			buttonText = "Grey Screen (M)",
			method = delegate
			{
				Mods.GreyZoneMods.GreyAll();
			},
			nontoggleable = true,
			enabled = false,
			toolTip = "Toggles the grey screen (Master Client only)!"
		},
		new ButtonInfo
		{
			buttonText = "Object Flicker Gun (M)",
			method = delegate
			{
				Mods.ObjectFlickerGun();
			},
			enabled = false,
			toolTip = "Flickers an object (Requires Master Client)!"
		},
		new ButtonInfo
		{
			buttonText = "Block Crash Gun (M)",
			method = delegate
			{
				Mods.BlockCrashGun();
			},
			enabled = false,
			toolTip = "Crashes whoever your hand desires using blocks!"
		},
		new ButtonInfo
		{
			buttonText = "Block Crash All (M)",
			method = delegate
			{
				Mods.BlockCrashAll();
			},
			enabled = false,
			toolTip = "Crashes everyone with blocks!"
		},
		new ButtonInfo
		{
			buttonText = "Block Bring Gun (M)",
			method = delegate
			{
				Mods.BlockBringGun();
			},
			enabled = false,
			toolTip = "Pulls the targeted player toward you using blocks!"
		},
		new ButtonInfo
		{
			buttonText = "Block Bring All (M)",
			method = delegate
			{
				Mods.BlockBringAll();
			},
			enabled = false,
			toolTip = "Pulls all players toward you using block paths!"
		},
		new ButtonInfo
		{
			buttonText = "Block Fling Gun (M)",
			method = delegate
			{
				Mods.BlockFlingGun();
			},
			enabled = false,
			toolTip = "Launches the targeted player away using directional block force!"
		},
		new ButtonInfo
		{
			buttonText = "Block Fling All (M)",
			method = delegate
			{
				Mods.BlockFlingAll();
			},
			enabled = false,
			toolTip = "Launches all players away using block force waves!"
		},
		new ButtonInfo
		{
			buttonText = "Block Float Gun (M)",
			method = delegate
			{
				Mods.BlockFloatGun();
			},
			enabled = false,
			toolTip = "Lifts the targeted player into the air using block platforms!"
		},
		new ButtonInfo
		{
			buttonText = "Block Float All (M)",
			method = delegate
			{
				Mods.BlockFloatAll();
			},
			enabled = false,
			toolTip = "Floats all players using stacked block support!"
		},
		new ButtonInfo
		{
			buttonText = "Block Freeze Gun (M)",
			method = delegate
			{
				Mods.BlockFreezeGun();
			},
			enabled = false,
			toolTip = "Freezes whoever your hand is aimed at using a block cage!"
		},
		new ButtonInfo
		{
			buttonText = "Block Freeze All (M)",
			method = delegate
			{
				Mods.BlockFreezeAll();
			},
			enabled = false,
			toolTip = "Freezes all players in block spheres!"
		},
		new ButtonInfo
		{
			buttonText = "Lock Room (M)",
			method = delegate
			{
				Mods.LockRoom();
			},
			enabled = false,
			nontoggleable = true,
			toolTip = "Locks the room!"
		},
		new ButtonInfo
		{
			buttonText = "Un-Lock Room (M)",
			method = delegate
			{
				Mods.UnlockRoom();
			},
			enabled = false,
			nontoggleable = true,
			toolTip = "Un-Locks the room!"
		},
		new ButtonInfo
		{
			buttonText = "Spaz Room (M)",
			method = delegate
			{
				Mods.SpazRoom();
			},
			enabled = false,
			toolTip = "Spaz locks and unlocks the room!"
		},
		new ButtonInfo
		{
			buttonText = "Anti-Ban",
			method = delegate
			{
				Mods.AntiBan();
			},
			enabled = false,
			toolTip = "Makes you unbannable!"
		}
	};

	public static List<ButtonInfo> CatButtons2 = new List<ButtonInfo>
	{
		new ButtonInfo
		{
			buttonText = "Exit Movement Mods",
			method = delegate
			{
				Mods.Cat2();
			},
			enabled = false,
			toolTip = "Go to Main!"
		},
		new ButtonInfo
		{
			buttonText = "Platforms (LG) (RG)",
			method = delegate
			{
				Mods.Platforms();
			},
			enabled = false,
			toolTip = "Platforms!"
		},
		new ButtonInfo
		{
			buttonText = "Invis Platforms (LG) (RG)",
			method = delegate
			{
				Mods.Invisableplatforms();
			},
			enabled = false,
			toolTip = "Platforms but invisible!"
		},
		new ButtonInfo
		{
			buttonText = "Speed Boost",
			method = delegate
			{
				Mods.SpeedBoost();
			},
			enabled = false,
			toolTip = "Speed Boost!"
		},
		new ButtonInfo
		{
			buttonText = "NoClip (RT)",
			method = delegate
			{
				Mods.Noclip();
			},
			enabled = false,
			toolTip = "Go through anything!"
		},
		new ButtonInfo
		{
			buttonText = "Fly (A)",
			method = delegate
			{
				Mods.FlyMeth(15f);
			},
			enabled = false,
			toolTip = "Fly like a bird!"
		},
		new ButtonInfo
		{
			buttonText = "Reset Arms",
			method = delegate
			{
				Mods.ResetArms();
			},
			nontoggleable = true,
			enabled = false,
			toolTip = "Resets your arms!"
		},
		new ButtonInfo
		{
			buttonText = "Long Arms",
			method = delegate
			{
				Mods.LongArms();
			},
			nontoggleable = true,
			enabled = false,
			toolTip = "Gives you long arms!"
		},
		new ButtonInfo
		{
			buttonText = "Longer Arms",
			method = delegate
			{
				Mods.LongerArms();
			},
			nontoggleable = true,
			enabled = false,
			toolTip = "Gives you longer arms than long arms!"
		},
		new ButtonInfo
		{
			buttonText = "Extreme Long Arms",
			method = delegate
			{
				Mods.LongererArms();
			},
			nontoggleable = true,
			enabled = false,
			toolTip = "Gives you the longest arms!"
		}
	};

	public static List<ButtonInfo> CatButtons3 = new List<ButtonInfo>
	{
		new ButtonInfo
		{
			buttonText = "Exit Safety Mods",
			method = delegate
			{
				Mods.Cat3();
			},
			enabled = false,
			toolTip = "Go to Main!"
		},
		new ButtonInfo
		{
			buttonText = "Anti-Report",
			method = delegate
			{
				Mods.AntiReport();
			},
			enabled = false,
			toolTip = "Kicks you if someone tries to report you!"
		},
		new ButtonInfo
		{
			buttonText = "Anti Cheat Detection Notifications",
			method = delegate
			{
				Mods.AntiCheatMonitor();
			},
			enabled = false,
			toolTip = "Notifies you if the Anti Cheat tries to report you!"
		},
		new ButtonInfo
		{
			buttonText = "Flush RPCs",
			method = delegate
			{
				Mods.RPCFlush();
			},
			enabled = false,
			nontoggleable = true,
			toolTip = "Flushes all of your RPCs!"
		}
	};

	public static List<ButtonInfo> CatButtons4 = new List<ButtonInfo>
	{
		new ButtonInfo
		{
			buttonText = "Exit Fun Mods",
			method = delegate
			{
				Mods.Cat4();
			},
			enabled = false,
			toolTip = "Go to Main!"
		},
		new ButtonInfo
		{
			buttonText = "Untag Gun (M)",
			method = delegate
			{
				Mods.UntagGun();
			},
			enabled = false,
			toolTip = "Untags whoever your hand desires!"
		},
		new ButtonInfo
		{
			buttonText = "Bug Gun",
			method = delegate
			{
				Mods.ExampleOnHowToUseGunLib();
			},
			enabled = false,
			toolTip = "Puts the bug wherever your hand desires!"
		},
		new ButtonInfo
		{
			buttonText = "Mats Gun",
			method = delegate
			{
				Mods.MatGun();
			},
			enabled = false,
			toolTip = "Spams mats on whoever your hand desires!!"
		},
		new ButtonInfo
		{
			buttonText = "Get Bracelet",
			method = delegate
			{
				Mods.braceletstuff.getbracelet();
			},
			enabled = false,
			nontoggleable = true,
			toolTip = "Gives yourself a bracelet!"
		},
		new ButtonInfo
		{
			buttonText = "Remove Bracelet",
			method = delegate
			{
				Mods.braceletstuff.removebracelet();
			},
			enabled = false,
			nontoggleable = true,
			toolTip = "Removes your bracelet!"
		},
		new ButtonInfo
		{
			buttonText = "Projectile Gun",
			method = delegate
			{
				Mods.ProjectileGun();
			},
			enabled = false,
			toolTip = "Shoots projectiles wherever your hand desires!"
		},
		new ButtonInfo
		{
			buttonText = "Kick Gun (CS)",
			method = delegate
			{
				Mods.ClientKickMod.ClientKickGun();
			},
			enabled = false,
			toolTip = "Kicks whoever your hand desires Client-Sidedly!"
		},
		new ButtonInfo
		{
			buttonText = "Un-Kick All (CS)",
			method = delegate
			{
				Mods.ClientKickMod.UnClientKickAll();
			},
			enabled = false,
			toolTip = "Un-Kicks everyone you have Client-Sidedly kicked!"
		},
		new ButtonInfo
		{
			buttonText = "Set Guardian Self (M)",
			method = delegate
			{
				Mods.GuardianMods.SetGuardianSelf();
			},
			nontoggleable = true,
			enabled = false,
			toolTip = "Sets yourself to guardian!"
		},
		new ButtonInfo
		{
			buttonText = "Un-Guardian Self (M)",
			method = delegate
			{
				Mods.GuardianMods.RemoveGuardianSelf();
			},
			nontoggleable = true,
			enabled = false,
			toolTip = "Removes yourself from guardian!"
		},
		new ButtonInfo
		{
			buttonText = "Set Guardian Gun (M)",
			method = delegate
			{
				Mods.SetGuardianGun();
			},
			enabled = false,
			toolTip = "Sets whoever your hand desires to guardian!"
		},
		new ButtonInfo
		{
			buttonText = "Un-Guardian Gun (M)",
			method = delegate
			{
				Mods.RemoveGuardianGun();
			},
			enabled = false,
			toolTip = "Removes whoever your hand desires from guardian!"
		},
		new ButtonInfo
		{
			buttonText = "Despawn All Critters (M)",
			method = delegate
			{
				Mods.DespawnAllCritters();
			},
			enabled = false,
			nontoggleable = true,
			toolTip = "Removes every critter in the critter area!"
		},
		new ButtonInfo
		{
			buttonText = "Bring All Critters (M)",
			method = delegate
			{
				Mods.BringAllCritters();
			},
			enabled = false,
			nontoggleable = true,
			toolTip = "Teleports all critters to you!"
		},
		new ButtonInfo
		{
			buttonText = "Giant Critters (M)",
			method = delegate
			{
				Mods.GiantCritters();
			},
			enabled = false,
			nontoggleable = true,
			toolTip = "Makes all critters huge!"
		},
		new ButtonInfo
		{
			buttonText = "Tiny Critters (M)",
			method = delegate
			{
				Mods.TinyCritters();
			},
			enabled = false,
			nontoggleable = true,
			toolTip = "Shrinks all critters!"
		},
		new ButtonInfo
		{
			buttonText = "Reset Critter Sizes (M)",
			method = delegate
			{
				Mods.ResetCritterSizes();
			},
			enabled = false,
			nontoggleable = true,
			toolTip = "Returns critters to normal size!"
		},
		new ButtonInfo
		{
			buttonText = "Critter Sticky Goo Gun (M)",
			method = delegate
			{
				Mods.CritterGuns.CritterStickyGooGun();
			},
			enabled = false,
			toolTip = "Shoots sticky goo wherever your hand desires!"
		},
		new ButtonInfo
		{
			buttonText = "Critter Spawn Gun (M)",
			method = delegate
			{
				Mods.CritterGuns.CritterSpawnGun();
			},
			enabled = false,
			toolTip = "Spawns a critter wherever your hand desires!"
		},
		new ButtonInfo
		{
			buttonText = "Critter Despawn Gun (M)",
			method = delegate
			{
				Mods.CritterGuns.CritterDespawnGun();
			},
			enabled = false,
			toolTip = "Removes a critter on whoever your hand desires!"
		},
		new ButtonInfo
		{
			buttonText = "Rainbow Color Self",
			method = delegate
			{
				Mods.RainbowColor();
			},
			enabled = false,
			toolTip = "Makes you rainbow!"
		},
		new ButtonInfo
		{
			buttonText = "Block Gun (M)",
			method = delegate
			{
				Mods.RandomBlockGun();
			},
			enabled = false,
			toolTip = "Adds a block wherever your hand desires!"
		},
		new ButtonInfo
		{
			buttonText = "Destroy Block Gun (M)",
			method = delegate
			{
				Mods.DestroyBlockGun();
			},
			enabled = false,
			toolTip = "Destroys whatever block your hand desires!"
		},
		new ButtonInfo
		{
			buttonText = "Destroy All Blocks (M)",
			method = delegate
			{
				Mods.RecycleAllBlocks();
			},
			enabled = false,
			nontoggleable = true,
			toolTip = "Destroys every block!"
		},
		new ButtonInfo
		{
			buttonText = "Create Menu Lobby",
			method = delegate
			{
				Mods.CreateLobby("<color=#8A2BE2>Nebula Client V1.1.8</color>", "\"Nebula Menu\"");
			},
			enabled = false,
			nontoggleable = true,
			toolTip = "Makes a lobby that says the menu name!"
		},
		new ButtonInfo
		{
			buttonText = "Create Big Menu Is Best Lobby",
			method = delegate
			{
				Mods.CreateLobby("<size=2050><color=red>Nebula Is The Best!\n\n</color></size>", "\"Best Menu\"");
			},
			enabled = false,
			nontoggleable = true,
			toolTip = "Makes a lobby that says nebula is the best!"
		},
		new ButtonInfo
		{
			buttonText = "Create Fake Ban Lobby",
			method = delegate
			{
				Mods.CreateLobby("<size=200><color=red>YOU HAVE\nBEEN BANNED\n FOR 4 WEEKS\n FOR TOXICITY</color></size>", "\"Fake Ban\"");
			},
			enabled = false,
			nontoggleable = true,
			toolTip = "Makes a lobby that fakes a ban!"
		},
		new ButtonInfo
		{
			buttonText = "Create Big Rainbow Leaderboard Lobby",
			method = delegate
			{
				Mods.CreateLobby("<size=2050><color=red>■</color></size><size=2050><color=orange>■</color></size><size=2050><color=yellow>■</color></size><size=2050><color=green>■</color></size><size=2050><color=blue>■</color></size><size=2050><color=purple>■</color></size><size=2050><color=#FFC0CB>■</color></size>", "\"Big Rainbow Lobby\"");
			},
			enabled = false,
			nontoggleable = true,
			toolTip = "Makes a lobby that has a huge, rainbow leaderboard!"
		},
		new ButtonInfo
		{
			buttonText = "Create Rainbow Leaderboard Lobby",
			method = delegate
			{
				Mods.CreateLobby("<color=red>■</color><color=orange>■</color><color=yellow>■</color><color=green>■</color><color=blue>■</color><color=purple>■</color><color=#FFC0CB>■</color>", "\"Rainbow Lobby\"");
			},
			enabled = false,
			nontoggleable = true,
			toolTip = "Makes a lobby that has rainbow for the room name!"
		}
	};

	public static List<ButtonInfo> CatButtons5 = new List<ButtonInfo>
	{
		new ButtonInfo
		{
			buttonText = "Exit Visual Mods",
			method = delegate
			{
				Mods.Cat5();
			},
			enabled = false,
			toolTip = "Go to Main!"
		},
		new ButtonInfo
		{
			buttonText = "Tracers",
			method = delegate
			{
				Mods.Tracers();
			},
			enabled = false,
			toolTip = "Makes lines towards everyone!"
		}
	};

	public static List<ButtonInfo> CatButtons6 = new List<ButtonInfo>
	{
		new ButtonInfo
		{
			buttonText = "Exit Utility Mods",
			method = delegate
			{
				Mods.Cat6();
			},
			enabled = false,
			toolTip = "Go to Main!"
		},
		new ButtonInfo
		{
			buttonText = "Add Barrel To Cart",
			method = delegate
			{
				Mods.BuyBarrel();
			},
			enabled = false,
			toolTip = "Adds The Barrel Cosmetic To Your Cart!"
		}
	};

	public static List<ButtonInfo> CatButtons7 = new List<ButtonInfo>
	{
		new ButtonInfo
		{
			buttonText = "Exit Broken Mods",
			method = delegate
			{
				Mods.Cat7();
			},
			enabled = false,
			toolTip = "Go to Main!"
		}
	};

	public static List<ButtonInfo> CatButtons8 = new List<ButtonInfo>
	{
		new ButtonInfo
		{
			buttonText = "nothin here rn",
			method = delegate
			{
				Mods.Cat8();
			},
			enabled = false,
			toolTip = "Go to Main!"
		}
	};

	public static List<ButtonInfo> CatButtons9 = new List<ButtonInfo>
	{
		new ButtonInfo
		{
			buttonText = "Exit Detected Mods",
			method = delegate
			{
				Mods.Cat9();
			},
			enabled = false,
			toolTip = "Go to Main!"
		},
		new ButtonInfo
		{
			buttonText = "READ TOOLTIP BEFORE USING!",
			method = delegate
			{
				Mods.PLACEHOLDER();
			},
			enabled = false,
			toolTip = "REMEMBER WHEN USING THESE MODS YOU WILL GET BANNED MOST LIKELY SO MAKE SURE TO ONLY USE THESE IF YOU KNOW WHAT YOU ARE DOING. I AM NOT RESPONSIBLE FOR ANY BANS USING THIS MENU."
		},
		new ButtonInfo
		{
			buttonText = "Kick Gun (M) (D)",
			method = delegate
			{
				Mods.KickGunMoreDetected();
			},
			enabled = false,
			toolTip = "Kicks a player from the lobby (Requires Master Client)!"
		},
		new ButtonInfo
		{
			buttonText = "Set Master Self (D)",
			method = delegate
			{
				Mods.SetMasterClient();
			},
			enabled = false,
			nontoggleable = true,
			toolTip = "Makes you the master client in the server!"
		},
		new ButtonInfo
		{
			buttonText = "Crash Gun (D)",
			method = delegate
			{
				Mods.CrashGun();
			},
			enabled = false,
			toolTip = "Crashes whoever your hand desires!"
		},
		new ButtonInfo
		{
			buttonText = "Freeze Gun (D)",
			method = delegate
			{
				Mods.FreezeGun();
			},
			enabled = false,
			toolTip = "Freezes whoever your hand desires!"
		},
		new ButtonInfo
		{
			buttonText = "Lag Gun (D)",
			method = delegate
			{
				Mods.LagGun();
			},
			enabled = false,
			toolTip = "Lags whoever your hand desires!"
		},
		new ButtonInfo
		{
			buttonText = "Unban Self (Only Temp Bans) (D)",
			method = delegate
			{
				Mods.UnbanSelf();
			},
			enabled = false,
			toolTip = "Attempts to clear local ban!"
		},
		new ButtonInfo
		{
			buttonText = "Strong Wind Fling Gun (M) (D) (BROKEN)",
			method = delegate
			{
				Mods.WindFlingStrongGun();
			},
			enabled = false,
			toolTip = "Adds a wind object to launch a player up!"
		},
		new ButtonInfo
		{
			buttonText = "Crash Gun V2 (M) (D)",
			method = delegate
			{
				Mods.CrashGunLD();
			},
			enabled = false,
			toolTip = "V2 Of Crash Gun, Uses RaiseEvents."
		},
		new ButtonInfo
		{
			buttonText = "Destroy Gun (D)",
			method = delegate
			{
				Mods.CrashGunLD();
			},
			enabled = false,
			toolTip = "Destroys whoever your hand desires!"
		},
		new ButtonInfo
		{
			buttonText = "Break Audio Gun (D)",
			method = delegate
			{
				Mods.BreakAudioGun();
			},
			enabled = false,
			toolTip = "Breaks whoever's audio your hand desires!"
		},
		new ButtonInfo
		{
			buttonText = "Lag Gun V2 (D)",
			method = delegate
			{
				Mods.LagGunLD();
			},
			enabled = false,
			toolTip = "Lags whoever's audio your hand desires!"
		},
		new ButtonInfo
		{
			buttonText = "Lag All V2 (D)",
			method = delegate
			{
				Mods.LagAll();
			},
			enabled = false,
			toolTip = "Lags everyone in the lobby!"
		},
		new ButtonInfo
		{
			buttonText = "Barrel Crash Gun",
			method = delegate
			{
				Mods.BarrelCrashGun();
			},
			enabled = false,
			toolTip = "Crashes whoever your hand desires with the barrel cosmetic!"
		},
		new ButtonInfo
		{
			buttonText = "Barrel Kick Gun",
			method = delegate
			{
				Mods.BarrelKickGun();
			},
			enabled = false,
			toolTip = "Kicks whoever your hand desires with the barrel cosmetic!"
		},
		new ButtonInfo
		{
			buttonText = "Barrel Fling Gun",
			method = delegate
			{
				Mods.BarrelFlingGun();
			},
			enabled = false,
			toolTip = "Flings whoever your hand desires with the barrel cosmetic!"
		}
	};

	public static List<ButtonInfo> CatButtons10 = new List<ButtonInfo>
	{
		new ButtonInfo
		{
			buttonText = "Exit Category 10",
			method = delegate
			{
				Mods.Cat10();
			},
			enabled = false,
			toolTip = "Go to Main!"
		},
		new ButtonInfo
		{
			buttonText = "This Category Is Not Being Used.",
			method = delegate
			{
				Mods.PLACEHOLDER();
			},
			enabled = false,
			toolTip = "This Category Is Currently Not Being Used Currently In The Menu."
		}
	};

	public static List<ButtonInfo> CatButtons11 = new List<ButtonInfo>
	{
		new ButtonInfo
		{
			buttonText = "Exit Category 11",
			method = delegate
			{
				Mods.Cat11();
			},
			enabled = false,
			toolTip = "Go to Main!"
		},
		new ButtonInfo
		{
			buttonText = "This Category Is Not Being Used.",
			method = delegate
			{
				Mods.PLACEHOLDER();
			},
			enabled = false,
			toolTip = "This Category Is Currently Not Being Used Currently In The Menu."
		}
	};

	public static string[] CustomBoardTexts = new string[4] { "Thanks For Using Nebula 1.1.8!", "My YT Channel", "My YT channel name is @Grayson_GT      Thanks for 42 subs!\nLETTER MEANINGS\nServer Sided (SS)\nClient Sided (CS)\nNot Working (NW)\nMaybe Working (MW)\nDetected (D)\nDetected Over Time (TD)\nLeft Grip (LG)\nRight Grip (RG)\nLeft Trigger (LT)\nRight Trigger (RT)", "Thanks for using build 1.1.8 of Nebula! This menu is made by @Grayson_GT. This menu is powered by Malachis Menu Temp. I am NOT responsible for any bans using this menu. Note: The detected mods (D) on the menu are extremeley bannable, but right now, there are no detected mods so you should be fine." };

	public static string FolderName = "Malachi_Temp";

	public static bool ChangingColors = false;

	public static Color FirstColor1 = new Color(0f, 0f, 0f, 1f);

	public static Color SecondColor = new Color(0.15f, 0f, 0f, 1f);

	public static Color NormalColor = new Color(0f, 0f, 0f, 1f);

	public static Color ButtonColorDisable = new Color(1f, 0f, 0f, 1f);

	public static Color ButtonColorEnabled = new Color(1f, 0.9215686f, 0.01568628f, 1f);

	public static Color EnableTextColor = new Color(1f, 0.9215686f, 0.01568628f, 1f);

	public static Color DIsableTextColor = new Color(1f, 0.9215686f, 0.01568628f, 1f);

	public static Color MenuTitleColor = new Color(1f, 0.9215686f, 0.01568628f, 1f);

	public static Color ToolTipColor = new Color(1f, 0.9215686f, 0.01568628f, 1f);

	public static Color DisconnectButtonColor = new Color(0.5f, 0f, 0f, 1f);

	public static Color DisconnectTextColor = new Color(1f, 0.9215686f, 0.01568628f, 1f);

	public static Color NextPrevButtonColor = new Color(0.2f, 0f, 0f, 1f);

	public static Color NextPrevTextColor = new Color(1f, 0.9215686f, 0.01568628f, 1f);

	public static Vector3 MenuScale = new Vector3(0.1f, 1f, 1f) * 1f;

	public static Vector3 MenuPos = new Vector3(0.05f, 0f, 0f) * 1f;

	public static Vector3 PointerScale = new Vector3(0.01f, 0.01f, 0.01f);

	public static Vector3 PointerPos = new Vector3(0f, -0.1f, 0f);

	public static Vector3 ToolTipPos = new Vector3(0.06f, 0f, -0.18f) * 1f;

	public static Vector2 ToolTipScale = new Vector2(0.2f, 0.03f) * 1f;

	public static Vector3 MenuTitlePos = new Vector3(0.06f, 0f, 0.175f);

	public static Vector2 MenuTitleScale = new Vector2(0.28f, 0.05f);

	public static Vector3 ButtonScale = new Vector3(0.09f, 0.8f, 0.08f);

	public static Vector2 ButtonTextScale = new Vector2(0.2f, 0.03f) * 1f;

	public static bool gripDownR;

	public static bool triggerDownR;

	public static bool abuttonDown;

	public static bool bbuttonDown;

	public static bool xbuttonDown;

	public static bool ybuttonDown;

	public static bool gripDownL;

	public static bool triggerDownL;

	public static bool customboards = false;

	public static int lastPressedButtonIndex = -1;

	public static GameObject menu = null;

	public static GameObject canvasObj = null;

	public static GameObject reference = null;

	public static int pageNumber = 0;

	public static WristMenu instance = new WristMenu();

	public static GameObject menuObj;

	public static int selectedButton = 1;

	public static Text tooltipText;

	public static string tooltipString;

	public static bool toggle = false;

	public static bool toggle1 = false;

	public static bool toggle2 = false;

	public static bool toggle3 = false;

	public static int pageSize = 4;

	public static bool toggle4 = false;

	public static int ClickCooldown = 10;

	public static int MaxNotis = 5;

	public static bool custom = true;

	public static Text titiel;

	public static GameObject Button;

	public static Text text2;

	private void Update()
	{
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_03be: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_030d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0435: Unknown result type (might be due to invalid IL or missing references)
		//IL_043f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0459: Unknown result type (might be due to invalid IL or missing references)
		//IL_0463: Unknown result type (might be due to invalid IL or missing references)
		//IL_0482: Unknown result type (might be due to invalid IL or missing references)
		//IL_0491: Unknown result type (might be due to invalid IL or missing references)
		//IL_0602: Unknown result type (might be due to invalid IL or missing references)
		//IL_0613: Unknown result type (might be due to invalid IL or missing references)
		//IL_04de: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_050d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0551: Unknown result type (might be due to invalid IL or missing references)
		//IL_0533: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (Time.time > Mods.balll435342111 + 0.1f && Mods.FPSPage)
			{
				Mods.balll435342111 = Time.time;
				int num = Mathf.RoundToInt(1f / Time.deltaTime);
				titiel.text = MenuTitle + $"\n Fps: {num} | Page: {pageNumber + 1}";
			}
			gripDownL = ((ControllerInputPoller)ControllerInputPoller.instance).leftGrab;
			gripDownR = ((ControllerInputPoller)ControllerInputPoller.instance).rightGrab;
			triggerDownL = ((ControllerInputPoller)ControllerInputPoller.instance).leftControllerIndexFloat == 1f;
			triggerDownR = ((ControllerInputPoller)ControllerInputPoller.instance).rightControllerIndexFloat == 1f;
			abuttonDown = ((ControllerInputPoller)ControllerInputPoller.instance).rightControllerPrimaryButton;
			bbuttonDown = ((ControllerInputPoller)ControllerInputPoller.instance).rightControllerSecondaryButton;
			xbuttonDown = ((ControllerInputPoller)ControllerInputPoller.instance).leftControllerPrimaryButton;
			ybuttonDown = ((ControllerInputPoller)ControllerInputPoller.instance).leftControllerSecondaryButton;
			if (Mods.change7 == 5 && !Object.op_Implicit((Object)(object)menu.GetComponent<Rigidbody>()))
			{
				if (triggerDownL)
				{
					if (!toggle)
					{
						Toggle("PreviousPage");
						VRRig.LocalRig.PlayHandTapLocal(Mods.ButtonSound, false, 0.1f);
						toggle = true;
					}
				}
				else
				{
					toggle = false;
				}
				if (triggerDownR)
				{
					if (!toggle1)
					{
						Toggle("NextPage");
						VRRig.LocalRig.PlayHandTapLocal(Mods.ButtonSound, false, 0.1f);
						toggle1 = true;
					}
				}
				else
				{
					toggle1 = false;
				}
			}
			if (ybuttonDown && !Mods.right)
			{
				if ((Object)(object)menu == (Object)null)
				{
					instance.Draw();
				}
				else if (!Mods.right)
				{
					menu.transform.position = GTPlayer.Instance.LeftHand.controllerTransform.position;
					menu.transform.rotation = GTPlayer.Instance.LeftHand.controllerTransform.rotation;
					if ((Object)(object)reference == (Object)null)
					{
						reference = GameObject.CreatePrimitive((PrimitiveType)0);
						((Object)reference).name = "buttonPresser";
					}
					reference.transform.parent = GTPlayer.Instance.RightHand.controllerTransform;
					reference.transform.localPosition = PointerPos;
					reference.transform.localScale = PointerScale;
					if (ChangingColors)
					{
						reference.GetComponent<Renderer>().material.color = FirstColor1;
					}
					else
					{
						reference.GetComponent<Renderer>().material.color = NormalColor;
					}
				}
				if (Object.op_Implicit((Object)(object)menu.GetComponent<Rigidbody>()))
				{
					Object.Destroy((Object)(object)menu.GetComponent<Rigidbody>());
				}
			}
			else if (!ybuttonDown && !Mods.right && !Object.op_Implicit((Object)(object)menu.GetComponent<Rigidbody>()))
			{
				Object.Destroy((Object)(object)reference);
				reference = null;
				menu.AddComponent<Rigidbody>();
				menu.GetComponent<Rigidbody>().isKinematic = false;
				menu.GetComponent<Rigidbody>().useGravity = true;
				menu.GetComponent<Rigidbody>().linearVelocity = GTPlayer.Instance.LeftHand.velocityTracker.GetAverageVelocity(true, 0f, false);
			}
			if (bbuttonDown && Mods.right)
			{
				if ((Object)(object)menu == (Object)null)
				{
					instance.Draw();
				}
				else if (Mods.right)
				{
					menu.transform.position = GTPlayer.Instance.RightHand.controllerTransform.position;
					menu.transform.rotation = GTPlayer.Instance.RightHand.controllerTransform.rotation;
					menu.transform.RotateAround(menu.transform.position, menu.transform.forward, 180f);
					if ((Object)(object)reference == (Object)null)
					{
						reference = GameObject.CreatePrimitive((PrimitiveType)0);
						((Object)reference).name = "buttonPresser";
					}
					reference.transform.parent = GTPlayer.Instance.LeftHand.controllerTransform;
					reference.transform.localPosition = PointerPos;
					reference.transform.localScale = PointerScale;
					if (ChangingColors)
					{
						reference.GetComponent<Renderer>().material.color = FirstColor1;
					}
					else
					{
						reference.GetComponent<Renderer>().material.color = NormalColor;
					}
				}
				if (Object.op_Implicit((Object)(object)menu.GetComponent<Rigidbody>()))
				{
					Object.Destroy((Object)(object)menu.GetComponent<Rigidbody>());
				}
			}
			else if (!abuttonDown && Mods.right && !Object.op_Implicit((Object)(object)menu.GetComponent<Rigidbody>()))
			{
				Object.Destroy((Object)(object)reference);
				reference = null;
				menu.AddComponent<Rigidbody>();
				menu.GetComponent<Rigidbody>().isKinematic = false;
				menu.GetComponent<Rigidbody>().useGravity = true;
				menu.GetComponent<Rigidbody>().linearVelocity = GTPlayer.Instance.RightHand.velocityTracker.GetAverageVelocity(true, 0f, false);
			}
			foreach (ButtonInfo settingsbutton in settingsbuttons)
			{
				if (settingsbutton.method != null)
				{
					if (settingsbutton.enabled.GetValueOrDefault() && settingsbutton.nontoggleable == false)
					{
						settingsbutton.method();
					}
					if (settingsbutton.enabled.GetValueOrDefault() && settingsbutton.nontoggleable.GetValueOrDefault())
					{
						settingsbutton.method();
						Mods.DisableButton(settingsbutton.buttonText);
					}
					if (settingsbutton.enabled == false && settingsbutton.disableMethod != null)
					{
						settingsbutton.disableMethod();
					}
				}
			}
			foreach (ButtonInfo button in buttons)
			{
				if (button.method != null)
				{
					if (button.enabled.GetValueOrDefault() && button.nontoggleable == false)
					{
						button.method();
					}
					if (button.enabled.GetValueOrDefault() && button.nontoggleable.GetValueOrDefault())
					{
						button.method();
						Mods.DisableButton(button.buttonText);
					}
					if (button.enabled == false && button.disableMethod != null)
					{
						button.disableMethod();
					}
				}
			}
			foreach (ButtonInfo item in CatButtons1)
			{
				if (item.method != null)
				{
					if (item.enabled.GetValueOrDefault() && item.nontoggleable == false)
					{
						item.method();
					}
					if (item.enabled.GetValueOrDefault() && item.nontoggleable.GetValueOrDefault())
					{
						item.method();
						Mods.DisableButton(item.buttonText);
					}
					if (item.enabled == false && item.disableMethod != null)
					{
						item.disableMethod();
					}
				}
			}
			foreach (ButtonInfo item2 in CatButtons9)
			{
				if (item2.method != null)
				{
					if (item2.enabled.GetValueOrDefault() && item2.nontoggleable == false)
					{
						item2.method();
					}
					if (item2.enabled.GetValueOrDefault() && item2.nontoggleable.GetValueOrDefault())
					{
						item2.method();
						Mods.DisableButton(item2.buttonText);
					}
					if (item2.enabled == false && item2.disableMethod != null)
					{
						item2.disableMethod();
					}
				}
			}
			foreach (ButtonInfo item3 in CatButtons2)
			{
				if (item3.method != null)
				{
					if (item3.enabled.GetValueOrDefault() && item3.nontoggleable == false)
					{
						item3.method();
					}
					if (item3.enabled.GetValueOrDefault() && item3.nontoggleable.GetValueOrDefault())
					{
						item3.method();
						Mods.DisableButton(item3.buttonText);
					}
					if (item3.enabled == false && item3.disableMethod != null)
					{
						item3.disableMethod();
					}
				}
			}
			foreach (ButtonInfo item4 in CatButtons3)
			{
				if (item4.method != null)
				{
					if (item4.enabled.GetValueOrDefault() && item4.nontoggleable == false)
					{
						item4.method();
					}
					if (item4.enabled.GetValueOrDefault() && item4.nontoggleable.GetValueOrDefault())
					{
						item4.method();
						Mods.DisableButton(item4.buttonText);
					}
					if (item4.enabled == false && item4.disableMethod != null)
					{
						item4.disableMethod();
					}
				}
			}
			foreach (ButtonInfo item5 in CatButtons4)
			{
				if (item5.method != null)
				{
					if (item5.enabled.GetValueOrDefault() && item5.nontoggleable == false)
					{
						item5.method();
					}
					if (item5.enabled.GetValueOrDefault() && item5.nontoggleable.GetValueOrDefault())
					{
						item5.method();
						Mods.DisableButton(item5.buttonText);
					}
					if (item5.enabled == false && item5.disableMethod != null)
					{
						item5.disableMethod();
					}
				}
			}
			foreach (ButtonInfo item6 in CatButtons5)
			{
				if (item6.method != null)
				{
					if (item6.enabled.GetValueOrDefault() && item6.nontoggleable == false)
					{
						item6.method();
					}
					if (item6.enabled.GetValueOrDefault() && item6.nontoggleable.GetValueOrDefault())
					{
						item6.method();
						Mods.DisableButton(item6.buttonText);
					}
					if (item6.enabled == false && item6.disableMethod != null)
					{
						item6.disableMethod();
					}
				}
			}
			foreach (ButtonInfo item7 in CatButtons6)
			{
				if (item7.method != null)
				{
					if (item7.enabled.GetValueOrDefault() && item7.nontoggleable == false)
					{
						item7.method();
					}
					if (item7.enabled.GetValueOrDefault() && item7.nontoggleable.GetValueOrDefault())
					{
						item7.method();
						Mods.DisableButton(item7.buttonText);
					}
					if (item7.enabled == false && item7.disableMethod != null)
					{
						item7.disableMethod();
					}
				}
			}
			foreach (ButtonInfo item8 in CatButtons7)
			{
				if (item8.method != null)
				{
					if (item8.enabled.GetValueOrDefault() && item8.nontoggleable == false)
					{
						item8.method();
					}
					if (item8.enabled.GetValueOrDefault() && item8.nontoggleable.GetValueOrDefault())
					{
						item8.method();
						Mods.DisableButton(item8.buttonText);
					}
					if (item8.enabled == false && item8.disableMethod != null)
					{
						item8.disableMethod();
					}
				}
			}
			foreach (ButtonInfo item9 in CatButtons8)
			{
				if (item9.method != null)
				{
					if (item9.enabled.GetValueOrDefault() && item9.nontoggleable == false)
					{
						item9.method();
					}
					if (item9.enabled.GetValueOrDefault() && item9.nontoggleable.GetValueOrDefault())
					{
						item9.method();
						Mods.DisableButton(item9.buttonText);
					}
					if (item9.enabled == false && item9.disableMethod != null)
					{
						item9.disableMethod();
					}
				}
			}
			foreach (ButtonInfo item10 in CatButtons9)
			{
				if (item10.method != null)
				{
					if (item10.enabled.GetValueOrDefault() && item10.nontoggleable == false)
					{
						item10.method();
					}
					if (item10.enabled.GetValueOrDefault() && item10.nontoggleable.GetValueOrDefault())
					{
						item10.method();
						Mods.DisableButton(item10.buttonText);
					}
					if (item10.enabled == false && item10.disableMethod != null)
					{
						item10.disableMethod();
					}
				}
			}
			foreach (ButtonInfo item11 in CatButtons10)
			{
				if (item11.method != null)
				{
					if (item11.enabled.GetValueOrDefault() && item11.nontoggleable == false)
					{
						item11.method();
					}
					if (item11.enabled.GetValueOrDefault() && item11.nontoggleable.GetValueOrDefault())
					{
						item11.method();
						Mods.DisableButton(item11.buttonText);
					}
					if (item11.enabled == false && item11.disableMethod != null)
					{
						item11.disableMethod();
					}
				}
			}
			if (!Directory.Exists(FolderName))
			{
				Directory.CreateDirectory(FolderName);
			}
			if (custom)
			{
				((TMP_Text)GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/motdHeadingText").GetComponent<TextMeshPro>()).text = CustomBoardTexts[0];
				((TMP_Text)GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/CodeOfConductHeadingText").GetComponent<TextMeshPro>()).text = CustomBoardTexts[1];
				((TMP_Text)GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/COCBodyText_TitleData").GetComponent<TextMeshPro>()).text = CustomBoardTexts[2];
				if (PhotonNetwork.IsConnectedAndReady)
				{
					((TMP_Text)GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/motdBodyText").GetComponent<TextMeshPro>()).text = CustomBoardTexts[3];
					custom = false;
				}
			}
		}
		catch
		{
		}
	}

	private static string GetButtonTooltip(int index)
	{
		if (Mods.inSettings)
		{
			ButtonInfo buttonInfo = settingsbuttons[index];
			return buttonInfo.buttonText + ": " + buttonInfo.toolTip;
		}
		if (Mods.inCat1)
		{
			ButtonInfo buttonInfo2 = CatButtons1[index];
			return buttonInfo2.buttonText + ": " + buttonInfo2.toolTip;
		}
		if (Mods.inCat2)
		{
			ButtonInfo buttonInfo3 = CatButtons2[index];
			return buttonInfo3.buttonText + ": " + buttonInfo3.toolTip;
		}
		if (Mods.inCat3)
		{
			ButtonInfo buttonInfo4 = CatButtons3[index];
			return buttonInfo4.buttonText + ": " + buttonInfo4.toolTip;
		}
		if (Mods.inCat4)
		{
			ButtonInfo buttonInfo5 = CatButtons4[index];
			return buttonInfo5.buttonText + ": " + buttonInfo5.toolTip;
		}
		if (Mods.inCat5)
		{
			ButtonInfo buttonInfo6 = CatButtons5[index];
			return buttonInfo6.buttonText + ": " + buttonInfo6.toolTip;
		}
		if (Mods.inCat6)
		{
			ButtonInfo buttonInfo7 = CatButtons6[index];
			return buttonInfo7.buttonText + ": " + buttonInfo7.toolTip;
		}
		if (Mods.inCat7)
		{
			ButtonInfo buttonInfo8 = CatButtons7[index];
			return buttonInfo8.buttonText + ": " + buttonInfo8.toolTip;
		}
		if (Mods.inCat8)
		{
			ButtonInfo buttonInfo9 = CatButtons8[index];
			return buttonInfo9.buttonText + ": " + buttonInfo9.toolTip;
		}
		if (Mods.inCat9)
		{
			ButtonInfo buttonInfo10 = CatButtons9[index];
			return buttonInfo10.buttonText + ": " + buttonInfo10.toolTip;
		}
		if (Mods.inCat10)
		{
			ButtonInfo buttonInfo11 = CatButtons10[index];
			return buttonInfo11.buttonText + ": " + buttonInfo11.toolTip;
		}
		if (Mods.inCat11)
		{
			ButtonInfo buttonInfo12 = CatButtons11[index];
			return buttonInfo12.buttonText + ": " + buttonInfo12.toolTip;
		}
		ButtonInfo buttonInfo13 = buttons[index];
		return buttonInfo13.buttonText + ": " + buttonInfo13.toolTip;
	}

	public void Draw()
	{
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e9: Expected O, but got Unknown
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Expected O, but got Unknown
		//IL_034c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0357: Unknown result type (might be due to invalid IL or missing references)
		//IL_0361: Expected O, but got Unknown
		//IL_03b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0419: Unknown result type (might be due to invalid IL or missing references)
		//IL_0424: Unknown result type (might be due to invalid IL or missing references)
		//IL_0429: Unknown result type (might be due to invalid IL or missing references)
		//IL_0435: Unknown result type (might be due to invalid IL or missing references)
		//IL_043a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0475: Unknown result type (might be due to invalid IL or missing references)
		//IL_0482: Unknown result type (might be due to invalid IL or missing references)
		//IL_048f: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_044f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c30: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c37: Expected O, but got Unknown
		//IL_0c64: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c6e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cf8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d03: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d08: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d14: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d19: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d49: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d56: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d63: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d7f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d84: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d32: Unknown result type (might be due to invalid IL or missing references)
		menu = GameObject.CreatePrimitive((PrimitiveType)3);
		Object.Destroy((Object)(object)menu.GetComponent<Rigidbody>());
		Object.Destroy((Object)(object)menu.GetComponent<BoxCollider>());
		Object.Destroy((Object)(object)menu.GetComponent<Renderer>());
		menu.transform.localScale = new Vector3(0.1f, 0.3f, 0.4f) * GTPlayer.Instance.scale;
		menuObj = GameObject.CreatePrimitive((PrimitiveType)3);
		Object.Destroy((Object)(object)menuObj.GetComponent<Rigidbody>());
		Object.Destroy((Object)(object)menuObj.GetComponent<BoxCollider>());
		menuObj.transform.parent = menu.transform;
		menuObj.transform.rotation = Quaternion.identity;
		menuObj.transform.localScale = MenuScale;
		if (ChangingColors)
		{
			if (Mods.RGBMenu)
			{
				GradientColorKey[] array = (GradientColorKey[])(object)new GradientColorKey[7];
				array[0].color = Color.red;
				array[0].time = 0f;
				array[1].color = Color.yellow;
				array[1].time = 0.2f;
				array[2].color = Color.green;
				array[2].time = 0.3f;
				array[3].color = Color.cyan;
				array[3].time = 0.5f;
				array[4].color = Color.blue;
				array[4].time = 0.6f;
				array[5].color = Color.magenta;
				array[5].time = 0.8f;
				array[6].color = Color.red;
				array[6].time = 1f;
				ColorChanger colorChanger = menuObj.AddComponent<ColorChanger>();
				colorChanger.colors = new Gradient
				{
					colorKeys = array
				};
				colorChanger.Start();
			}
			else
			{
				GradientColorKey[] array2 = (GradientColorKey[])(object)new GradientColorKey[4];
				array2[0].color = FirstColor1;
				array2[0].time = 0f;
				array2[1].color = FirstColor1;
				array2[1].time = 0.3f;
				array2[2].color = SecondColor;
				array2[2].time = 0.6f;
				array2[3].color = FirstColor1;
				array2[3].time = 1f;
				ColorChanger colorChanger2 = menuObj.AddComponent<ColorChanger>();
				colorChanger2.colors = new Gradient
				{
					colorKeys = array2
				};
				colorChanger2.Start();
			}
		}
		else
		{
			menuObj.GetComponent<Renderer>().material.color = NormalColor;
		}
		if (Mods.change10 == 10)
		{
			Object.Destroy((Object)(object)menuObj.GetComponent<Renderer>());
			Object.Destroy((Object)(object)menuObj.GetComponent<ColorChanger>());
		}
		menuObj.transform.position = MenuPos;
		canvasObj = new GameObject();
		canvasObj.transform.parent = menu.transform;
		Canvas val = canvasObj.AddComponent<Canvas>();
		CanvasScaler val2 = canvasObj.AddComponent<CanvasScaler>();
		canvasObj.AddComponent<GraphicRaycaster>();
		val.renderMode = (RenderMode)2;
		val2.dynamicPixelsPerUnit = 1000f;
		GameObject val3 = new GameObject();
		val3.transform.parent = canvasObj.transform;
		Text val4 = val3.AddComponent<Text>();
		((Object)((Component)val4).gameObject).name = "name";
		titiel = val4;
		val4.font = MenuFont;
		int num = pageNumber + 1;
		val4.text = MenuTitle;
		val4.fontSize = 1;
		val4.alignment = (TextAnchor)4;
		((Graphic)val4).color = MenuTitleColor;
		if (FirstColor1 == Color.white && SecondColor == Color.white)
		{
			((Graphic)val4).color = Color.black;
		}
		val4.resizeTextForBestFit = true;
		val4.resizeTextMinSize = 0;
		RectTransform component = ((Component)val4).GetComponent<RectTransform>();
		((Transform)component).localPosition = Vector3.zero;
		component.sizeDelta = MenuTitleScale;
		((Transform)component).position = MenuTitlePos;
		((Transform)component).rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
		AddPageButtons();
		string[] array3 = (from button in buttons.Skip(pageNumber * pageSize).Take(pageSize)
			select button.buttonText).ToArray();
		string[] array4 = (from button in settingsbuttons.Skip(pageNumber * pageSize).Take(pageSize)
			select button.buttonText).ToArray();
		string[] array5 = (from button in CatButtons1.Skip(pageNumber * pageSize).Take(pageSize)
			select button.buttonText).ToArray();
		string[] array6 = (from button in CatButtons2.Skip(pageNumber * pageSize).Take(pageSize)
			select button.buttonText).ToArray();
		string[] array7 = (from button in CatButtons9.Skip(pageNumber * pageSize).Take(pageSize)
			select button.buttonText).ToArray();
		string[] array8 = (from button in CatButtons3.Skip(pageNumber * pageSize).Take(pageSize)
			select button.buttonText).ToArray();
		string[] array9 = (from button in CatButtons4.Skip(pageNumber * pageSize).Take(pageSize)
			select button.buttonText).ToArray();
		string[] array10 = (from button in CatButtons5.Skip(pageNumber * pageSize).Take(pageSize)
			select button.buttonText).ToArray();
		string[] array11 = (from button in CatButtons6.Skip(pageNumber * pageSize).Take(pageSize)
			select button.buttonText).ToArray();
		string[] array12 = (from button in CatButtons7.Skip(pageNumber * pageSize).Take(pageSize)
			select button.buttonText).ToArray();
		string[] array13 = (from button in CatButtons8.Skip(pageNumber * pageSize).Take(pageSize)
			select button.buttonText).ToArray();
		string[] array14 = (from button in CatButtons10.Skip(pageNumber * pageSize).Take(pageSize)
			select button.buttonText).ToArray();
		string[] array15 = (from button in CatButtons11.Skip(pageNumber * pageSize).Take(pageSize)
			select button.buttonText).ToArray();
		if (Mods.inSettings)
		{
			for (int i = 0; i < array4.Length; i++)
			{
				AddButton((float)i * 0.13f + 0.26f, array4[i]);
			}
		}
		else
		{
			if (Mods.inCat1)
			{
				for (int j = 0; j < array5.Length; j++)
				{
					AddButton((float)j * 0.13f + 0.26f, array5[j]);
				}
			}
			if (Mods.inCat2)
			{
				for (int k = 0; k < array6.Length; k++)
				{
					AddButton((float)k * 0.13f + 0.26f, array6[k]);
				}
			}
			if (Mods.inCat9)
			{
				for (int l = 0; l < array7.Length; l++)
				{
					AddButton((float)l * 0.13f + 0.26f, array7[l]);
				}
			}
			if (Mods.inCat3)
			{
				for (int m = 0; m < array8.Length; m++)
				{
					AddButton((float)m * 0.13f + 0.26f, array8[m]);
				}
			}
			if (Mods.inCat4)
			{
				for (int n = 0; n < array9.Length; n++)
				{
					AddButton((float)n * 0.13f + 0.26f, array9[n]);
				}
			}
			if (Mods.inCat5)
			{
				for (int num2 = 0; num2 < array10.Length; num2++)
				{
					AddButton((float)num2 * 0.13f + 0.26f, array10[num2]);
				}
			}
			if (Mods.inCat6)
			{
				for (int num3 = 0; num3 < array11.Length; num3++)
				{
					AddButton((float)num3 * 0.13f + 0.26f, array11[num3]);
				}
			}
			if (Mods.inCat7)
			{
				for (int num4 = 0; num4 < array12.Length; num4++)
				{
					AddButton((float)num4 * 0.13f + 0.26f, array12[num4]);
				}
			}
			if (Mods.inCat8)
			{
				for (int num5 = 0; num5 < array13.Length; num5++)
				{
					AddButton((float)num5 * 0.13f + 0.26f, array13[num5]);
				}
			}
			if (Mods.inCat10)
			{
				for (int num6 = 0; num6 < array14.Length; num6++)
				{
					AddButton((float)num6 * 0.13f + 0.26f, array14[num6]);
				}
			}
			if (Mods.inCat11)
			{
				for (int num7 = 0; num7 < array15.Length; num7++)
				{
					AddButton((float)num7 * 0.13f + 0.26f, array15[num7]);
				}
			}
			if (!Mods.inCat1 && !Mods.inCat2 && !Mods.inCat3 && !Mods.inCat4 && !Mods.inCat10 && !Mods.inCat5 && !Mods.inCat6 && !Mods.inCat7 && !Mods.inCat9 && !Mods.inCat8 && !Mods.inSettings)
			{
				for (int num8 = 0; num8 < array3.Length; num8++)
				{
					AddButton((float)num8 * 0.13f + 0.26f, array3[num8]);
				}
			}
		}
		GameObject val5 = new GameObject();
		val5.transform.SetParent(canvasObj.transform);
		val5.transform.localPosition = new Vector3(0f, 0f, 1f) * 1f;
		tooltipText = val5.GetComponent<Text>();
		if ((Object)(object)tooltipText == (Object)null)
		{
			tooltipText = val5.AddComponent<Text>();
		}
		tooltipText.font = MenuFont;
		tooltipText.text = tooltipString;
		tooltipText.fontSize = 20;
		tooltipText.alignment = (TextAnchor)4;
		tooltipText.resizeTextForBestFit = true;
		tooltipText.resizeTextMinSize = 0;
		((Graphic)tooltipText).color = ToolTipColor;
		if (FirstColor1 == Color.white && SecondColor == Color.white)
		{
			((Graphic)tooltipText).color = Color.black;
		}
		RectTransform component2 = val5.GetComponent<RectTransform>();
		((Transform)component2).localPosition = Vector3.zero;
		component2.sizeDelta = ToolTipScale;
		((Transform)component2).position = ToolTipPos;
		((Transform)component2).rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
	}

	private static void AddPageButtons()
	{
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Expected O, but got Unknown
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_060d: Unknown result type (might be due to invalid IL or missing references)
		//IL_062e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0656: Unknown result type (might be due to invalid IL or missing references)
		//IL_0669: Unknown result type (might be due to invalid IL or missing references)
		//IL_066e: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0714: Unknown result type (might be due to invalid IL or missing references)
		//IL_0730: Unknown result type (might be due to invalid IL or missing references)
		//IL_0735: Unknown result type (might be due to invalid IL or missing references)
		//IL_0786: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_07fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0800: Unknown result type (might be due to invalid IL or missing references)
		//IL_085d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0873: Unknown result type (might be due to invalid IL or missing references)
		//IL_088a: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0939: Unknown result type (might be due to invalid IL or missing references)
		//IL_095a: Unknown result type (might be due to invalid IL or missing references)
		//IL_097b: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a10: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a26: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a3d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a59: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a75: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a7a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b15: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b3d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b48: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b4d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0baa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bc0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bd7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bf3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c0f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c14: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c86: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ca7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cc8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cf0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d03: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d08: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d65: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d7b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d92: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dca: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dcf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e20: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e41: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e62: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e8a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e95: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e9a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ef7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f0d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f24: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f40: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f5c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f61: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0302: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_035a: Unknown result type (might be due to invalid IL or missing references)
		//IL_037b: Unknown result type (might be due to invalid IL or missing references)
		//IL_039f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0426: Unknown result type (might be due to invalid IL or missing references)
		//IL_042b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0430: Unknown result type (might be due to invalid IL or missing references)
		//IL_045a: Unknown result type (might be due to invalid IL or missing references)
		//IL_045f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0464: Unknown result type (might be due to invalid IL or missing references)
		//IL_0486: Unknown result type (might be due to invalid IL or missing references)
		//IL_048b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0499: Expected O, but got Unknown
		//IL_04a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_051d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0534: Unknown result type (might be due to invalid IL or missing references)
		//IL_0559: Unknown result type (might be due to invalid IL or missing references)
		//IL_0575: Unknown result type (might be due to invalid IL or missing references)
		//IL_057a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ff0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1011: Unknown result type (might be due to invalid IL or missing references)
		//IL_1035: Unknown result type (might be due to invalid IL or missing references)
		//IL_105d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1068: Unknown result type (might be due to invalid IL or missing references)
		//IL_106d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1079: Unknown result type (might be due to invalid IL or missing references)
		//IL_10d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_10ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_1103: Unknown result type (might be due to invalid IL or missing references)
		//IL_1128: Unknown result type (might be due to invalid IL or missing references)
		//IL_1144: Unknown result type (might be due to invalid IL or missing references)
		//IL_1149: Unknown result type (might be due to invalid IL or missing references)
		//IL_1215: Unknown result type (might be due to invalid IL or missing references)
		//IL_1236: Unknown result type (might be due to invalid IL or missing references)
		//IL_125a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1282: Unknown result type (might be due to invalid IL or missing references)
		//IL_128d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1292: Unknown result type (might be due to invalid IL or missing references)
		//IL_129e: Unknown result type (might be due to invalid IL or missing references)
		//IL_12fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1311: Unknown result type (might be due to invalid IL or missing references)
		//IL_1328: Unknown result type (might be due to invalid IL or missing references)
		//IL_134d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1369: Unknown result type (might be due to invalid IL or missing references)
		//IL_136e: Unknown result type (might be due to invalid IL or missing references)
		//IL_117c: Unknown result type (might be due to invalid IL or missing references)
		//IL_11a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1426: Unknown result type (might be due to invalid IL or missing references)
		//IL_1447: Unknown result type (might be due to invalid IL or missing references)
		//IL_1468: Unknown result type (might be due to invalid IL or missing references)
		//IL_1490: Unknown result type (might be due to invalid IL or missing references)
		//IL_149b: Unknown result type (might be due to invalid IL or missing references)
		//IL_14a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_14ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_14ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_1516: Unknown result type (might be due to invalid IL or missing references)
		//IL_152c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1543: Unknown result type (might be due to invalid IL or missing references)
		//IL_155f: Unknown result type (might be due to invalid IL or missing references)
		//IL_157b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1580: Unknown result type (might be due to invalid IL or missing references)
		//IL_13a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_13c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_162c: Unknown result type (might be due to invalid IL or missing references)
		//IL_164d: Unknown result type (might be due to invalid IL or missing references)
		//IL_166e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1696: Unknown result type (might be due to invalid IL or missing references)
		//IL_16a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_16a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_16b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_16f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_171c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1732: Unknown result type (might be due to invalid IL or missing references)
		//IL_1749: Unknown result type (might be due to invalid IL or missing references)
		//IL_1765: Unknown result type (might be due to invalid IL or missing references)
		//IL_1781: Unknown result type (might be due to invalid IL or missing references)
		//IL_1786: Unknown result type (might be due to invalid IL or missing references)
		//IL_15b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_15cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_17b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_17d2: Unknown result type (might be due to invalid IL or missing references)
		int num = (buttons.Count + pageSize - 1) / pageSize;
		int num2 = pageNumber + 1;
		int num3 = pageNumber - 1;
		if (num2 > num - 1)
		{
			num2 = 0;
		}
		if (num3 < 0)
		{
			num3 = num - 1;
		}
		if (Mods.change7 == 1)
		{
			float num4 = 0f;
			GameObject val = GameObject.CreatePrimitive((PrimitiveType)3);
			Object.Destroy((Object)(object)val.GetComponent<Rigidbody>());
			((Collider)val.GetComponent<BoxCollider>()).isTrigger = true;
			val.transform.parent = menu.transform;
			val.transform.rotation = Quaternion.identity;
			val.transform.localScale = new Vector3(0.09f, 0.8f, 0.08f);
			val.transform.localPosition = new Vector3(0.56f, 0f, 0.28f - num4);
			val.AddComponent<BtnCollider>().relatedText = "PreviousPage";
			val.GetComponent<Renderer>().material.SetColor("_Color", Color.grey);
			GradientColorKey[] array = (GradientColorKey[])(object)new GradientColorKey[3];
			array[0].color = Color32.op_Implicit(new Color32((byte)50, (byte)50, (byte)50, byte.MaxValue));
			array[0].time = 0f;
			array[1].color = Color32.op_Implicit(new Color32((byte)90, (byte)90, (byte)90, byte.MaxValue));
			array[1].time = 0.5f;
			array[2].color = Color32.op_Implicit(new Color32((byte)50, (byte)50, (byte)50, byte.MaxValue));
			array[2].time = 1f;
			ColorChanger colorChanger = val.AddComponent<ColorChanger>();
			colorChanger.colors = new Gradient
			{
				colorKeys = array
			};
			colorChanger.Start();
			GameObject val2 = new GameObject();
			val2.transform.parent = canvasObj.transform;
			Text val3 = val2.AddComponent<Text>();
			val3.font = MenuFont;
			val3.text = "[" + num3 + "] << Prev";
			val3.fontSize = 1;
			val3.alignment = (TextAnchor)4;
			((Graphic)val3).color = MenuTitleColor;
			if (FirstColor1 == Color.white && SecondColor == Color.white)
			{
				((Graphic)val3).color = Color.black;
			}
			val3.resizeTextForBestFit = true;
			val3.resizeTextMinSize = 0;
			RectTransform component = ((Component)val3).GetComponent<RectTransform>();
			((Transform)component).localPosition = Vector3.zero;
			component.sizeDelta = new Vector2(0.2f, 0.03f);
			((Transform)component).localPosition = new Vector3(0.064f, 0f, 0.111f - num4 / 2.55f);
			((Transform)component).rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
			num4 = 0.13f;
			GameObject val4 = GameObject.CreatePrimitive((PrimitiveType)3);
			Object.Destroy((Object)(object)val4.GetComponent<Rigidbody>());
			((Collider)val4.GetComponent<BoxCollider>()).isTrigger = true;
			val4.transform.parent = menu.transform;
			val4.transform.rotation = Quaternion.identity;
			val4.transform.localScale = new Vector3(0.09f, 0.8f, 0.08f);
			val4.transform.localPosition = new Vector3(0.56f, 0f, 0.28f - num4);
			val4.AddComponent<BtnCollider>().relatedText = "NextPage";
			val4.GetComponent<Renderer>().material.SetColor("_Color", Color.grey);
			GradientColorKey[] array2 = (GradientColorKey[])(object)new GradientColorKey[3];
			array2[0].color = Color32.op_Implicit(new Color32((byte)50, (byte)50, (byte)50, byte.MaxValue));
			array2[0].time = 0f;
			array2[1].color = Color32.op_Implicit(new Color32((byte)90, (byte)90, (byte)90, byte.MaxValue));
			array2[1].time = 0.5f;
			array2[2].color = Color32.op_Implicit(new Color32((byte)50, (byte)50, (byte)50, byte.MaxValue));
			array2[2].time = 1f;
			ColorChanger colorChanger2 = val4.AddComponent<ColorChanger>();
			colorChanger2.colors = new Gradient
			{
				colorKeys = array2
			};
			colorChanger2.Start();
			GameObject val5 = new GameObject();
			val5.transform.parent = canvasObj.transform;
			Text val6 = val5.AddComponent<Text>();
			val6.font = MenuFont;
			val6.text = "Next >> [" + num2 + "]";
			val6.fontSize = 1;
			val6.alignment = (TextAnchor)4;
			val6.resizeTextForBestFit = true;
			val6.resizeTextMinSize = 0;
			RectTransform component2 = ((Component)val6).GetComponent<RectTransform>();
			((Transform)component2).localPosition = Vector3.zero;
			component2.sizeDelta = new Vector2(0.2f, 0.03f);
			((Transform)component2).localPosition = new Vector3(0.064f, 0f, 0.111f - num4 / 2.55f);
			((Transform)component2).rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
			pageSize = 4;
		}
		if (Mods.change7 == 2)
		{
			GameObject val7 = GameObject.CreatePrimitive((PrimitiveType)3);
			((Object)val7).name = "prev";
			Object.Destroy((Object)(object)val7.GetComponent<Rigidbody>());
			((Collider)val7.GetComponent<BoxCollider>()).isTrigger = true;
			val7.transform.parent = menu.transform;
			val7.transform.rotation = Quaternion.identity;
			val7.transform.localScale = new Vector3(0.045f, 0.25f, 0.064295f);
			val7.transform.localPosition = new Vector3(0.56f, 0.37f, 0.541f);
			val7.AddComponent<BtnCollider>().relatedText = "PreviousPage";
			val7.GetComponent<Renderer>().material.color = NextPrevButtonColor;
			GameObject val8 = GameObject.CreatePrimitive((PrimitiveType)3);
			GameObject val9 = new GameObject();
			val9.transform.parent = canvasObj.transform;
			Text val10 = val9.AddComponent<Text>();
			val10.font = MenuFont;
			val10.text = "<";
			val10.fontSize = 1;
			val10.alignment = (TextAnchor)4;
			val10.resizeTextForBestFit = true;
			val10.resizeTextMinSize = 0;
			((Graphic)val10).color = NextPrevTextColor;
			RectTransform component3 = ((Component)val10).GetComponent<RectTransform>();
			((Transform)component3).localPosition = Vector3.zero;
			component3.sizeDelta = new Vector2(0.2f, 0.03f);
			((Transform)component3).localPosition = new Vector3(0.064f, 0.11f, 0.215f);
			((Transform)component3).rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
			((Object)val8).name = "next";
			Object.Destroy((Object)(object)val8.GetComponent<Rigidbody>());
			((Collider)val8.GetComponent<BoxCollider>()).isTrigger = true;
			val8.transform.parent = menu.transform;
			val8.transform.rotation = Quaternion.identity;
			val8.transform.localScale = new Vector3(0.045f, 0.25f, 0.064295f);
			val8.transform.localPosition = new Vector3(0.56f, -0.37f, 0.541f);
			val8.AddComponent<BtnCollider>().relatedText = "NextPage";
			val8.GetComponent<Renderer>().material.color = NextPrevButtonColor;
			GameObject val11 = new GameObject();
			val11.transform.parent = canvasObj.transform;
			Text val12 = val11.AddComponent<Text>();
			val12.font = MenuFont;
			val12.text = ">";
			val12.fontSize = 1;
			val12.alignment = (TextAnchor)4;
			val12.resizeTextForBestFit = true;
			val12.resizeTextMinSize = 0;
			((Graphic)val12).color = NextPrevTextColor;
			RectTransform component4 = ((Component)val12).GetComponent<RectTransform>();
			((Transform)component4).localPosition = Vector3.zero;
			component4.sizeDelta = new Vector2(0.2f, 0.03f);
			((Transform)component4).localPosition = new Vector3(0.064f, -0.11f, 0.215f);
			((Transform)component4).rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
			pageSize = 6;
		}
		if (Mods.change7 == 3)
		{
			GameObject val13 = GameObject.CreatePrimitive((PrimitiveType)3);
			((Object)val13).name = "prev";
			Object.Destroy((Object)(object)val13.GetComponent<Rigidbody>());
			((Collider)val13.GetComponent<BoxCollider>()).isTrigger = true;
			val13.transform.parent = menu.transform;
			val13.transform.rotation = Quaternion.identity;
			val13.transform.localScale = new Vector3(0.045f, 0.25f, 0.8936298f);
			val13.transform.localPosition = new Vector3(0.56f, 0.657f, 0.0063f);
			val13.AddComponent<BtnCollider>().relatedText = "PreviousPage";
			val13.GetComponent<Renderer>().material.color = NextPrevButtonColor;
			GameObject val14 = new GameObject();
			val14.transform.parent = canvasObj.transform;
			Text val15 = val14.AddComponent<Text>();
			val15.font = MenuFont;
			val15.text = "<";
			val15.fontSize = 1;
			val15.alignment = (TextAnchor)4;
			val15.resizeTextForBestFit = true;
			val15.resizeTextMinSize = 0;
			((Graphic)val15).color = NextPrevTextColor;
			RectTransform component5 = ((Component)val15).GetComponent<RectTransform>();
			((Transform)component5).localPosition = Vector3.zero;
			component5.sizeDelta = new Vector2(0.2f, 0.03f);
			((Transform)component5).localPosition = new Vector3(0.064f, 0.2f, 0.0063f);
			((Transform)component5).rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
			GameObject val16 = GameObject.CreatePrimitive((PrimitiveType)3);
			((Object)val16).name = "next";
			Object.Destroy((Object)(object)val16.GetComponent<Rigidbody>());
			((Collider)val16.GetComponent<BoxCollider>()).isTrigger = true;
			val16.transform.parent = menu.transform;
			val16.transform.rotation = Quaternion.identity;
			val16.transform.localScale = new Vector3(0.045f, 0.25f, 0.8936298f);
			val16.transform.localPosition = new Vector3(0.56f, -0.657f, 0.0063f);
			val16.AddComponent<BtnCollider>().relatedText = "NextPage";
			val16.GetComponent<Renderer>().material.color = NextPrevButtonColor;
			GameObject val17 = new GameObject();
			val17.transform.parent = canvasObj.transform;
			Text val18 = val17.AddComponent<Text>();
			val18.font = MenuFont;
			val18.text = ">";
			val18.fontSize = 1;
			val18.alignment = (TextAnchor)4;
			val18.resizeTextForBestFit = true;
			val18.resizeTextMinSize = 0;
			((Graphic)val18).color = NextPrevTextColor;
			RectTransform component6 = ((Component)val18).GetComponent<RectTransform>();
			((Transform)component6).localPosition = Vector3.zero;
			component6.sizeDelta = new Vector2(0.2f, 0.03f);
			((Transform)component6).localPosition = new Vector3(0.064f, -0.2f, 0.0063f);
			((Transform)component6).rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
			pageSize = 6;
		}
		if (Mods.change7 == 4)
		{
			GameObject val19 = GameObject.CreatePrimitive((PrimitiveType)3);
			((Object)val19).name = "prev";
			Object.Destroy((Object)(object)val19.GetComponent<Rigidbody>());
			((Collider)val19.GetComponent<BoxCollider>()).isTrigger = true;
			val19.transform.parent = menu.transform;
			val19.transform.rotation = Quaternion.identity;
			val19.transform.localScale = new Vector3(0.045f, 0.25f, 0.064295f);
			val19.transform.localPosition = new Vector3(0.56f, 0.37f, -0.541f);
			val19.AddComponent<BtnCollider>().relatedText = "PreviousPage";
			val19.GetComponent<Renderer>().material.color = NextPrevButtonColor;
			GameObject val20 = GameObject.CreatePrimitive((PrimitiveType)3);
			GameObject val21 = new GameObject();
			val21.transform.parent = canvasObj.transform;
			Text val22 = val21.AddComponent<Text>();
			val22.font = MenuFont;
			val22.text = "<";
			val22.fontSize = 1;
			val22.alignment = (TextAnchor)4;
			val22.resizeTextForBestFit = true;
			val22.resizeTextMinSize = 0;
			((Graphic)val22).color = NextPrevTextColor;
			RectTransform component7 = ((Component)val22).GetComponent<RectTransform>();
			((Transform)component7).localPosition = Vector3.zero;
			component7.sizeDelta = new Vector2(0.2f, 0.03f);
			((Transform)component7).localPosition = new Vector3(0.064f, 0.11f, -0.215f);
			((Transform)component7).rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
			((Object)val20).name = "next";
			Object.Destroy((Object)(object)val20.GetComponent<Rigidbody>());
			((Collider)val20.GetComponent<BoxCollider>()).isTrigger = true;
			val20.transform.parent = menu.transform;
			val20.transform.rotation = Quaternion.identity;
			val20.transform.localScale = new Vector3(0.045f, 0.25f, 0.064295f);
			val20.transform.localPosition = new Vector3(0.56f, -0.37f, -0.541f);
			val20.AddComponent<BtnCollider>().relatedText = "NextPage";
			val20.GetComponent<Renderer>().material.color = NextPrevButtonColor;
			GameObject val23 = new GameObject();
			val23.transform.parent = canvasObj.transform;
			Text val24 = val23.AddComponent<Text>();
			val24.font = MenuFont;
			val24.text = ">";
			val24.fontSize = 1;
			val24.alignment = (TextAnchor)4;
			val24.resizeTextForBestFit = true;
			val24.resizeTextMinSize = 0;
			((Graphic)val24).color = NextPrevTextColor;
			RectTransform component8 = ((Component)val24).GetComponent<RectTransform>();
			((Transform)component8).localPosition = Vector3.zero;
			component8.sizeDelta = new Vector2(0.2f, 0.03f);
			((Transform)component8).localPosition = new Vector3(0.064f, -0.11f, -0.215f);
			((Transform)component8).rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
			pageSize = 6;
		}
		if (Mods.change7 == 5)
		{
			pageSize = 6;
		}
		if (Mods.change4 == 1)
		{
			float num5 = 0.26f;
			GameObject val25 = GameObject.CreatePrimitive((PrimitiveType)3);
			((Object)val25).name = "disconnect";
			Object.Destroy((Object)(object)val25.GetComponent<Rigidbody>());
			((Collider)val25.GetComponent<BoxCollider>()).isTrigger = true;
			val25.transform.parent = menu.transform;
			val25.transform.rotation = Quaternion.identity;
			val25.transform.localScale = new Vector3(0.045f, 0.55f, 0.16f);
			val25.transform.localPosition = new Vector3(0.56f, -0.8f, 0.35f - num5);
			val25.AddComponent<BtnCollider>().relatedText = "DisconnectingButton";
			val25.GetComponent<Renderer>().material.color = DisconnectButtonColor;
			GameObject val26 = new GameObject
			{
				name = "disconnect text"
			};
			val26.transform.parent = canvasObj.transform;
			Text val27 = val26.AddComponent<Text>();
			val27.font = MenuFont;
			val27.text = "Disconnect";
			val27.fontSize = 1;
			val27.alignment = (TextAnchor)4;
			val27.resizeTextForBestFit = true;
			val27.resizeTextMinSize = 0;
			((Graphic)val27).color = DisconnectTextColor;
			RectTransform component9 = ((Component)val27).GetComponent<RectTransform>();
			((Transform)component9).localPosition = Vector3.zero;
			component9.sizeDelta = new Vector2(0.2f, 0.03f);
			((Transform)component9).localPosition = new Vector3(0.06f, -0.24f, 0.14f - num5 / 2.55f);
			((Transform)component9).rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
			if (Mods.change7 == 3)
			{
				val25.transform.localPosition = new Vector3(0.56f, -1.1f, 0.35f - num5);
				((Transform)component9).localPosition = new Vector3(0.06f, -0.33f, 0.14f - num5 / 2.55f);
			}
		}
		if (Mods.change4 == 2)
		{
			float num6 = 0.26f;
			GameObject val28 = GameObject.CreatePrimitive((PrimitiveType)3);
			((Object)val28).name = "disconnect";
			Object.Destroy((Object)(object)val28.GetComponent<Rigidbody>());
			((Collider)val28.GetComponent<BoxCollider>()).isTrigger = true;
			val28.transform.parent = menu.transform;
			val28.transform.rotation = Quaternion.identity;
			val28.transform.localScale = new Vector3(0.045f, 0.55f, 0.16f);
			val28.transform.localPosition = new Vector3(0.56f, 0.8f, 0.35f - num6);
			val28.AddComponent<BtnCollider>().relatedText = "DisconnectingButton";
			val28.GetComponent<Renderer>().material.color = DisconnectButtonColor;
			GameObject val29 = new GameObject
			{
				name = "disconnect text"
			};
			val29.transform.parent = canvasObj.transform;
			Text val30 = val29.AddComponent<Text>();
			val30.font = MenuFont;
			val30.text = "Disconnect";
			val30.fontSize = 1;
			val30.alignment = (TextAnchor)4;
			val30.resizeTextForBestFit = true;
			val30.resizeTextMinSize = 0;
			((Graphic)val30).color = DisconnectTextColor;
			RectTransform component10 = ((Component)val30).GetComponent<RectTransform>();
			((Transform)component10).localPosition = Vector3.zero;
			component10.sizeDelta = new Vector2(0.2f, 0.03f);
			((Transform)component10).localPosition = new Vector3(0.06f, 0.24f, 0.14f - num6 / 2.55f);
			((Transform)component10).rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
			if (Mods.change7 == 3)
			{
				val28.transform.localPosition = new Vector3(0.56f, 1.1f, 0.35f - num6);
				((Transform)component10).localPosition = new Vector3(0.06f, 0.33f, 0.14f - num6 / 2.55f);
			}
		}
		if (Mods.change4 == 3)
		{
			GameObject val31 = GameObject.CreatePrimitive((PrimitiveType)3);
			((Object)val31).name = "disconnect";
			((Collider)val31.GetComponent<BoxCollider>()).isTrigger = true;
			val31.transform.parent = menu.transform;
			val31.transform.rotation = Quaternion.identity;
			val31.transform.localScale = new Vector3(0.12f, 0.9f, 0.1f);
			val31.transform.localPosition = new Vector3(0.56f, 0f, 0.6f);
			val31.AddComponent<BtnCollider>().relatedText = "DisconnectingButton";
			val31.GetComponent<Renderer>().material.color = DisconnectButtonColor;
			GameObject val32 = new GameObject
			{
				name = "disconnect text"
			};
			val32.transform.parent = canvasObj.transform;
			Text val33 = val32.AddComponent<Text>();
			val33.font = MenuFont;
			val33.text = "Disconnect";
			val33.fontSize = 1;
			((Graphic)val33).color = Color.white;
			val33.alignment = (TextAnchor)4;
			val33.resizeTextForBestFit = true;
			val33.resizeTextMinSize = 0;
			((Graphic)val33).color = DisconnectTextColor;
			RectTransform component11 = ((Component)val33).GetComponent<RectTransform>();
			((Transform)component11).localPosition = Vector3.zero;
			component11.sizeDelta = new Vector2(0.2f, 0.03f);
			((Transform)component11).localPosition = new Vector3(0.064f, 0f, 0.24f);
			((Transform)component11).rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
			if (Mods.change7 == 2)
			{
				val31.transform.localPosition = new Vector3(0.56f, 0f, 0.7f);
				((Transform)component11).localPosition = new Vector3(0.064f, 0f, 0.28f);
			}
		}
		if (Mods.change4 == 4)
		{
			GameObject val34 = GameObject.CreatePrimitive((PrimitiveType)3);
			((Object)val34).name = "disconnect";
			((Collider)val34.GetComponent<BoxCollider>()).isTrigger = true;
			val34.transform.parent = menu.transform;
			val34.transform.rotation = Quaternion.identity;
			val34.transform.localScale = new Vector3(0.12f, 0.9f, 0.1f);
			val34.transform.localPosition = new Vector3(0.56f, 0f, -0.6f);
			val34.AddComponent<BtnCollider>().relatedText = "DisconnectingButton";
			val34.GetComponent<Renderer>().material.color = DisconnectButtonColor;
			GameObject val35 = new GameObject
			{
				name = "disconnect text"
			};
			val35.transform.parent = canvasObj.transform;
			Text val36 = val35.AddComponent<Text>();
			val36.font = MenuFont;
			val36.text = "Disconnect";
			val36.fontSize = 1;
			((Graphic)val36).color = Color.white;
			val36.alignment = (TextAnchor)4;
			val36.resizeTextForBestFit = true;
			val36.resizeTextMinSize = 0;
			((Graphic)val36).color = DisconnectTextColor;
			RectTransform component12 = ((Component)val36).GetComponent<RectTransform>();
			((Transform)component12).localPosition = Vector3.zero;
			component12.sizeDelta = new Vector2(0.2f, 0.03f);
			((Transform)component12).localPosition = new Vector3(0.064f, 0f, -0.24f);
			((Transform)component12).rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
			if (Mods.change7 == 4)
			{
				val34.transform.localPosition = new Vector3(0.56f, 0f, -0.7f);
				((Transform)component12).localPosition = new Vector3(0.064f, 0f, -0.28f);
			}
		}
	}

	public static void DestroyMenu()
	{
		Object.Destroy((Object)(object)menu);
		Object.Destroy((Object)(object)canvasObj);
		Object.Destroy((Object)(object)reference);
		menu = null;
		menuObj = null;
		canvasObj = null;
		reference = null;
	}

	private static void AddButton(float offset, string text)
	{
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0585: Unknown result type (might be due to invalid IL or missing references)
		//IL_058a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0602: Unknown result type (might be due to invalid IL or missing references)
		//IL_060e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0640: Unknown result type (might be due to invalid IL or missing references)
		//IL_06aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_06af: Unknown result type (might be due to invalid IL or missing references)
		//IL_068e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0743: Unknown result type (might be due to invalid IL or missing references)
		//IL_0753: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0704: Unknown result type (might be due to invalid IL or missing references)
		//IL_0810: Unknown result type (might be due to invalid IL or missing references)
		//IL_0820: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0888: Unknown result type (might be due to invalid IL or missing references)
		//IL_0898: Unknown result type (might be due to invalid IL or missing references)
		//IL_099e: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_094f: Unknown result type (might be due to invalid IL or missing references)
		//IL_095f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a65: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a75: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a16: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a26: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b2c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b3c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0add: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bf3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c03: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ba4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bb4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cba: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cca: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c6b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c7b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d81: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d91: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d32: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d42: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e48: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e58: Unknown result type (might be due to invalid IL or missing references)
		//IL_0df9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e09: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f0f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f1f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ec0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ed0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fd4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fe4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f87: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f97: Unknown result type (might be due to invalid IL or missing references)
		//IL_10e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_10f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1096: Unknown result type (might be due to invalid IL or missing references)
		//IL_10a6: Unknown result type (might be due to invalid IL or missing references)
		Button = GameObject.CreatePrimitive((PrimitiveType)3);
		Object.Destroy((Object)(object)Button.GetComponent<Rigidbody>());
		((Collider)Button.GetComponent<BoxCollider>()).isTrigger = true;
		Button.transform.parent = menu.transform;
		Button.transform.rotation = Quaternion.identity;
		Button.transform.localScale = ButtonScale * GTPlayer.Instance.scale;
		if (Mods.change7 == 1)
		{
			Button.transform.localPosition = new Vector3(0.56f, 0f, 0.28f - offset);
		}
		if ((Mods.change7 == 2) | (Mods.change7 == 3) | (Mods.change7 == 4) | (Mods.change7 == 5))
		{
			Button.transform.localPosition = new Vector3(0.56f, 0f, 0.6f - offset);
		}
		Button.AddComponent<BtnCollider>().relatedText = text;
		int index = -1;
		if (Mods.inSettings)
		{
			for (int i = 0; i < settingsbuttons.Count; i++)
			{
				if (text == settingsbuttons[i].buttonText)
				{
					index = i;
					break;
				}
			}
		}
		else
		{
			if (Mods.inCat1)
			{
				for (int j = 0; j < CatButtons1.Count; j++)
				{
					if (text == CatButtons1[j].buttonText)
					{
						index = j;
						break;
					}
				}
			}
			if (Mods.inCat2)
			{
				for (int k = 0; k < CatButtons2.Count; k++)
				{
					if (text == CatButtons2[k].buttonText)
					{
						index = k;
						break;
					}
				}
			}
			if (Mods.inCat9)
			{
				for (int l = 0; l < CatButtons9.Count; l++)
				{
					if (text == CatButtons9[l].buttonText)
					{
						index = l;
						break;
					}
				}
			}
			if (Mods.inCat3)
			{
				for (int m = 0; m < CatButtons3.Count; m++)
				{
					if (text == CatButtons3[m].buttonText)
					{
						index = m;
						break;
					}
				}
			}
			if (Mods.inCat4)
			{
				for (int n = 0; n < CatButtons4.Count; n++)
				{
					if (text == CatButtons4[n].buttonText)
					{
						index = n;
						break;
					}
				}
			}
			if (Mods.inCat5)
			{
				for (int num = 0; num < CatButtons5.Count; num++)
				{
					if (text == CatButtons5[num].buttonText)
					{
						index = num;
						break;
					}
				}
			}
			if (Mods.inCat6)
			{
				for (int num2 = 0; num2 < CatButtons6.Count; num2++)
				{
					if (text == CatButtons6[num2].buttonText)
					{
						index = num2;
						break;
					}
				}
			}
			if (Mods.inCat7)
			{
				for (int num3 = 0; num3 < CatButtons7.Count; num3++)
				{
					if (text == CatButtons7[num3].buttonText)
					{
						index = num3;
						break;
					}
				}
			}
			if (Mods.inCat8)
			{
				for (int num4 = 0; num4 < CatButtons8.Count; num4++)
				{
					if (text == CatButtons8[num4].buttonText)
					{
						index = num4;
						break;
					}
				}
			}
			if (Mods.inCat10)
			{
				for (int num5 = 0; num5 < CatButtons10.Count; num5++)
				{
					if (text == CatButtons10[num5].buttonText)
					{
						index = num5;
						break;
					}
				}
			}
			if (Mods.inCat11)
			{
				for (int num6 = 0; num6 < CatButtons11.Count; num6++)
				{
					if (text == CatButtons11[num6].buttonText)
					{
						index = num6;
						break;
					}
				}
			}
			if (!Mods.inCat1 && !Mods.inCat2 && !Mods.inCat3 && !Mods.inCat4 && !Mods.inCat10 && !Mods.inCat5 && !Mods.inCat6 && !Mods.inCat7 && !Mods.inCat9 && !Mods.inCat8 && !Mods.inSettings)
			{
				for (int num7 = 0; num7 < buttons.Count; num7++)
				{
					if (text == buttons[num7].buttonText)
					{
						index = num7;
						break;
					}
				}
			}
		}
		GameObject val = new GameObject();
		val.transform.parent = canvasObj.transform;
		text2 = val.AddComponent<Text>();
		text2.font = MenuFont;
		text2.text = text;
		text2.fontSize = 1;
		text2.alignment = (TextAnchor)4;
		text2.resizeTextForBestFit = true;
		text2.resizeTextMinSize = 0;
		RectTransform component = ((Component)text2).GetComponent<RectTransform>();
		((Transform)component).localPosition = Vector3.zero;
		component.sizeDelta = ButtonTextScale;
		if (Mods.change7 == 1)
		{
			((Transform)component).localPosition = new Vector3(0.064f, 0f, 0.111f - offset / 2.55f);
		}
		if ((Mods.change7 == 2) | (Mods.change7 == 3) | (Mods.change7 == 4) | (Mods.change7 == 5))
		{
			((Transform)component).localPosition = new Vector3(0.064f, 0f, 0.237f - offset / 2.55f);
		}
		((Transform)component).rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
		if (Mods.inSettings)
		{
			if (settingsbuttons[index].enabled.GetValueOrDefault())
			{
				Button.GetComponent<Renderer>().material.color = ButtonColorEnabled;
				((Graphic)text2).color = EnableTextColor;
				if (Mods.change13 == 10)
				{
					Object.Destroy((Object)(object)Button.GetComponent<Renderer>());
				}
			}
			else
			{
				Button.GetComponent<Renderer>().material.color = ButtonColorDisable;
				((Graphic)text2).color = DIsableTextColor;
				if (Mods.change12 == 10)
				{
					Object.Destroy((Object)(object)Button.GetComponent<Renderer>());
				}
			}
			return;
		}
		if (Mods.inCat1)
		{
			if (CatButtons1[index].enabled.GetValueOrDefault())
			{
				Button.GetComponent<Renderer>().material.color = ButtonColorEnabled;
				((Graphic)text2).color = EnableTextColor;
				if (Mods.change13 == 10)
				{
					Object.Destroy((Object)(object)Button.GetComponent<Renderer>());
				}
			}
			else
			{
				Button.GetComponent<Renderer>().material.color = ButtonColorDisable;
				((Graphic)text2).color = DIsableTextColor;
				if (Mods.change12 == 10)
				{
					Object.Destroy((Object)(object)Button.GetComponent<Renderer>());
				}
			}
		}
		if (Mods.inCat2)
		{
			if (CatButtons2[index].enabled.GetValueOrDefault())
			{
				Button.GetComponent<Renderer>().material.color = ButtonColorEnabled;
				((Graphic)text2).color = EnableTextColor;
				if (Mods.change13 == 10)
				{
					Object.Destroy((Object)(object)Button.GetComponent<Renderer>());
				}
			}
			else
			{
				Button.GetComponent<Renderer>().material.color = ButtonColorDisable;
				((Graphic)text2).color = DIsableTextColor;
				if (Mods.change12 == 10)
				{
					Object.Destroy((Object)(object)Button.GetComponent<Renderer>());
				}
			}
		}
		if (Mods.inCat9)
		{
			if (CatButtons9[index].enabled.GetValueOrDefault())
			{
				Button.GetComponent<Renderer>().material.color = ButtonColorEnabled;
				((Graphic)text2).color = EnableTextColor;
				if (Mods.change13 == 10)
				{
					Object.Destroy((Object)(object)Button.GetComponent<Renderer>());
				}
			}
			else
			{
				Button.GetComponent<Renderer>().material.color = ButtonColorDisable;
				((Graphic)text2).color = DIsableTextColor;
				if (Mods.change12 == 10)
				{
					Object.Destroy((Object)(object)Button.GetComponent<Renderer>());
				}
			}
		}
		if (Mods.inCat3)
		{
			if (CatButtons3[index].enabled.GetValueOrDefault())
			{
				Button.GetComponent<Renderer>().material.color = ButtonColorEnabled;
				((Graphic)text2).color = EnableTextColor;
				if (Mods.change13 == 10)
				{
					Object.Destroy((Object)(object)Button.GetComponent<Renderer>());
				}
			}
			else
			{
				Button.GetComponent<Renderer>().material.color = ButtonColorDisable;
				((Graphic)text2).color = DIsableTextColor;
				if (Mods.change12 == 10)
				{
					Object.Destroy((Object)(object)Button.GetComponent<Renderer>());
				}
			}
		}
		if (Mods.inCat4)
		{
			if (CatButtons4[index].enabled.GetValueOrDefault())
			{
				Button.GetComponent<Renderer>().material.color = ButtonColorEnabled;
				((Graphic)text2).color = EnableTextColor;
				if (Mods.change13 == 10)
				{
					Object.Destroy((Object)(object)Button.GetComponent<Renderer>());
				}
			}
			else
			{
				Button.GetComponent<Renderer>().material.color = ButtonColorDisable;
				((Graphic)text2).color = DIsableTextColor;
				if (Mods.change12 == 10)
				{
					Object.Destroy((Object)(object)Button.GetComponent<Renderer>());
				}
			}
		}
		if (Mods.inCat5)
		{
			if (CatButtons5[index].enabled.GetValueOrDefault())
			{
				Button.GetComponent<Renderer>().material.color = ButtonColorEnabled;
				((Graphic)text2).color = EnableTextColor;
				if (Mods.change13 == 10)
				{
					Object.Destroy((Object)(object)Button.GetComponent<Renderer>());
				}
			}
			else
			{
				Button.GetComponent<Renderer>().material.color = ButtonColorDisable;
				((Graphic)text2).color = DIsableTextColor;
				if (Mods.change12 == 10)
				{
					Object.Destroy((Object)(object)Button.GetComponent<Renderer>());
				}
			}
		}
		if (Mods.inCat6)
		{
			if (CatButtons6[index].enabled.GetValueOrDefault())
			{
				Button.GetComponent<Renderer>().material.color = ButtonColorEnabled;
				((Graphic)text2).color = EnableTextColor;
				if (Mods.change13 == 10)
				{
					Object.Destroy((Object)(object)Button.GetComponent<Renderer>());
				}
			}
			else
			{
				Button.GetComponent<Renderer>().material.color = ButtonColorDisable;
				((Graphic)text2).color = DIsableTextColor;
				if (Mods.change12 == 10)
				{
					Object.Destroy((Object)(object)Button.GetComponent<Renderer>());
				}
			}
		}
		if (Mods.inCat7)
		{
			if (CatButtons7[index].enabled.GetValueOrDefault())
			{
				Button.GetComponent<Renderer>().material.color = ButtonColorEnabled;
				((Graphic)text2).color = EnableTextColor;
				if (Mods.change13 == 10)
				{
					Object.Destroy((Object)(object)Button.GetComponent<Renderer>());
				}
			}
			else
			{
				Button.GetComponent<Renderer>().material.color = ButtonColorDisable;
				((Graphic)text2).color = DIsableTextColor;
				if (Mods.change12 == 10)
				{
					Object.Destroy((Object)(object)Button.GetComponent<Renderer>());
				}
			}
		}
		if (Mods.inCat8)
		{
			if (CatButtons8[index].enabled.GetValueOrDefault())
			{
				Button.GetComponent<Renderer>().material.color = ButtonColorEnabled;
				((Graphic)text2).color = EnableTextColor;
				if (Mods.change13 == 10)
				{
					Object.Destroy((Object)(object)Button.GetComponent<Renderer>());
				}
			}
			else
			{
				Button.GetComponent<Renderer>().material.color = ButtonColorDisable;
				((Graphic)text2).color = DIsableTextColor;
				if (Mods.change12 == 10)
				{
					Object.Destroy((Object)(object)Button.GetComponent<Renderer>());
				}
			}
		}
		if (Mods.inCat10)
		{
			if (CatButtons10[index].enabled.GetValueOrDefault())
			{
				Button.GetComponent<Renderer>().material.color = ButtonColorEnabled;
				((Graphic)text2).color = EnableTextColor;
				if (Mods.change13 == 10)
				{
					Object.Destroy((Object)(object)Button.GetComponent<Renderer>());
				}
			}
			else
			{
				Button.GetComponent<Renderer>().material.color = ButtonColorDisable;
				((Graphic)text2).color = DIsableTextColor;
				if (Mods.change12 == 10)
				{
					Object.Destroy((Object)(object)Button.GetComponent<Renderer>());
				}
			}
		}
		if (Mods.inCat11)
		{
			if (CatButtons11[index].enabled.GetValueOrDefault())
			{
				Button.GetComponent<Renderer>().material.color = ButtonColorEnabled;
				((Graphic)text2).color = EnableTextColor;
				if (Mods.change13 == 10)
				{
					Object.Destroy((Object)(object)Button.GetComponent<Renderer>());
				}
			}
			else
			{
				Button.GetComponent<Renderer>().material.color = ButtonColorDisable;
				((Graphic)text2).color = DIsableTextColor;
				if (Mods.change12 == 10)
				{
					Object.Destroy((Object)(object)Button.GetComponent<Renderer>());
				}
			}
		}
		if (Mods.inCat1 || Mods.inCat2 || Mods.inCat3 || Mods.inCat4 || Mods.inCat10 || Mods.inCat5 || Mods.inCat6 || Mods.inCat7 || Mods.inCat9 || Mods.inCat8 || Mods.inSettings)
		{
			return;
		}
		if (buttons[index].enabled.GetValueOrDefault())
		{
			Button.GetComponent<Renderer>().material.color = ButtonColorEnabled;
			((Graphic)text2).color = EnableTextColor;
			if (Mods.change13 == 10)
			{
				Object.Destroy((Object)(object)Button.GetComponent<Renderer>());
			}
		}
		else
		{
			Button.GetComponent<Renderer>().material.color = ButtonColorDisable;
			((Graphic)text2).color = DIsableTextColor;
			if (Mods.change12 == 10)
			{
				Object.Destroy((Object)(object)Button.GetComponent<Renderer>());
			}
		}
	}

	public void Start()
	{
		Draw();
	}

	public static void Toggle(string relatedText)
	{
		if (Mods.inSettings)
		{
			int num = (settingsbuttons.Count + pageSize - 1) / pageSize;
			switch (relatedText)
			{
			case "NextPage":
				if (pageNumber < num - 1)
				{
					pageNumber++;
				}
				else
				{
					pageNumber = 0;
				}
				DestroyMenu();
				instance.Draw();
				return;
			case "PreviousPage":
				if (pageNumber > 0)
				{
					pageNumber--;
				}
				else
				{
					pageNumber = num - 1;
				}
				DestroyMenu();
				instance.Draw();
				return;
			case "DisconnectingButton":
				PhotonNetwork.Disconnect();
				VRRig.LocalRig.PlayHandTapLocal(Mods.ButtonSound, false, 0.1f);
				return;
			}
			int index = -1;
			for (int i = 0; i < settingsbuttons.Count; i++)
			{
				if (relatedText == settingsbuttons[i].buttonText)
				{
					index = i;
					break;
				}
			}
			if (settingsbuttons[index].enabled.HasValue)
			{
				settingsbuttons[index].enabled = !settingsbuttons[index].enabled;
				lastPressedButtonIndex = index;
				if (lastPressedButtonIndex != -1 && lastPressedButtonIndex < settingsbuttons.Count)
				{
					tooltipString = GetButtonTooltip(lastPressedButtonIndex);
					tooltipText.text = tooltipString;
					lastPressedButtonIndex = -1;
				}
				DestroyMenu();
				instance.Draw();
			}
			return;
		}
		if (Mods.inCat1)
		{
			int num2 = (CatButtons1.Count + pageSize - 1) / pageSize;
			switch (relatedText)
			{
			case "NextPage":
				if (pageNumber < num2 - 1)
				{
					pageNumber++;
				}
				else
				{
					pageNumber = 0;
				}
				DestroyMenu();
				instance.Draw();
				break;
			case "PreviousPage":
				if (pageNumber > 0)
				{
					pageNumber--;
				}
				else
				{
					pageNumber = num2 - 1;
				}
				DestroyMenu();
				instance.Draw();
				break;
			case "DisconnectingButton":
				PhotonNetwork.Disconnect();
				VRRig.LocalRig.PlayHandTapLocal(Mods.ButtonSound, false, 0.1f);
				break;
			default:
			{
				int index2 = -1;
				for (int j = 0; j < CatButtons1.Count; j++)
				{
					if (relatedText == CatButtons1[j].buttonText)
					{
						index2 = j;
						break;
					}
				}
				if (CatButtons1[index2].enabled.HasValue)
				{
					CatButtons1[index2].enabled = !CatButtons1[index2].enabled;
					lastPressedButtonIndex = index2;
					if (lastPressedButtonIndex != -1 && lastPressedButtonIndex < CatButtons1.Count)
					{
						tooltipString = GetButtonTooltip(lastPressedButtonIndex);
						tooltipText.text = tooltipString;
						lastPressedButtonIndex = -1;
					}
					DestroyMenu();
					instance.Draw();
				}
				break;
			}
			}
		}
		if (Mods.inCat2)
		{
			int num3 = (CatButtons2.Count + pageSize - 1) / pageSize;
			switch (relatedText)
			{
			case "NextPage":
				if (pageNumber < num3 - 1)
				{
					pageNumber++;
				}
				else
				{
					pageNumber = 0;
				}
				DestroyMenu();
				instance.Draw();
				break;
			case "PreviousPage":
				if (pageNumber > 0)
				{
					pageNumber--;
				}
				else
				{
					pageNumber = num3 - 1;
				}
				DestroyMenu();
				instance.Draw();
				break;
			case "DisconnectingButton":
				PhotonNetwork.Disconnect();
				VRRig.LocalRig.PlayHandTapLocal(Mods.ButtonSound, false, 0.1f);
				break;
			default:
			{
				int index3 = -1;
				for (int k = 0; k < CatButtons2.Count; k++)
				{
					if (relatedText == CatButtons2[k].buttonText)
					{
						index3 = k;
						break;
					}
				}
				if (CatButtons2[index3].enabled.HasValue)
				{
					CatButtons2[index3].enabled = !CatButtons2[index3].enabled;
					lastPressedButtonIndex = index3;
					if (lastPressedButtonIndex != -1 && lastPressedButtonIndex < CatButtons2.Count)
					{
						tooltipString = GetButtonTooltip(lastPressedButtonIndex);
						tooltipText.text = tooltipString;
						lastPressedButtonIndex = -1;
					}
					DestroyMenu();
					instance.Draw();
				}
				break;
			}
			}
		}
		if (Mods.inCat9)
		{
			int num4 = (CatButtons9.Count + pageSize - 1) / pageSize;
			switch (relatedText)
			{
			case "NextPage":
				if (pageNumber < num4 - 1)
				{
					pageNumber++;
				}
				else
				{
					pageNumber = 0;
				}
				DestroyMenu();
				instance.Draw();
				break;
			case "PreviousPage":
				if (pageNumber > 0)
				{
					pageNumber--;
				}
				else
				{
					pageNumber = num4 - 1;
				}
				DestroyMenu();
				instance.Draw();
				break;
			case "DisconnectingButton":
				PhotonNetwork.Disconnect();
				VRRig.LocalRig.PlayHandTapLocal(Mods.ButtonSound, false, 0.1f);
				break;
			default:
			{
				int index4 = -1;
				for (int l = 0; l < CatButtons9.Count; l++)
				{
					if (relatedText == CatButtons9[l].buttonText)
					{
						index4 = l;
						break;
					}
				}
				if (CatButtons9[index4].enabled.HasValue)
				{
					CatButtons9[index4].enabled = !CatButtons9[index4].enabled;
					lastPressedButtonIndex = index4;
					if (lastPressedButtonIndex != -1 && lastPressedButtonIndex < CatButtons9.Count)
					{
						tooltipString = GetButtonTooltip(lastPressedButtonIndex);
						tooltipText.text = tooltipString;
						lastPressedButtonIndex = -1;
					}
					DestroyMenu();
					instance.Draw();
				}
				break;
			}
			}
		}
		if (Mods.inCat3)
		{
			int num5 = (CatButtons3.Count + pageSize - 1) / pageSize;
			switch (relatedText)
			{
			case "NextPage":
				if (pageNumber < num5 - 1)
				{
					pageNumber++;
				}
				else
				{
					pageNumber = 0;
				}
				DestroyMenu();
				instance.Draw();
				break;
			case "PreviousPage":
				if (pageNumber > 0)
				{
					pageNumber--;
				}
				else
				{
					pageNumber = num5 - 1;
				}
				DestroyMenu();
				instance.Draw();
				break;
			case "DisconnectingButton":
				PhotonNetwork.Disconnect();
				VRRig.LocalRig.PlayHandTapLocal(Mods.ButtonSound, false, 0.1f);
				break;
			default:
			{
				int index5 = -1;
				for (int m = 0; m < CatButtons3.Count; m++)
				{
					if (relatedText == CatButtons3[m].buttonText)
					{
						index5 = m;
						break;
					}
				}
				if (CatButtons3[index5].enabled.HasValue)
				{
					CatButtons3[index5].enabled = !CatButtons3[index5].enabled;
					lastPressedButtonIndex = index5;
					if (lastPressedButtonIndex != -1 && lastPressedButtonIndex < CatButtons3.Count)
					{
						tooltipString = GetButtonTooltip(lastPressedButtonIndex);
						tooltipText.text = tooltipString;
						lastPressedButtonIndex = -1;
					}
					DestroyMenu();
					instance.Draw();
				}
				break;
			}
			}
		}
		if (Mods.inCat4)
		{
			int num6 = (CatButtons4.Count + pageSize - 1) / pageSize;
			switch (relatedText)
			{
			case "NextPage":
				if (pageNumber < num6 - 1)
				{
					pageNumber++;
				}
				else
				{
					pageNumber = 0;
				}
				DestroyMenu();
				instance.Draw();
				break;
			case "PreviousPage":
				if (pageNumber > 0)
				{
					pageNumber--;
				}
				else
				{
					pageNumber = num6 - 1;
				}
				DestroyMenu();
				instance.Draw();
				break;
			case "DisconnectingButton":
				PhotonNetwork.Disconnect();
				VRRig.LocalRig.PlayHandTapLocal(Mods.ButtonSound, false, 0.1f);
				break;
			default:
			{
				int index6 = -1;
				for (int n = 0; n < CatButtons4.Count; n++)
				{
					if (relatedText == CatButtons4[n].buttonText)
					{
						index6 = n;
						break;
					}
				}
				if (CatButtons4[index6].enabled.HasValue)
				{
					CatButtons4[index6].enabled = !CatButtons4[index6].enabled;
					lastPressedButtonIndex = index6;
					if (lastPressedButtonIndex != -1 && lastPressedButtonIndex < CatButtons4.Count)
					{
						tooltipString = GetButtonTooltip(lastPressedButtonIndex);
						tooltipText.text = tooltipString;
						lastPressedButtonIndex = -1;
					}
					DestroyMenu();
					instance.Draw();
				}
				break;
			}
			}
		}
		if (Mods.inCat6)
		{
			int num7 = (CatButtons6.Count + pageSize - 1) / pageSize;
			switch (relatedText)
			{
			case "NextPage":
				if (pageNumber < num7 - 1)
				{
					pageNumber++;
				}
				else
				{
					pageNumber = 0;
				}
				DestroyMenu();
				instance.Draw();
				break;
			case "PreviousPage":
				if (pageNumber > 0)
				{
					pageNumber--;
				}
				else
				{
					pageNumber = num7 - 1;
				}
				DestroyMenu();
				instance.Draw();
				break;
			case "DisconnectingButton":
				PhotonNetwork.Disconnect();
				VRRig.LocalRig.PlayHandTapLocal(Mods.ButtonSound, false, 0.1f);
				break;
			default:
			{
				int index7 = -1;
				for (int num8 = 0; num8 < CatButtons6.Count; num8++)
				{
					if (relatedText == CatButtons6[num8].buttonText)
					{
						index7 = num8;
						break;
					}
				}
				if (CatButtons6[index7].enabled.HasValue)
				{
					CatButtons6[index7].enabled = !CatButtons6[index7].enabled;
					lastPressedButtonIndex = index7;
					if (lastPressedButtonIndex != -1 && lastPressedButtonIndex < CatButtons6.Count)
					{
						tooltipString = GetButtonTooltip(lastPressedButtonIndex);
						tooltipText.text = tooltipString;
						lastPressedButtonIndex = -1;
					}
					DestroyMenu();
					instance.Draw();
				}
				break;
			}
			}
		}
		if (Mods.inCat7)
		{
			int num9 = (CatButtons7.Count + pageSize - 1) / pageSize;
			switch (relatedText)
			{
			case "NextPage":
				if (pageNumber < num9 - 1)
				{
					pageNumber++;
				}
				else
				{
					pageNumber = 0;
				}
				DestroyMenu();
				instance.Draw();
				break;
			case "PreviousPage":
				if (pageNumber > 0)
				{
					pageNumber--;
				}
				else
				{
					pageNumber = num9 - 1;
				}
				DestroyMenu();
				instance.Draw();
				break;
			case "DisconnectingButton":
				PhotonNetwork.Disconnect();
				VRRig.LocalRig.PlayHandTapLocal(Mods.ButtonSound, false, 0.1f);
				break;
			default:
			{
				int index8 = -1;
				for (int num10 = 0; num10 < CatButtons7.Count; num10++)
				{
					if (relatedText == CatButtons7[num10].buttonText)
					{
						index8 = num10;
						break;
					}
				}
				if (CatButtons7[index8].enabled.HasValue)
				{
					CatButtons7[index8].enabled = !CatButtons7[index8].enabled;
					lastPressedButtonIndex = index8;
					if (lastPressedButtonIndex != -1 && lastPressedButtonIndex < CatButtons7.Count)
					{
						tooltipString = GetButtonTooltip(lastPressedButtonIndex);
						tooltipText.text = tooltipString;
						lastPressedButtonIndex = -1;
					}
					DestroyMenu();
					instance.Draw();
				}
				break;
			}
			}
		}
		if (Mods.inCat8)
		{
			int num11 = (CatButtons8.Count + pageSize - 1) / pageSize;
			switch (relatedText)
			{
			case "NextPage":
				if (pageNumber < num11 - 1)
				{
					pageNumber++;
				}
				else
				{
					pageNumber = 0;
				}
				DestroyMenu();
				instance.Draw();
				break;
			case "PreviousPage":
				if (pageNumber > 0)
				{
					pageNumber--;
				}
				else
				{
					pageNumber = num11 - 1;
				}
				DestroyMenu();
				instance.Draw();
				break;
			case "DisconnectingButton":
				PhotonNetwork.Disconnect();
				VRRig.LocalRig.PlayHandTapLocal(Mods.ButtonSound, false, 0.1f);
				break;
			default:
			{
				int index9 = -1;
				for (int num12 = 0; num12 < CatButtons8.Count; num12++)
				{
					if (relatedText == CatButtons8[num12].buttonText)
					{
						index9 = num12;
						break;
					}
				}
				if (CatButtons8[index9].enabled.HasValue)
				{
					CatButtons8[index9].enabled = !CatButtons8[index9].enabled;
					lastPressedButtonIndex = index9;
					if (lastPressedButtonIndex != -1 && lastPressedButtonIndex < CatButtons8.Count)
					{
						tooltipString = GetButtonTooltip(lastPressedButtonIndex);
						tooltipText.text = tooltipString;
						lastPressedButtonIndex = -1;
					}
					DestroyMenu();
					instance.Draw();
				}
				break;
			}
			}
		}
		if (Mods.inCat5)
		{
			int num13 = (CatButtons5.Count + pageSize - 1) / pageSize;
			switch (relatedText)
			{
			case "NextPage":
				if (pageNumber < num13 - 1)
				{
					pageNumber++;
				}
				else
				{
					pageNumber = 0;
				}
				DestroyMenu();
				instance.Draw();
				break;
			case "PreviousPage":
				if (pageNumber > 0)
				{
					pageNumber--;
				}
				else
				{
					pageNumber = num13 - 1;
				}
				DestroyMenu();
				instance.Draw();
				break;
			case "DisconnectingButton":
				PhotonNetwork.Disconnect();
				VRRig.LocalRig.PlayHandTapLocal(Mods.ButtonSound, false, 0.1f);
				break;
			default:
			{
				int index10 = -1;
				for (int num14 = 0; num14 < CatButtons5.Count; num14++)
				{
					if (relatedText == CatButtons5[num14].buttonText)
					{
						index10 = num14;
						break;
					}
				}
				if (CatButtons5[index10].enabled.HasValue)
				{
					CatButtons5[index10].enabled = !CatButtons5[index10].enabled;
					lastPressedButtonIndex = index10;
					if (lastPressedButtonIndex != -1 && lastPressedButtonIndex < CatButtons5.Count)
					{
						tooltipString = GetButtonTooltip(lastPressedButtonIndex);
						tooltipText.text = tooltipString;
						lastPressedButtonIndex = -1;
					}
					DestroyMenu();
					instance.Draw();
				}
				break;
			}
			}
		}
		if (Mods.inCat10)
		{
			int num15 = (CatButtons10.Count + pageSize - 1) / pageSize;
			switch (relatedText)
			{
			case "NextPage":
				if (pageNumber < num15 - 1)
				{
					pageNumber++;
				}
				else
				{
					pageNumber = 0;
				}
				DestroyMenu();
				instance.Draw();
				break;
			case "PreviousPage":
				if (pageNumber > 0)
				{
					pageNumber--;
				}
				else
				{
					pageNumber = num15 - 1;
				}
				DestroyMenu();
				instance.Draw();
				break;
			case "DisconnectingButton":
				PhotonNetwork.Disconnect();
				VRRig.LocalRig.PlayHandTapLocal(Mods.ButtonSound, false, 0.1f);
				break;
			default:
			{
				int index11 = -1;
				for (int num16 = 0; num16 < CatButtons10.Count; num16++)
				{
					if (relatedText == CatButtons10[num16].buttonText)
					{
						index11 = num16;
						break;
					}
				}
				if (CatButtons10[index11].enabled.HasValue)
				{
					CatButtons10[index11].enabled = !CatButtons10[index11].enabled;
					lastPressedButtonIndex = index11;
					if (lastPressedButtonIndex != -1 && lastPressedButtonIndex < CatButtons10.Count)
					{
						tooltipString = GetButtonTooltip(lastPressedButtonIndex);
						tooltipText.text = tooltipString;
						lastPressedButtonIndex = -1;
					}
					DestroyMenu();
					instance.Draw();
				}
				break;
			}
			}
		}
		if (Mods.inCat11)
		{
			int num17 = (CatButtons11.Count + pageSize - 1) / pageSize;
			switch (relatedText)
			{
			case "NextPage":
				if (pageNumber < num17 - 1)
				{
					pageNumber++;
				}
				else
				{
					pageNumber = 0;
				}
				DestroyMenu();
				instance.Draw();
				break;
			case "PreviousPage":
				if (pageNumber > 0)
				{
					pageNumber--;
				}
				else
				{
					pageNumber = num17 - 1;
				}
				DestroyMenu();
				instance.Draw();
				break;
			case "DisconnectingButton":
				PhotonNetwork.Disconnect();
				VRRig.LocalRig.PlayHandTapLocal(Mods.ButtonSound, false, 0.1f);
				break;
			default:
			{
				int index12 = -1;
				for (int num18 = 0; num18 < CatButtons11.Count; num18++)
				{
					if (relatedText == CatButtons11[num18].buttonText)
					{
						index12 = num18;
						break;
					}
				}
				if (CatButtons11[index12].enabled.HasValue)
				{
					CatButtons11[index12].enabled = !CatButtons11[index12].enabled;
					lastPressedButtonIndex = index12;
					if (lastPressedButtonIndex != -1 && lastPressedButtonIndex < CatButtons11.Count)
					{
						tooltipString = GetButtonTooltip(lastPressedButtonIndex);
						tooltipText.text = tooltipString;
						lastPressedButtonIndex = -1;
					}
					DestroyMenu();
					instance.Draw();
				}
				break;
			}
			}
		}
		if (Mods.inCat1 || Mods.inCat2 || Mods.inCat3 || Mods.inCat4 || Mods.inCat10 || Mods.inCat5 || Mods.inCat6 || Mods.inCat7 || Mods.inCat9 || Mods.inCat8 || Mods.inSettings)
		{
			return;
		}
		int num19 = (buttons.Count + pageSize - 1) / pageSize;
		switch (relatedText)
		{
		case "NextPage":
			if (pageNumber < num19 - 1)
			{
				pageNumber++;
			}
			else
			{
				pageNumber = 0;
			}
			DestroyMenu();
			instance.Draw();
			return;
		case "PreviousPage":
			if (pageNumber > 0)
			{
				pageNumber--;
			}
			else
			{
				pageNumber = num19 - 1;
			}
			DestroyMenu();
			instance.Draw();
			return;
		case "DisconnectingButton":
			PhotonNetwork.Disconnect();
			VRRig.LocalRig.PlayHandTapLocal(Mods.ButtonSound, false, 0.1f);
			return;
		}
		int index13 = -1;
		for (int num20 = 0; num20 < buttons.Count; num20++)
		{
			if (relatedText == buttons[num20].buttonText)
			{
				index13 = num20;
				break;
			}
		}
		if (buttons[index13].enabled.HasValue)
		{
			buttons[index13].enabled = !buttons[index13].enabled;
			lastPressedButtonIndex = index13;
			if (lastPressedButtonIndex != -1 && lastPressedButtonIndex < buttons.Count)
			{
				tooltipString = GetButtonTooltip(lastPressedButtonIndex);
				tooltipText.text = tooltipString;
				lastPressedButtonIndex = -1;
			}
			DestroyMenu();
			instance.Draw();
		}
	}
}
