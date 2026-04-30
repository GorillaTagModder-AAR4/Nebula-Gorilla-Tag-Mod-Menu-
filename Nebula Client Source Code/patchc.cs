using HarmonyLib;

[HarmonyPatch(typeof(MonkeAgent), "CheckReports")]
public class patchc
{
	private static bool Prefix()
	{
		return false;
	}
}
