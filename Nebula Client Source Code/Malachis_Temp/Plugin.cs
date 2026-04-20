using System;
using BepInEx;
using HarmonyLib;
using Loading;

namespace Malachis_Temp;

[BepInPlugin("malachistemp", "malachis.temp", "1.0.1")]
public class Plugin : BaseUnityPlugin
{
	[Serializable]
	public class LoginData
	{
		public string license;
	}

	public const string Name = "malachistemp";

	public const string GUID = "malachis.temp";

	public const string Version = "1.0.1";

	private bool patchedHarmony = false;

	private void Awake()
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Expected O, but got Unknown
		if (!patchedHarmony && !Loader.loaded)
		{
			Harmony val = new Harmony("malachis.temp");
			val.PatchAll();
			patchedHarmony = true;
			Loader.loaded = true;
		}
	}
}
