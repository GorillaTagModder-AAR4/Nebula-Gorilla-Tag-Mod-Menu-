using MalachiTemp.Backend;
using MalachiTemp.UI;
using UnityEngine;

internal class BtnCollider : MonoBehaviour
{
	public static int framePressCooldown;

	public string relatedText;

	private void OnTriggerEnter(Collider collider)
	{
		if (Time.frameCount >= framePressCooldown + WristMenu.ClickCooldown && ((Object)collider).name == "buttonPresser")
		{
			if (!Mods.right)
			{
				GorillaTagger.Instance.StartVibration(false, 0.01f, 0.001f);
				VRRig.LocalRig.PlayHandTapLocal(Mods.ButtonSound, true, 0.1f);
			}
			else
			{
				GorillaTagger.Instance.StartVibration(true, 0.01f, 0.001f);
				VRRig.LocalRig.PlayHandTapLocal(Mods.ButtonSound, false, 0.1f);
			}
			WristMenu.Toggle(relatedText);
			framePressCooldown = Time.frameCount;
		}
	}
}
