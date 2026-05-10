using HarmonyLib;
using UnityEngine;

namespace MalachiTemp.Backend;

[HarmonyPatch(typeof(VRRig), "PostTick")]
public static class Bullshit2
{
	public static bool Prefix(VRRig __instance)
	{
		return !__instance.isLocal || ((Behaviour)__instance).enabled;
	}
}
