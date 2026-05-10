using BepInEx;

namespace MalachiTemp.Backend;

public class GTAGTest : BaseUnityPlugin
{
	public static GTAGTest Instance;

	public void Awake()
	{
		Instance = this;
	}

	public void Log(string susReason, string susId, string susNick)
	{
		((BaseUnityPlugin)this).Logger.LogInfo((object)("[ANTICHEAT] You have been disconnected from the lobby because of an Anti Cheat report. The report has been blocked by the disconnect. You were reported for " + susReason));
	}

	public static void SetLog(string susReason, string susId, string susNick)
	{
		Instance?.Log(susReason, susId, susNick);
	}
}
