using HarmonyLib;
using UnityEngine;

namespace MalachiTemp.Backend;

[HarmonyPatch(typeof(VRRig), "OnDisable")]
internal class GhostPatch : MonoBehaviour
{
	public static bool Prefix(VRRig __instance)
	{
		if ((Object)(object)__instance == (Object)(object)VRRig.LocalRig)
		{
			return false;
		}
		return true;
	}
}
