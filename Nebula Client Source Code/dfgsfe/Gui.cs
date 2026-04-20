using System.Collections.Generic;
using BepInEx;
using MalachiTemp.Backend;
using MalachiTemp.UI;
using MalachiTemp.Utilities;
using Photon.Pun;
using UnityEngine;

namespace dfgsfe;

[BepInPlugin("malachis.temp", "malachis.temp.GUI", "1.0.0")]
public class Gui : BaseUnityPlugin
{
	private bool open = true;

	public static bool CatOpen;

	public static KeyCode OpenAndCloseGUI = (KeyCode)277;

	private static Vector2 scrollPosition;

	public static Rect window = new Rect(0f, 0f, 340f, 280f);

	private Vector2 dragstart;

	private bool dragging = false;

	public static List<ButtonInfo> buttons = new List<ButtonInfo>();

	public void OnGUI()
	{
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0461: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0577: Unknown result type (might be due to invalid IL or missing references)
		//IL_0602: Unknown result type (might be due to invalid IL or missing references)
		//IL_068d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0718: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0830: Unknown result type (might be due to invalid IL or missing references)
		//IL_08bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Expected O, but got Unknown
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_036d: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d6: Unknown result type (might be due to invalid IL or missing references)
		if (!open)
		{
			return;
		}
		if (Mods.RGBMenu)
		{
			GUI.color = Color.Lerp(WristMenu.menuObj.GetComponent<ColorChanger>().color, WristMenu.menuObj.GetComponent<ColorChanger>().color, Mathf.PingPong(Time.time, 1f));
			GUI.backgroundColor = Color.Lerp(WristMenu.menuObj.GetComponent<ColorChanger>().color, WristMenu.menuObj.GetComponent<ColorChanger>().color, Mathf.PingPong(Time.time, 1f));
			GUI.contentColor = Color.Lerp(WristMenu.menuObj.GetComponent<ColorChanger>().color, WristMenu.menuObj.GetComponent<ColorChanger>().color, Mathf.PingPong(Time.time, 1f));
		}
		else
		{
			GUI.color = Color.Lerp(WristMenu.FirstColor1, WristMenu.SecondColor, Mathf.PingPong(Time.time, 1f));
			GUI.backgroundColor = Color.Lerp(WristMenu.FirstColor1, WristMenu.SecondColor, Mathf.PingPong(Time.time, 1f));
			GUI.contentColor = Color.Lerp(WristMenu.FirstColor1, WristMenu.SecondColor, Mathf.PingPong(Time.time, 1f));
		}
		GUI.Box(new Rect(((Rect)(ref window)).x, ((Rect)(ref window)).y + 7f, ((Rect)(ref window)).width, ((Rect)(ref window)).height + 2f), "");
		GUIStyle val = new GUIStyle(GUI.skin.label)
		{
			fontSize = 13
		};
		GUI.Label(new Rect(((Rect)(ref window)).x, ((Rect)(ref window)).y + 5f, 500f, 500f), "<color=white>|\n|\n|\n|\n|\n|\n|\n|\n|\n|\n|\n|\n|\n|\n|\n|\n|\n|\n|</color>", val);
		GUI.Label(new Rect(((Rect)(ref window)).x + 100f, ((Rect)(ref window)).y + 5f, 500f, 500f), "<color=white>|\n|\n|\n|\n|\n|\n|\n|\n|\n|\n|\n|\n|\n|\n|\n|\n|</color>", val);
		GUI.Label(new Rect(((Rect)(ref window)).x + 340f, ((Rect)(ref window)).y + 5f, 500f, 500f), "<color=white>|\n|\n|\n|\n|\n|\n|\n|\n|\n|\n|\n|\n|\n|\n|\n|\n|\n|\n|</color>", val);
		GUI.Label(new Rect(((Rect)(ref window)).x + 3f, ((Rect)(ref window)).y, 500f, 500f), "<color=white>------------------------------------------------------------------------------</color>", val);
		GUI.Label(new Rect(((Rect)(ref window)).x + 3f, ((Rect)(ref window)).y + 250f, 500f, 500f), "<color=white>------------------------------------------------------------------------------</color>", val);
		GUI.Label(new Rect(((Rect)(ref window)).x + 3f, ((Rect)(ref window)).y + 280f, 500f, 500f), "<color=white>------------------------------------------------------------------------------</color>", val);
		if (GUI.Button(new Rect(((Rect)(ref window)).x + 4f, ((Rect)(ref window)).y + 265f, 334f, 25f), "<color=white>Disconnect</color>"))
		{
			PhotonNetwork.Disconnect();
			VRRig.LocalRig.PlayHandTapLocal(Mods.ButtonSound, false, 0.1f);
		}
		if (GUI.Button(new Rect(((Rect)(ref window)).x + 4f, ((Rect)(ref window)).y + 15f, 96f, 20f), "<color=white>Settings</color>"))
		{
			VRRig.LocalRig.PlayHandTapLocal(Mods.ButtonSound, false, 0.1f);
			buttons = WristMenu.settingsbuttons;
			CatOpen = true;
		}
		try
		{
			if (GUI.Button(new Rect(((Rect)(ref window)).x + 4f, ((Rect)(ref window)).y + 37f, 96f, 20f), "<color=white>" + WristMenu.buttons[3].buttonText + "</color>"))
			{
				VRRig.LocalRig.PlayHandTapLocal(Mods.ButtonSound, false, 0.1f);
				buttons = WristMenu.CatButtons1;
				CatOpen = true;
			}
		}
		catch
		{
		}
		try
		{
			if (GUI.Button(new Rect(((Rect)(ref window)).x + 4f, ((Rect)(ref window)).y + 59f, 96f, 20f), "<color=white>" + WristMenu.buttons[4].buttonText + "</color>"))
			{
				VRRig.LocalRig.PlayHandTapLocal(Mods.ButtonSound, false, 0.1f);
				buttons = WristMenu.CatButtons2;
				CatOpen = true;
			}
		}
		catch
		{
		}
		try
		{
			if (GUI.Button(new Rect(((Rect)(ref window)).x + 4f, ((Rect)(ref window)).y + 81f, 96f, 20f), "<color=white>" + WristMenu.buttons[5].buttonText + "</color>"))
			{
				VRRig.LocalRig.PlayHandTapLocal(Mods.ButtonSound, false, 0.1f);
				buttons = WristMenu.CatButtons3;
				CatOpen = true;
			}
		}
		catch
		{
		}
		try
		{
			if (GUI.Button(new Rect(((Rect)(ref window)).x + 4f, ((Rect)(ref window)).y + 102f, 96f, 20f), "<color=white>" + WristMenu.buttons[6].buttonText + "</color>"))
			{
				VRRig.LocalRig.PlayHandTapLocal(Mods.ButtonSound, false, 0.1f);
				buttons = WristMenu.CatButtons4;
				CatOpen = true;
			}
		}
		catch
		{
		}
		try
		{
			if (GUI.Button(new Rect(((Rect)(ref window)).x + 4f, ((Rect)(ref window)).y + 124f, 96f, 20f), "<color=white>" + WristMenu.buttons[7].buttonText + "</color>"))
			{
				VRRig.LocalRig.PlayHandTapLocal(Mods.ButtonSound, false, 0.1f);
				buttons = WristMenu.CatButtons5;
				CatOpen = true;
			}
		}
		catch
		{
		}
		try
		{
			if (GUI.Button(new Rect(((Rect)(ref window)).x + 4f, ((Rect)(ref window)).y + 146f, 96f, 20f), "<color=white>" + WristMenu.buttons[8].buttonText + "</color>"))
			{
				VRRig.LocalRig.PlayHandTapLocal(Mods.ButtonSound, false, 0.1f);
				buttons = WristMenu.CatButtons6;
				CatOpen = true;
			}
		}
		catch
		{
		}
		try
		{
			if (GUI.Button(new Rect(((Rect)(ref window)).x + 4f, ((Rect)(ref window)).y + 168f, 96f, 20f), "<color=white>" + WristMenu.buttons[9].buttonText + "</color>"))
			{
				VRRig.LocalRig.PlayHandTapLocal(Mods.ButtonSound, false, 0.1f);
				buttons = WristMenu.CatButtons7;
				CatOpen = true;
			}
		}
		catch
		{
		}
		try
		{
			if (GUI.Button(new Rect(((Rect)(ref window)).x + 4f, ((Rect)(ref window)).y + 190f, 96f, 20f), "<color=white>" + WristMenu.buttons[10].buttonText + "</color>"))
			{
				VRRig.LocalRig.PlayHandTapLocal(Mods.ButtonSound, false, 0.1f);
				buttons = WristMenu.CatButtons8;
				CatOpen = true;
			}
		}
		catch
		{
		}
		try
		{
			if (GUI.Button(new Rect(((Rect)(ref window)).x + 4f, ((Rect)(ref window)).y + 212f, 96f, 20f), "<color=white>" + WristMenu.buttons[11].buttonText + "</color>"))
			{
				VRRig.LocalRig.PlayHandTapLocal(Mods.ButtonSound, false, 0.1f);
				buttons = WristMenu.CatButtons9;
				CatOpen = true;
			}
		}
		catch
		{
		}
		try
		{
			if (GUI.Button(new Rect(((Rect)(ref window)).x + 4f, ((Rect)(ref window)).y + 234f, 96f, 20f), "<color=white>" + WristMenu.buttons[12].buttonText + "</color>"))
			{
				VRRig.LocalRig.PlayHandTapLocal(Mods.ButtonSound, false, 0.1f);
				buttons = WristMenu.CatButtons10;
				CatOpen = true;
			}
		}
		catch
		{
		}
		DrawButtons(buttons);
		Dragging();
	}

	public void Update()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		if (UnityInput.Current.GetKeyDown(OpenAndCloseGUI))
		{
			open = !open;
		}
	}

	private void Dragging()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Invalid comparison between Unknown and I4
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		if ((int)Event.current.type == 0 && ((Rect)(ref window)).Contains(Event.current.mousePosition))
		{
			dragging = true;
			dragstart = Event.current.mousePosition - new Vector2(((Rect)(ref window)).x, ((Rect)(ref window)).y);
		}
		else if ((int)Event.current.type == 1)
		{
			dragging = false;
		}
		if (dragging)
		{
			((Rect)(ref window)).position = Event.current.mousePosition - dragstart;
		}
	}

	public static void DrawButtons(List<ButtonInfo> Buttons)
	{
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		if (!CatOpen)
		{
			return;
		}
		float num = ((Buttons.Count <= 9) ? 228f : 218f);
		scrollPosition = GUI.BeginScrollView(new Rect(((Rect)(ref window)).x + 100f, ((Rect)(ref window)).y + 15f, 240f, 240f), scrollPosition, new Rect(0f, 0f, 0f, (float)(Buttons.Count * 26)));
		for (int i = 0; i < Buttons.Count; i++)
		{
			if (GUI.Button(new Rect(7f, (float)(5 + i * 26), num, 20f), "<color=white>" + Buttons[i].buttonText + "</color>"))
			{
				if (Buttons[i].buttonText.Contains("Exit"))
				{
					CatOpen = false;
					continue;
				}
				Buttons[i].enabled = !Buttons[i].enabled;
				WristMenu.lastPressedButtonIndex = i;
				VRRig.LocalRig.PlayHandTapLocal(Mods.ButtonSound, false, 0.1f);
			}
		}
		GUI.EndScrollView();
	}
}
