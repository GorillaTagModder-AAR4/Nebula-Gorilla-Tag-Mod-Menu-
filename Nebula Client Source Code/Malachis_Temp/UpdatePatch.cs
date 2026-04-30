using GTAG_NotificationLib;
using GorillaLocomotion;
using HarmonyLib;
using MalachiTemp.Backend;
using MalachiTemp.UI;
using UnityEngine;
using dark.efijiPOIWikjek;

namespace Malachis_Temp;

[HarmonyPatch(typeof(GTPlayer), "FixedUpdate")]
internal class UpdatePatch
{
	private static bool alreadyInit;

	public static GameObject Gameobject;

	private static void Postfix()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Expected O, but got Unknown
		if (!alreadyInit)
		{
			alreadyInit = true;
			Gameobject = new GameObject();
			Gameobject.AddComponent<Plugin>();
			Gameobject.AddComponent<WristMenu>();
			Gameobject.AddComponent<RigShit>();
			Gameobject.AddComponent<Mods>();
			Gameobject.AddComponent<GhostPatch>();
			Gameobject.AddComponent<NotifiLib>();
			try
			{
				Mods.Load();
			}
			catch
			{
			}
			Object.DontDestroyOnLoad((Object)(object)Gameobject);
		}
	}
}
