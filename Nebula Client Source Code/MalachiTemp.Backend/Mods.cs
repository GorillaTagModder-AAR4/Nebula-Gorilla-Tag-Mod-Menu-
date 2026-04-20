using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using ExitGames.Client.Photon;
using GTAG_NotificationLib;
using GorillaLocomotion;
using GorillaNetworking;
using GorillaTagScripts;
using HarmonyLib;
using MalachiTemp.UI;
using MalachiTemp.Utilities;
using Photon.Pun;
using Photon.Realtime;
using PlayFab;
using PlayFab.Internal;
using UnityEngine;
using UnityEngine.Events;
using dark.efijiPOIWikjek;

namespace MalachiTemp.Backend;

internal class Mods : MonoBehaviour
{
	public static class RainbowFurGun
	{
		private static float colorTimer;

		public static void RainbowFurGunU()
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			MakeGun(Color.white, new Vector3(0.15f, 0.15f, 0.15f), 0.025f, (PrimitiveType)0, GTPlayer.Instance.RightHand.controllerTransform, liner: true, delegate
			{
				if (Time.time > colorTimer)
				{
					colorTimer = Time.time + 0.1f;
					Player playerFromGun = RigShit.GetPlayerFromGun();
					if (playerFromGun != null)
					{
						VRRig vRRigFromPlayer = RigShit.GetVRRigFromPlayer(playerFromGun);
						if (!((Object)(object)vRRigFromPlayer == (Object)null))
						{
							PhotonView component = ((Component)vRRigFromPlayer).GetComponent<PhotonView>();
							if (!((Object)(object)component == (Object)null))
							{
								float num = Mathf.Abs(Mathf.Sin(Time.time * 2f));
								float num2 = Mathf.Abs(Mathf.Sin(Time.time * 2f + 2f));
								float num3 = Mathf.Abs(Mathf.Sin(Time.time * 2f + 4f));
								component.RPC("RPC_InitializeNoobMaterial", (RpcTarget)0, new object[3] { num, num2, num3 });
							}
						}
					}
				}
			}, delegate
			{
			});
		}
	}

	public static class BlackFurGun
	{
		private static float delay;

		public static void BlackFurGunU()
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			MakeGun(Color.black, new Vector3(0.15f, 0.15f, 0.15f), 0.025f, (PrimitiveType)0, GTPlayer.Instance.RightHand.controllerTransform, liner: true, delegate
			{
				if (Time.time > delay)
				{
					delay = Time.time + 0.2f;
					Player playerFromGun = GetPlayerFromGun();
					if (playerFromGun != null)
					{
						VRRig vRRigFromPlayer = RigShit.GetVRRigFromPlayer(playerFromGun);
						if (!((Object)(object)vRRigFromPlayer == (Object)null))
						{
							PhotonView component = ((Component)vRRigFromPlayer).GetComponent<PhotonView>();
							if (!((Object)(object)component == (Object)null))
							{
								component.RPC("RPC_InitializeNoobMaterial", (RpcTarget)0, new object[3] { 0f, 0f, 0f });
							}
						}
					}
				}
			}, delegate
			{
			});
		}
	}

	public static class WhiteFurGun
	{
		private static float delay;

		public static void WhiteFurGunU()
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			MakeGun(Color.white, new Vector3(0.15f, 0.15f, 0.15f), 0.025f, (PrimitiveType)0, GTPlayer.Instance.RightHand.controllerTransform, liner: true, delegate
			{
				if (Time.time > delay)
				{
					delay = Time.time + 0.2f;
					Player playerFromGun = RigShit.GetPlayerFromGun();
					if (playerFromGun != null)
					{
						VRRig vRRigFromPlayer = RigShit.GetVRRigFromPlayer(playerFromGun);
						if (!((Object)(object)vRRigFromPlayer == (Object)null))
						{
							PhotonView component = ((Component)vRRigFromPlayer).GetComponent<PhotonView>();
							if (!((Object)(object)component == (Object)null))
							{
								component.RPC("RPC_InitializeNoobMaterial", (RpcTarget)0, new object[3] { 1f, 1f, 1f });
							}
						}
					}
				}
			}, delegate
			{
			});
		}
	}

	public static class GreyZoneMods
	{
		public static bool greyEnabled;

		public static bool noGravEnabled;

		private const int NORMAL_GREY = 1;

		public static void SetGrey(bool on, int gravityFactor)
		{
			if (!PhotonNetwork.IsMasterClient)
			{
				return;
			}
			GreyZoneManager instance = GreyZoneManager.Instance;
			if ((Object)(object)instance == (Object)null)
			{
				return;
			}
			greyEnabled = on;
			RPCFlush();
			FieldInfo field = typeof(GreyZoneManager).GetField("gravityFactorOptionSelection", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			if (field != null)
			{
				field.SetValue(instance, gravityFactor);
			}
			if (on)
			{
				if (!instance.GreyZoneActive)
				{
					instance.ActivateGreyZoneAuthority();
				}
				RPCFlush();
			}
			else
			{
				if (instance.GreyZoneActive)
				{
					instance.DeactivateGreyZoneAuthority();
				}
				RPCFlush();
			}
		}

		public static void GreyAll()
		{
			if (greyEnabled)
			{
				SetGrey(on: false, 1);
			}
			else
			{
				SetGrey(on: true, 1);
			}
		}

		public static void NoGravAll()
		{
			noGravEnabled = !noGravEnabled;
			GreyZoneManager instance = GreyZoneManager.Instance;
			if ((Object)(object)instance == (Object)null)
			{
				return;
			}
			if (noGravEnabled)
			{
				FieldInfo field = typeof(GreyZoneManager).GetField("gravityFactorOptionSelection", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				if (field != null)
				{
					field.SetValue(instance, int.MaxValue);
				}
				if (!instance.GreyZoneActive)
				{
					instance.ActivateGreyZoneAuthority();
				}
			}
			else
			{
				SetGrey(on: false, 1);
			}
		}
	}

	public class Delay : MonoBehaviour
	{
		private static Dictionary<int, float> d = new Dictionary<int, float>();

		public void D(float t, Action s)
		{
			int metadataToken = s.Method.MetadataToken;
			if (!d.ContainsKey(metadataToken) || Time.time > d[metadataToken])
			{
				s();
				d[metadataToken] = Time.time + t;
			}
		}
	}

	public static class CritterGuns
	{
		private static float nextUseTime;

		private const float cooldown = 3f;

		private static bool CanFire()
		{
			if (Time.time < nextUseTime)
			{
				return false;
			}
			nextUseTime = Time.time + 3f;
			return true;
		}

		public static void CritterStunBombGun()
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			MakeGun(CurrentGunColor, new Vector3(0.15f, 0.15f, 0.15f), 0.025f, (PrimitiveType)0, GTPlayer.Instance.RightHand.controllerTransform, liner: true, delegate
			{
				//IL_003c: Unknown result type (might be due to invalid IL or missing references)
				//IL_0041: Unknown result type (might be due to invalid IL or missing references)
				//IL_0060: Unknown result type (might be due to invalid IL or missing references)
				//IL_006f: Unknown result type (might be due to invalid IL or missing references)
				//IL_0070: Unknown result type (might be due to invalid IL or missing references)
				if (CanFire())
				{
					CrittersManager crittersManager = GetCrittersManager();
					if (!((Object)(object)crittersManager == (Object)null) && crittersManager.LocalAuthority())
					{
						Vector3 position = pointer.transform.position;
						CrittersActor val = crittersManager.SpawnActor((CrittersActorType)13, -1);
						if ((Object)(object)val != (Object)null)
						{
							((Component)val).transform.position = position;
							crittersManager.TriggerEvent((CritterEvent)0, val.actorId, position, Quaternion.identity);
						}
					}
				}
			}, delegate
			{
			});
		}

		public static void CritterStickyTrapGun()
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			MakeGun(CurrentGunColor, new Vector3(0.15f, 0.15f, 0.15f), 0.025f, (PrimitiveType)0, GTPlayer.Instance.RightHand.controllerTransform, liner: true, delegate
			{
				//IL_0057: Unknown result type (might be due to invalid IL or missing references)
				if (CanFire())
				{
					CrittersManager crittersManager = GetCrittersManager();
					if (!((Object)(object)crittersManager == (Object)null) && crittersManager.LocalAuthority())
					{
						CrittersActor val = crittersManager.SpawnActor((CrittersActorType)17, -1);
						if ((Object)(object)val != (Object)null)
						{
							((Component)val).transform.position = pointer.transform.position;
						}
					}
				}
			}, delegate
			{
			});
		}

		public static void CritterNoiseMakerGun()
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			MakeGun(CurrentGunColor, new Vector3(0.15f, 0.15f, 0.15f), 0.025f, (PrimitiveType)0, GTPlayer.Instance.RightHand.controllerTransform, liner: true, delegate
			{
				//IL_003c: Unknown result type (might be due to invalid IL or missing references)
				//IL_0041: Unknown result type (might be due to invalid IL or missing references)
				//IL_0060: Unknown result type (might be due to invalid IL or missing references)
				//IL_006f: Unknown result type (might be due to invalid IL or missing references)
				//IL_0070: Unknown result type (might be due to invalid IL or missing references)
				if (CanFire())
				{
					CrittersManager crittersManager = GetCrittersManager();
					if (!((Object)(object)crittersManager == (Object)null) && crittersManager.LocalAuthority())
					{
						Vector3 position = pointer.transform.position;
						CrittersActor val = crittersManager.SpawnActor((CrittersActorType)16, -1);
						if ((Object)(object)val != (Object)null)
						{
							((Component)val).transform.position = position;
							crittersManager.TriggerEvent((CritterEvent)1, val.actorId, position, Quaternion.identity);
						}
					}
				}
			}, delegate
			{
			});
		}

		public static void CritterCageGun()
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			MakeGun(CurrentGunColor, new Vector3(0.15f, 0.15f, 0.15f), 0.025f, (PrimitiveType)0, GTPlayer.Instance.RightHand.controllerTransform, liner: true, delegate
			{
				//IL_0057: Unknown result type (might be due to invalid IL or missing references)
				if (CanFire())
				{
					CrittersManager crittersManager = GetCrittersManager();
					if (!((Object)(object)crittersManager == (Object)null) && crittersManager.LocalAuthority())
					{
						CrittersActor val = crittersManager.SpawnActor((CrittersActorType)10, -1);
						if ((Object)(object)val != (Object)null)
						{
							((Component)val).transform.position = pointer.transform.position;
						}
					}
				}
			}, delegate
			{
			});
		}

		public static void CritterFoodGun()
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			MakeGun(CurrentGunColor, new Vector3(0.15f, 0.15f, 0.15f), 0.025f, (PrimitiveType)0, GTPlayer.Instance.RightHand.controllerTransform, liner: true, delegate
			{
				//IL_0056: Unknown result type (might be due to invalid IL or missing references)
				if (CanFire())
				{
					CrittersManager crittersManager = GetCrittersManager();
					if (!((Object)(object)crittersManager == (Object)null) && crittersManager.LocalAuthority())
					{
						CrittersActor val = crittersManager.SpawnActor((CrittersActorType)1, -1);
						if ((Object)(object)val != (Object)null)
						{
							((Component)val).transform.position = pointer.transform.position;
						}
					}
				}
			}, delegate
			{
			});
		}

		public static void CritterStickyGooGun()
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			MakeGun(CurrentGunColor, new Vector3(0.15f, 0.15f, 0.15f), 0.025f, (PrimitiveType)0, GTPlayer.Instance.RightHand.controllerTransform, liner: true, delegate
			{
				//IL_003c: Unknown result type (might be due to invalid IL or missing references)
				//IL_0041: Unknown result type (might be due to invalid IL or missing references)
				//IL_0060: Unknown result type (might be due to invalid IL or missing references)
				//IL_006f: Unknown result type (might be due to invalid IL or missing references)
				//IL_0070: Unknown result type (might be due to invalid IL or missing references)
				if (CanFire())
				{
					CrittersManager crittersManager = GetCrittersManager();
					if (!((Object)(object)crittersManager == (Object)null) && crittersManager.LocalAuthority())
					{
						Vector3 position = pointer.transform.position;
						CrittersActor val = crittersManager.SpawnActor((CrittersActorType)18, -1);
						if ((Object)(object)val != (Object)null)
						{
							((Component)val).transform.position = position;
							crittersManager.TriggerEvent((CritterEvent)2, val.actorId, position, Quaternion.identity);
						}
					}
				}
			}, delegate
			{
			});
		}

		public static void CritterSpawnGun()
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			MakeGun(CurrentGunColor, new Vector3(0.15f, 0.15f, 0.15f), 0.025f, (PrimitiveType)0, GTPlayer.Instance.RightHand.controllerTransform, liner: true, delegate
			{
				//IL_0055: Unknown result type (might be due to invalid IL or missing references)
				if (CanFire())
				{
					CrittersManager crittersManager = GetCrittersManager();
					if (!((Object)(object)crittersManager == (Object)null) && crittersManager.LocalAuthority())
					{
						CrittersPawn val = crittersManager.SpawnCritter(-1);
						if ((Object)(object)val != (Object)null)
						{
							((Component)val).transform.position = pointer.transform.position;
						}
					}
				}
			}, delegate
			{
			});
		}

		public static void CritterDespawnGun()
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			MakeGun(CurrentGunColor, new Vector3(0.15f, 0.15f, 0.15f), 0.025f, (PrimitiveType)0, GTPlayer.Instance.RightHand.controllerTransform, liner: true, delegate
			{
				if (CanFire())
				{
					CrittersManager crittersManager = GetCrittersManager();
					if (!((Object)(object)crittersManager == (Object)null) && crittersManager.LocalAuthority() && (Object)(object)((RaycastHit)(ref raycastHit)).collider != (Object)null)
					{
						CrittersPawn componentInParent = ((Component)((RaycastHit)(ref raycastHit)).collider).GetComponentInParent<CrittersPawn>();
						if ((Object)(object)componentInParent != (Object)null)
						{
							crittersManager.DespawnActor((CrittersActor)(object)componentInParent);
						}
						else
						{
							CrittersActor componentInParent2 = ((Component)((RaycastHit)(ref raycastHit)).collider).GetComponentInParent<CrittersActor>();
							if ((Object)(object)componentInParent2 != (Object)null)
							{
								crittersManager.DespawnActor(componentInParent2);
							}
						}
					}
				}
			}, delegate
			{
			});
		}
	}

	public static class ClientKickMod
	{
		private static List<VRRig> clientKickedRigs = new List<VRRig>();

		private static List<GameObject> hiddenLeaderboardEntries = new List<GameObject>();

		public static void ClientKickGun()
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			MakeGun(CurrentGunColor, new Vector3(0.15f, 0.15f, 0.15f), 0.025f, (PrimitiveType)0, GTPlayer.Instance.RightHand.controllerTransform, liner: true, delegate
			{
				VRRig targetFromPointer = GetTargetFromPointer();
				if (!((Object)(object)targetFromPointer == (Object)null) && !clientKickedRigs.Contains(targetFromPointer))
				{
					clientKickedRigs.Add(targetFromPointer);
					Renderer[] componentsInChildren = ((Component)targetFromPointer).GetComponentsInChildren<Renderer>();
					foreach (Renderer val in componentsInChildren)
					{
						val.enabled = false;
					}
					Collider[] componentsInChildren2 = ((Component)targetFromPointer).GetComponentsInChildren<Collider>();
					foreach (Collider val2 in componentsInChildren2)
					{
						val2.enabled = false;
					}
					AudioSource[] componentsInChildren3 = ((Component)targetFromPointer).GetComponentsInChildren<AudioSource>();
					foreach (AudioSource val3 in componentsInChildren3)
					{
						((Behaviour)val3).enabled = false;
					}
					((Component)targetFromPointer).gameObject.SetActive(false);
					RemoveFromLeaderboard(targetFromPointer);
				}
			}, delegate
			{
			});
		}

		private static VRRig GetTargetFromPointer()
		{
			if ((Object)(object)((RaycastHit)(ref raycastHit)).collider == (Object)null)
			{
				return null;
			}
			return ((Component)((RaycastHit)(ref raycastHit)).collider).GetComponentInParent<VRRig>();
		}

		private static void RemoveFromLeaderboard(VRRig rig)
		{
			if ((Object)(object)rig == (Object)null)
			{
				return;
			}
			Player playerFromVRRig = RigShit.GetPlayerFromVRRig(rig);
			if (playerFromVRRig == null)
			{
				return;
			}
			foreach (GorillaPlayerScoreboardLine allScoreboardLine in GorillaScoreboardTotalUpdater.allScoreboardLines)
			{
				if (!((Object)(object)allScoreboardLine == (Object)null) && allScoreboardLine.linePlayer != null && allScoreboardLine.linePlayer.ActorNumber == playerFromVRRig.ActorNumber)
				{
					GameObject gameObject = ((Component)allScoreboardLine).gameObject;
					if (!hiddenLeaderboardEntries.Contains(gameObject))
					{
						hiddenLeaderboardEntries.Add(gameObject);
						gameObject.SetActive(false);
					}
				}
			}
		}

		public static void UnClientKickAll()
		{
			foreach (VRRig clientKickedRig in clientKickedRigs)
			{
				if (!((Object)(object)clientKickedRig == (Object)null))
				{
					Renderer[] componentsInChildren = ((Component)clientKickedRig).GetComponentsInChildren<Renderer>();
					foreach (Renderer val in componentsInChildren)
					{
						val.enabled = true;
					}
					Collider[] componentsInChildren2 = ((Component)clientKickedRig).GetComponentsInChildren<Collider>();
					foreach (Collider val2 in componentsInChildren2)
					{
						val2.enabled = true;
					}
					AudioSource[] componentsInChildren3 = ((Component)clientKickedRig).GetComponentsInChildren<AudioSource>();
					foreach (AudioSource val3 in componentsInChildren3)
					{
						((Behaviour)val3).enabled = true;
					}
					((Component)clientKickedRig).gameObject.SetActive(true);
				}
			}
			clientKickedRigs.Clear();
			foreach (GameObject hiddenLeaderboardEntry in hiddenLeaderboardEntries)
			{
				if ((Object)(object)hiddenLeaderboardEntry != (Object)null)
				{
					hiddenLeaderboardEntry.SetActive(true);
				}
			}
			hiddenLeaderboardEntries.Clear();
		}
	}

	public class WindManagerRPC : MonoBehaviourPun
	{
		[PunRPC]
		public void RPC_MoveWind(Vector3 pos)
		{
			//IL_004b: Unknown result type (might be due to invalid IL or missing references)
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0036: Unknown result type (might be due to invalid IL or missing references)
			if (!((Object)(object)Mods.cachedWindComponent == (Object)null))
			{
				Component cachedWindComponent = Mods.cachedWindComponent;
				Rigidbody val = (Rigidbody)(object)((cachedWindComponent is Rigidbody) ? cachedWindComponent : null);
				if ((Object)(object)val != (Object)null)
				{
					val.velocity = Vector3.zero;
					val.MovePosition(pos);
				}
				else
				{
					Mods.cachedWindComponent.transform.position = pos;
				}
			}
		}
	}

	public static class GuardianMods
	{
		[CompilerGenerated]
		private sealed class _003CDelayRoutine_003Ed__5 : IEnumerator<object>, IDisposable, IEnumerator
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public float time;

			public Action action;

			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return _003C_003E2__current;
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return _003C_003E2__current;
				}
			}

			[DebuggerHidden]
			public _003CDelayRoutine_003Ed__5(int _003C_003E1__state)
			{
				this._003C_003E1__state = _003C_003E1__state;
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				_003C_003E1__state = -2;
			}

			private bool MoveNext()
			{
				//IL_0027: Unknown result type (might be due to invalid IL or missing references)
				//IL_0031: Expected O, but got Unknown
				switch (_003C_003E1__state)
				{
				default:
					return false;
				case 0:
					_003C_003E1__state = -1;
					_003C_003E2__current = (object)new WaitForSeconds(time);
					_003C_003E1__state = 1;
					return true;
				case 1:
					_003C_003E1__state = -1;
					action?.Invoke();
					RPCFlush();
					return false;
				}
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}
		}

		public static void SetGuardianSelf()
		{
			NetPlayer localPlayer = NetworkSystem.Instance.LocalPlayer;
			if (localPlayer == null)
			{
				return;
			}
			foreach (GorillaGuardianZoneManager zoneManager in GorillaGuardianZoneManager.zoneManagers)
			{
				zoneManager.SetGuardian(localPlayer);
			}
			RPCFlush();
		}

		public static void RemoveGuardianSelf()
		{
			foreach (GorillaGuardianZoneManager zoneManager in GorillaGuardianZoneManager.zoneManagers)
			{
				if (zoneManager.CurrentGuardian == NetworkSystem.Instance.LocalPlayer)
				{
					zoneManager.SetGuardian((NetPlayer)null);
				}
			}
			RPCFlush();
		}

		private static VRRig GetLocalRig()
		{
			foreach (RigContainer activeRigContainer in VRRigCache.ActiveRigContainers)
			{
				if ((Object)(object)activeRigContainer != (Object)null && (Object)(object)activeRigContainer.Rig != (Object)null && activeRigContainer.Rig.isLocal)
				{
					return activeRigContainer.Rig;
				}
			}
			return null;
		}

		private static bool IsGuardian(NetPlayer player)
		{
			foreach (GorillaGuardianZoneManager zoneManager in GorillaGuardianZoneManager.zoneManagers)
			{
				if (zoneManager.CurrentGuardian == player)
				{
					return true;
				}
			}
			return false;
		}

		private static void RunAfterDelay(float time, Action action)
		{
			((MonoBehaviour)GTPlayer.Instance).StartCoroutine(DelayRoutine(time, action));
		}

		[IteratorStateMachine(typeof(_003CDelayRoutine_003Ed__5))]
		private static IEnumerator DelayRoutine(float time, Action action)
		{
			//yield-return decompiler failed: Unexpected instruction in Iterator.Dispose()
			return new _003CDelayRoutine_003Ed__5(0)
			{
				time = time,
				action = action
			};
		}

		private static Vector3 RandomOffset(float r)
		{
			//IL_0019: Unknown result type (might be due to invalid IL or missing references)
			//IL_001e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			return new Vector3(Random.Range(0f - r, r), Random.Range(0f - r, r), Random.Range(0f - r, r));
		}
	}

	public static class braceletstuff
	{
		private static bool _spawned;

		private static bool _removed;

		public static void getbracelet()
		{
			if (!_spawned)
			{
				_spawned = true;
				_removed = false;
				setbracelet(enable: false, isLeftHand: true);
				setbracelet(enable: true, isLeftHand: false);
				RPCProtection();
				Debug.Log((object)"Right-hand bracelet spawned");
			}
		}

		public static void removebracelet()
		{
			if (!_removed)
			{
				_removed = true;
				_spawned = false;
				setbracelet(enable: false, isLeftHand: true);
				setbracelet(enable: false, isLeftHand: false);
				RPCProtection();
				Debug.Log((object)"Bracelet removed");
			}
		}

		private static void setbracelet(bool enable, bool isLeftHand)
		{
			GorillaTagger.Instance.myVRRig.SendRPC("EnableNonCosmeticHandItemRPC", (RpcTarget)0, new object[2] { enable, isLeftHand });
		}
	}

	public class FullKickSystem : MonoBehaviour
	{
		[CompilerGenerated]
		private sealed class _003CDelayCoroutine_003Ed__26 : IEnumerator<object>, IDisposable, IEnumerator
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public float time;

			public Action action;

			public FullKickSystem _003C_003E4__this;

			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return _003C_003E2__current;
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return _003C_003E2__current;
				}
			}

			[DebuggerHidden]
			public _003CDelayCoroutine_003Ed__26(int _003C_003E1__state)
			{
				this._003C_003E1__state = _003C_003E1__state;
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				_003C_003E1__state = -2;
			}

			private bool MoveNext()
			{
				//IL_0027: Unknown result type (might be due to invalid IL or missing references)
				//IL_0031: Expected O, but got Unknown
				switch (_003C_003E1__state)
				{
				default:
					return false;
				case 0:
					_003C_003E1__state = -1;
					_003C_003E2__current = (object)new WaitForSeconds(time);
					_003C_003E1__state = 1;
					return true;
				case 1:
					_003C_003E1__state = -1;
					action?.Invoke();
					return false;
				}
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}
		}

		[CompilerGenerated]
		private sealed class _003CKickRoutine_003Ed__17 : IEnumerator<object>, IDisposable, IEnumerator
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public VRRig op;

			private float _003Ctimer_003E5__1;

			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return _003C_003E2__current;
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return _003C_003E2__current;
				}
			}

			[DebuggerHidden]
			public _003CKickRoutine_003Ed__17(int _003C_003E1__state)
			{
				this._003C_003E1__state = _003C_003E1__state;
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				_003C_003E1__state = -2;
			}

			private bool MoveNext()
			{
				//IL_0076: Unknown result type (might be due to invalid IL or missing references)
				//IL_0080: Expected O, but got Unknown
				//IL_0196: Unknown result type (might be due to invalid IL or missing references)
				//IL_01a0: Expected O, but got Unknown
				//IL_0156: Unknown result type (might be due to invalid IL or missing references)
				switch (_003C_003E1__state)
				{
				default:
					return false;
				case 0:
					_003C_003E1__state = -1;
					if (!k)
					{
						Fly();
						Instance.RunDelay(0.8f, delegate
						{
							RPCFlush();
						});
						_003C_003E2__current = (object)new WaitForSeconds(0.2f);
						_003C_003E1__state = 1;
						return true;
					}
					goto IL_0097;
				case 1:
					_003C_003E1__state = -1;
					k = true;
					goto IL_0097;
				case 2:
					{
						_003C_003E1__state = -1;
						break;
					}
					IL_0097:
					if (!fk)
					{
						ogl = lag;
						lag = new object[2] { 23, 6.5f };
						FakeLag();
						lag = ogl;
						fk = true;
					}
					_003Ctimer_003E5__1 = 0f;
					break;
				}
				if (_003Ctimer_003E5__1 < 10f)
				{
					kt++;
					lag = new object[2] { 140, 1f };
					if (!logpos.ContainsKey(op))
					{
						logpos.Add(op, ((Component)op).transform.position);
					}
					VelCheck(op);
					SendKickRPC(GetPlayer(op));
					_003Ctimer_003E5__1 += 0.5f;
					_003C_003E2__current = (object)new WaitForSeconds(0.5f);
					_003C_003E1__state = 2;
					return true;
				}
				Debug.Log((object)"Kick failed fallback");
				Reset();
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}
		}

		public static Dictionary<VRRig, Vector3> logpos = new Dictionary<VRRig, Vector3>();

		public static object[] lag = new object[2];

		public static object[] ogl = new object[2];

		public static int vec = -1;

		public static int kickty = 0;

		public static int kt = 0;

		public static bool k = false;

		public static bool fk = false;

		public static bool c = false;

		public static Coroutine kk;

		public static VRRig sel;

		private static FullKickSystem instance;

		private static MethodInfo cachedGetPlayerRef;

		public static FullKickSystem Instance
		{
			get
			{
				//IL_0016: Unknown result type (might be due to invalid IL or missing references)
				//IL_001c: Expected O, but got Unknown
				if ((Object)(object)instance == (Object)null)
				{
					GameObject val = new GameObject("FullKickSystem");
					instance = val.AddComponent<FullKickSystem>();
					Object.DontDestroyOnLoad((Object)(object)val);
				}
				return instance;
			}
		}

		private void Awake()
		{
			instance = this;
			Object.DontDestroyOnLoad((Object)(object)((Component)this).gameObject);
		}

		public static void RunGun()
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			MakeGun(CurrentGunColor, new Vector3(0.15f, 0.15f, 0.15f), 0.025f, (PrimitiveType)0, GTPlayer.Instance.RightHand.controllerTransform, liner: true, delegate
			{
				//IL_0003: Unknown result type (might be due to invalid IL or missing references)
				//IL_0008: Unknown result type (might be due to invalid IL or missing references)
				VRRig val = null;
				RaycastHit raycastHit = Mods.raycastHit;
				if ((Object)(object)((RaycastHit)(ref raycastHit)).collider != (Object)null)
				{
					val = ((Component)((RaycastHit)(ref raycastHit)).collider).GetComponentInParent<VRRig>();
				}
				if ((Object)(object)val == (Object)null)
				{
					Player playerFromGun = RigShit.GetPlayerFromGun();
					if (playerFromGun != null)
					{
						val = RigShit.GetVRRigFromPlayer(playerFromGun);
					}
				}
				if ((Object)(object)val != (Object)null)
				{
					StartKick(val);
				}
			}, delegate
			{
			});
		}

		public static void StartKick(VRRig target)
		{
			if (PhotonNetwork.InRoom && !c)
			{
				sel = target;
				kickty = 2;
				kt = 0;
				if (kk != null)
				{
					((MonoBehaviour)Instance).StopCoroutine(kk);
				}
				kk = ((MonoBehaviour)Instance).StartCoroutine(KickRoutine(target));
				c = true;
			}
		}

		[IteratorStateMachine(typeof(_003CKickRoutine_003Ed__17))]
		private static IEnumerator KickRoutine(VRRig op)
		{
			//yield-return decompiler failed: Unexpected instruction in Iterator.Dispose()
			return new _003CKickRoutine_003Ed__17(0)
			{
				op = op
			};
		}

		private static void VelCheck(VRRig op)
		{
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			if (logpos.ContainsKey(op) && Vector3.Distance(((Component)op).transform.position, logpos[op]) > 0.1f)
			{
				vec++;
				if (vec > 1)
				{
					vec = 0;
				}
				if (vec == 1)
				{
					Debug.Log((object)"Kick failed");
					Reset();
				}
			}
		}

		private static void Fly()
		{
			Player[] playerList = PhotonNetwork.PlayerList;
			foreach (Player target in playerList)
			{
				for (int j = 0; j < 10; j++)
				{
					SendKickRPC(target);
				}
			}
			Instance.RunDelay(0.8f, delegate
			{
				RPCFlush();
			});
		}

		private static void FakeLag()
		{
			for (int i = 0; i < 50; i++)
			{
				PhotonNetwork.SendAllOutgoingCommands();
			}
		}

		private static void SendKickRPC(Player target)
		{
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_0017: Expected O, but got Unknown
			//IL_002d: Unknown result type (might be due to invalid IL or missing references)
			if (target != null)
			{
				RaiseEventOptions val = new RaiseEventOptions();
				val.TargetActors = new int[1] { target.ActorNumber };
				PhotonNetwork.RaiseEvent((byte)200, (object)null, val, SendOptions.SendUnreliable);
			}
		}

		private static void RPCFlush()
		{
			((PhotonPeer)PhotonNetwork.NetworkingClient.LoadBalancingPeer).SendOutgoingCommands();
		}

		private static Player GetPlayer(VRRig rig)
		{
			if ((Object)(object)rig == (Object)null || rig.Creator == null)
			{
				return null;
			}
			NetPlayer creator = rig.Creator;
			if (cachedGetPlayerRef == null)
			{
				cachedGetPlayerRef = ((object)creator).GetType().GetMethod("GetPlayerRef");
				if (cachedGetPlayerRef == null)
				{
					Debug.LogWarning((object)("GetPlayerRef() not found on type: " + ((object)creator).GetType().FullName));
					return null;
				}
			}
			try
			{
				object obj = cachedGetPlayerRef.Invoke(creator, null);
				Player val = (Player)((obj is Player) ? obj : null);
				if (val != null)
				{
					return val;
				}
				Debug.LogWarning((object)"GetPlayerRef() returned null or wrong type");
				return null;
			}
			catch (Exception arg)
			{
				Debug.LogError((object)$"GetPlayerRef invocation failed: {arg}");
				return null;
			}
		}

		public void RunDelay(float time, Action action)
		{
			((MonoBehaviour)this).StartCoroutine(DelayCoroutine(time, action));
		}

		[IteratorStateMachine(typeof(_003CDelayCoroutine_003Ed__26))]
		private IEnumerator DelayCoroutine(float time, Action action)
		{
			//yield-return decompiler failed: Unexpected instruction in Iterator.Dispose()
			return new _003CDelayCoroutine_003Ed__26(0)
			{
				_003C_003E4__this = this,
				time = time,
				action = action
			};
		}

		public static void Reset()
		{
			if (kk != null)
			{
				((MonoBehaviour)Instance).StopCoroutine(kk);
				kk = null;
			}
			logpos.Clear();
			lag = ogl;
			vec = -1;
			sel = null;
			k = false;
			fk = false;
			c = false;
		}
	}

	public class DestroyOnTouchComponent : MonoBehaviour
	{
		private HashSet<int> _handled = new HashSet<int>();

		private void OnCollisionEnter(Collision collision)
		{
			TryHandle(collision.collider);
		}

		private void OnTriggerEnter(Collider other)
		{
			TryHandle(other);
		}

		private void TryHandle(Collider col)
		{
			//IL_0048: Unknown result type (might be due to invalid IL or missing references)
			//IL_0053: Unknown result type (might be due to invalid IL or missing references)
			if (!((Object)(object)col == (Object)null))
			{
				BuilderPiece componentInParent = ((Component)col).GetComponentInParent<BuilderPiece>();
				if (!((Object)(object)componentInParent == (Object)null) && _handled.Add(componentInParent.pieceId))
				{
					DestroyBlock(componentInParent.pieceId, ((Component)componentInParent).transform.position, ((Component)componentInParent).transform.rotation, PlaySfx: true);
					RPCFlush();
				}
			}
		}
	}

	public class Projectiles
	{
		public static float ProjDelay = 0.18f;

		private static Dictionary<string, float> gunCooldowns = new Dictionary<string, float>();

		private static SnowballThrowable projectile;

		private static bool initialized = false;

		private static GorillaVelocityEstimator velocityEstimator;

		private static GameObject velocityEstimatorObj;

		private static Transform cachedTransform;

		private static void Init()
		{
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Expected O, but got Unknown
			if (!initialized)
			{
				if ((Object)(object)velocityEstimator == (Object)null)
				{
					velocityEstimatorObj = new GameObject("VelocityEstimator");
					velocityEstimator = velocityEstimatorObj.AddComponent<GorillaVelocityEstimator>();
				}
				initialized = true;
			}
		}

		private static Transform GetProjectileTransform(string path, string name)
		{
			if ((Object)(object)cachedTransform != (Object)null)
			{
				return cachedTransform;
			}
			GameObject val = GameObject.Find(path);
			if ((Object)(object)val == (Object)null)
			{
				return null;
			}
			Transform val2 = val.transform.Find(name);
			if ((Object)(object)val2 != (Object)null)
			{
				cachedTransform = val2;
			}
			return val2;
		}

		public static void Shoot(string gunID, string path, string name, Vector3 pos, Quaternion rot, Vector3 vel)
		{
			//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
			//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
			//IL_010b: Unknown result type (might be due to invalid IL or missing references)
			Init();
			if (!gunCooldowns.ContainsKey(gunID))
			{
				gunCooldowns[gunID] = 0f;
			}
			if (Time.time < gunCooldowns[gunID])
			{
				return;
			}
			gunCooldowns[gunID] = Time.time + ProjDelay;
			Transform projectileTransform = GetProjectileTransform(path, name);
			if ((Object)(object)projectileTransform == (Object)null)
			{
				return;
			}
			projectile = ((Component)projectileTransform).GetComponent<SnowballThrowable>();
			if (!((Object)(object)projectile == (Object)null))
			{
				if (!((Component)projectile).gameObject.activeSelf)
				{
					projectile.SetSnowballActiveLocal(true);
					((Component)projectile).gameObject.SetActive(true);
				}
				projectile.velocityEstimator = velocityEstimator;
				((Component)projectile).transform.position = pos;
				((Component)projectile).transform.rotation = rot;
				SendProjectile(pos, vel, (int)((Component)projectile).transform.lossyScale.x);
			}
		}

		private static void SendProjectile(Vector3 pos, Vector3 vel, int size)
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0014: Unknown result type (might be due to invalid IL or missing references)
			//IL_0089: Unknown result type (might be due to invalid IL or missing references)
			//IL_008e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0090: Unknown result type (might be due to invalid IL or missing references)
			//IL_0095: Unknown result type (might be due to invalid IL or missing references)
			//IL_009f: Expected O, but got Unknown
			object[] array = new object[9] { pos, vel, 0, size, false, 1f, 1f, 1f, 1f };
			PhotonNetwork.RaiseEvent((byte)3, (object)new object[2]
			{
				NetworkSystem.Instance.ServerTimestamp,
				array
			}, new RaiseEventOptions
			{
				Receivers = (ReceiverGroup)1
			}, SendOptions.SendUnreliable);
		}

		public static void Cleanup()
		{
			if ((Object)(object)projectile != (Object)null)
			{
				((Component)projectile).gameObject.SetActive(false);
			}
		}

		public static void ShootSnowball(Vector3 pos, Vector3 vel)
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			Shoot("snowball", "Player Objects/.../SnowballRightAnchor(Clone)", "LMACF. RIGHT.", pos, Quaternion.identity, vel);
		}

		public static void ShootGift(Vector3 pos, Vector3 vel)
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			Shoot("gift", "Player Objects/.../BucketGiftFunctionalAnchor_Right(Clone)", "LMAHR. RIGHT.", pos, Quaternion.identity, vel);
		}

		public static void ShootRock(Vector3 pos, Vector3 vel)
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			Shoot("rock", "Player Objects/.../VotingRockAnchor_RIGHT(Clone)", "LMAMT. RIGHT.", pos, Quaternion.identity, vel);
		}
	}

	private static bool noclipEnabled = false;

	private const byte LoadMapEventCode = 200;

	private static GameObject mapLoaderPointer;

	private static bool network = false;

	private static float crashDelay = 0f;

	private static int lastRpcLimit = -1;

	private static int lastRpcErrors = -1;

	private static int lastLogErrors = -1;

	private static Component cachedWindComponent;

	private static GameObject windManager;

	private static VRRig lockedRig;

	private static float maxLockAngle = 25f;

	private static byte[] cachedSerializedRpc;

	private static DateTime lastAntiBanCall = DateTime.MinValue;

	private static readonly TimeSpan antiBanInterval = TimeSpan.FromSeconds(5.0);

	private static bool initialized;

	private static FieldInfo authContextField;

	private static FieldInfo photonViewListField;

	private static FieldInfo userRPCCallsField;

	private static FieldInfo reportedPlayersField;

	private static FieldInfo sendReportField;

	private static FieldInfo suspiciousPlayerIdField;

	private static FieldInfo suspiciousReasonField;

	private static FieldInfo suspiciousPlayerNameField;

	private static FieldInfo cachedDataField;

	private static FieldInfo monoRPCMethodsCacheField;

	private static MethodInfo clearAllEventsMethod;

	private static FieldInfo staticPlayerField;

	private static FieldInfo requestTimeoutField;

	private static FieldInfo compressApiDataField;

	private static FieldInfo disableFocusTimeCollectionField;

	private static FieldInfo sentCountAllowanceField;

	private static FieldInfo quickResendAttemptsField;

	private static FieldInfo outgoingStreamQueueField;

	public static Random rand = new Random();

	public static object[] Reportdata = new object[6];

	public static BuilderPiece piece = null;

	public static List<int> blockIds = new List<int>
	{
		857098599, -2063561053, 1510110959, 1848143946, 866161220, -604999206, 1844542113, -1514335082, 868696147, -460092905,
		1000122295, 757678001, 1513669651, -1537067750, 944837962, -1499829961, -604288536, -1, -1, -1,
		-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
		-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
		-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
		-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
		-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
		-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
		-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
		-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
		-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
		-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
		-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
		-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
		-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
		-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
		-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
		-1, -1, -1, -1, -1, -1, -709037470, -1724151965, -350300979, -1,
		-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
		-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
		-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
		-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
		-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
		-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
		-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
		-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
		-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
		-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
		-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
		-1, -1, -1, 1858470402, -1, 1794855203, -1326806786, 724396559, 1755661147, 1459635109,
		-566818631, 1925587737, -1441835191, -1324502924, 111152940, 798264081, -1821684029, 1895524638, 1961336659, -1059201160,
		1051576141, 539529939, -1535427925, 1210710592, 1228919111, 252298128, -648273975, 1120512569, 532163265, -845420418,
		1834228748, 1063967233, 1700948013, 2059548340, -1447051713, 1134055607, 1700655257, -1724819324, -1218055069, 251444537,
		-1446121736, -1927069002, -385891195, -196038879, -993249117, 1145900217, 1859614656, 1821589092, 661312857, 1701825380,
		-1621444201, 1924370326, -1193326485, -1194390666, -751675075, -933358727, 24270440, -1
	};

	public static Random random = new Random();

	private static bool wasTriggering;

	public static bool inCat11 = false;

	public static bool inSettings = false;

	public static bool inCat1 = false;

	public static bool inCat2 = false;

	public static bool inCat3 = false;

	public static bool inCat4 = false;

	public static bool inCat5 = false;

	public static bool inCat6 = false;

	public static bool inCat7 = false;

	public static bool inCat8 = false;

	public static bool inCat9 = false;

	public static bool inCat10 = false;

	public static Color CurrentGunColor = Color.blue;

	public static Color CurrentESPColor = Color.blue;

	public static int change1 = 1;

	public static int change2 = 1;

	public static int change3 = 1;

	public static int change4 = 1;

	public static int change6 = 1;

	public static int change7 = 1;

	public static int change8 = 1;

	public static int change9 = 1;

	public static int change10 = 1;

	public static int change11 = 1;

	public static int change12 = 1;

	public static int change13 = 1;

	public static int change14 = 1;

	public static int change15 = 1;

	public static int change16 = 1;

	public static VRRig gv;

	public static bool ghostMonke = false;

	public static bool invisMonke1 = false;

	public static bool rightHand = false;

	public static bool lastHit;

	public static bool lastHit2;

	public static GameObject orb;

	public static GameObject orb2;

	private static Vector3 frozenPosition;

	private static Quaternion frozenRotation;

	public static bool FPSPage;

	public static bool RGBMenu;

	public static bool right;

	public static bool fps;

	public static int ButtonSound = 67;

	public static float balll435342111;

	public static bool longArms;

	public static bool longgArms;

	public static bool longggArms;

	public static float colorChangerDelay;

	public static bool last2;

	public static GameObject pointer = null;

	public static LineRenderer Line;

	public static RaycastHit raycastHit;

	public static bool hand = false;

	public static bool hand1 = false;

	public static bool invisplat = false;

	public static bool invisMonke = false;

	public static bool stickyplatforms = false;

	private static Vector3 scale = new Vector3(0.0125f, 0.28f, 0.3825f);

	private static bool once_left;

	private static bool once_right;

	private static bool once_left_false;

	private static bool once_right_false;

	private static GameObject jump_left_local = null;

	private static GameObject jump_right_local = null;

	private static GradientColorKey[] colorKeysPlatformMonke = (GradientColorKey[])(object)new GradientColorKey[4];

	public static bool TriggerPlats;

	public static bool RPlat;

	public static bool LPlat;

	private static float rpcDel = 0f;

	private static float breakAudioDelay = 0f;

	private static float delay = 0f;

	public static object Delay2 { get; internal set; }

	public static void DV(PhotonView v)
	{
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Invalid comparison between Unknown and I4
		if (!PhotonNetwork.IsMasterClient)
		{
			NotifiLib.SendNotification("<color=red>Must be Master Client!</color>");
		}
		else
		{
			if ((Object)(object)v == (Object)null)
			{
				return;
			}
			try
			{
				if (!v.AmOwner && (int)v.OwnershipTransfer > 0)
				{
					v.RequestOwnership();
				}
				GameObject gameObject = ((Component)v).gameObject;
				gameObject.SetActive(false);
				gameObject.SetActive(true);
			}
			catch
			{
			}
		}
	}

	public static void DisableButton(string name)
	{
		GetButton(name).enabled = false;
		WristMenu.DestroyMenu();
		WristMenu.instance.Draw();
	}

	public static void PLACEHOLDER()
	{
		NotifiLib.SendNotification("<color=red>[Notice]:</color> This is a placeholder. This mod does nothing.");
	}

	public static void DrawHandOrbs()
	{
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		orb = GameObject.CreatePrimitive((PrimitiveType)0);
		orb2 = GameObject.CreatePrimitive((PrimitiveType)0);
		Object.Destroy((Object)(object)orb.GetComponent<Rigidbody>());
		Object.Destroy((Object)(object)orb.GetComponent<SphereCollider>());
		Object.Destroy((Object)(object)orb2.GetComponent<Rigidbody>());
		Object.Destroy((Object)(object)orb2.GetComponent<SphereCollider>());
		orb.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		orb2.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		orb.transform.position = GorillaTagger.Instance.leftHandTransform.position;
		orb2.transform.position = GorillaTagger.Instance.rightHandTransform.position;
		orb.GetComponent<Renderer>().material.color = CurrentGunColor;
		orb2.GetComponent<Renderer>().material.color = CurrentGunColor;
		Object.Destroy((Object)(object)orb, Time.deltaTime);
		Object.Destroy((Object)(object)orb2, Time.deltaTime);
		Color playerColor = RigShit.GetOwnVRRig().playerColor;
		playerColor.a = 0.1f;
		((Renderer)gv.mainSkin).material.color = playerColor;
	}

	public static void SpeedBoost()
	{
		GTPlayer.Instance.maxJumpSpeed = 8.5f;
		GTPlayer.Instance.jumpMultiplier = 1.5f;
	}

	public static void FlyMeth(float speed)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		if (WristMenu.abuttonDown)
		{
			Transform transform = ((Component)GTPlayer.Instance).transform;
			transform.position += ((Component)GTPlayer.Instance.headCollider).transform.forward * Time.deltaTime * speed;
			((Component)GTPlayer.Instance).GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
		}
	}

	public static void Platforms()
	{
		PlatformsThing(invisplat, stickyplatforms);
	}

	public static void Invisableplatforms()
	{
		PlatformsThing(invis: true, sticky: false);
	}

	public static void Noclip()
	{
		MeshCollider[] array = Resources.FindObjectsOfTypeAll<MeshCollider>();
		foreach (MeshCollider val in array)
		{
			((Collider)val).enabled = !WristMenu.triggerDownR;
		}
	}

	public static void InitMapLoader()
	{
		PhotonNetwork.NetworkingClient.EventReceived += OnMapEventReceived;
	}

	public static void CleanupMapLoader()
	{
		PhotonNetwork.NetworkingClient.EventReceived -= OnMapEventReceived;
	}

	public static void UnloadMapsGun()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		MakeUnloadGun(Color.red, new Vector3(0.15f, 0.15f, 0.15f), 0.025f, (PrimitiveType)0, GTPlayer.Instance.RightHand.controllerTransform, active: true, delegate
		{
			//IL_0050: Unknown result type (might be due to invalid IL or missing references)
			//IL_005f: Unknown result type (might be due to invalid IL or missing references)
			if (!((Object)(object)mapLoaderPointer == (Object)null))
			{
				float num = 1f;
				Player val = null;
				Player[] playerListOthers = PhotonNetwork.PlayerListOthers;
				foreach (Player val2 in playerListOthers)
				{
					VRRig vRRigFromPlayer = RigShit.GetVRRigFromPlayer(val2);
					if (!((Object)(object)vRRigFromPlayer == (Object)null))
					{
						float num2 = Vector3.Distance(((Component)vRRigFromPlayer).transform.position, mapLoaderPointer.transform.position);
						if (num2 < num)
						{
							num = num2;
							val = val2;
						}
					}
				}
				if (val != null)
				{
					SendUnloadEvent(val);
				}
			}
		}, delegate
		{
		});
	}

	public static void SendUnloadEvent(Player targetPlayer)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		if (targetPlayer != null)
		{
			object[] array = new object[1] { "UNLOAD_ALL" };
			RaiseEventOptions val = new RaiseEventOptions();
			val.TargetActors = new int[1] { targetPlayer.ActorNumber };
			RaiseEventOptions val2 = val;
			PhotonNetwork.RaiseEvent((byte)200, (object)array, val2, SendOptions.SendReliable);
		}
	}

	private static void OnMapEventReceived(EventData photonEvent)
	{
		if (photonEvent.Code != 200)
		{
			return;
		}
		try
		{
			string[] array = new string[10] { "Forest", "City", "Canyons", "Cave", "Mountain", "Sky", "Beach", "Basement", "Clouds", "MonkeBlocks" };
			string[] array2 = array;
			foreach (string text in array2)
			{
				GameObject val = GameObject.Find(text);
				if ((Object)(object)val != (Object)null)
				{
					val.SetActive(false);
				}
			}
		}
		catch (Exception)
		{
		}
	}

	public static Player GetPlayerFromGun()
	{
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)pointer == (Object)null)
		{
			return null;
		}
		float num = 1f;
		Player result = null;
		Player[] playerListOthers = PhotonNetwork.PlayerListOthers;
		foreach (Player val in playerListOthers)
		{
			VRRig vRRigFromPlayer = RigShit.GetVRRigFromPlayer(val);
			if (!((Object)(object)vRRigFromPlayer == (Object)null))
			{
				float num2 = Vector3.Distance(((Component)vRRigFromPlayer).transform.position, pointer.transform.position);
				if (num2 < num)
				{
					num = num2;
					result = val;
				}
			}
		}
		return result;
	}

	private static void MakeUnloadGun(Color color, Vector3 scale, float radius, PrimitiveType type, Transform parent, bool active, Action onTrigger, Action onRelease)
	{
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)mapLoaderPointer == (Object)null)
		{
			mapLoaderPointer = GameObject.CreatePrimitive(type);
			mapLoaderPointer.transform.localScale = scale;
			mapLoaderPointer.GetComponent<Renderer>().material.color = color;
			mapLoaderPointer.GetComponent<Collider>().enabled = false;
			Object.Destroy((Object)(object)mapLoaderPointer.GetComponent<Rigidbody>());
		}
		mapLoaderPointer.transform.position = parent.position + parent.forward * 5f;
		if (active)
		{
			onTrigger();
		}
	}

	public static void KickGunMoreDetected()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		MakeGun(Color.black, new Vector3(0.15f, 0.15f, 0.15f), 0.025f, (PrimitiveType)0, GTPlayer.Instance.RightHand.controllerTransform, liner: true, delegate
		{
			//IL_0069: Unknown result type (might be due to invalid IL or missing references)
			//IL_0070: Expected O, but got Unknown
			//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c3: Expected O, but got Unknown
			//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
			//IL_010c: Unknown result type (might be due to invalid IL or missing references)
			//IL_010e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0123: Unknown result type (might be due to invalid IL or missing references)
			Player playerFromGun = GetPlayerFromGun();
			if (playerFromGun != null)
			{
				PhotonHandler component = GameObject.Find("PhotonMono").GetComponent<PhotonHandler>();
				((object)component).GetType().GetField("nextSendTickCountOnSerialize", BindingFlags.Instance | BindingFlags.NonPublic)?.SetValue(component, (int)(Time.realtimeSinceStartup * 9999f));
				PhotonView photonView = ((NetworkSceneObject)FriendshipGroupDetection.Instance).photonView;
				for (int i = 0; i < 3960; i++)
				{
					Hashtable val = new Hashtable();
					val.Add((byte)0, (object)photonView.ViewID);
					val.Add((byte)2, (object)(PhotonNetwork.ServerTimestamp + -2147483647));
					val.Add((byte)3, (object)"VerifyPartyMember");
					val.Add((byte)5, (object)70);
					RaiseEventOptions val2 = new RaiseEventOptions();
					val2.TargetActors = new int[1] { playerFromGun.ActorNumber };
					val2.InterestGroup = photonView.Group;
					RaiseEventOptions val3 = val2;
					SendOptions val4 = default(SendOptions);
					((SendOptions)(ref val4)).Reliability = true;
					val4.DeliveryMode = (DeliveryMode)3;
					val4.Encrypt = false;
					SendOptions val5 = val4;
					PhotonNetwork.NetworkingClient.LoadBalancingPeer.OpRaiseEvent((byte)200, (object)val, val3, val5);
				}
				PhotonNetwork.SendAllOutgoingCommands();
			}
		}, delegate
		{
		});
	}

	public static void CacheKickGun()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		MakeGun(CurrentGunColor, new Vector3(0.15f, 0.15f, 0.15f), 0.025f, (PrimitiveType)0, GTPlayer.Instance.RightHand.controllerTransform, liner: true, delegate
		{
			Player playerFromGun = GetPlayerFromGun();
			if (playerFromGun != null)
			{
				CacheKick(playerFromGun);
			}
		}, delegate
		{
		});
	}

	private static void CacheKick(Player target)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Expected O, but got Unknown
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 255; i++)
		{
			object[] array = new object[2] { i, target.ActorNumber };
			byte num = (byte)i;
			RaiseEventOptions val = new RaiseEventOptions();
			val.TargetActors = new int[1] { target.ActorNumber };
			PhotonNetwork.RaiseEvent(num, (object)array, val, SendOptions.SendUnreliable);
		}
	}

	public static void DisableNetworkTriggers()
	{
		network = true;
		GameObject.Find("Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab").SetActive(false);
	}

	public static void EnableNetworkTriggers()
	{
		if (network)
		{
			GameObject.Find("Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab").SetActive(true);
			network = false;
		}
	}

	public static void UnbanSelf()
	{
		PlayerPrefs.DeleteKey("banned");
		PlayerPrefs.DeleteKey("banExpiration");
		PlayerPrefs.DeleteKey("tempBanExpiration");
		PlayerPrefs.Save();
		try
		{
			typeof(GorillaGameManager).GetField("isBanned", BindingFlags.Static | BindingFlags.NonPublic)?.SetValue(null, false);
		}
		catch
		{
		}
	}

	public static CosmeticsController GetCosmetics()
	{
		return CosmeticsController.instance;
	}

	public static void UnlockAllCosmetics()
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Expected I4, but got Unknown
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		CosmeticsController cosmetics = GetCosmetics();
		if ((Object)(object)cosmetics == (Object)null)
		{
			return;
		}
		foreach (CosmeticItem allCosmetic in cosmetics.allCosmetics)
		{
			if (allCosmetic.isNullItem || cosmetics.unlockedCosmetics.Contains(allCosmetic))
			{
				continue;
			}
			cosmetics.unlockedCosmetics.Add(allCosmetic);
			CosmeticCategory itemCategory = allCosmetic.itemCategory;
			CosmeticCategory val = itemCategory;
			switch (val - 1)
			{
			case 0:
				if (!cosmetics.unlockedHats.Contains(allCosmetic))
				{
					cosmetics.unlockedHats.Add(allCosmetic);
				}
				break;
			case 2:
				if (!cosmetics.unlockedFaces.Contains(allCosmetic))
				{
					cosmetics.unlockedFaces.Add(allCosmetic);
				}
				break;
			case 1:
				if (!cosmetics.unlockedBadges.Contains(allCosmetic))
				{
					cosmetics.unlockedBadges.Add(allCosmetic);
				}
				break;
			case 3:
				if (!allCosmetic.isThrowable)
				{
					if (!cosmetics.unlockedPaws.Contains(allCosmetic))
					{
						cosmetics.unlockedPaws.Add(allCosmetic);
					}
				}
				else if (!cosmetics.unlockedThrowables.Contains(allCosmetic))
				{
					cosmetics.unlockedThrowables.Add(allCosmetic);
				}
				break;
			case 5:
				if (!cosmetics.unlockedFurs.Contains(allCosmetic))
				{
					cosmetics.unlockedFurs.Add(allCosmetic);
				}
				break;
			case 6:
				if (!cosmetics.unlockedShirts.Contains(allCosmetic))
				{
					cosmetics.unlockedShirts.Add(allCosmetic);
				}
				break;
			case 7:
				if (!cosmetics.unlockedBacks.Contains(allCosmetic))
				{
					cosmetics.unlockedBacks.Add(allCosmetic);
				}
				break;
			case 8:
				if (!cosmetics.unlockedArms.Contains(allCosmetic))
				{
					cosmetics.unlockedArms.Add(allCosmetic);
				}
				break;
			case 4:
				if (!cosmetics.unlockedChests.Contains(allCosmetic))
				{
					cosmetics.unlockedChests.Add(allCosmetic);
				}
				break;
			case 9:
				if (!cosmetics.unlockedPants.Contains(allCosmetic))
				{
					cosmetics.unlockedPants.Add(allCosmetic);
				}
				break;
			case 10:
				if (!cosmetics.unlockedTagFX.Contains(allCosmetic))
				{
					cosmetics.unlockedTagFX.Add(allCosmetic);
				}
				break;
			}
		}
		cosmetics.UpdateWardrobeModelsAndButtons();
		cosmetics.OnCosmeticsUpdated?.Invoke();
	}

	public static void CrashGun()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		MakeGun(Color.red, new Vector3(0.15f, 0.15f, 0.15f), 0.025f, (PrimitiveType)0, GTPlayer.Instance.RightHand.controllerTransform, liner: true, delegate
		{
			//IL_002d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0033: Expected O, but got Unknown
			//IL_0035: Unknown result type (might be due to invalid IL or missing references)
			//IL_0050: Unknown result type (might be due to invalid IL or missing references)
			Player playerFromGun = RigShit.GetPlayerFromGun();
			if (playerFromGun != null)
			{
				for (int i = 0; i < 150; i++)
				{
					LoadBalancingClient networkingClient = PhotonNetwork.NetworkingClient;
					object[] obj = new object[1] { "Nebula" };
					RaiseEventOptions val = new RaiseEventOptions();
					val.CachingOption = (EventCaching)0;
					val.TargetActors = new int[1] { playerFromGun.ActorNumber };
					networkingClient.OpRaiseEvent((byte)204, (object)obj, val, SendOptions.SendReliable);
				}
			}
		}, delegate
		{
		});
	}

	public static void LagGun()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		MakeGun(Color.magenta, new Vector3(0.15f, 0.15f, 0.15f), 0.025f, (PrimitiveType)0, GTPlayer.Instance.RightHand.controllerTransform, liner: true, delegate
		{
			Player playerFromGun = RigShit.GetPlayerFromGun();
			if (playerFromGun != null)
			{
				VRRig vRRigFromPlayer = RigShit.GetVRRigFromPlayer(playerFromGun);
				if ((Object)(object)vRRigFromPlayer != (Object)null)
				{
					PhotonView[] componentsInChildren = ((Component)vRRigFromPlayer).GetComponentsInChildren<PhotonView>();
					PhotonView[] array = componentsInChildren;
					foreach (PhotonView val in array)
					{
						for (int j = 0; j < 20; j++)
						{
							val.RPC("RPC_PlayHandTap", playerFromGun, new object[3] { 67, true, 0.1f });
						}
					}
				}
			}
		}, delegate
		{
		});
	}

	public static void FreezeGun()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		MakeGun(Color.cyan, new Vector3(0.15f, 0.15f, 0.15f), 0.025f, (PrimitiveType)0, GTPlayer.Instance.RightHand.controllerTransform, liner: true, delegate
		{
			//IL_007e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0085: Expected O, but got Unknown
			//IL_00db: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f7: Expected O, but got Unknown
			Player playerFromGun = RigShit.GetPlayerFromGun();
			if (playerFromGun != null)
			{
				VRRig vRRigFromPlayer = RigShit.GetVRRigFromPlayer(playerFromGun);
				if ((Object)(object)vRRigFromPlayer != (Object)null)
				{
					List<int> list = new List<int>();
					Player[] playerListOthers = PhotonNetwork.PlayerListOthers;
					foreach (Player val in playerListOthers)
					{
						if (val != playerFromGun)
						{
							list.Add(val.ActorNumber);
						}
					}
					int viewID = ((Component)vRRigFromPlayer).GetComponent<PhotonView>().ViewID;
					Hashtable val2 = new Hashtable();
					val2.Add((byte)0, (object)"Player Network Controller");
					val2.Add((byte)6, (object)PhotonNetwork.ServerTimestamp);
					val2.Add((byte)4, (object)new int[2] { viewID, viewID });
					val2.Add((byte)7, (object)viewID);
					PhotonNetwork.NetworkingClient.OpRaiseEvent((byte)202, (object)val2, new RaiseEventOptions
					{
						TargetActors = list.ToArray()
					}, SendOptions.SendReliable);
				}
			}
		}, delegate
		{
		});
	}

	public static void Unbanner()
	{
		if (!PhotonNetwork.IsMasterClient)
		{
			NotifiLib.SendNotification("<color=red>Must be Master Client!</color>");
			return;
		}
		Player[] playerList = PhotonNetwork.PlayerList;
		foreach (Player val in playerList)
		{
			VRRig vRRigFromPlayer = RigShit.GetVRRigFromPlayer(val);
			PhotonView val2 = ((vRRigFromPlayer != null) ? ((Component)vRRigFromPlayer).GetComponent<PhotonView>() : null);
			if ((Object)(object)val2 != (Object)null)
			{
				val2.RPC("RPC_UnbanPlayer", val, new object[0]);
			}
		}
	}

	public static void ObjectFlickerGun()
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		if (!PhotonNetwork.IsMasterClient)
		{
			NotifiLib.SendNotification("<color=red>Must be Master Client!</color>");
			DisableButton("Object Flicker Gun (M)");
			return;
		}
		MakeGun(Color.magenta, new Vector3(0.15f, 0.15f, 0.15f), 0.025f, (PrimitiveType)0, GTPlayer.Instance.RightHand.controllerTransform, liner: true, delegate
		{
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			//IL_002e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0038: Unknown result type (might be due to invalid IL or missing references)
			//IL_003d: Unknown result type (might be due to invalid IL or missing references)
			RaycastHit val = default(RaycastHit);
			if (!((Object)(object)pointer == (Object)null) && Physics.Raycast(GTPlayer.Instance.RightHand.controllerTransform.position, -GTPlayer.Instance.RightHand.controllerTransform.up, ref val))
			{
				Collider obj = ((RaycastHit)(ref val)).collider;
				PhotonView val2 = ((obj != null) ? ((Component)obj).GetComponentInParent<PhotonView>() : null);
				if (!((Object)(object)val2 == (Object)null))
				{
					if (val2.IsMine || val2.AmOwner || val2.AmController)
					{
						ServerFlicker(val2);
					}
					RPCFlush();
				}
			}
		}, delegate
		{
		});
	}

	private static async void ServerFlicker(PhotonView v)
	{
		try
		{
			((Component)v).gameObject.SetActive(false);
			((Component)v).gameObject.SetActive(true);
			new Delay().D(0.089f, delegate
			{
				PhotonNetwork.Destroy(v);
			});
			new Delay().D(0.3f, delegate
			{
				RPCFlush();
			});
			RPCFlush();
		}
		catch
		{
		}
	}

	public static void RPCFlush()
	{
		if (!PhotonNetwork.InRoom)
		{
			return;
		}
		try
		{
			MonkeAgent instance = MonkeAgent.instance;
			if ((Object)(object)instance == (Object)null)
			{
				return;
			}
			instance.rpcCallLimit = int.MaxValue;
			instance.rpcErrorMax = int.MaxValue;
			instance.logErrorMax = int.MaxValue;
			FieldInfo field = ((object)instance).GetType().GetField("gravityFactorOptionSelection", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			if (field != null)
			{
				field.SetValue(instance, float.MaxValue);
			}
			VRRig component = ((Component)GTPlayer.Instance).GetComponent<VRRig>();
			if ((Object)(object)component != (Object)null)
			{
				PhotonNetwork.RemoveBufferedRPCs(((Component)component).GetComponent<PhotonView>().ViewID, (string)null, (int[])null);
			}
			PhotonNetwork.OpCleanActorRpcBuffer(PhotonNetwork.LocalPlayer.ActorNumber);
			PhotonNetwork.SendAllOutgoingCommands();
			try
			{
				instance.OnPlayerLeftRoom(NetworkSystem.Instance.LocalPlayer);
			}
			catch
			{
			}
		}
		catch
		{
		}
	}

	public static CrittersManager GetCrittersManager()
	{
		return CrittersManager.instance;
	}

	public static void DespawnAllCritters()
	{
		if (PhotonNetwork.IsMasterClient)
		{
			CrittersManager crittersManager = GetCrittersManager();
			if ((Object)(object)crittersManager != (Object)null)
			{
				crittersManager.QueueDespawnAllCritters();
			}
		}
	}

	public static void MakeCrittersPeaceful()
	{
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		CrittersManager crittersManager = GetCrittersManager();
		if ((Object)(object)crittersManager == (Object)null)
		{
			return;
		}
		foreach (CrittersActor crittersActor in crittersManager.crittersActors)
		{
			CrittersPawn val = (CrittersPawn)(object)((crittersActor is CrittersPawn) ? crittersActor : null);
			if (val != null)
			{
				val.currentState = (CreatureState)0;
			}
		}
	}

	public static void MakeCrittersEat()
	{
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		CrittersManager crittersManager = GetCrittersManager();
		if ((Object)(object)crittersManager == (Object)null)
		{
			return;
		}
		foreach (CrittersActor crittersActor in crittersManager.crittersActors)
		{
			CrittersPawn val = (CrittersPawn)(object)((crittersActor is CrittersPawn) ? crittersActor : null);
			if (val != null)
			{
				val.currentState = (CreatureState)1;
			}
		}
	}

	public static void MakeCrittersRun()
	{
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		CrittersManager crittersManager = GetCrittersManager();
		if ((Object)(object)crittersManager == (Object)null)
		{
			return;
		}
		foreach (CrittersActor crittersActor in crittersManager.crittersActors)
		{
			CrittersPawn val = (CrittersPawn)(object)((crittersActor is CrittersPawn) ? crittersActor : null);
			if (val != null)
			{
				val.currentState = (CreatureState)3;
			}
		}
	}

	public static void MakeCrittersSleep()
	{
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		CrittersManager crittersManager = GetCrittersManager();
		if ((Object)(object)crittersManager == (Object)null)
		{
			return;
		}
		foreach (CrittersActor crittersActor in crittersManager.crittersActors)
		{
			CrittersPawn val = (CrittersPawn)(object)((crittersActor is CrittersPawn) ? crittersActor : null);
			if (val != null)
			{
				val.currentState = (CreatureState)5;
			}
		}
	}

	public static void SpawnFoodAtPlayer()
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		CrittersManager crittersManager = GetCrittersManager();
		if (!((Object)(object)crittersManager == (Object)null))
		{
			Vector3 position = ((Component)GTPlayer.Instance).transform.position + Vector3.up * 2f;
			CrittersActor val = crittersManager.SpawnActor((CrittersActorType)1, -1);
			if ((Object)(object)val != (Object)null)
			{
				((Component)val).transform.position = position;
			}
		}
	}

	public static void SpawnCageAtPlayer()
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		CrittersManager crittersManager = GetCrittersManager();
		if (!((Object)(object)crittersManager == (Object)null))
		{
			Vector3 position = ((Component)GTPlayer.Instance).transform.position + Vector3.up * 2f;
			CrittersActor val = crittersManager.SpawnActor((CrittersActorType)10, -1);
			if ((Object)(object)val != (Object)null)
			{
				((Component)val).transform.position = position;
			}
		}
	}

	public static void SpawnStunBombAtPlayer()
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		CrittersManager crittersManager = GetCrittersManager();
		if (!((Object)(object)crittersManager == (Object)null))
		{
			Vector3 position = ((Component)GTPlayer.Instance).transform.position + Vector3.up * 2f;
			CrittersActor val = crittersManager.SpawnActor((CrittersActorType)13, -1);
			if ((Object)(object)val != (Object)null)
			{
				((Component)val).transform.position = position;
			}
		}
	}

	public static void SpawnNoiseMakerAtPlayer()
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		CrittersManager crittersManager = GetCrittersManager();
		if (!((Object)(object)crittersManager == (Object)null))
		{
			Vector3 position = ((Component)GTPlayer.Instance).transform.position + Vector3.up * 2f;
			CrittersActor val = crittersManager.SpawnActor((CrittersActorType)16, -1);
			if ((Object)(object)val != (Object)null)
			{
				((Component)val).transform.position = position;
			}
		}
	}

	public static void SpawnStickyTrapAtPlayer()
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		CrittersManager crittersManager = GetCrittersManager();
		if (!((Object)(object)crittersManager == (Object)null))
		{
			Vector3 position = ((Component)GTPlayer.Instance).transform.position + Vector3.up * 2f;
			CrittersActor val = crittersManager.SpawnActor((CrittersActorType)17, -1);
			if ((Object)(object)val != (Object)null)
			{
				((Component)val).transform.position = position;
			}
		}
	}

	public static void TriggerMassStun()
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		if (PhotonNetwork.IsMasterClient)
		{
			CrittersManager crittersManager = GetCrittersManager();
			if (!((Object)(object)crittersManager == (Object)null))
			{
				Vector3 position = ((Component)GTPlayer.Instance).transform.position;
				crittersManager.TriggerEvent((CritterEvent)0, -1, position, Quaternion.identity);
			}
		}
	}

	public static void DespawnAllFood()
	{
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Invalid comparison between Unknown and I4
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		CrittersManager crittersManager = GetCrittersManager();
		if ((Object)(object)crittersManager == (Object)null)
		{
			return;
		}
		foreach (CrittersActor allActor in crittersManager.allActors)
		{
			if ((int)allActor.crittersActorType == 1 && ((Component)allActor).gameObject.activeSelf)
			{
				crittersManager.DespawnActor(allActor);
			}
		}
	}

	public static void DespawnAllTraps()
	{
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Invalid comparison between Unknown and I4
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Invalid comparison between Unknown and I4
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		CrittersManager crittersManager = GetCrittersManager();
		if ((Object)(object)crittersManager == (Object)null)
		{
			return;
		}
		foreach (CrittersActor allActor in crittersManager.allActors)
		{
			if (((int)allActor.crittersActorType == 17 || (int)allActor.crittersActorType == 10) && ((Component)allActor).gameObject.activeSelf)
			{
				crittersManager.DespawnActor(allActor);
			}
		}
	}

	public static void BringAllCritters()
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		CrittersManager crittersManager = GetCrittersManager();
		if ((Object)(object)crittersManager == (Object)null)
		{
			return;
		}
		Vector3 position = ((Component)GTPlayer.Instance).transform.position;
		foreach (CrittersActor crittersActor in crittersManager.crittersActors)
		{
			if (crittersActor is CrittersPawn)
			{
				((Component)crittersActor).transform.position = position + Vector3.up * 2f + Random.insideUnitSphere * 3f;
			}
		}
	}

	public static void GiantCritters()
	{
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		CrittersManager crittersManager = GetCrittersManager();
		if ((Object)(object)crittersManager == (Object)null)
		{
			return;
		}
		foreach (CrittersActor crittersActor in crittersManager.crittersActors)
		{
			if (crittersActor is CrittersPawn)
			{
				((Component)crittersActor).transform.localScale = Vector3.one * 5f;
			}
		}
	}

	public static void TinyCritters()
	{
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		CrittersManager crittersManager = GetCrittersManager();
		if ((Object)(object)crittersManager == (Object)null)
		{
			return;
		}
		foreach (CrittersActor crittersActor in crittersManager.crittersActors)
		{
			if (crittersActor is CrittersPawn)
			{
				((Component)crittersActor).transform.localScale = Vector3.one * 0.2f;
			}
		}
	}

	public static void ResetCritterSizes()
	{
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		CrittersManager crittersManager = GetCrittersManager();
		if ((Object)(object)crittersManager == (Object)null)
		{
			return;
		}
		foreach (CrittersActor crittersActor in crittersManager.crittersActors)
		{
			if (crittersActor is CrittersPawn)
			{
				((Component)crittersActor).transform.localScale = Vector3.one;
			}
		}
	}

	public static void Arm(float scale)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		((Component)Loco()).transform.localScale = new Vector3(scale, scale, scale);
	}

	public static void ResetArms()
	{
		Arm(1f);
		longArms = false;
		longgArms = false;
		longggArms = false;
	}

	public static GTPlayer Loco()
	{
		return GTPlayer.Instance;
	}

	public static void LongArms()
	{
		longArms = true;
		Arm(1.09f);
	}

	public static void LongerArms()
	{
		longgArms = true;
		Arm(1.2f);
	}

	public static void LongererArms()
	{
		longggArms = true;
		Arm(1.6f);
	}

	public static void ChangeColor(Color color, object target = null)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		PlayerPrefs.SetFloat("redValue", Mathf.Clamp(color.r, 0f, 1f));
		PlayerPrefs.SetFloat("greenValue", Mathf.Clamp(color.g, 0f, 1f));
		PlayerPrefs.SetFloat("blueValue", Mathf.Clamp(color.b, 0f, 1f));
		GorillaTagger.Instance.UpdateColor(color.r, color.g, color.b);
		PlayerPrefs.Save();
		try
		{
			if (target != null)
			{
				NetPlayer val = (NetPlayer)((target is NetPlayer) ? target : null);
				if (val == null)
				{
					if (target is RpcTarget val2)
					{
						GorillaTagger.Instance.myVRRig.SendRPC("RPC_InitializeNoobMaterial", val2, new object[3] { color.r, color.g, color.b });
					}
				}
				else
				{
					GorillaTagger.Instance.myVRRig.SendRPC("RPC_InitializeNoobMaterial", val, new object[3] { color.r, color.g, color.b });
				}
			}
			else
			{
				GorillaTagger.Instance.myVRRig.SendRPC("RPC_InitializeNoobMaterial", (RpcTarget)0, new object[3] { color.r, color.g, color.b });
			}
			AntiRPCKick();
			RPCProtection();
		}
		catch
		{
		}
	}

	public static void RainbowColor()
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		if (Time.time > colorChangerDelay)
		{
			colorChangerDelay = Time.time + 0.05f;
			ChangeColor(Color.HSVToRGB((float)Time.frameCount / 180f % 1f, 1f, 1f));
		}
	}

	public static void CrashGunLD()
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		if (!PhotonNetwork.IsMasterClient)
		{
			NotifiLib.SendNotification("<color=red>You must be Master Client to use this!</color>");
			return;
		}
		MakeGun(CurrentGunColor, new Vector3(0.15f, 0.15f, 0.15f), 0.025f, (PrimitiveType)0, GTPlayer.Instance.RightHand.controllerTransform, liner: true, delegate
		{
			Player playerFromGun = RigShit.GetPlayerFromGun();
			if (playerFromGun != null)
			{
				VRRig vRRigFromPlayer = RigShit.GetVRRigFromPlayer(playerFromGun);
				if (!((Object)(object)vRRigFromPlayer == (Object)null))
				{
					CrashPlayerTemp(vRRigFromPlayer);
				}
			}
		}, delegate
		{
		});
	}

	private static void CrashPlayerTemp(VRRig player)
	{
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Expected O, but got Unknown
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Expected O, but got Unknown
		if (Time.time <= crashDelay)
		{
			return;
		}
		Player playerFromVRRig = RigShit.GetPlayerFromVRRig(player);
		if (playerFromVRRig == null)
		{
			return;
		}
		for (int i = 0; i < 1875; i++)
		{
			LoadBalancingPeer loadBalancingPeer = PhotonNetwork.NetworkingClient.LoadBalancingPeer;
			Hashtable val = new Hashtable();
			RaiseEventOptions val2 = new RaiseEventOptions();
			val2.TargetActors = new int[1] { playerFromVRRig.ActorNumber };
			SendOptions val3 = default(SendOptions);
			((SendOptions)(ref val3)).Reliability = false;
			loadBalancingPeer.OpRaiseEvent((byte)3, (object)val, val2, val3);
			AntiRPCKick();
		}
		((PhotonPeer)PhotonNetwork.NetworkingClient.LoadBalancingPeer).SendOutgoingCommands();
		for (int j = 0; j < 10; j++)
		{
			try
			{
				((MonkeAgent)MonkeAgent.instance).OnPlayerLeftRoom(NetworkSystem.Instance.LocalPlayer);
			}
			catch
			{
			}
		}
		crashDelay = Time.time + 4.5f;
	}

	public static void AntiCheatMonitor()
	{
		MonkeAgent instance = MonkeAgent.instance;
		if ((Object)(object)instance == (Object)null)
		{
			return;
		}
		try
		{
			if (instance.rpcCallLimit != lastRpcLimit)
			{
				lastRpcLimit = instance.rpcCallLimit;
				if (instance.rpcCallLimit < 1000)
				{
					NotifiLib.SendNotification("<color=yellow>⚠ RPC Limit Dropping (Possible Detection)</color>");
				}
			}
			if (instance.rpcErrorMax != lastRpcErrors)
			{
				lastRpcErrors = instance.rpcErrorMax;
				if (instance.rpcErrorMax < 1000)
				{
					NotifiLib.SendNotification("<color=orange>⚠ RPC Errors Increasing</color>");
				}
			}
			if (instance.logErrorMax != lastLogErrors)
			{
				lastLogErrors = instance.logErrorMax;
				if (instance.logErrorMax < 1000)
				{
					NotifiLib.SendNotification("<color=red>⚠ Anti-Cheat Triggered</color>");
				}
			}
		}
		catch
		{
		}
	}

	private static VRRig GetLockTarget()
	{
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		VRRig result = null;
		float num = maxLockAngle;
		Player[] playerList = PhotonNetwork.PlayerList;
		foreach (Player p in playerList)
		{
			VRRig vRRigFromPlayer = RigShit.GetVRRigFromPlayer(p);
			if (!((Object)(object)vRRigFromPlayer == (Object)null) && !((Object)(object)vRRigFromPlayer == (Object)(object)GorillaTagger.Instance.offlineVRRig))
			{
				Vector3 val = ((Component)vRRigFromPlayer).transform.position - ((Component)Camera.main).transform.position;
				Vector3 normalized = ((Vector3)(ref val)).normalized;
				float num2 = Vector3.Angle(((Component)Camera.main).transform.forward, normalized);
				if (num2 < num)
				{
					num = num2;
					result = vRRigFromPlayer;
				}
			}
		}
		return result;
	}

	private static void CacheWindComponent()
	{
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Expected O, but got Unknown
		if ((Object)(object)cachedWindComponent != (Object)null && (Object)(object)windManager != (Object)null)
		{
			return;
		}
		windManager = GameObject.Find("WindManager");
		if ((Object)(object)windManager == (Object)null)
		{
			windManager = new GameObject("WindManager");
			windManager.AddComponent<PhotonView>();
			windManager.AddComponent<WindManagerRPC>();
			Debug.Log((object)"Created WindManager");
		}
		cachedWindComponent = null;
		GameObject[] array = Object.FindObjectsOfType<GameObject>();
		foreach (GameObject val in array)
		{
			if ((Object)(object)val == (Object)null || !((Object)val).name.ToLower().Contains("wind"))
			{
				continue;
			}
			Debug.Log((object)("FOUND WIND OBJECT: " + ((Object)val).name));
			Component[] components = val.GetComponents<Component>();
			foreach (Component val2 in components)
			{
				Debug.Log((object)(((Object)val).name + " -> " + ((object)val2).GetType().Name));
				string text = ((object)val2).GetType().Name.ToLower();
				if (text.Contains("wind") || text.Contains("force") || text.Contains("elevator"))
				{
					cachedWindComponent = val2;
					Debug.Log((object)("SELECTED COMPONENT: " + ((object)val2).GetType().Name));
					break;
				}
			}
			if ((Object)(object)cachedWindComponent != (Object)null)
			{
				break;
			}
		}
		if ((Object)(object)cachedWindComponent == (Object)null)
		{
			NotifiLib.SendNotification("<color=red>No wind component found!</color>");
		}
	}

	private static PhotonView GetWindManagerPV()
	{
		if ((Object)(object)windManager == (Object)null)
		{
			return null;
		}
		return windManager.GetComponent<PhotonView>();
	}

	public static void WindGun()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		CacheWindComponent();
		MakeGun(CurrentGunColor, new Vector3(0.15f, 0.15f, 0.15f), 0.025f, (PrimitiveType)0, GTPlayer.Instance.RightHand.controllerTransform, liner: true, delegate
		{
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_0047: Unknown result type (might be due to invalid IL or missing references)
			if (PhotonNetwork.IsMasterClient && !((Object)(object)cachedWindComponent == (Object)null))
			{
				Vector3 point = ((RaycastHit)(ref raycastHit)).point;
				PhotonView windManagerPV = GetWindManagerPV();
				if ((Object)(object)windManagerPV != (Object)null)
				{
					windManagerPV.RPC("RPC_MoveWind", (RpcTarget)0, new object[1] { point });
				}
			}
		}, delegate
		{
		});
	}

	public static void WindFlingGun()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		CacheWindComponent();
		MakeGun(CurrentGunColor, new Vector3(0.15f, 0.15f, 0.15f), 0.025f, (PrimitiveType)0, GTPlayer.Instance.RightHand.controllerTransform, liner: true, delegate
		{
			//IL_0052: Unknown result type (might be due to invalid IL or missing references)
			//IL_0057: Unknown result type (might be due to invalid IL or missing references)
			//IL_0061: Unknown result type (might be due to invalid IL or missing references)
			//IL_0066: Unknown result type (might be due to invalid IL or missing references)
			//IL_006b: Unknown result type (might be due to invalid IL or missing references)
			//IL_008e: Unknown result type (might be due to invalid IL or missing references)
			if (PhotonNetwork.IsMasterClient && !((Object)(object)cachedWindComponent == (Object)null))
			{
				if ((Object)(object)lockedRig == (Object)null)
				{
					lockedRig = GetLockTarget();
				}
				if (!((Object)(object)lockedRig == (Object)null))
				{
					Vector3 val = ((Component)lockedRig).transform.position + Vector3.down * 1.5f;
					PhotonView windManagerPV = GetWindManagerPV();
					if ((Object)(object)windManagerPV != (Object)null)
					{
						windManagerPV.RPC("RPC_MoveWind", (RpcTarget)0, new object[1] { val });
					}
				}
			}
		}, delegate
		{
			lockedRig = null;
		});
	}

	public static void WindFlingStrongGun()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		CacheWindComponent();
		MakeGun(CurrentGunColor, new Vector3(0.15f, 0.15f, 0.15f), 0.025f, (PrimitiveType)0, GTPlayer.Instance.RightHand.controllerTransform, liner: true, delegate
		{
			//IL_009e: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
			//IL_00be: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
			if (PhotonNetwork.IsMasterClient && !((Object)(object)cachedWindComponent == (Object)null))
			{
				if ((Object)(object)lockedRig == (Object)null)
				{
					lockedRig = GetLockTarget();
				}
				if (!((Object)(object)lockedRig == (Object)null))
				{
					PhotonView windManagerPV = GetWindManagerPV();
					if (!((Object)(object)windManagerPV == (Object)null))
					{
						Vector3 val = default(Vector3);
						for (int i = 0; i < 5; i++)
						{
							((Vector3)(ref val))._002Ector(Random.Range(-0.1f, 0.1f), 0f, Random.Range(-0.1f, 0.1f));
							Vector3 val2 = ((Component)lockedRig).transform.position + Vector3.down * 1.5f + val;
							windManagerPV.RPC("RPC_MoveWind", (RpcTarget)0, new object[1] { val2 });
						}
					}
				}
			}
		}, delegate
		{
			lockedRig = null;
		});
	}

	public static void DestroyGun()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		MakeGun(CurrentGunColor, new Vector3(0.15f, 0.15f, 0.15f), 0.025f, (PrimitiveType)0, GTPlayer.Instance.RightHand.controllerTransform, liner: true, delegate
		{
			if (!((Object)(object)((RaycastHit)(ref raycastHit)).collider == (Object)null))
			{
				VRRig componentInParent = ((Component)((RaycastHit)(ref raycastHit)).collider).GetComponentInParent<VRRig>();
				if (!((Object)(object)componentInParent == (Object)null))
				{
					Player playerFromVRRig = RigShit.GetPlayerFromVRRig(componentInParent);
					if (playerFromVRRig != null)
					{
						PhotonNetwork.OpRemoveCompleteCacheOfPlayer(playerFromVRRig.ActorNumber);
					}
				}
			}
		}, delegate
		{
		});
	}

	public static void AntiRPCKick()
	{
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Expected O, but got Unknown
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Expected O, but got Unknown
		//IL_044c: Unknown result type (might be due to invalid IL or missing references)
		//IL_044f: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			AntiRPCKicker();
			Type typeFromHandle = typeof(MonkeAgent);
			MonkeAgent instance = MonkeAgent.instance;
			if ((Object)(object)instance == (Object)null)
			{
				return;
			}
			((MonkeAgent)MonkeAgent.instance).rpcErrorMax = int.MaxValue;
			((MonkeAgent)MonkeAgent.instance).rpcCallLimit = int.MaxValue;
			((MonkeAgent)MonkeAgent.instance).logErrorMax = int.MaxValue;
			PhotonNetwork.MaxResendsBeforeDisconnect = int.MaxValue;
			PhotonNetwork.QuickResends = int.MaxValue;
			LoadBalancingClient networkingClient = PhotonNetwork.NetworkingClient;
			LoadBalancingPeer val = ((networkingClient != null) ? networkingClient.LoadBalancingPeer : null);
			if (val != null)
			{
				((PhotonPeer)val).SentCountAllowance = int.MaxValue;
				((PhotonPeer)val).QuickResendAttempts = 3;
				((PhotonPeer)val).CrcEnabled = false;
				((PhotonPeer)val).UseByteArraySlicePoolForEvents = false;
				((PhotonPeer)val).TrafficStatsEnabled = false;
				((PhotonPeer)val).TrafficStatsReset();
				((PhotonPeer)val).SendOutgoingCommands();
				try
				{
					Type type = ((object)val).GetType();
					if (type.GetField("outgoingStreamQueue", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(val) is IList list)
					{
						list.Clear();
					}
					if (type.GetField("commandList", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(val) is IList list2)
					{
						list2.Clear();
					}
					type.GetField("resentCommandsCount", BindingFlags.Instance | BindingFlags.NonPublic)?.SetValue(val, 0);
				}
				catch
				{
				}
			}
			PhotonNetwork.NetworkStatisticsEnabled = false;
			(Type, object, string, bool)[] array = new(Type, object, string, bool)[7]
			{
				(typeFromHandle, instance, "rpcErrorMax", false),
				(typeFromHandle, instance, "rpcCallLimit", false),
				(typeFromHandle, instance, "logErrorMax", false),
				(typeFromHandle, instance, "userRPCCalls", false),
				(typeFromHandle, instance, "_sendReport", false),
				(typeof(PhotonNetwork), null, "QuickResends", true),
				(typeof(PhotonNetwork), null, "MaxResendsBeforeDisconnect", true)
			};
			(Type, object, string, bool)[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				(Type, object, string, bool) tuple = array2[i];
				TrySetMember(tuple.Item1, tuple.Item2, tuple.Item3, GetDefaultValue(tuple.Item3), tuple.Item4);
			}
			try
			{
				if (typeFromHandle.GetField("userRPCCalls", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(instance) is IDictionary dictionary)
				{
					dictionary.Clear();
				}
			}
			catch
			{
			}
			LoadBalancingClient networkingClient2 = PhotonNetwork.NetworkingClient;
			Hashtable val2 = new Hashtable();
			val2.Add((byte)0, (object)GorillaTagger.Instance.myVRRig.ViewID);
			RaiseEventOptions val3 = new RaiseEventOptions();
			val3.CachingOption = (EventCaching)6;
			val3.TargetActors = new int[1] { PhotonNetwork.LocalPlayer.ActorNumber };
			networkingClient2.OpRaiseEvent((byte)200, (object)val2, val3, SendOptions.SendReliable);
			if (Time.time > rpcDel)
			{
				try
				{
					rpcDel = Time.time + 0.47f;
					PhotonNetwork.RemoveBufferedRPCs(int.MaxValue, (string)null, (int[])null);
					PhotonNetwork.RemoveRPCs(PhotonNetwork.LocalPlayer);
					PhotonNetwork.OpCleanActorRpcBuffer(PhotonNetwork.LocalPlayer.ActorNumber);
					PhotonNetwork.OpCleanRpcBuffer(GorillaTagger.Instance.myVRRig.GetView);
					((PhotonPeer)PhotonNetwork.NetworkingClient.LoadBalancingPeer).SendOutgoingCommands();
					Traverse val4 = Traverse.Create(typeof(PhotonNetwork));
					val4.Property("ResentReliableCommands", (object[])null).SetValue((object)0);
					PhotonNetwork.NetworkingClient.Service();
					PhotonNetwork.NetworkingClient.OpChangeGroups((byte[])null, new byte[4] { 1, 2, 3, 4 });
					((PhotonPeer)PhotonNetwork.NetworkingClient.LoadBalancingPeer).TrafficStatsReset();
					try
					{
						Type type2 = AppDomain.CurrentDomain.GetAssemblies().First((Assembly a) => a.GetName().Name == "Assembly-CSharp").GetType("RoomSystem");
						type2.GetMethod("OnPlayerLeftRoom", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(null, new object[1] { NetworkSystem.Instance.LocalPlayer });
					}
					catch
					{
					}
					try
					{
						NetSystemState val5 = (NetSystemState)0;
						PeerStateValue val6 = (PeerStateValue)0;
						((object)(NetSystemState)(ref val5)).Equals((object)(NetSystemState)3);
						((object)(PeerStateValue)(ref val6)).Equals((object)(PeerStateValue)3);
						RunViewUpdate();
					}
					catch
					{
					}
					PhotonNetwork.SendAllOutgoingCommands();
					try
					{
						if (typeof(PhotonNetwork).GetField("photonViewList", BindingFlags.Static | BindingFlags.NonPublic)?.GetValue(null) is IDictionary dictionary2)
						{
							ArrayList arrayList = new ArrayList();
							foreach (DictionaryEntry item in dictionary2)
							{
								object? value = item.Value;
								PhotonView val7 = (PhotonView)((value is PhotonView) ? value : null);
								if ((Object)(object)val7 != (Object)null && val7.IsMine && val7.isRuntimeInstantiated)
								{
									arrayList.Add(item.Key);
								}
							}
							foreach (object item2 in arrayList)
							{
								dictionary2.Remove(item2);
							}
						}
					}
					catch
					{
					}
				}
				catch
				{
				}
			}
			typeFromHandle.GetMethod("RefreshRPCs", BindingFlags.Instance | BindingFlags.NonPublic)?.Invoke(instance, null);
		}
		catch
		{
		}
	}

	private static void AntiRPCKicker()
	{
		for (int i = 0; i < 1300; i++)
		{
			ResendCachedRpc();
		}
		try
		{
			LoadBalancingPeer loadBalancingPeer = PhotonNetwork.NetworkingClient.LoadBalancingPeer;
			FieldInfo field = ((object)loadBalancingPeer).GetType().GetField("outgoingStreamQueue", BindingFlags.Instance | BindingFlags.NonPublic);
			if (field != null && field.GetValue(loadBalancingPeer) is IList { Count: >0 } list)
			{
				cachedSerializedRpc = list[list.Count - 1] as byte[];
			}
		}
		catch
		{
			cachedSerializedRpc = null;
		}
	}

	private static void ResendCachedRpc()
	{
		if (cachedSerializedRpc == null)
		{
			return;
		}
		try
		{
			LoadBalancingPeer loadBalancingPeer = PhotonNetwork.NetworkingClient.LoadBalancingPeer;
			Type type = ((object)loadBalancingPeer).GetType();
			(type.GetMethod("SendReliable", BindingFlags.Instance | BindingFlags.NonPublic) ?? type.GetMethod("SendUnreliable", BindingFlags.Instance | BindingFlags.NonPublic))?.Invoke(loadBalancingPeer, new object[1] { cachedSerializedRpc });
		}
		catch
		{
			SetTick(9999f);
		}
	}

	public static void SetTick(float tickMultiplier)
	{
		GameObject obj = GameObject.Find("PhotonMono");
		PhotonHandler val = ((obj != null) ? obj.GetComponent<PhotonHandler>() : null);
		if ((Object)(object)val != (Object)null)
		{
			Traverse.Create((object)val).Field("nextSendTickCountOnSerialize").SetValue((object)(int)(Time.realtimeSinceStartup * tickMultiplier));
			PhotonHandler.SendAsap = true;
		}
	}

	private static bool TrySetMember(Type type, object instance, string fieldName, object value, bool isStatic)
	{
		try
		{
			FieldInfo field = type.GetField(fieldName, (isStatic ? BindingFlags.Static : BindingFlags.Instance) | BindingFlags.Public | BindingFlags.NonPublic);
			if (field != null)
			{
				field.SetValue(instance, value);
				return true;
			}
			return false;
		}
		catch
		{
			return false;
		}
	}

	private static object GetDefaultValue(string fieldName)
	{
		if (fieldName.Contains("Max") || fieldName.Contains("Limit") || fieldName.Contains("Count"))
		{
			return int.MaxValue;
		}
		if (fieldName.Contains("userRPCCalls"))
		{
			return null;
		}
		if (fieldName.Contains("_sendReport"))
		{
			return false;
		}
		return null;
	}

	public static void RPCProtection()
	{
		AntiRPCKick();
	}

	public static object RunViewUpdate()
	{
		return typeof(PhotonNetwork).GetMethod("RunViewUpdate", BindingFlags.Static | BindingFlags.NonPublic)?.Invoke(null, null);
	}

	public static void InitializeAntiBanHelper()
	{
		if (initialized)
		{
			return;
		}
		try
		{
			Type typeFromHandle = typeof(PlayFabHttp);
			clearAllEventsMethod = typeFromHandle.GetMethod("ClearAllEvents", BindingFlags.Static | BindingFlags.Public);
			authContextField = typeof(PlayFabAuthenticationAPI).GetField("_authenticationContext", BindingFlags.Static | BindingFlags.NonPublic);
			staticPlayerField = typeof(PlayFabSettings).GetField("staticPlayer", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			requestTimeoutField = typeof(PlayFabSettings).GetField("RequestTimeout", BindingFlags.Static | BindingFlags.Public);
			compressApiDataField = typeof(PlayFabSettings).GetField("CompressApiData", BindingFlags.Static | BindingFlags.Public);
			disableFocusTimeCollectionField = typeof(PlayFabSettings).GetField("DisableFocusTimeCollection", BindingFlags.Static | BindingFlags.Public);
			Type typeFromHandle2 = typeof(MonkeAgent);
			userRPCCallsField = typeFromHandle2.GetField("userRPCCalls", BindingFlags.Instance | BindingFlags.NonPublic);
			reportedPlayersField = typeFromHandle2.GetField("reportedPlayers", BindingFlags.Instance | BindingFlags.NonPublic);
			sendReportField = typeFromHandle2.GetField("_sendReport", BindingFlags.Instance | BindingFlags.NonPublic);
			suspiciousPlayerIdField = typeFromHandle2.GetField("_suspiciousPlayerId", BindingFlags.Instance | BindingFlags.NonPublic);
			suspiciousPlayerNameField = typeFromHandle2.GetField("_suspiciousPlayerName", BindingFlags.Instance | BindingFlags.NonPublic);
			suspiciousReasonField = typeFromHandle2.GetField("_suspiciousReason", BindingFlags.Instance | BindingFlags.NonPublic);
			Type typeFromHandle3 = typeof(PhotonNetwork);
			photonViewListField = typeFromHandle3.GetField("photonViewList", BindingFlags.Static | BindingFlags.NonPublic);
			cachedDataField = typeFromHandle3.GetField("cachedData", BindingFlags.Static | BindingFlags.NonPublic);
			monoRPCMethodsCacheField = typeFromHandle3.GetField("monoRPCMethodsCache", BindingFlags.Static | BindingFlags.NonPublic);
			Type typeFromHandle4 = typeof(LoadBalancingPeer);
			sentCountAllowanceField = typeFromHandle4.GetField("SentCountAllowance", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			quickResendAttemptsField = typeFromHandle4.GetField("QuickResendAttempts", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			outgoingStreamQueueField = typeFromHandle4.GetField("outgoingStreamQueue", BindingFlags.Instance | BindingFlags.NonPublic);
			initialized = true;
		}
		catch
		{
		}
	}

	public static void AntiBan()
	{
		InitializeAntiBanHelper();
		if (!PhotonNetwork.InRoom)
		{
			return;
		}
		try
		{
			MonkeAgent instance = MonkeAgent.instance;
			if ((Object)(object)instance != (Object)null)
			{
				instance.rpcErrorMax = int.MaxValue;
				instance.rpcCallLimit = int.MaxValue;
				instance.logErrorMax = int.MaxValue;
				userRPCCallsField?.SetValue(instance, new Dictionary<string, Dictionary<string, object>>());
				reportedPlayersField?.SetValue(instance, new List<string>());
				sendReportField?.SetValue(instance, false);
				suspiciousPlayerIdField?.SetValue(instance, "");
				suspiciousPlayerNameField?.SetValue(instance, "");
				suspiciousReasonField?.SetValue(instance, "");
			}
			PhotonNetwork.MaxResendsBeforeDisconnect = int.MaxValue;
			PhotonNetwork.QuickResends = int.MaxValue;
			PhotonNetwork.NetworkStatisticsEnabled = false;
			LoadBalancingClient networkingClient = PhotonNetwork.NetworkingClient;
			LoadBalancingPeer val = ((networkingClient != null) ? networkingClient.LoadBalancingPeer : null);
			if (val != null)
			{
				sentCountAllowanceField?.SetValue(val, int.MaxValue);
				quickResendAttemptsField?.SetValue(val, (byte)3);
				if (outgoingStreamQueueField?.GetValue(val) is IList list)
				{
					list.Clear();
				}
				((object)val).GetType().GetField("resentCommandsCount", BindingFlags.Instance | BindingFlags.NonPublic)?.SetValue(val, 0);
				((PhotonPeer)val).SendOutgoingCommands();
			}
			PhotonNetwork.SendAllOutgoingCommands();
			photonViewListField?.SetValue(null, Activator.CreateInstance(photonViewListField.FieldType));
			cachedDataField?.SetValue(null, new Dictionary<int, Dictionary<int, Queue<object[]>>>());
			monoRPCMethodsCacheField?.SetValue(null, new Dictionary<Type, List<MethodInfo>>());
			if (DateTime.UtcNow - lastAntiBanCall < antiBanInterval)
			{
				return;
			}
			lastAntiBanCall = DateTime.UtcNow;
			if (!PhotonNetwork.IsConnected)
			{
				authContextField?.SetValue(null, null);
				clearAllEventsMethod?.Invoke(null, null);
			}
			else
			{
				if (!PlayFabAuthenticationAPI.IsEntityLoggedIn())
				{
					return;
				}
				try
				{
					clearAllEventsMethod?.Invoke(null, null);
					requestTimeoutField?.SetValue(null, 30000);
					compressApiDataField?.SetValue(null, true);
					disableFocusTimeCollectionField?.SetValue(null, true);
					object? obj = staticPlayerField?.GetValue(null);
					PlayFabAuthenticationContext val2 = (PlayFabAuthenticationContext)((obj is PlayFabAuthenticationContext) ? obj : null);
					if (val2 != null)
					{
						val2.ForgetAllCredentials();
					}
					RPCFlush();
					return;
				}
				catch
				{
					return;
				}
			}
		}
		catch
		{
		}
	}

	public static SendOptions STS()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		SendOptions result = default(SendOptions);
		result.Encrypt = true;
		result.DeliveryMode = (DeliveryMode)3;
		((SendOptions)(ref result)).Reliability = true;
		return result;
	}

	public static DeployedChild GetChild(DeployableObject obj)
	{
		return Traverse.Create((object)obj).Field("_child").GetValue<DeployedChild>();
	}

	public static RaiseEventOptions TargetedWCO(int actor, EventCaching cache = 5)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Expected O, but got Unknown
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		RaiseEventOptions val = new RaiseEventOptions();
		val.CachingOption = cache;
		val.TargetActors = new int[1] { actor };
		return val;
	}

	public static void InvokeOnDeployMultiple(DeployableObject deployable, int times = 5)
	{
		UnityEvent value = Traverse.Create((object)deployable).Field("_onDeploy").GetValue<UnityEvent>();
		for (int i = 0; i < times; i++)
		{
			value.Invoke();
		}
	}

	public static void SpamDeploythxboowomp(DeployableObject deployable)
	{
		UnityEvent value = Traverse.Create((object)deployable).Field("_onDeploy").GetValue<UnityEvent>();
		for (int i = 0; i > 2; i++)
		{
			value.Invoke();
		}
	}

	public static void SpamDeploythxboowompCrash(DeployableObject deployable)
	{
		UnityEvent value = Traverse.Create((object)deployable).Field("_onDeploy").GetValue<UnityEvent>();
		for (int i = 0; i > 1; i++)
		{
			value.Invoke();
		}
	}

	private static object GetDeploySignalId(DeployableObject deployable)
	{
		if ((Object)(object)deployable == (Object)null)
		{
			return 0;
		}
		try
		{
			object value = Traverse.Create((object)deployable).Field("_deploySignal").GetValue();
			if (value != null)
			{
				FieldInfo field = value.GetType().GetField("_signalID", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				if (field != null)
				{
					return field.GetValue(value);
				}
			}
			FieldInfo fieldInfo = ((object)deployable).GetType().GetField("deploySignal", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic) ?? ((object)deployable).GetType().GetField("DeploySignal", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			if (fieldInfo != null)
			{
				object value2 = fieldInfo.GetValue(deployable);
				if (value2 != null)
				{
					FieldInfo field2 = value2.GetType().GetField("_signalID", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
					if (field2 != null)
					{
						return field2.GetValue(value2);
					}
				}
			}
		}
		catch
		{
		}
		return 0;
	}

	private static void SendBarrel(VRRig target, Vector3 velocity)
	{
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		DeployableObject component = GameObject.Find("Player Objects/Local VRRig/Local Gorilla Player/GorillaPlayerNetworkedRigAnchor/rig/body/shoulder.R/upper_arm.R/forearm.R/Right Arm Item Anchor/DropZoneAnchor/HoldableThrowableBarrelLeprechaun_Anchor(Clone)/LMAPE.").GetComponent<DeployableObject>();
		DeployedChild child = GetChild(component);
		Rigidbody value = Traverse.Create((object)child).Field("_rigidbody").GetValue<Rigidbody>();
		object[] array = PhotonUtils.FetchScratchArray(5);
		array[0] = GetDeploySignalId(component);
		array[1] = PhotonNetwork.ServerTimestamp;
		array[2] = BitPackUtils.PackWorldPosForNetwork(((Component)target.bodyTransform).transform.position + new Vector3(0f, -0.3f));
		array[3] = 469893376;
		array[4] = BitPackUtils.PackWorldPosForNetwork(velocity);
		PhotonNetwork.RaiseEvent((byte)177, (object)array, TargetedWCO(target.Creator.ActorNumber, (EventCaching)5), STS());
		child.Deploy(component, ((Component)target.bodyTransform).transform.position + new Vector3(0f, -0.3f), Quaternion.identity, velocity, false);
		component.DeployChild();
		SpamDeploythxboowomp(component);
		value.linearDamping = 0f;
		value.angularDamping = 0f;
		value.linearVelocity = velocity;
		value.solverVelocityIterations = 9998;
		value.detectCollisions = false;
		value.inertiaTensor = ((Component)target.bodyTransform).transform.up * 9998.99f;
		value.AddForce(velocity * 100f, (ForceMode)1);
		child.ReturnToParent(2f);
	}

	private static void SendBarrelFixIDK(VRRig target, Vector3 velocity)
	{
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		DeployableObject val = FindBarrel();
		if ((Object)(object)val == (Object)null)
		{
			return;
		}
		DeployedChild child = GetChild(val);
		if (!((Object)(object)child == (Object)null))
		{
			Rigidbody value = Traverse.Create((object)child).Field("_rigidbody").GetValue<Rigidbody>();
			if (!((Object)(object)value == (Object)null))
			{
				object[] array = PhotonUtils.FetchScratchArray(5);
				array[0] = GetDeploySignalId(val);
				array[1] = PhotonNetwork.ServerTimestamp;
				array[2] = BitPackUtils.PackWorldPosForNetwork(target.bodyTransform.position + new Vector3(0f, -0.3f, 0f));
				array[3] = BitPackUtils.PackQuaternionForNetwork(Quaternion.identity);
				array[4] = BitPackUtils.PackWorldPosForNetwork(velocity);
				PhotonNetwork.RaiseEvent((byte)177, (object)array, TargetedWCO(target.Creator.ActorNumber, (EventCaching)5), STS());
				Vector3 val2 = target.bodyTransform.position + new Vector3(0f, -0.3f, 0f);
				child.Deploy(val, val2, Quaternion.identity, velocity, false);
				val.DeployChild();
				SpamDeploythxboowomp(val);
				value.linearDamping = 0f;
				value.angularDamping = 0f;
				value.linearVelocity = velocity;
				value.solverVelocityIterations = 9998;
				value.detectCollisions = false;
				value.inertiaTensor = target.bodyTransform.up * 9998.99f;
				value.AddForce(velocity * 100f, (ForceMode)1);
				child.ReturnToParent(2f);
			}
		}
	}

	private static DeployableObject FindBarrel()
	{
		GameObject val = GameObject.Find("Player Objects/Local VRRig/Local Gorilla Player/GorillaPlayerNetworkedRigAnchor/rig/body/shoulder.R/upper_arm.R/forearm.R/Right Arm Item Anchor/DropZoneAnchor/HoldableThrowableBarrelLeprechaun_Anchor(Clone)/LMAPE.");
		if ((Object)(object)val != (Object)null)
		{
			return val.GetComponent<DeployableObject>();
		}
		val = GameObject.Find("Player Objects/Local VRRig/Local Gorilla Player/GorillaPlayerNetworkedRigAnchor/rig/body/shoulder.L/upper_arm.L/forearm.L/Left Arm Item Anchor/DropZoneAnchor/HoldableThrowableBarrelLeprechaun_Anchor(Clone)/LMAPE.");
		if ((Object)(object)val != (Object)null)
		{
			return val.GetComponent<DeployableObject>();
		}
		return null;
	}

	public static void BarrelFlingGun2()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		MakeGun(CurrentGunColor, new Vector3(0.15f, 0.15f, 0.15f), 0.025f, (PrimitiveType)0, GTPlayer.Instance.RightHand.controllerTransform, liner: true, delegate
		{
			//IL_0035: Unknown result type (might be due to invalid IL or missing references)
			Player playerFromGun = RigShit.GetPlayerFromGun();
			if (playerFromGun != null)
			{
				VRRig vRRigFromPlayer = RigShit.GetVRRigFromPlayer(playerFromGun);
				if (!((Object)(object)vRRigFromPlayer == (Object)null))
				{
					SendBarrelFixIDK(vRRigFromPlayer, new Vector3(0f, 9998.99f, 0f));
				}
			}
		}, delegate
		{
		});
	}

	public static void BuyBarrel()
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		((CosmeticsController)CosmeticsController.instance).currentCart.Insert(0, ((CosmeticsController)CosmeticsController.instance).GetItemFromDict("LMAPE."));
	}

	public static void BarrelFlingGun()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		MakeGun(CurrentGunColor, new Vector3(0.15f, 0.15f, 0.15f), 0.025f, (PrimitiveType)0, GTPlayer.Instance.RightHand.controllerTransform, liner: true, delegate
		{
			//IL_0035: Unknown result type (might be due to invalid IL or missing references)
			Player playerFromGun = RigShit.GetPlayerFromGun();
			if (playerFromGun != null)
			{
				VRRig vRRigFromPlayer = RigShit.GetVRRigFromPlayer(playerFromGun);
				if (!((Object)(object)vRRigFromPlayer == (Object)null))
				{
					SendBarrel(vRRigFromPlayer, new Vector3(0f, 9998.99f, 0f));
				}
			}
		}, delegate
		{
		});
	}

	public static void BarrelCrashGun()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		MakeGun(CurrentGunColor, new Vector3(0.15f, 0.15f, 0.15f), 0.025f, (PrimitiveType)0, GTPlayer.Instance.RightHand.controllerTransform, liner: true, delegate
		{
			//IL_0035: Unknown result type (might be due to invalid IL or missing references)
			Player playerFromGun = RigShit.GetPlayerFromGun();
			if (playerFromGun != null)
			{
				VRRig vRRigFromPlayer = RigShit.GetVRRigFromPlayer(playerFromGun);
				if (!((Object)(object)vRRigFromPlayer == (Object)null))
				{
					SendBarrel(vRRigFromPlayer, new Vector3(0f, 500000f, 0f));
				}
			}
		}, delegate
		{
		});
	}

	public static void BarrelKickGun()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		MakeGun(CurrentGunColor, new Vector3(0.15f, 0.15f, 0.15f), 0.025f, (PrimitiveType)0, GTPlayer.Instance.RightHand.controllerTransform, liner: true, delegate
		{
			//IL_003f: Unknown result type (might be due to invalid IL or missing references)
			//IL_004b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0050: Unknown result type (might be due to invalid IL or missing references)
			//IL_0055: Unknown result type (might be due to invalid IL or missing references)
			//IL_0059: Unknown result type (might be due to invalid IL or missing references)
			//IL_005e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0060: Unknown result type (might be due to invalid IL or missing references)
			//IL_0066: Unknown result type (might be due to invalid IL or missing references)
			Player playerFromGun = RigShit.GetPlayerFromGun();
			if (playerFromGun != null)
			{
				VRRig vRRigFromPlayer = RigShit.GetVRRigFromPlayer(playerFromGun);
				if (!((Object)(object)vRRigFromPlayer == (Object)null))
				{
					Vector3 val = default(Vector3);
					((Vector3)(ref val))._002Ector(-71.33718f, 101.4977f, -93.09029f);
					Vector3 val2 = val - vRRigFromPlayer.headMesh.transform.position;
					Vector3 normalized = ((Vector3)(ref val2)).normalized;
					SendBarrel(vRRigFromPlayer, normalized * 9998.99f);
				}
			}
		}, delegate
		{
		});
	}

	public static void UntagGun()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		MakeGun(CurrentGunColor, new Vector3(0.15f, 0.15f, 0.15f), 0.025f, (PrimitiveType)0, GTPlayer.Instance.RightHand.controllerTransform, liner: true, delegate
		{
			if (PhotonNetwork.LocalPlayer.IsMasterClient)
			{
				Player val = RigShit.GetPlayerFromGun();
				if (val == null && (Object)(object)((RaycastHit)(ref raycastHit)).collider != (Object)null)
				{
					VRRig componentInParent = ((Component)((RaycastHit)(ref raycastHit)).collider).GetComponentInParent<VRRig>();
					if ((Object)(object)componentInParent != (Object)null)
					{
						val = RigShit.GetPlayerFromVRRig(componentInParent);
					}
				}
				if (val != null)
				{
					NetPlayer val2 = NetPlayer.Get(val);
					if (val2 != null)
					{
						GorillaTagManager[] array = Object.FindObjectsByType<GorillaTagManager>((FindObjectsSortMode)0);
						foreach (GorillaTagManager val3 in array)
						{
							try
							{
								val3.currentInfected.Remove(val2);
							}
							catch
							{
							}
						}
					}
				}
			}
		}, delegate
		{
		});
	}

	public static void MatGun()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		MakeGun(CurrentGunColor, new Vector3(0.15f, 0.15f, 0.15f), 0.025f, (PrimitiveType)0, GTPlayer.Instance.RightHand.controllerTransform, liner: true, delegate
		{
			Player playerFromGun = RigShit.GetPlayerFromGun();
			NetPlayer val = null;
			if (playerFromGun != null)
			{
				val = NetPlayer.Get(playerFromGun);
			}
			else if ((Object)(object)((RaycastHit)(ref raycastHit)).collider != (Object)null)
			{
				VRRig componentInParent = ((Component)((RaycastHit)(ref raycastHit)).collider).GetComponentInParent<VRRig>();
				if ((Object)(object)componentInParent != (Object)null)
				{
					val = componentInParent.Creator;
				}
			}
			if (val != null)
			{
				MasterTag(val);
				RunViewUpdate();
			}
		}, delegate
		{
		});
	}

	public static void MasterTag(NetPlayer plr)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Expected O, but got Unknown
		GorillaTagManager val = (GorillaTagManager)GorillaGameManager.instance;
		if (Time.time > delay)
		{
			val.currentInfected.Add(plr);
			delay = Time.time + 0.25f;
			val.currentInfected.Remove(plr);
		}
	}

	public static void MyPreset()
	{
		FPSPage = true;
		change4 = 2;
		Changedisconnect();
		change7 = 2;
		Changepagebutton();
		change10 = 7;
		ThemeChangerV1();
		change11 = 0;
		ThemeChangerV2();
		change12 = 6;
		ThemeChangerV3();
		change13 = 6;
		ThemeChangerV4();
		change14 = 2;
		ThemeChangerV5();
		change15 = 1;
		ThemeChangerV6();
		change16 = 6;
		ThemeChangerV7();
		DisableButton("My Personal Preset");
	}

	public static void UnlockRoom()
	{
		if (PhotonNetwork.InRoom && PhotonNetwork.IsMasterClient)
		{
			PhotonNetwork.CurrentRoom.IsVisible = true;
			PhotonNetwork.CurrentRoom.IsOpen = true;
			GorillaScoreboardTotalUpdater.instance.UpdateActiveScoreboards();
			RPCFlush();
		}
	}

	public static void LockRoom()
	{
		if (PhotonNetwork.InRoom && PhotonNetwork.IsMasterClient)
		{
			PhotonNetwork.CurrentRoom.IsVisible = false;
			PhotonNetwork.CurrentRoom.IsOpen = false;
			GorillaScoreboardTotalUpdater.instance.UpdateActiveScoreboards();
			RPCFlush();
		}
	}

	public static void SpazRoom()
	{
		if (PhotonNetwork.InRoom && PhotonNetwork.IsMasterClient)
		{
			for (int i = 0; i < 100; i++)
			{
				PhotonNetwork.CurrentRoom.IsVisible = i % 2 == 0;
				PhotonNetwork.CurrentRoom.IsOpen = i % 2 == 0;
			}
			GorillaScoreboardTotalUpdater.instance.UpdateActiveScoreboards();
			AntiRPCKick();
			RPCFlush();
		}
	}

	public static void SetGuardianGun()
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		if (!PhotonNetwork.IsMasterClient)
		{
			NotifiLib.SendNotification("<color=red>Must be Master Client!</color>");
			return;
		}
		MakeGun(Color.green, new Vector3(0.15f, 0.15f, 0.15f), 0.025f, (PrimitiveType)0, GTPlayer.Instance.RightHand.controllerTransform, liner: true, delegate
		{
			if (!((Object)(object)((RaycastHit)(ref raycastHit)).collider == (Object)null))
			{
				VRRig componentInParent = ((Component)((RaycastHit)(ref raycastHit)).collider).GetComponentInParent<VRRig>();
				if (!((Object)(object)componentInParent == (Object)null))
				{
					NetPlayer creator = componentInParent.Creator;
					if (creator != null)
					{
						foreach (GorillaGuardianZoneManager zoneManager in GorillaGuardianZoneManager.zoneManagers)
						{
							zoneManager.SetGuardian(creator);
						}
					}
				}
			}
		}, delegate
		{
		});
		RPCFlush();
	}

	public static void RemoveGuardianGun()
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		if (!PhotonNetwork.IsMasterClient)
		{
			NotifiLib.SendNotification("<color=red>Must be Master Client!</color>");
			return;
		}
		MakeGun(Color.red, new Vector3(0.15f, 0.15f, 0.15f), 0.025f, (PrimitiveType)0, GTPlayer.Instance.RightHand.controllerTransform, liner: true, delegate
		{
			if (!((Object)(object)((RaycastHit)(ref raycastHit)).collider == (Object)null))
			{
				VRRig componentInParent = ((Component)((RaycastHit)(ref raycastHit)).collider).GetComponentInParent<VRRig>();
				if (!((Object)(object)componentInParent == (Object)null))
				{
					NetPlayer creator = componentInParent.Creator;
					if (creator != null)
					{
						foreach (GorillaGuardianZoneManager zoneManager in GorillaGuardianZoneManager.zoneManagers)
						{
							if (zoneManager.CurrentGuardian == creator)
							{
								zoneManager.SetGuardian((NetPlayer)null);
							}
						}
					}
				}
			}
		}, delegate
		{
		});
		RPCFlush();
	}

	public static void Tracers()
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		Player[] playerListOthers = PhotonNetwork.PlayerListOthers;
		foreach (Player p in playerListOthers)
		{
			VRRig vRRigFromPlayer = RigShit.GetVRRigFromPlayer(p);
			GameObject val = new GameObject("Line");
			LineRenderer val2 = val.AddComponent<LineRenderer>();
			val2.startWidth = 0.01f;
			val2.endWidth = 0.01f;
			val2.positionCount = 2;
			val2.useWorldSpace = true;
			val2.SetPosition(0, GTPlayer.Instance.RightHand.controllerTransform.position);
			val2.SetPosition(1, ((Component)vRRigFromPlayer).transform.position);
			((Renderer)val2).material.shader = Shader.Find("GUI/Text Shader");
			val2.startColor = CurrentESPColor;
			val2.endColor = CurrentESPColor;
			Object.Destroy((Object)(object)val2, Time.deltaTime);
		}
	}

	public static void FPSboost()
	{
		fps = true;
		if (fps)
		{
			QualitySettings.globalTextureMipmapLimit = 999999999;
			QualitySettings.maxQueuedFrames = 60;
			Application.targetFrameRate = 144;
		}
	}

	public static void fixFPS()
	{
		if (fps)
		{
			QualitySettings.globalTextureMipmapLimit = 0;
			QualitySettings.maxQueuedFrames = 0;
			fps = false;
		}
	}

	public static void BreakAudioGun()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		MakeGun(CurrentGunColor, new Vector3(0.15f, 0.15f, 0.15f), 0.025f, (PrimitiveType)0, GTPlayer.Instance.RightHand.controllerTransform, liner: true, delegate
		{
			if (!((Object)(object)((RaycastHit)(ref raycastHit)).collider == (Object)null))
			{
				VRRig componentInParent = ((Component)((RaycastHit)(ref raycastHit)).collider).GetComponentInParent<VRRig>();
				if (!((Object)(object)componentInParent == (Object)null))
				{
					Player playerFromVRRig = RigShit.GetPlayerFromVRRig(componentInParent);
					if (playerFromVRRig != null && Time.time > breakAudioDelay)
					{
						breakAudioDelay = Time.time + 1.5E-13f;
						try
						{
							((Component)VRRig.LocalRig).GetComponent<PhotonView>().RPC("EnableNonCosmeticHandItemRPC", (RpcTarget)0, new object[2] { true, false });
							((Component)VRRig.LocalRig).GetComponent<PhotonView>().RPC("EnableNonCosmeticHandItemRPC", (RpcTarget)0, new object[2] { false, false });
							GorillaTagger.Instance.myVRRig.SendRPC("RPC_PlayHandTap", NetPlayer.op_Implicit(RigShit.GetPlayerFromVRRig(componentInParent)), new object[3] { 111, false, 999999f });
						}
						catch
						{
						}
						AntiRPCKick();
					}
				}
			}
		}, delegate
		{
		});
	}

	public static void SetMasterClient()
	{
		if (PhotonNetwork.InRoom && !PhotonNetwork.IsMasterClient)
		{
			PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);
		}
	}

	public static void LagPlayer(VRRig player)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Expected O, but got Unknown
		if (!(Time.time <= delay))
		{
			for (int i = 0; i < 600; i++)
			{
				LoadBalancingPeer loadBalancingPeer = PhotonNetwork.NetworkingClient.LoadBalancingPeer;
				Hashtable val = new Hashtable();
				RaiseEventOptions val2 = new RaiseEventOptions();
				val2.TargetActors = new int[1] { player.Creator.ActorNumber };
				loadBalancingPeer.OpRaiseEvent((byte)3, (object)val, val2, SendOptions.SendUnreliable);
			}
			((PhotonPeer)PhotonNetwork.NetworkingClient.LoadBalancingPeer).SendOutgoingCommands();
			AntiRPCKick();
			delay = Time.time + 1.3f;
		}
	}

	public static void LagGunLD()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		MakeGun(CurrentGunColor, new Vector3(0.15f, 0.15f, 0.15f), 0.025f, (PrimitiveType)0, GTPlayer.Instance.RightHand.controllerTransform, liner: true, delegate
		{
			Player playerFromGun = RigShit.GetPlayerFromGun();
			if (playerFromGun != null)
			{
				VRRig vRRigFromPlayer = RigShit.GetVRRigFromPlayer(playerFromGun);
				if (!((Object)(object)vRRigFromPlayer == (Object)null))
				{
					LagPlayer(vRRigFromPlayer);
				}
			}
		}, delegate
		{
		});
	}

	public static void LagAll()
	{
		foreach (VRRig item in VRRigCache.ActiveRigs.Where((VRRig r) => (Object)(object)r != (Object)(object)VRRig.LocalRig))
		{
			LagPlayer(item);
		}
	}

	public static void GrabBug()
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		if (WristMenu.gripDownR)
		{
			GameObject val = GameObject.Find("Floating Bug Holdable");
			Vector3 position = GTPlayer.Instance.RightHand.controllerTransform.position;
			val.transform.position = position;
			val.transform.SetParent(GTPlayer.Instance.RightHand.controllerTransform);
		}
		if (WristMenu.gripDownL)
		{
			GameObject val2 = GameObject.Find("Floating Bug Holdable");
			Vector3 position2 = GTPlayer.Instance.LeftHand.controllerTransform.position;
			val2.transform.position = position2;
			val2.transform.SetParent(GTPlayer.Instance.LeftHand.controllerTransform);
		}
	}

	public static void GrabBat()
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		if (WristMenu.gripDownR)
		{
			GameObject val = GameObject.Find("Cave Bat Holdable");
			Vector3 position = GTPlayer.Instance.RightHand.controllerTransform.position;
			val.transform.position = position;
			val.transform.SetParent(GTPlayer.Instance.RightHand.controllerTransform);
		}
		if (WristMenu.gripDownL)
		{
			GameObject val2 = GameObject.Find("Cave Bat Holdable");
			Vector3 position2 = GTPlayer.Instance.LeftHand.controllerTransform.position;
			val2.transform.position = position2;
			val2.transform.SetParent(GTPlayer.Instance.LeftHand.controllerTransform);
		}
	}

	public static void GrabFirefly()
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		if (WristMenu.gripDownR)
		{
			GameObject val = GameObject.Find("Firefly");
			Vector3 position = GTPlayer.Instance.RightHand.controllerTransform.position;
			val.transform.position = position;
			val.transform.SetParent(GTPlayer.Instance.RightHand.controllerTransform);
		}
		if (WristMenu.gripDownL)
		{
			GameObject val2 = GameObject.Find("Firefly");
			Vector3 position2 = GTPlayer.Instance.LeftHand.controllerTransform.position;
			val2.transform.position = position2;
			val2.transform.SetParent(GTPlayer.Instance.LeftHand.controllerTransform);
		}
	}

	public static void SoundSpammer(int num)
	{
		if ((((ControllerInputPoller)ControllerInputPoller.instance).rightGrab || ((ControllerInputPoller)ControllerInputPoller.instance).leftGrab) && Time.time > delay)
		{
			delay = Time.time + 0.1f;
			if (PhotonNetwork.InRoom)
			{
				GorillaTagger.Instance.myVRRig.SendRPC("RPC_PlayHandTap", (RpcTarget)0, new object[3] { num, false, 999999f });
			}
			else
			{
				GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(num, false, 999999f);
			}
		}
		RPCFlush();
	}

	public static void BassSoundSpam()
	{
		SoundSpammer(68);
	}

	public static void MetalSoundSpam()
	{
		SoundSpammer(18);
	}

	public static void WolfSoundSpam()
	{
		SoundSpammer(195);
	}

	public static void CatSoundSpam()
	{
		SoundSpammer(236);
	}

	public static void TurkeySoundSpam()
	{
		SoundSpammer(83);
	}

	public static void FrogSoundSpam()
	{
		SoundSpammer(91);
	}

	public static void BeeSoundSpam()
	{
		SoundSpammer(191);
	}

	public static void SqueakSoundSpam()
	{
		SoundSpammer(215);
	}

	public static void EarrapeSoundSpam()
	{
		SoundSpammer(215);
	}

	public static void DingSoundSpam()
	{
		SoundSpammer(244);
	}

	public static void BigCrystalSoundSpam()
	{
		SoundSpammer(213);
	}

	public static void PanSoundSpam()
	{
		SoundSpammer(248);
	}

	public static void AK47SoundSpam()
	{
		SoundSpammer(203);
	}

	public static void TickSoundSpam()
	{
		SoundSpammer(148);
	}

	public static void PianoSoundSpam()
	{
		if ((((ControllerInputPoller)ControllerInputPoller.instance).rightGrab || ((ControllerInputPoller)ControllerInputPoller.instance).leftGrab) && Time.time > delay)
		{
			delay = Time.time + 0.1f;
			int num = Random.Range(295, 308);
			if (PhotonNetwork.InRoom)
			{
				GorillaTagger.Instance.myVRRig.SendRPC("RPC_PlayHandTap", (RpcTarget)0, new object[3] { num, false, 999999f });
			}
			else
			{
				GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(num, false, 999999f);
			}
		}
		RPCFlush();
	}

	public static void RandomSoundSpam()
	{
		if ((((ControllerInputPoller)ControllerInputPoller.instance).rightGrab || ((ControllerInputPoller)ControllerInputPoller.instance).leftGrab) && Time.time > delay)
		{
			delay = Time.time + 0.1f;
			int num = Random.Range(0, 350);
			if (PhotonNetwork.InRoom)
			{
				GorillaTagger.Instance.myVRRig.SendRPC("RPC_PlayHandTap", (RpcTarget)0, new object[3] { num, false, 999999f });
			}
			else
			{
				GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(num, false, 999999f);
			}
		}
		RPCFlush();
	}

	public static void CrystalSoundSpam()
	{
		int[] array = new int[5] { 213, 214, 215, 216, 217 };
		if ((((ControllerInputPoller)ControllerInputPoller.instance).rightGrab || ((ControllerInputPoller)ControllerInputPoller.instance).leftGrab) && Time.time > delay)
		{
			delay = Time.time + 0.15f;
			int num = array[Random.Range(0, array.Length)];
			if (PhotonNetwork.InRoom)
			{
				GorillaTagger.Instance.myVRRig.SendRPC("RPC_PlayHandTap", (RpcTarget)0, new object[3] { num, false, 999999f });
			}
			else
			{
				GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(num, false, 999999f);
			}
		}
		RPCFlush();
	}

	public static void SirenSoundSpam()
	{
		int[] array = new int[4] { 250, 251, 252, 253 };
		if ((((ControllerInputPoller)ControllerInputPoller.instance).rightGrab || ((ControllerInputPoller)ControllerInputPoller.instance).leftGrab) && Time.time > delay)
		{
			delay = Time.time + 0.2f;
			int num = array[Random.Range(0, array.Length)];
			if (PhotonNetwork.InRoom)
			{
				GorillaTagger.Instance.myVRRig.SendRPC("RPC_PlayHandTap", (RpcTarget)0, new object[3] { num, false, 999999f });
			}
			else
			{
				GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(num, false, 999999f);
			}
		}
		RPCFlush();
	}

	public static void VirtualStumpKickGun()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		MakeGun(CurrentGunColor, new Vector3(0.15f, 0.15f, 0.15f), 0.025f, (PrimitiveType)0, GTPlayer.Instance.RightHand.controllerTransform, liner: true, delegate
		{
			if (PhotonNetwork.IsMasterClient)
			{
				Player val = RigShit.GetPlayerFromGun();
				if (val == null && (Object)(object)((RaycastHit)(ref raycastHit)).collider != (Object)null)
				{
					VRRig componentInParent = ((Component)((RaycastHit)(ref raycastHit)).collider).GetComponentInParent<VRRig>();
					if ((Object)(object)componentInParent != (Object)null)
					{
						val = RigShit.GetPlayerFromVRRig(componentInParent);
					}
				}
				if (val != null)
				{
					BuilderTableNetworking terminalNetwork = GetTerminalNetwork();
					if ((Object)(object)((terminalNetwork != null) ? ((MonoBehaviourPun)terminalNetwork).photonView : null) != (Object)null)
					{
						try
						{
							((MonoBehaviourPun)terminalNetwork).photonView.RPC("SetRoomMap_RPC", val, new object[1] { -1 });
							RPCFlush();
						}
						catch
						{
						}
					}
				}
			}
		}, delegate
		{
		});
	}

	public static void VirtualStumpKickAll()
	{
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		BuilderTableNetworking terminalNetwork = GetTerminalNetwork();
		foreach (VRRig activeRig in VRRigCache.ActiveRigs)
		{
			if (!activeRig.isOfflineVRRig)
			{
				NetPlayer val = activeRig.Creator ?? NetworkSystem.Instance.GetPlayer(NetworkSystem.Instance.GetOwningPlayerID(((Component)activeRig).gameObject));
				Player playerRef = val.GetPlayerRef();
				((MonoBehaviourPun)terminalNetwork).photonView.RPC("SetRoomMap_RPC", playerRef, new object[1] { -1 });
				RPCFlush();
			}
		}
	}

	private static BuilderTableNetworking GetTerminalNetwork()
	{
		GameObject obj = GameObject.Find("Environment Objects/MonkeBlocksRoomPersistent/BuilderNetworking");
		return (obj != null) ? obj.GetComponent<BuilderTableNetworking>() : null;
	}

	public static void SlowPlayer(VRRig Player)
	{
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Expected O, but got Unknown
		if (PhotonNetwork.LocalPlayer.IsMasterClient)
		{
			byte b = 2;
			Type type = Type.GetType("RoomSystem");
			if (type != null)
			{
				FieldInfo field = type.GetField("statusSendData", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				Type nestedType = type.GetNestedType("StatusEffects", BindingFlags.Public | BindingFlags.NonPublic);
				object obj = Enum.Parse(nestedType, "TaggedTime");
				object[] array = field.GetValue(null) as object[];
				array[0] = obj;
				MethodInfo method = type.GetMethod("SendEvent", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				if (method != null)
				{
					object[] obj2 = new object[4] { b, array, null, null };
					NetEventOptions val = new NetEventOptions();
					val.TargetActors = new int[1] { Player.OwningNetPlayer.ActorNumber };
					obj2[2] = val;
					obj2[3] = false;
					method.Invoke(null, obj2);
				}
			}
		}
		RPCFlush();
	}

	public static void SlowGun()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		MakeGun(Color.yellow, new Vector3(0.15f, 0.15f, 0.15f), 0.025f, (PrimitiveType)0, GTPlayer.Instance.RightHand.controllerTransform, liner: true, delegate
		{
			if (PhotonNetwork.IsMasterClient && !((Object)(object)((RaycastHit)(ref raycastHit)).collider == (Object)null))
			{
				VRRig val = ((Component)((RaycastHit)(ref raycastHit)).collider).GetComponentInParent<VRRig>();
				if ((Object)(object)val == (Object)null)
				{
					Player playerFromGun = RigShit.GetPlayerFromGun();
					if (playerFromGun != null)
					{
						val = RigShit.GetVRRigFromPlayer(playerFromGun);
					}
				}
				if ((Object)(object)val != (Object)null)
				{
					SlowPlayer(val);
				}
			}
		}, delegate
		{
		});
	}

	public static void SpawnBlock(int id, Vector3 position, Quaternion rotation)
	{
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		if (!PhotonNetwork.IsMasterClient)
		{
			NotifiLib.SendNotification("<color=red>You Must Be Master Client To Use This!</color>");
		}
		else if (PhotonNetwork.InRoom)
		{
			BuilderTable component = GameObject.Find("Environment Objects/MonkeBlocksRoomPersistent/BuilderTable").GetComponent<BuilderTable>();
			BuilderTableNetworking component2 = GameObject.Find("Environment Objects/MonkeBlocksRoomPersistent/BuilderNetworking").GetComponent<BuilderTableNetworking>();
			((MonoBehaviourPun)component2).photonView.RPC("PieceCreatedByShelfRPC", (RpcTarget)0, new object[8]
			{
				id,
				component.CreatePieceId(),
				BitPackUtils.PackWorldPosForNetwork(position),
				BitPackUtils.PackQuaternionForNetwork(rotation),
				0,
				(byte)4,
				1,
				PhotonNetwork.LocalPlayer
			});
		}
	}

	public static void DestroyBlock(int id, Vector3 position, Quaternion rotation, bool PlaySfx)
	{
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		if (!PhotonNetwork.IsMasterClient)
		{
			NotifiLib.SendNotification("<color=red>You Must Be Master Client To Use This!</color>");
		}
		else if (PhotonNetwork.InRoom)
		{
			BuilderTableNetworking component = GameObject.Find("Environment Objects/MonkeBlocksRoomPersistent/BuilderNetworking").GetComponent<BuilderTableNetworking>();
			((MonoBehaviourPun)component).photonView.RPC("PieceDestroyedRPC", (RpcTarget)0, new object[5]
			{
				id,
				BitPackUtils.PackWorldPosForNetwork(position),
				BitPackUtils.PackQuaternionForNetwork(rotation),
				PlaySfx,
				(short)1
			});
			RPCFlush();
		}
	}

	public static int GetRandomId()
	{
		return blockIds[random.Next(blockIds.Count)];
	}

	public static void BringPlayer(VRRig player)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		if (PhotonNetwork.IsMasterClient && !((Object)(object)player == (Object)null))
		{
			Vector3 position = ((Component)VRRig.LocalRig).transform.position;
			Vector3 position2 = ((Component)player).transform.position;
			float num = Vector3.Distance(position2, position);
			int num2 = Mathf.FloorToInt(num / 0.5f);
			for (int i = 0; i <= num2; i++)
			{
				float num3 = (float)i / (float)Mathf.Max(num2, 1);
				Vector3 position3 = Vector3.Lerp(position2, position, num3);
				position3.y = position.y;
				SpawnBlock(1858470402, position3, Quaternion.identity);
			}
			for (int j = 0; j < 6; j++)
			{
				float num4 = (float)j * 60f * ((float)Math.PI / 180f);
				Vector3 position4 = position2 + new Vector3(Mathf.Cos(num4), 1f, Mathf.Sin(num4)) * 0.5f;
				SpawnBlock(GetRandomId(), position4, Quaternion.identity);
			}
			RPCFlush();
		}
	}

	public static void FlingPlayer(VRRig player)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		if (PhotonNetwork.IsMasterClient && !((Object)(object)player == (Object)null))
		{
			Vector3 val = ((Component)player).transform.position - ((Component)VRRig.LocalRig).transform.position;
			Vector3 val2 = ((Vector3)(ref val)).normalized;
			if (val2 == Vector3.zero)
			{
				val2 = Random.onUnitSphere;
			}
			for (int i = 0; i < 12; i++)
			{
				Vector3 position = ((Component)player).transform.position + val2 * ((float)i * 0.3f);
				SpawnBlock(GetRandomId(), position, Random.rotation);
			}
			for (int j = 1; j <= 5; j++)
			{
				Vector3 position2 = ((Component)player).transform.position + val2 * (float)j;
				SpawnBlock(-1447051713, position2, Quaternion.identity);
			}
			RPCFlush();
		}
	}

	public static void FloatPlayer(VRRig player)
	{
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		if (!PhotonNetwork.IsMasterClient || (Object)(object)player == (Object)null)
		{
			return;
		}
		Vector3 val = default(Vector3);
		for (int i = -1; i <= 1; i++)
		{
			for (int j = -1; j <= 1; j++)
			{
				((Vector3)(ref val))._002Ector((float)i * 0.3f, -0.5f, (float)j * 0.3f);
				SpawnBlock(1858470402, ((Component)player).transform.position + val, Quaternion.identity);
			}
		}
		SpawnBlock(-1447051713, ((Component)player).transform.position + Vector3.up * 0.2f, Quaternion.identity);
		RPCFlush();
	}

	public static void FreezePlayer(VRRig player)
	{
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		if (!PhotonNetwork.IsMasterClient || (Object)(object)player == (Object)null)
		{
			return;
		}
		float num = 1f;
		float num2 = 0.15f;
		Vector3 val = default(Vector3);
		for (float num3 = 0f - num; num3 <= num; num3 += num2)
		{
			float num4 = Mathf.Sqrt(num * num - num3 * num3);
			for (float num5 = 0f; num5 < 360f; num5 += 20f)
			{
				float num6 = num5 * ((float)Math.PI / 180f);
				((Vector3)(ref val))._002Ector(Mathf.Cos(num6) * num4, num3, Mathf.Sin(num6) * num4);
				SpawnBlock(857098599, ((Component)player).transform.position + val, Quaternion.identity);
			}
		}
		RPCFlush();
	}

	public static void Gun(Color color, Action onTrigger)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		MakeGun(color, new Vector3(0.15f, 0.15f, 0.15f), 0.025f, (PrimitiveType)0, GTPlayer.Instance.RightHand.controllerTransform, liner: true, delegate
		{
			VRRig aimedRig = GetAimedRig();
			if ((Object)(object)aimedRig != (Object)null)
			{
				onTrigger?.Invoke();
			}
		}, delegate
		{
		});
	}

	public static VRRig GetAimedRig()
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		VRRig result = null;
		float num = 0.6f;
		foreach (VRRig activeRig in VRRigCache.ActiveRigs)
		{
			if (!((Object)(object)activeRig == (Object)(object)VRRig.LocalRig))
			{
				float num2 = Vector3.Distance(((Component)activeRig).transform.position, ((Component)VRRig.LocalRig).transform.position);
				if (num2 < num)
				{
					num = num2;
					result = activeRig;
				}
			}
		}
		return result;
	}

	public static void BlockBringGun()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		MakeGun(Color.cyan, new Vector3(0.15f, 0.15f, 0.15f), 0.025f, (PrimitiveType)0, GTPlayer.Instance.RightHand.controllerTransform, liner: true, delegate
		{
			if (PhotonNetwork.IsMasterClient)
			{
				VRRig val = null;
				if ((Object)(object)((RaycastHit)(ref raycastHit)).collider != (Object)null)
				{
					val = ((Component)((RaycastHit)(ref raycastHit)).collider).GetComponentInParent<VRRig>();
				}
				if ((Object)(object)val == (Object)null)
				{
					Player playerFromGun = RigShit.GetPlayerFromGun();
					if (playerFromGun != null)
					{
						val = RigShit.GetVRRigFromPlayer(playerFromGun);
					}
				}
				if ((Object)(object)val != (Object)null)
				{
					BringPlayer(val);
				}
			}
		}, delegate
		{
		});
	}

	public static void BlockFlingGun()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		MakeGun(Color.red, new Vector3(0.15f, 0.15f, 0.15f), 0.025f, (PrimitiveType)0, GTPlayer.Instance.RightHand.controllerTransform, liner: true, delegate
		{
			if (PhotonNetwork.IsMasterClient)
			{
				VRRig val = null;
				if ((Object)(object)((RaycastHit)(ref raycastHit)).collider != (Object)null)
				{
					val = ((Component)((RaycastHit)(ref raycastHit)).collider).GetComponentInParent<VRRig>();
				}
				if ((Object)(object)val == (Object)null)
				{
					Player playerFromGun = RigShit.GetPlayerFromGun();
					if (playerFromGun != null)
					{
						val = RigShit.GetVRRigFromPlayer(playerFromGun);
					}
				}
				if ((Object)(object)val != (Object)null)
				{
					FlingPlayer(val);
				}
			}
		}, delegate
		{
		});
	}

	public static void BlockFloatGun()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		MakeGun(Color.blue, new Vector3(0.15f, 0.15f, 0.15f), 0.025f, (PrimitiveType)0, GTPlayer.Instance.RightHand.controllerTransform, liner: true, delegate
		{
			if (PhotonNetwork.IsMasterClient)
			{
				VRRig val = null;
				if ((Object)(object)((RaycastHit)(ref raycastHit)).collider != (Object)null)
				{
					val = ((Component)((RaycastHit)(ref raycastHit)).collider).GetComponentInParent<VRRig>();
				}
				if ((Object)(object)val == (Object)null)
				{
					Player playerFromGun = RigShit.GetPlayerFromGun();
					if (playerFromGun != null)
					{
						val = RigShit.GetVRRigFromPlayer(playerFromGun);
					}
				}
				if ((Object)(object)val != (Object)null)
				{
					FloatPlayer(val);
				}
			}
		}, delegate
		{
		});
	}

	public static void BlockFreezeGun()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		MakeGun(Color.blue, new Vector3(0.15f, 0.15f, 0.15f), 0.025f, (PrimitiveType)0, GTPlayer.Instance.RightHand.controllerTransform, liner: true, delegate
		{
			if (PhotonNetwork.IsMasterClient)
			{
				VRRig val = null;
				if ((Object)(object)((RaycastHit)(ref raycastHit)).collider != (Object)null)
				{
					val = ((Component)((RaycastHit)(ref raycastHit)).collider).GetComponentInParent<VRRig>();
				}
				if ((Object)(object)val == (Object)null)
				{
					Player playerFromGun = RigShit.GetPlayerFromGun();
					if (playerFromGun != null)
					{
						val = RigShit.GetVRRigFromPlayer(playerFromGun);
					}
				}
				if ((Object)(object)val != (Object)null)
				{
					FreezePlayer(val);
				}
			}
		}, delegate
		{
		});
	}

	public static void BlockCrashGun()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		MakeGun(Color.yellow, new Vector3(0.15f, 0.15f, 0.15f), 0.025f, (PrimitiveType)0, GTPlayer.Instance.RightHand.controllerTransform, liner: true, delegate
		{
			//IL_007c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0081: Unknown result type (might be due to invalid IL or missing references)
			if (PhotonNetwork.IsMasterClient)
			{
				VRRig val = null;
				if ((Object)(object)((RaycastHit)(ref raycastHit)).collider != (Object)null)
				{
					val = ((Component)((RaycastHit)(ref raycastHit)).collider).GetComponentInParent<VRRig>();
				}
				if ((Object)(object)val == (Object)null)
				{
					Player playerFromGun = RigShit.GetPlayerFromGun();
					if (playerFromGun != null)
					{
						val = RigShit.GetVRRigFromPlayer(playerFromGun);
					}
				}
				if (!((Object)(object)val == (Object)null))
				{
					SpawnBlock(-1447051713, ((Component)val).transform.position, Quaternion.identity);
					if ((Object)(object)piece == (Object)null)
					{
						piece = Object.FindObjectOfType<BuilderPiece>();
					}
					if ((Object)(object)piece != (Object)null && (piece.pieceType == -1447051713 || piece.pieceId == -1447051713))
					{
						((Component)piece).gameObject.SetActive(false);
					}
					RPCFlush();
				}
			}
		}, delegate
		{
		});
	}

	public static void BlockDrawGun()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		Gun(Color.green, delegate
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			SpawnBlock(GetRandomId(), ((Component)VRRig.LocalRig).transform.position, Quaternion.identity);
			RPCFlush();
		});
	}

	public static void BlockBringAll()
	{
		foreach (VRRig activeRig in VRRigCache.ActiveRigs)
		{
			if ((Object)(object)activeRig != (Object)(object)VRRig.LocalRig)
			{
				BringPlayer(activeRig);
			}
		}
	}

	public static void BlockFlingAll()
	{
		foreach (VRRig activeRig in VRRigCache.ActiveRigs)
		{
			if ((Object)(object)activeRig != (Object)(object)VRRig.LocalRig)
			{
				FlingPlayer(activeRig);
			}
		}
	}

	public static void BlockFloatAll()
	{
		foreach (VRRig activeRig in VRRigCache.ActiveRigs)
		{
			if ((Object)(object)activeRig != (Object)(object)VRRig.LocalRig)
			{
				FloatPlayer(activeRig);
			}
		}
	}

	public static void BlockFreezeAll()
	{
		foreach (VRRig activeRig in VRRigCache.ActiveRigs)
		{
			if ((Object)(object)activeRig != (Object)(object)VRRig.LocalRig)
			{
				FreezePlayer(activeRig);
			}
		}
	}

	public static void RecycleAllBlocks()
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		BuilderPiece[] array = Object.FindObjectsOfType<BuilderPiece>();
		foreach (BuilderPiece val in array)
		{
			if ((Object)(object)val != (Object)null)
			{
				DestroyBlock(val.pieceId, ((Component)val).transform.position, ((Component)val).transform.rotation, PlaySfx: false);
			}
			RPCFlush();
		}
	}

	public static List<T> GetAllType<T>() where T : Object
	{
		return Resources.FindObjectsOfTypeAll<T>().ToList();
	}

	public static void BlockCrashAll()
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		if (!PhotonNetwork.IsMasterClient)
		{
			NotifiLib.SendNotification("<color=red>You Must Be Master Client To Use This!</color>");
			return;
		}
		SpawnBlock(-1447051713, ((Component)VRRig.LocalRig).transform.position, Quaternion.identity);
		if ((Object)(object)piece == (Object)null)
		{
			piece = Object.FindObjectOfType<BuilderPiece>();
		}
		if ((Object)(object)piece != (Object)null && (piece.pieceType == -1447051713 || piece.pieceId == -1447051713))
		{
			((Component)piece).gameObject.SetActive(false);
		}
		RPCFlush();
	}

	public static void BlockDestroyGun()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		MakeGun(CurrentGunColor, new Vector3(0.15f, 0.15f, 0.15f), 0.025f, (PrimitiveType)0, GTPlayer.Instance.RightHand.controllerTransform, liner: true, delegate
		{
			//IL_004c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0051: Unknown result type (might be due to invalid IL or missing references)
			if (PhotonNetwork.IsMasterClient && !((Object)(object)((RaycastHit)(ref raycastHit)).collider == (Object)null))
			{
				BuilderPiece componentInParent = ((Component)((RaycastHit)(ref raycastHit)).collider).GetComponentInParent<BuilderPiece>();
				if ((Object)(object)componentInParent != (Object)null)
				{
					DestroyBlock(componentInParent.pieceId, ((RaycastHit)(ref raycastHit)).point, Quaternion.identity, PlaySfx: true);
					RPCFlush();
				}
			}
		}, delegate
		{
		});
	}

	public static void RandomBlockGun()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		MakeGun(CurrentGunColor, new Vector3(0.15f, 0.15f, 0.15f), 0.025f, (PrimitiveType)0, GTPlayer.Instance.RightHand.controllerTransform, liner: true, delegate
		{
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			//IL_002e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0034: Unknown result type (might be due to invalid IL or missing references)
			//IL_0035: Unknown result type (might be due to invalid IL or missing references)
			Vector3 position = (((Object)(object)((RaycastHit)(ref raycastHit)).collider != (Object)null) ? ((RaycastHit)(ref raycastHit)).point : ((Component)VRRig.LocalRig).transform.position);
			SpawnBlock(GetRandomId(), position, Quaternion.identity);
			RPCFlush();
		}, delegate
		{
		});
	}

	public static void UngrabbableBlockGun()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		MakeGun(CurrentGunColor, new Vector3(0.15f, 0.15f, 0.15f), 0.025f, (PrimitiveType)0, GTPlayer.Instance.RightHand.controllerTransform, liner: true, delegate
		{
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			//IL_002e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0034: Unknown result type (might be due to invalid IL or missing references)
			//IL_0035: Unknown result type (might be due to invalid IL or missing references)
			Vector3 position = (((Object)(object)((RaycastHit)(ref raycastHit)).collider != (Object)null) ? ((RaycastHit)(ref raycastHit)).point : ((Component)VRRig.LocalRig).transform.position);
			SpawnBlock(1858470402, position, Quaternion.identity);
			RPCFlush();
		}, delegate
		{
		});
	}

	public static void DestroyBlockGun()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		MakeGun(CurrentGunColor, new Vector3(0.15f, 0.15f, 0.15f), 0.025f, (PrimitiveType)0, GTPlayer.Instance.RightHand.controllerTransform, liner: true, delegate
		{
			//IL_004e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0059: Unknown result type (might be due to invalid IL or missing references)
			if (PhotonNetwork.IsMasterClient && !((Object)(object)((RaycastHit)(ref raycastHit)).collider == (Object)null))
			{
				BuilderPiece componentInParent = ((Component)((RaycastHit)(ref raycastHit)).collider).GetComponentInParent<BuilderPiece>();
				if (!((Object)(object)componentInParent == (Object)null))
				{
					DestroyBlock(componentInParent.pieceId, ((Component)componentInParent).transform.position, ((Component)componentInParent).transform.rotation, PlaySfx: true);
					RPCFlush();
				}
			}
		}, delegate
		{
		});
	}

	public static void Ghostmonke()
	{
		bool flag = (right ? WristMenu.ybuttonDown : WristMenu.bbuttonDown);
		if (flag && !lastHit)
		{
			ghostMonke = !ghostMonke;
		}
		lastHit = flag;
		((Behaviour)VRRig.LocalRig).enabled = !ghostMonke;
		if (ghostMonke)
		{
			DrawHandOrbs();
		}
	}

	public static void InvisMonke()
	{
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		VRRig ownVRRig = RigShit.GetOwnVRRig();
		if (!((Object)(object)ownVRRig == (Object)null))
		{
			bool flag = (right ? WristMenu.ybuttonDown : WristMenu.bbuttonDown);
			if (flag && !last2)
			{
				invisMonke = !invisMonke;
			}
			last2 = flag;
			((Behaviour)ownVRRig).enabled = !invisMonke;
			if (invisMonke)
			{
				((Component)ownVRRig).transform.position = new Vector3(0f, -100f, 0f);
				DrawHandOrbs();
			}
		}
	}

	public static async void CreateLobby(string n, string b)
	{
		RoomConfig val = new RoomConfig();
		val.createIfMissing = true;
		val.isJoinable = true;
		val.isPublic = true;
		val.MaxPlayers = 10;
		Hashtable val2 = new Hashtable();
		((Dictionary<object, object>)(object)val2).Add("gameMode", ((Object)((PhotonNetworkController)PhotonNetworkController.Instance).currentJoinTrigger == (Object)null || ((PhotonNetworkController)PhotonNetworkController.Instance).currentJoinTrigger.networkZone == "private") ? ((from e in Mods.F<GorillaNetworkJoinTrigger>(5f)
			orderby Vector3.Distance(((Component)GorillaTagger.Instance.myVRRig).transform.position, ((Component)e).gameObject.transform.position)
			select e).First().networkZone + "|" + ((GorillaComputer)GorillaComputer.instance).currentQueue + "|" + ((object)((GorillaComputer)GorillaComputer.instance).currentGameMode).ToString()) : ((PhotonNetworkController)PhotonNetworkController.Instance).currentJoinTrigger.GetFullDesiredGameModeString());
		((Dictionary<object, object>)(object)val2).Add("platform", ((object)PhotonNetworkController.Instance).GetType().GetField("fieldName", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)?.GetValue(PhotonNetworkController.Instance)?.ToString());
		((Dictionary<object, object>)(object)val2).Add("queueName", ((GorillaComputer)GorillaComputer.instance).currentQueue);
		string language;
		try
		{
			Type locType = (AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault((Assembly a) => a.GetName().Name == "Assembly-CSharp") ?? Assembly.GetExecutingAssembly())?.GetType("LocalisationManager");
			language = ((!(locType != null)) ? CultureInfo.CurrentCulture.Name : ((locType.GetProperty("CurrentLanguage", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)?.GetValue(null))?.ToString() ?? CultureInfo.CurrentCulture.Name));
		}
		catch
		{
			language = CultureInfo.CurrentCulture.Name;
		}
		((Dictionary<object, object>)(object)val2).Add((object)"language", (object)language);
		((Dictionary<object, object>)(object)val2).Add("fan_club", SubscriptionManager.IsLocalSubscribed().ToString().ToLower());
		val.CustomProps = val2;
		RoomConfig r = val;
		await NetworkSystem.Instance.ConnectToRoom(n, r, -1);
		string s = (b.Contains("Rainbow Lobby") ? "Create Rainbow Leaderboard Lobby" : (b.Contains("Big Rainbow Lobby") ? "Create Big Rainbow Leaderboard Lobby" : (b.Contains("Fake Ban") ? "Create Fake Ban Lobby" : (b.Contains("Menu") ? "Create Menu Lobby" : (b.Contains("Best Menu") ? "Create Big Menu Is Best Lobby" : "Public Lobby")))));
		NotifiLib.SendNotification("[<color=blue>ROOM</color>] Creating A " + s + "!");
		DisableButton("Create " + b + " Lobby" + ((b == "Public") ? "" : " (SS)"));
	}

	public static IEnumerable<T> F<T>(float maxDistance) where T : Object
	{
		return Object.FindObjectsOfType<T>().Where(delegate(T obj)
		{
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			//IL_001e: Unknown result type (might be due to invalid IL or missing references)
			Vector3 position = ((Component)GTPlayer.Instance).transform.position;
			object obj2 = obj;
			object obj3 = ((obj2 is Component) ? obj2 : null);
			return Vector3.Distance(position, (obj3 != null) ? ((Component)obj3).transform.position : Vector3.zero) <= maxDistance;
		});
	}

	public static bool CosDone()
	{
		return false;
	}

	public static void VibratePlayer(VRRig Player)
	{
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Expected O, but got Unknown
		if (PhotonNetwork.LocalPlayer.IsMasterClient)
		{
			byte b = 2;
			Type type = Type.GetType("RoomSystem");
			if (type != null)
			{
				FieldInfo field = type.GetField("statusSendData", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				Type nestedType = type.GetNestedType("StatusEffects", BindingFlags.Public | BindingFlags.NonPublic);
				object obj = Enum.Parse(nestedType, "JoinedTaggedTime");
				object[] array = field.GetValue(null) as object[];
				array[0] = obj;
				MethodInfo method = type.GetMethod("SendEvent", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				if (method != null)
				{
					object[] obj2 = new object[4] { b, array, null, null };
					NetEventOptions val = new NetEventOptions();
					val.TargetActors = new int[1] { Player.OwningNetPlayer.ActorNumber };
					obj2[2] = val;
					obj2[3] = false;
					method.Invoke(null, obj2);
				}
			}
		}
		RPCFlush();
	}

	public static void VibrateGun()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		MakeGun(CurrentGunColor, new Vector3(0.15f, 0.15f, 0.15f), 0.025f, (PrimitiveType)0, GTPlayer.Instance.RightHand.controllerTransform, liner: true, delegate
		{
			VRRig val = null;
			if ((Object)(object)((RaycastHit)(ref raycastHit)).collider != (Object)null)
			{
				val = ((Component)((RaycastHit)(ref raycastHit)).collider).GetComponentInParent<VRRig>();
			}
			if ((Object)(object)val == (Object)null)
			{
				Player playerFromGun = RigShit.GetPlayerFromGun();
				if (playerFromGun != null)
				{
					val = RigShit.GetVRRigFromPlayer(playerFromGun);
				}
			}
			if ((Object)(object)val != (Object)null)
			{
				VibratePlayer(val);
			}
		}, delegate
		{
		});
	}

	public static void ProjectileGun()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		MakeGun(CurrentGunColor, new Vector3(0.15f, 0.15f, 0.15f), 0.025f, (PrimitiveType)0, GTPlayer.Instance.RightHand.controllerTransform, liner: true, delegate
		{
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			//IL_003e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0048: Unknown result type (might be due to invalid IL or missing references)
			//IL_004d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0052: Unknown result type (might be due to invalid IL or missing references)
			//IL_005d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0067: Unknown result type (might be due to invalid IL or missing references)
			//IL_006c: Unknown result type (might be due to invalid IL or missing references)
			//IL_006d: Unknown result type (might be due to invalid IL or missing references)
			//IL_006e: Unknown result type (might be due to invalid IL or missing references)
			if (!((Object)(object)pointer == (Object)null) && !wasTriggering)
			{
				wasTriggering = true;
				Vector3 pos = pointer.transform.position + pointer.transform.forward * 0.1f;
				Vector3 vel = pointer.transform.forward * 30f;
				Projectiles.ShootSnowball(pos, vel);
			}
		}, delegate
		{
			wasTriggering = false;
		});
	}

	public static void AntiReport()
	{
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			foreach (GorillaPlayerScoreboardLine allScoreboardLine in GorillaScoreboardTotalUpdater.allScoreboardLines)
			{
				if (allScoreboardLine.linePlayer != NetworkSystem.Instance.LocalPlayer)
				{
					continue;
				}
				Transform transform = ((Component)allScoreboardLine.reportButton).gameObject.transform;
				foreach (VRRig activeRig in VRRigCache.ActiveRigs)
				{
					if ((Object)(object)activeRig != (Object)(object)GorillaTagger.Instance.offlineVRRig)
					{
						float num = Vector3.Distance(activeRig.rightHandTransform.position, transform.position);
						float num2 = Vector3.Distance(activeRig.leftHandTransform.position, transform.position);
						float num3 = 0.5f;
						if (num < num3 || num2 < num3)
						{
							PhotonNetwork.Disconnect();
						}
					}
				}
			}
		}
		catch
		{
		}
	}

	public static void Save1()
	{
		List<string> list = new List<string>();
		foreach (ButtonInfo item in WristMenu.CatButtons1)
		{
			if (item.enabled == true)
			{
				list.Add(item.buttonText);
			}
		}
		foreach (ButtonInfo item2 in WristMenu.CatButtons2)
		{
			if (item2.enabled == true)
			{
				list.Add(item2.buttonText);
			}
		}
		foreach (ButtonInfo item3 in WristMenu.CatButtons3)
		{
			if (item3.enabled == true)
			{
				list.Add(item3.buttonText);
			}
		}
		foreach (ButtonInfo item4 in WristMenu.CatButtons4)
		{
			if (item4.enabled == true)
			{
				list.Add(item4.buttonText);
			}
		}
		foreach (ButtonInfo item5 in WristMenu.CatButtons5)
		{
			if (item5.enabled == true)
			{
				list.Add(item5.buttonText);
			}
		}
		foreach (ButtonInfo item6 in WristMenu.CatButtons6)
		{
			if (item6.enabled == true)
			{
				list.Add(item6.buttonText);
			}
		}
		foreach (ButtonInfo item7 in WristMenu.CatButtons7)
		{
			if (item7.enabled == true)
			{
				list.Add(item7.buttonText);
			}
		}
		foreach (ButtonInfo item8 in WristMenu.CatButtons8)
		{
			if (item8.enabled == true)
			{
				list.Add(item8.buttonText);
			}
		}
		foreach (ButtonInfo item9 in WristMenu.CatButtons9)
		{
			if (item9.enabled == true)
			{
				list.Add(item9.buttonText);
			}
		}
		foreach (ButtonInfo item10 in WristMenu.CatButtons10)
		{
			if (item10.enabled == true)
			{
				list.Add(item10.buttonText);
			}
		}
		File.WriteAllLines(WristMenu.FolderName + "\\Saved_Buttons.txt", list);
		NotifiLib.SendNotification("<color=white>[</color><color=blue>SAVE</color><color=white>]</color> <color=white>Saved Buttons Successfully!</color>");
	}

	public static void Load1()
	{
		string[] array = File.ReadAllLines(WristMenu.FolderName + "\\Saved_Buttons.txt");
		string[] array2 = array;
		foreach (string text in array2)
		{
			foreach (ButtonInfo item in WristMenu.CatButtons1)
			{
				if (item.buttonText == text)
				{
					item.enabled = true;
				}
			}
			foreach (ButtonInfo item2 in WristMenu.CatButtons2)
			{
				if (item2.buttonText == text)
				{
					item2.enabled = true;
				}
			}
			foreach (ButtonInfo item3 in WristMenu.CatButtons3)
			{
				if (item3.buttonText == text)
				{
					item3.enabled = true;
				}
			}
			foreach (ButtonInfo item4 in WristMenu.CatButtons4)
			{
				if (item4.buttonText == text)
				{
					item4.enabled = true;
				}
			}
			foreach (ButtonInfo item5 in WristMenu.CatButtons5)
			{
				if (item5.buttonText == text)
				{
					item5.enabled = true;
				}
			}
			foreach (ButtonInfo item6 in WristMenu.CatButtons6)
			{
				if (item6.buttonText == text)
				{
					item6.enabled = true;
				}
			}
			foreach (ButtonInfo item7 in WristMenu.CatButtons7)
			{
				if (item7.buttonText == text)
				{
					item7.enabled = true;
				}
			}
			foreach (ButtonInfo item8 in WristMenu.CatButtons8)
			{
				if (item8.buttonText == text)
				{
					item8.enabled = true;
				}
			}
			foreach (ButtonInfo item9 in WristMenu.CatButtons9)
			{
				if (item9.buttonText == text)
				{
					item9.enabled = true;
				}
			}
			foreach (ButtonInfo item10 in WristMenu.CatButtons10)
			{
				if (item10.buttonText == text)
				{
					item10.enabled = true;
				}
			}
		}
		NotifiLib.SendNotification("<color=white>[</color><color=blue>LOAD</color><color=white>]</color> <color=white>Loaded Buttons Successfully!</color>");
	}

	public static void Save()
	{
		List<string> list = new List<string>();
		foreach (ButtonInfo settingsbutton in WristMenu.settingsbuttons)
		{
			if (settingsbutton.enabled == true && settingsbutton.buttonText != "Save Settings")
			{
				list.Add(settingsbutton.buttonText);
			}
		}
		File.WriteAllLines(WristMenu.FolderName + "\\Saved_Settings.txt", list);
		string text = change1 + "\n" + change2 + "\n" + change3 + "\n" + change4 + "\n" + change6 + "\n" + change7 + "\n" + change8 + "\n" + change9 + "\n" + change10 + "\n" + change11 + "\n" + change12 + "\n" + change13 + "\n" + change14 + "\n" + change15 + "\n" + change16;
		File.WriteAllText(WristMenu.FolderName + "/Saved_Settings2.txt", text.ToString());
		NotifiLib.SendNotification("<color=white>[</color><color=blue>SAVE</color><color=white>]</color> <color=white>Saved Settings Successfully!</color>");
	}

	public static void Load()
	{
		string[] array = File.ReadAllLines(WristMenu.FolderName + "\\Saved_Settings.txt");
		string[] array2 = array;
		foreach (string text in array2)
		{
			foreach (ButtonInfo settingsbutton in WristMenu.settingsbuttons)
			{
				if (settingsbutton.buttonText == text)
				{
					settingsbutton.enabled = true;
				}
			}
		}
		try
		{
			string text2 = File.ReadAllText(WristMenu.FolderName + "/Saved_Settings2.txt");
			string[] array3 = text2.Split(new string[1] { "\n" }, StringSplitOptions.None);
			change1 = int.Parse(array3[0]) - 1;
			Changeplat();
			change2 = int.Parse(array3[1]) - 1;
			Changenoti();
			change3 = int.Parse(array3[2]) - 1;
			ChangeFPS();
			change4 = int.Parse(array3[3]) - 1;
			Changedisconnect();
			change6 = int.Parse(array3[4]) - 1;
			Changemenu();
			change7 = int.Parse(array3[5]) - 1;
			Changepagebutton();
			change8 = int.Parse(array3[6]) - 1;
			ChangeOrbColor();
			change9 = int.Parse(array3[7]) - 1;
			ChangeVisualColor();
			change10 = int.Parse(array3[8]) - 1;
			ThemeChangerV1();
			change11 = int.Parse(array3[9]) - 1;
			ThemeChangerV2();
			change12 = int.Parse(array3[10]) - 1;
			ThemeChangerV3();
			change13 = int.Parse(array3[11]) - 1;
			ThemeChangerV4();
			change14 = int.Parse(array3[12]) - 1;
			ThemeChangerV5();
			change15 = int.Parse(array3[13]) - 1;
			ThemeChangerV6();
			change16 = int.Parse(array3[14]) - 1;
			ThemeChangerV7();
		}
		catch
		{
		}
		NotifiLib.SendNotification("<color=white>[</color><color=blue>LOAD</color><color=white>]</color> <color=white>Loaded settings successfully!</color>");
	}

	private static void PlatformsThing(bool invis, bool sticky)
	{
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Expected O, but got Unknown
		//IL_0389: Unknown result type (might be due to invalid IL or missing references)
		//IL_039a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0315: Unknown result type (might be due to invalid IL or missing references)
		//IL_031a: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Expected O, but got Unknown
		if (TriggerPlats)
		{
			RPlat = WristMenu.triggerDownR;
			LPlat = WristMenu.triggerDownL;
		}
		else
		{
			RPlat = WristMenu.gripDownR;
			LPlat = WristMenu.gripDownL;
		}
		if (RPlat)
		{
			if (!once_right && (Object)(object)jump_right_local == (Object)null)
			{
				if (sticky)
				{
					jump_right_local = GameObject.CreatePrimitive((PrimitiveType)0);
				}
				else
				{
					jump_right_local = GameObject.CreatePrimitive((PrimitiveType)3);
				}
				if (invis)
				{
					Object.Destroy((Object)(object)jump_right_local.GetComponent<Renderer>());
				}
				jump_right_local.transform.localScale = scale;
				jump_right_local.transform.position = new Vector3(0f, -0.01f, 0f) + GTPlayer.Instance.RightHand.controllerTransform.position;
				jump_right_local.transform.rotation = GTPlayer.Instance.RightHand.controllerTransform.rotation;
				jump_right_local.AddComponent<GorillaSurfaceOverride>().overrideIndex = jump_right_local.GetComponent<GorillaSurfaceOverride>().overrideIndex;
				once_right = true;
				once_right_false = false;
				ColorChanger colorChanger = jump_right_local.AddComponent<ColorChanger>();
				colorChanger.colors = new Gradient
				{
					colorKeys = colorKeysPlatformMonke
				};
				colorChanger.Start();
			}
		}
		else if (!once_right_false && (Object)(object)jump_right_local != (Object)null)
		{
			Object.Destroy((Object)(object)jump_right_local.GetComponent<Collider>());
			Component obj = jump_right_local.AddComponent(typeof(Rigidbody));
			Rigidbody val = (Rigidbody)(object)((obj is Rigidbody) ? obj : null);
			val.linearVelocity = GTPlayer.Instance.RightHand.velocityTracker.GetAverageVelocity(true, 5f, false);
			Object.Destroy((Object)(object)jump_right_local, 2f);
			jump_right_local = null;
			once_right = false;
			once_right_false = true;
		}
		if (LPlat)
		{
			if (!once_left && (Object)(object)jump_left_local == (Object)null)
			{
				if (sticky)
				{
					jump_left_local = GameObject.CreatePrimitive((PrimitiveType)0);
				}
				else
				{
					jump_left_local = GameObject.CreatePrimitive((PrimitiveType)3);
				}
				if (invis)
				{
					Object.Destroy((Object)(object)jump_left_local.GetComponent<Renderer>());
				}
				jump_left_local.transform.localScale = scale;
				jump_left_local.transform.position = new Vector3(0f, -0.01f, 0f) + GTPlayer.Instance.LeftHand.controllerTransform.position;
				jump_left_local.transform.rotation = GTPlayer.Instance.LeftHand.controllerTransform.rotation;
				jump_left_local.AddComponent<GorillaSurfaceOverride>().overrideIndex = jump_left_local.GetComponent<GorillaSurfaceOverride>().overrideIndex;
				once_left = true;
				once_left_false = false;
				ColorChanger colorChanger2 = jump_left_local.AddComponent<ColorChanger>();
				colorChanger2.colors = new Gradient
				{
					colorKeys = colorKeysPlatformMonke
				};
				colorChanger2.Start();
			}
		}
		else if (!once_left_false && (Object)(object)jump_left_local != (Object)null)
		{
			Object.Destroy((Object)(object)jump_left_local.GetComponent<Collider>());
			Component obj2 = jump_left_local.AddComponent(typeof(Rigidbody));
			Rigidbody val2 = (Rigidbody)(object)((obj2 is Rigidbody) ? obj2 : null);
			val2.linearVelocity = GTPlayer.Instance.LeftHand.velocityTracker.GetAverageVelocity(true, 5f, false);
			Object.Destroy((Object)(object)jump_left_local, 2f);
			jump_left_local = null;
			once_left = false;
			once_left_false = true;
		}
	}

	public static ButtonInfo GetButton(string name)
	{
		foreach (ButtonInfo button in WristMenu.buttons)
		{
			if (button.buttonText == name)
			{
				return button;
			}
		}
		foreach (ButtonInfo settingsbutton in WristMenu.settingsbuttons)
		{
			if (settingsbutton.buttonText == name)
			{
				return settingsbutton;
			}
		}
		foreach (ButtonInfo item in WristMenu.CatButtons1)
		{
			if (item.buttonText == name)
			{
				return item;
			}
		}
		foreach (ButtonInfo item2 in WristMenu.CatButtons2)
		{
			if (item2.buttonText == name)
			{
				return item2;
			}
		}
		foreach (ButtonInfo item3 in WristMenu.CatButtons3)
		{
			if (item3.buttonText == name)
			{
				return item3;
			}
		}
		foreach (ButtonInfo item4 in WristMenu.CatButtons4)
		{
			if (item4.buttonText == name)
			{
				return item4;
			}
		}
		foreach (ButtonInfo item5 in WristMenu.CatButtons5)
		{
			if (item5.buttonText == name)
			{
				return item5;
			}
		}
		foreach (ButtonInfo item6 in WristMenu.CatButtons6)
		{
			if (item6.buttonText == name)
			{
				return item6;
			}
		}
		foreach (ButtonInfo item7 in WristMenu.CatButtons7)
		{
			if (item7.buttonText == name)
			{
				return item7;
			}
		}
		foreach (ButtonInfo item8 in WristMenu.CatButtons8)
		{
			if (item8.buttonText == name)
			{
				return item8;
			}
		}
		foreach (ButtonInfo item9 in WristMenu.CatButtons9)
		{
			if (item9.buttonText == name)
			{
				return item9;
			}
		}
		foreach (ButtonInfo item10 in WristMenu.CatButtons10)
		{
			if (item10.buttonText == name)
			{
				return item10;
			}
		}
		return null;
	}

	public static void Settings()
	{
		WristMenu.settingsbuttons[0].enabled = false;
		WristMenu.buttons[2].enabled = false;
		inSettings = !inSettings;
		if (inSettings)
		{
			WristMenu.pageNumber = 0;
		}
		if (!inSettings)
		{
			WristMenu.pageNumber = 0;
		}
		WristMenu.DestroyMenu();
		WristMenu.instance.Draw();
	}

	public static void Cat1()
	{
		WristMenu.CatButtons1[0].enabled = false;
		WristMenu.buttons[3].enabled = false;
		inCat1 = !inCat1;
		if (inCat1)
		{
			WristMenu.pageNumber = 0;
		}
		if (change7 == 1 && !inCat1)
		{
			WristMenu.pageNumber = 1;
		}
		if (((change7 == 2) | (change7 == 3) | (change7 == 4) | (change7 == 5)) && !inCat1)
		{
			WristMenu.pageNumber = 0;
		}
		WristMenu.DestroyMenu();
		WristMenu.instance.Draw();
	}

	public static void Cat2()
	{
		WristMenu.CatButtons2[0].enabled = false;
		WristMenu.buttons[4].enabled = false;
		inCat2 = !inCat2;
		if (inCat2)
		{
			WristMenu.pageNumber = 0;
		}
		if (change7 == 1 && !inCat2)
		{
			WristMenu.pageNumber = 1;
		}
		if (((change7 == 2) | (change7 == 3) | (change7 == 4) | (change7 == 5)) && !inCat2)
		{
			WristMenu.pageNumber = 0;
		}
		WristMenu.DestroyMenu();
		WristMenu.instance.Draw();
	}

	public static void Cat3()
	{
		WristMenu.CatButtons3[0].enabled = false;
		WristMenu.buttons[5].enabled = false;
		inCat3 = !inCat3;
		if (inCat3)
		{
			WristMenu.pageNumber = 0;
		}
		if (change7 == 1 && !inCat3)
		{
			WristMenu.pageNumber = 1;
		}
		if (((change7 == 2) | (change7 == 3) | (change7 == 4) | (change7 == 5)) && !inCat3)
		{
			WristMenu.pageNumber = 1;
		}
		WristMenu.DestroyMenu();
		WristMenu.instance.Draw();
	}

	public static void Cat4()
	{
		WristMenu.CatButtons4[0].enabled = false;
		WristMenu.buttons[6].enabled = false;
		inCat4 = !inCat4;
		if (inCat4)
		{
			WristMenu.pageNumber = 0;
		}
		if (change7 == 1 && !inCat4)
		{
			WristMenu.pageNumber = 1;
		}
		if (((change7 == 2) | (change7 == 3) | (change7 == 4) | (change7 == 5)) && !inCat4)
		{
			WristMenu.pageNumber = 1;
		}
		WristMenu.DestroyMenu();
		WristMenu.instance.Draw();
	}

	public static void Cat5()
	{
		WristMenu.CatButtons5[0].enabled = false;
		WristMenu.buttons[7].enabled = false;
		inCat5 = !inCat5;
		if (inCat5)
		{
			WristMenu.pageNumber = 0;
		}
		if (change7 == 1 && !inCat5)
		{
			WristMenu.pageNumber = 2;
		}
		if (((change7 == 2) | (change7 == 3) | (change7 == 4) | (change7 == 5)) && !inCat5)
		{
			WristMenu.pageNumber = 1;
		}
		WristMenu.DestroyMenu();
		WristMenu.instance.Draw();
	}

	public static void Cat6()
	{
		WristMenu.CatButtons6[0].enabled = false;
		WristMenu.buttons[8].enabled = false;
		inCat6 = !inCat6;
		if (inCat6)
		{
			WristMenu.pageNumber = 0;
		}
		if (change7 == 1 && !inCat6)
		{
			WristMenu.pageNumber = 2;
		}
		if (((change7 == 2) | (change7 == 3) | (change7 == 4) | (change7 == 5)) && !inCat6)
		{
			WristMenu.pageNumber = 1;
		}
		WristMenu.DestroyMenu();
		WristMenu.instance.Draw();
	}

	public static void Cat7()
	{
		WristMenu.CatButtons7[0].enabled = false;
		WristMenu.buttons[9].enabled = false;
		inCat7 = !inCat7;
		if (inCat7)
		{
			WristMenu.pageNumber = 0;
		}
		if (change7 == 1 && !inCat7)
		{
			WristMenu.pageNumber = 2;
		}
		if (((change7 == 2) | (change7 == 3) | (change7 == 4) | (change7 == 5)) && !inCat7)
		{
			WristMenu.pageNumber = 1;
		}
		WristMenu.DestroyMenu();
		WristMenu.instance.Draw();
	}

	public static void Cat8()
	{
		WristMenu.CatButtons8[0].enabled = false;
		WristMenu.buttons[10].enabled = false;
		inCat8 = !inCat8;
		if (inCat8)
		{
			WristMenu.pageNumber = 0;
		}
		if (change7 == 1 && !inCat8)
		{
			WristMenu.pageNumber = 2;
		}
		if (((change7 == 2) | (change7 == 3) | (change7 == 4) | (change7 == 5)) && !inCat8)
		{
			WristMenu.pageNumber = 1;
		}
		WristMenu.DestroyMenu();
		WristMenu.instance.Draw();
	}

	public static void Cat9()
	{
		WristMenu.CatButtons9[0].enabled = false;
		WristMenu.buttons[11].enabled = false;
		inCat9 = !inCat9;
		if (inCat9)
		{
			WristMenu.pageNumber = 0;
		}
		if (change7 == 1 && !inCat9)
		{
			WristMenu.pageNumber = 2;
		}
		if (((change7 == 2) | (change7 == 3) | (change7 == 4) | (change7 == 5)) && !inCat9)
		{
			WristMenu.pageNumber = 2;
		}
		WristMenu.DestroyMenu();
		WristMenu.instance.Draw();
	}

	public static void Cat10()
	{
		WristMenu.CatButtons10[0].enabled = false;
		WristMenu.buttons[12].enabled = false;
		inCat10 = !inCat10;
		if (inCat10)
		{
			WristMenu.pageNumber = 0;
		}
		if (change7 == 1 && !inCat10)
		{
			WristMenu.pageNumber = 3;
		}
		if (((change7 == 2) | (change7 == 3) | (change7 == 4) | (change7 == 5)) && !inCat10)
		{
			WristMenu.pageNumber = 2;
		}
		WristMenu.DestroyMenu();
		WristMenu.instance.Draw();
	}

	public static void Cat11()
	{
		WristMenu.CatButtons11[0].enabled = false;
		WristMenu.buttons[13].enabled = false;
		inCat11 = !inCat11;
		if (inCat11)
		{
			WristMenu.pageNumber = 0;
		}
		else
		{
			WristMenu.pageNumber = 0;
		}
		WristMenu.DestroyMenu();
		WristMenu.instance.Draw();
	}

	public static void Changeplat()
	{
		change1++;
		if (change1 > 2)
		{
			change1 = 1;
		}
		if (change1 == 1)
		{
			TriggerPlats = false;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>PLATFORMS</color><color=white>] Enable Platforms: Grips</color>");
		}
		if (change1 == 2)
		{
			TriggerPlats = true;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>PLATFORMS</color><color=white>] Enable Platforms: Triggers</color>");
		}
	}

	public static void Changenoti()
	{
		change2++;
		if (change2 > 2)
		{
			change2 = 1;
		}
		if (change2 == 1)
		{
			NotifiLib.IsEnabled = true;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>NOTIS</color><color=white>] Notis Enabled: Yes</color>");
		}
		if (change2 == 2)
		{
			NotifiLib.SendNotification("<color=white>[</color><color=blue>NOTIS</color><color=white>] Notis Enabled: No</color>");
			NotifiLib.IsEnabled = false;
		}
	}

	public static void ChangeFPS()
	{
		change3++;
		if (change3 > 2)
		{
			change3 = 1;
		}
		if (change3 == 1)
		{
			FPSPage = false;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>FPS & PAGE COUNTER</color><color=white>] Is Enabled: No</color>");
		}
		if (change3 == 2)
		{
			FPSPage = true;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>FPS & PAGE COUNTER</color><color=white>] Is Enabled: Yes</color>");
		}
	}

	public static void Changedisconnect()
	{
		change4++;
		if (change4 > 4)
		{
			change4 = 1;
		}
		if (change4 == 1)
		{
			NotifiLib.SendNotification("<color=white>[</color><color=blue>DISCONNECT BUTTON</color><color=white>] Disconnect Location: Right Side</color>");
		}
		if (change4 == 2)
		{
			NotifiLib.SendNotification("<color=white>[</color><color=blue>DISCONNECT BUTTON</color><color=white>] Disconnect Location: Left Side</color>");
		}
		if (change4 == 3)
		{
			NotifiLib.SendNotification("<color=white>[</color><color=blue>DISCONNECT BUTTON</color><color=white>] Disconnect Location: Top</color>");
		}
		if (change4 == 4)
		{
			NotifiLib.SendNotification("<color=white>[</color><color=blue>DISCONNECT BUTTON</color><color=white>] Disconnect Location: Bottom</color>");
		}
	}

	public static void Changemenu()
	{
		change6++;
		if (change6 > 2)
		{
			change6 = 1;
		}
		if (change6 == 1)
		{
			right = false;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>MENU LOCATION</color><color=white>] Current Location: Left Hand</color>");
		}
		if (change6 == 2)
		{
			right = true;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>MENU LOCATION</color><color=white>] Current Location: Right Hand</color>");
		}
	}

	public static void Changepagebutton()
	{
		change7++;
		if (change7 > 5)
		{
			change7 = 1;
		}
		if (change7 == 1)
		{
			NotifiLib.SendNotification("<color=white>[</color><color=blue>NEXT & PREV</color><color=white>] Page Change Button Location: On Menu</color>");
		}
		if (change7 == 2)
		{
			NotifiLib.SendNotification("<color=white>[</color><color=blue>NEXT & PREV</color><color=white>] Page Change Button Location: Top</color>");
		}
		if (change7 == 3)
		{
			NotifiLib.SendNotification("<color=white>[</color><color=blue>NEXT & PREV</color><color=white>] Page Change Button Location: Sides</color>");
		}
		if (change7 == 4)
		{
			NotifiLib.SendNotification("<color=white>[</color><color=blue>NEXT & PREV</color><color=white>] Page Change Button Location: Bottom</color>");
		}
		if (change7 == 5)
		{
			NotifiLib.SendNotification("<color=white>[</color><color=blue>NEXT & PREV</color><color=white>] Page Change Button Location: Triggers</color>");
		}
	}

	public static void ChangeOrbColor()
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		change8++;
		if (change8 > 9)
		{
			change8 = 1;
		}
		if (change8 == 1)
		{
			CurrentGunColor = Color.blue;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>GUN & HAND ORB COLOR</color><color=white>] Current Color: Blue</color>");
		}
		if (change8 == 2)
		{
			CurrentGunColor = Color.red;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>GUN & HAND ORB COLOR</color><color=white>] Current Color: Red</color>");
		}
		if (change8 == 3)
		{
			CurrentGunColor = Color.white;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>GUN & HAND ORB COLOR</color><color=white>] Current Color: White</color>");
		}
		if (change8 == 4)
		{
			CurrentGunColor = Color.green;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>GUN & HAND ORB COLOR</color><color=white>] Current Color: Green</color>");
		}
		if (change8 == 5)
		{
			CurrentGunColor = Color.magenta;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>GUN & HAND ORB COLOR</color><color=white>] Current Color: Magenta</color>");
		}
		if (change8 == 6)
		{
			CurrentGunColor = Color.cyan;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>GUN & HAND ORB COLOR</color><color=white>] Current Color: Cyan</color>");
		}
		if (change8 == 7)
		{
			CurrentGunColor = Color.yellow;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>GUN & HAND ORB COLOR</color><color=white>] Current Color: Yellow</color>");
		}
		if (change8 == 8)
		{
			CurrentGunColor = Color.black;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>GUN & HAND ORB COLOR</color><color=white>] Current Color: Black</color>");
		}
		if (change8 == 9)
		{
			CurrentGunColor = Color.grey;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>GUN & HAND ORB COLOR</color><color=white>] Current Color: Grey</color>");
		}
	}

	public static void ChangeVisualColor()
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		change9++;
		if (change9 > 9)
		{
			change9 = 1;
		}
		if (change9 == 1)
		{
			CurrentESPColor = Color.blue;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>ESP COLOR</color><color=white>] Current Color: Blue</color>");
		}
		if (change9 == 2)
		{
			CurrentESPColor = Color.red;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>ESP COLOR</color><color=white>] Current Color: Red</color>");
		}
		if (change9 == 3)
		{
			CurrentESPColor = Color.white;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>ESP COLOR</color><color=white>] Current Color: White</color>");
		}
		if (change9 == 4)
		{
			CurrentESPColor = Color.green;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>ESP COLOR</color><color=white>] Current Color: Green</color>");
		}
		if (change9 == 5)
		{
			CurrentESPColor = Color.magenta;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>ESP COLOR</color><color=white>] Current Color: Magenta</color>");
		}
		if (change9 == 6)
		{
			CurrentESPColor = Color.cyan;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>ESP COLOR</color><color=white>] Current Color: Cyan</color>");
		}
		if (change9 == 7)
		{
			CurrentESPColor = Color.yellow;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>ESP COLOR</color><color=white>] Current Color: Yellow</color>");
		}
		if (change9 == 8)
		{
			CurrentESPColor = Color.black;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>ESP COLOR</color><color=white>] Current Color: Black</color>");
		}
		if (change9 == 9)
		{
			CurrentESPColor = Color.grey;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>ESP COLOR</color><color=white>] Current Color: Grey</color>");
		}
	}

	public static void ThemeChangerV1()
	{
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		change10++;
		if (change10 > 11)
		{
			change10 = 1;
		}
		if (change10 == 1)
		{
			if (WristMenu.ChangingColors)
			{
				RGBMenu = false;
				WristMenu.FirstColor1 = Color.blue;
				NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] First Color: Blue</color>");
			}
			else
			{
				RGBMenu = false;
				WristMenu.NormalColor = Color.blue;
				NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Menu Color: Blue</color>");
			}
		}
		if (change10 == 2)
		{
			if (WristMenu.ChangingColors)
			{
				WristMenu.FirstColor1 = Color.red;
				NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] First Color: Red</color>");
			}
			else
			{
				WristMenu.NormalColor = Color.red;
				NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Menu Color: Red</color>");
			}
		}
		if (change10 == 3)
		{
			if (WristMenu.ChangingColors)
			{
				WristMenu.FirstColor1 = Color.white;
				NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] First Color: White</color>");
			}
			else
			{
				WristMenu.NormalColor = Color.white;
				NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Menu Color: White</color>");
			}
		}
		if (change10 == 4)
		{
			if (WristMenu.ChangingColors)
			{
				WristMenu.FirstColor1 = Color.green;
				NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] First Color: Green</color>");
			}
			else
			{
				WristMenu.NormalColor = Color.green;
				NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Menu Color: Green</color>");
			}
		}
		if (change10 == 5)
		{
			if (WristMenu.ChangingColors)
			{
				WristMenu.FirstColor1 = Color.magenta;
				NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] First Color: Magenta</color>");
			}
			else
			{
				WristMenu.NormalColor = Color.magenta;
				NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Menu Color: Magenta</color>");
			}
		}
		if (change10 == 6)
		{
			if (WristMenu.ChangingColors)
			{
				WristMenu.FirstColor1 = Color.cyan;
				NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] First Color: Cyan</color>");
			}
			else
			{
				WristMenu.NormalColor = Color.cyan;
				NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Menu Color: Cyan</color>");
			}
		}
		if (change10 == 7)
		{
			if (WristMenu.ChangingColors)
			{
				WristMenu.FirstColor1 = Color.yellow;
				NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] First Color: Yellow</color>");
			}
			else
			{
				WristMenu.NormalColor = Color.yellow;
				NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Menu Color: Yellow</color>");
			}
		}
		if (change10 == 8)
		{
			if (WristMenu.ChangingColors)
			{
				WristMenu.FirstColor1 = Color.black;
				NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] First Color: Black</color>");
			}
			else
			{
				WristMenu.NormalColor = Color.black;
				NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Menu Color: Black</color>");
			}
		}
		if (change10 == 9)
		{
			if (WristMenu.ChangingColors)
			{
				WristMenu.FirstColor1 = Color.grey;
				NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] First Color: Grey</color>");
			}
			else
			{
				WristMenu.NormalColor = Color.grey;
				NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Menu Color: Grey</color>");
			}
		}
		if (change10 == 10)
		{
			NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Menu Color: Clear</color>");
		}
		if (change10 == 11)
		{
			if (WristMenu.ChangingColors)
			{
				RGBMenu = true;
				NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Menu Color: RGB</color>");
			}
			else
			{
				NotifiLib.SendNotification("<color=white>[</color><color=red>ERROR</color><color=white>] Cannot Change The Menu To RGB Due To WristMenu.ChangingColors Being false</color>");
			}
		}
	}

	public static void ThemeChangerV2()
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		change11++;
		if (change11 > 9)
		{
			change11 = 1;
		}
		if (change11 == 1)
		{
			WristMenu.SecondColor = Color.black;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Second Color: Black</color>");
		}
		if (change11 == 2)
		{
			WristMenu.SecondColor = Color.red;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Second Color: Red</color>");
		}
		if (change11 == 3)
		{
			WristMenu.SecondColor = Color.white;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Second Color: White</color>");
		}
		if (change11 == 4)
		{
			WristMenu.SecondColor = Color.green;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Second Color: Green</color>");
		}
		if (change11 == 5)
		{
			WristMenu.SecondColor = Color.magenta;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Second Color: Magenta</color>");
		}
		if (change11 == 6)
		{
			WristMenu.SecondColor = Color.cyan;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Second Color: Cyan</color>");
		}
		if (change11 == 7)
		{
			WristMenu.SecondColor = Color.yellow;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Second Color: Yellow</color>");
		}
		if (change11 == 8)
		{
			WristMenu.SecondColor = Color.blue;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Second Color: Blue</color>");
		}
		if (change11 == 9)
		{
			WristMenu.SecondColor = Color.grey;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Second Color: Grey</color>");
		}
	}

	public static void ThemeChangerV3()
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		change12++;
		if (change12 > 10)
		{
			change12 = 1;
		}
		if (change12 == 1)
		{
			WristMenu.ButtonColorDisable = Color.yellow;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Disable Button Color: Yellow</color>");
		}
		if (change12 == 2)
		{
			WristMenu.ButtonColorDisable = Color.red;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Disable Button Color: Red</color>");
		}
		if (change12 == 3)
		{
			WristMenu.ButtonColorDisable = Color.white;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Disable Button Color: White</color>");
		}
		if (change12 == 4)
		{
			WristMenu.ButtonColorDisable = Color.green;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Disable Button Color: Green</color>");
		}
		if (change12 == 5)
		{
			WristMenu.ButtonColorDisable = Color.magenta;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Disable Button Color: Magenta</color>");
		}
		if (change12 == 6)
		{
			WristMenu.ButtonColorDisable = Color.cyan;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Disable Button Color: Cyan</color>");
		}
		if (change12 == 7)
		{
			WristMenu.ButtonColorDisable = Color.black;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Disable Button Color: Black</color>");
		}
		if (change12 == 8)
		{
			WristMenu.ButtonColorDisable = Color.blue;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Disable Button Color: Blue</color>");
		}
		if (change12 == 9)
		{
			WristMenu.ButtonColorDisable = Color.grey;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Disable Button Color: Grey</color>");
		}
		if (change12 == 10)
		{
			NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Disable Button Color: Clear</color>");
		}
	}

	public static void ThemeChangerV4()
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		change13++;
		if (change13 > 10)
		{
			change13 = 1;
		}
		if (change13 == 1)
		{
			WristMenu.ButtonColorEnabled = Color.magenta;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Enable Button Color: Magenta</color>");
		}
		if (change13 == 2)
		{
			WristMenu.ButtonColorEnabled = Color.red;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Enable Button Color: Red</color>");
		}
		if (change13 == 3)
		{
			WristMenu.ButtonColorEnabled = Color.white;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Enable Button Color: White</color>");
		}
		if (change13 == 4)
		{
			WristMenu.ButtonColorEnabled = Color.green;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Enable Button Color: Green</color>");
		}
		if (change13 == 5)
		{
			WristMenu.ButtonColorEnabled = Color.yellow;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Enable Button Color: Yellow</color>");
		}
		if (change13 == 6)
		{
			WristMenu.ButtonColorEnabled = Color.cyan;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Enable Button Color: Cyan</color>");
		}
		if (change13 == 7)
		{
			WristMenu.ButtonColorEnabled = Color.black;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Enable Button Color: Black</color>");
		}
		if (change13 == 8)
		{
			WristMenu.ButtonColorEnabled = Color.blue;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Enable Button Color: Blue</color>");
		}
		if (change13 == 9)
		{
			WristMenu.ButtonColorEnabled = Color.grey;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Enable Button Color: Grey</color>");
		}
		if (change13 == 10)
		{
			NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Enable Button Color: Clear</color>");
		}
	}

	public static void ThemeChangerV5()
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		change14++;
		if (change14 > 9)
		{
			change14 = 1;
		}
		if (change14 == 1)
		{
			WristMenu.EnableTextColor = Color.black;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Enabled Text Color: Black</color>");
		}
		if (change14 == 2)
		{
			WristMenu.EnableTextColor = Color.red;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Enabled Text Color: Red</color>");
		}
		if (change14 == 3)
		{
			WristMenu.EnableTextColor = Color.white;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Enabled Text Color: White</color>");
		}
		if (change14 == 4)
		{
			WristMenu.EnableTextColor = Color.green;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Enabled Text Color: Green</color>");
		}
		if (change14 == 5)
		{
			WristMenu.EnableTextColor = Color.yellow;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Enabled Text Color: Yellow</color>");
		}
		if (change14 == 6)
		{
			WristMenu.EnableTextColor = Color.cyan;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Enabled Text Color: Cyan</color>");
		}
		if (change14 == 7)
		{
			WristMenu.EnableTextColor = Color.magenta;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Enabled Text Color: Magenta</color>");
		}
		if (change14 == 8)
		{
			WristMenu.EnableTextColor = Color.blue;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Enabled Text Color: Blue</color>");
		}
		if (change14 == 9)
		{
			WristMenu.EnableTextColor = Color.grey;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Enabled Text Color: Grey</color>");
		}
	}

	public static void ThemeChangerV6()
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		change15++;
		if (change15 > 9)
		{
			change15 = 1;
		}
		if (change15 == 1)
		{
			WristMenu.DIsableTextColor = Color.black;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Disabled Text Color: Black</color>");
		}
		if (change15 == 2)
		{
			WristMenu.DIsableTextColor = Color.red;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Disabled Text Color: Red</color>");
		}
		if (change15 == 3)
		{
			WristMenu.DIsableTextColor = Color.white;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Disabled Text Color: White</color>");
		}
		if (change15 == 4)
		{
			WristMenu.DIsableTextColor = Color.green;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Disabled Text Color: Green</color>");
		}
		if (change15 == 5)
		{
			WristMenu.DIsableTextColor = Color.yellow;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Disabled Text Color: Yellow</color>");
		}
		if (change15 == 6)
		{
			WristMenu.DIsableTextColor = Color.cyan;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Disabled Text Color: Cyan</color>");
		}
		if (change15 == 7)
		{
			WristMenu.DIsableTextColor = Color.magenta;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Disabled Text Color: Magenta</color>");
		}
		if (change15 == 8)
		{
			WristMenu.DIsableTextColor = Color.blue;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Disabled Text Color: Blue</color>");
		}
		if (change15 == 9)
		{
			WristMenu.DIsableTextColor = Color.grey;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Disabled Text Color: Grey</color>");
		}
	}

	public static void ThemeChangerV7()
	{
		change16++;
		if (change16 > 6)
		{
			change16 = 1;
		}
		if (change16 == 1)
		{
			ButtonSound = 67;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Button Sound: Normal</color>");
		}
		if (change16 == 2)
		{
			ButtonSound = 8;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Button Sound: Stump</color>");
		}
		if (change16 == 3)
		{
			ButtonSound = 203;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Button Sound: AK47</color>");
		}
		if (change16 == 4)
		{
			ButtonSound = 50;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Button Sound: Glass</color>");
		}
		if (change16 == 5)
		{
			ButtonSound = 66;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Button Sound: KeyBoard</color>");
		}
		if (change16 == 6)
		{
			ButtonSound = 114;
			NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Button Sound: Cayon Bridge</color>");
		}
	}

	public static void MakeGun(Color color, Vector3 pointersize, float linesize, PrimitiveType pointershape, Transform arm, bool liner, Action shit, Action shit1)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Expected O, but got Unknown
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)arm == (Object)(object)GTPlayer.Instance.RightHand.controllerTransform)
		{
			hand = WristMenu.gripDownR;
			hand1 = WristMenu.triggerDownR;
		}
		else if ((Object)(object)arm == (Object)(object)GTPlayer.Instance.LeftHand.controllerTransform)
		{
			hand = WristMenu.gripDownL;
			hand1 = WristMenu.triggerDownL;
		}
		if (hand)
		{
			Physics.Raycast(arm.position, -arm.up, ref raycastHit);
			if ((Object)(object)pointer == (Object)null)
			{
				pointer = GameObject.CreatePrimitive(pointershape);
			}
			pointer.transform.localScale = pointersize;
			pointer.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
			pointer.transform.position = ((RaycastHit)(ref raycastHit)).point;
			pointer.GetComponent<Renderer>().material.color = color;
			if (liner)
			{
				GameObject val = new GameObject("Line");
				Line = val.AddComponent<LineRenderer>();
				((Renderer)Line).material.shader = Shader.Find("GUI/Text Shader");
				Line.startWidth = linesize;
				Line.endWidth = linesize;
				Line.startColor = color;
				Line.endColor = color;
				Line.positionCount = 2;
				Line.useWorldSpace = true;
				Line.SetPosition(0, arm.position);
				Line.SetPosition(1, pointer.transform.position);
				Object.Destroy((Object)(object)val, Time.deltaTime);
			}
			Object.Destroy((Object)(object)pointer.GetComponent<BoxCollider>());
			Object.Destroy((Object)(object)pointer.GetComponent<Rigidbody>());
			Object.Destroy((Object)(object)pointer.GetComponent<Collider>());
			if (hand1)
			{
				shit();
			}
			else
			{
				shit1();
			}
		}
		else if ((Object)(object)pointer != (Object)null)
		{
			Object.Destroy((Object)(object)pointer, Time.deltaTime);
		}
	}

	public static void ExampleOnHowToUseGunLib()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		MakeGun(CurrentGunColor, new Vector3(0.15f, 0.15f, 0.15f), 0.025f, (PrimitiveType)0, GTPlayer.Instance.RightHand.controllerTransform, liner: true, delegate
		{
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			//IL_002e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0033: Unknown result type (might be due to invalid IL or missing references)
			GameObject.Find("Floating Bug Holdable").transform.position = pointer.transform.position + new Vector3(0f, 0.25f, 0f);
		}, delegate
		{
		});
	}

	public static void ExampleOnHowToUseGunLibV2()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		MakeGun(CurrentGunColor, new Vector3(0.15f, 0.15f, 0.15f), 0.025f, (PrimitiveType)0, GTPlayer.Instance.RightHand.controllerTransform, liner: true, delegate
		{
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			//IL_003f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0044: Unknown result type (might be due to invalid IL or missing references)
			((Behaviour)GorillaTagger.Instance.offlineVRRig).enabled = false;
			((Component)GorillaTagger.Instance.offlineVRRig).transform.position = pointer.transform.position + new Vector3(0f, 0.3f, 0f);
		}, delegate
		{
			((Behaviour)GorillaTagger.Instance.offlineVRRig).enabled = true;
		});
	}
}
