using HarmonyLib;

[HarmonyPatch(typeof(MonkeAgent), "GetRPCCallTracker")]
internal class patchb
{
	private static bool Prefix()
	{
		return false;
	}
}
