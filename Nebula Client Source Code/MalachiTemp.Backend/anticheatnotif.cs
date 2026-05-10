using GTAG_NotificationLib;
using HarmonyLib;
using Photon.Pun;
using UnityEngine;

namespace MalachiTemp.Backend;

[HarmonyPatch(typeof(MonkeAgent), "SendReport")]
internal class anticheatnotif : MonoBehaviour
{
	private static bool Prefix(string susReason, string susId, string susNick)
	{
		if (susReason != "empty rig" && susId == PhotonNetwork.LocalPlayer.UserId)
		{
			PhotonNetwork.Disconnect();
			NotifiLib.SendNotification("[<color=red>ANTICHEAT</color>] You have been disconnected from the lobby because of an Anti Cheat report. The report has been blocked by the disconnect. You were reported for " + susReason);
			GTAGTest.SetLog(susReason, susId, susNick);
		}
		return false;
	}
}
