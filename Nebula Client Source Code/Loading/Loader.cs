using System;
using GTAG_NotificationLib;
using Malachis_Temp;
using UnityEngine;

namespace Loading;

public class Loader : MonoBehaviour
{
	private static GameObject gameobject;

	public static bool loaded;

	public static void Load()
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Expected O, but got Unknown
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		if (loaded)
		{
			return;
		}
		try
		{
			loaded = true;
			gameobject = new GameObject("NebulaLoader");
			gameobject.AddComponent<Plugin>();
			Object.DontDestroyOnLoad((Object)(object)gameobject);
			if ((Object)(object)CoroutineRunner.Instance == (Object)null)
			{
				new GameObject("CoroutineRunner").AddComponent<CoroutineRunner>();
			}
			NotifiLib.SendNotification("Nebula Client V1.2.1 Loaded!");
		}
		catch (Exception ex)
		{
			loaded = false;
			Debug.LogError((object)("Nebula Client V1.2.1 Failed To Load.\nRe-install the menu. If this error persists, restart your PC, or re-download Gorilla Tag. If the menu STILL doesn't load, contact me in the Discord!\n\nError: " + ex));
		}
	}
}
