using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using MalachiTemp.Backend;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace dark.efijiPOIWikjek;

internal class RigShit : MonoBehaviour
{
	public static int r;

	public static VRRig GetVRRigFromPlayer(Player p)
	{
		return GorillaGameManager.instance.FindPlayerVRRig(NetPlayer.op_Implicit(p));
	}

	public static List<VRRig> GetAllRigs(bool i = true)
	{
		return i ? VRRigCache.ActiveRigs.ToList() : GetOtherRigs();
	}

	public static List<VRRig> GetOtherRigs()
	{
		List<VRRig> list = new List<VRRig>();
		foreach (VRRig allRig in GetAllRigs())
		{
			if (!allRig.isOfflineVRRig && !list.Contains(allRig))
			{
				list.Add(allRig);
			}
		}
		return list;
	}

	public static Player GetPlayerFromVRRig(VRRig p)
	{
		return p.Creator.GetPlayerRef();
	}

	public static Player GetPlayerFromVRRigSafe(VRRig p)
	{
		try
		{
			return p.Creator.GetPlayerRef();
		}
		catch
		{
			return null;
		}
	}

	public static NetworkView GetNetworkFromRig(VRRig r)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Expected O, but got Unknown
		return (NetworkView)Traverse.Create((object)r).Field("netView").GetValue();
	}

	public static NetPlayer GetNetFromRig(VRRig r)
	{
		return GetNetworkFromRig(r).Owner;
	}

	public static List<NetPlayer> GetAllNets(bool i = true)
	{
		return i ? NetworkSystem.Instance.AllNetPlayers.ToList() : NetworkSystem.Instance.PlayerListOthers.ToList();
	}

	public static List<Player> GetAllPlayers(bool i = true)
	{
		return i ? PhotonNetwork.PlayerList.ToList() : PhotonNetwork.PlayerListOthers.ToList();
	}

	public static VRRig GetRigFromNet(NetPlayer p)
	{
		return GetVRRigFromPlayer(p.GetPlayerRef());
	}

	public static VRRig GetRandomRig(bool s, float d = 0.1f)
	{
		VRRig p = null;
		new Mods.Delay().D(d, delegate
		{
			r++;
			List<VRRig> allRigs = GetAllRigs(s);
			if (r > allRigs.Count)
			{
				r = 0;
			}
			p = allRigs[r];
		});
		return p;
	}
}
