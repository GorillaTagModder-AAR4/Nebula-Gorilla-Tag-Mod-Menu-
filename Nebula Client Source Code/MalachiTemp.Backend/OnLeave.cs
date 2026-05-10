using GTAG_NotificationLib;
using HarmonyLib;
using Photon.Pun;
using Photon.Realtime;

namespace MalachiTemp.Backend;

[HarmonyPatch(typeof(MonoBehaviourPunCallbacks), "OnPlayerLeftRoom")]
internal class OnLeave : HarmonyPatch
{
	private static void Prefix(Player otherPlayer)
	{
		if (otherPlayer != PhotonNetwork.LocalPlayer)
		{
			NotifiLib.SendNotification("[<color=blue>ROOM</color>] Player: " + otherPlayer.NickName + " Left Lobby");
		}
	}
}
