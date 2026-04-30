using HarmonyLib;
using UnityEngine;

namespace MalachiTemp.Backend;

internal class Patches
{
	public class GameplayPatches
	{
		[HarmonyPatch(typeof(SlingshotProjectile), "CheckForAOEKnockback")]
		public class CheckForAOEKnockbackPatch
		{
			public static bool Fling = true;

			private static bool Prefix(Vector3 impactPosition, float impactSpeed)
			{
				return Fling;
			}
		}

		[HarmonyPatch(/*Could not decode attribute arguments.*/)]
		public class AntiTeleportTechnologyPatch
		{
			private static bool Prefix()
			{
				return false;
			}
		}

		[HarmonyPatch(/*Could not decode attribute arguments.*/)]
		public class CreatePrimitivePatch
		{
			public static void Postfix(GameObject __result)
			{
				if ((Object)(object)__result.GetComponent<Renderer>() != (Object)null)
				{
					__result.GetComponent<Renderer>().material.shader = Shader.Find("GorillaTag/UberShader");
				}
			}
		}

		[HarmonyPatch(typeof(VRRig), "OnDisable")]
		public class OnDisablePatch
		{
			public static bool Prefix(VRRig __instance)
			{
				return !__instance.isLocal;
			}
		}

		[HarmonyPatch(typeof(VRRig), "Awake")]
		public class AwakePatch
		{
			public static bool Prefix(VRRig __instance)
			{
				return ((Object)((Component)__instance).gameObject).name != "Local Gorilla Player(Clone)";
			}
		}

		[HarmonyPatch(typeof(VRRig), "PostTick")]
		public class PostTickPatch
		{
			public static bool Prefix(VRRig __instance)
			{
				return !__instance.isLocal || ((Behaviour)__instance).enabled;
			}
		}
	}
}
