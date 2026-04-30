using HarmonyLib;
using UnityEngine;

namespace MalachiTemp.Backend;

[HarmonyPatch(typeof(VRRig), "CheckDistance")]
public class MoreBullshit
{
	public static bool enabled;

	public static void Postfix(ref bool __result, Vector3 position, float max)
	{
		if (enabled)
		{
			__result = true;
		}
	}
}
