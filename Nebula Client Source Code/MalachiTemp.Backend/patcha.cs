using HarmonyLib;

namespace MalachiTemp.Backend;

[HarmonyPatch(typeof(MonkeAgent), "DispatchReport")]
public class patcha
{
	private static bool Prefix()
	{
		return false;
	}
}
