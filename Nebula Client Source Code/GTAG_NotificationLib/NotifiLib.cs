using System;
using System.Linq;
using MalachiTemp.UI;
using UnityEngine;
using UnityEngine.UI;

namespace GTAG_NotificationLib;

public class NotifiLib : MonoBehaviour
{
	private GameObject HUDObj;

	private GameObject HUDObj2;

	private GameObject MainCamera;

	private Text Testtext;

	private Material AlertText = new Material(Shader.Find("GUI/Text Shader"));

	private int NotificationDecayTime = 150;

	private int NotificationDecayTimeCounter = 200;

	public static int NoticationThreshold = WristMenu.MaxNotis;

	private string[] Notifilines;

	private string newtext;

	public static string PreviousNotifi;

	private bool HasInit = false;

	private static Text NotifiText;

	public static bool IsEnabled = true;

	public static float ropedelay;

	private void Init()
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Expected O, but got Unknown
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Expected O, but got Unknown
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Expected O, but got Unknown
		//IL_02e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_033b: Unknown result type (might be due to invalid IL or missing references)
		MainCamera = GameObject.Find("Main Camera");
		if ((Object)(object)MainCamera == (Object)null)
		{
			Debug.LogError((object)"Main Camera not found.");
			return;
		}
		HUDObj = new GameObject("NOTIFICATIONLIB_HUD_OBJ");
		HUDObj2 = new GameObject("NOTIFICATIONLIB_HUD_OBJ2");
		HUDObj.AddComponent<Canvas>();
		HUDObj.AddComponent<CanvasScaler>();
		HUDObj.AddComponent<GraphicRaycaster>();
		Canvas component = HUDObj.GetComponent<Canvas>();
		if ((Object)(object)component == (Object)null)
		{
			Debug.LogError((object)"Canvas not added to HUDObj.");
			return;
		}
		((Behaviour)HUDObj.GetComponent<Canvas>()).enabled = true;
		HUDObj.GetComponent<Canvas>().renderMode = (RenderMode)2;
		HUDObj.GetComponent<Canvas>().worldCamera = MainCamera.GetComponent<Camera>();
		HUDObj.GetComponent<RectTransform>().sizeDelta = new Vector2(5f, 5f);
		((Transform)HUDObj.GetComponent<RectTransform>()).position = new Vector3(MainCamera.transform.position.x, MainCamera.transform.position.y, MainCamera.transform.position.z);
		HUDObj2.transform.position = new Vector3(MainCamera.transform.position.x, MainCamera.transform.position.y, MainCamera.transform.position.z - 4.6f);
		HUDObj.transform.SetParent(HUDObj2.transform);
		((Transform)HUDObj.GetComponent<RectTransform>()).localPosition = new Vector3(0f, 0f, 1.6f);
		Quaternion rotation = ((Transform)HUDObj.GetComponent<RectTransform>()).rotation;
		Vector3 eulerAngles = ((Quaternion)(ref rotation)).eulerAngles;
		eulerAngles.y = -270f;
		HUDObj.transform.localScale = new Vector3(1f, 1f, 1f);
		((Transform)HUDObj.GetComponent<RectTransform>()).rotation = Quaternion.Euler(eulerAngles);
		GameObject val = new GameObject();
		val.transform.parent = HUDObj.transform;
		Testtext = val.AddComponent<Text>();
		if ((Object)(object)Testtext == (Object)null)
		{
			Debug.LogError((object)"Text not added to TestText.");
			return;
		}
		Testtext.text = "";
		Testtext.fontSize = 9;
		Testtext.font = WristMenu.MenuFont;
		((Graphic)Testtext).rectTransform.sizeDelta = new Vector2(260f, 70f);
		Testtext.alignment = (TextAnchor)6;
		((Transform)((Graphic)Testtext).rectTransform).localScale = new Vector3(0.01f, 0.01f, 1f);
		((Transform)((Graphic)Testtext).rectTransform).localPosition = new Vector3(-1.2f, -0.7f, -0.6f);
		if ((Object)(object)AlertText == (Object)null)
		{
			Debug.LogError((object)"AlertText material is not set.");
			return;
		}
		((Graphic)Testtext).material = AlertText;
		NotifiText = Testtext;
	}

	private void FixedUpdate()
	{
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		if (!HasInit && (Object)(object)GameObject.Find("Main Camera") != (Object)null)
		{
			Init();
			HasInit = true;
		}
		HUDObj2.transform.position = new Vector3(MainCamera.transform.position.x, MainCamera.transform.position.y, MainCamera.transform.position.z);
		HUDObj2.transform.rotation = MainCamera.transform.rotation;
		if (Testtext.text != "")
		{
			NotificationDecayTimeCounter++;
			if (NotificationDecayTimeCounter <= NotificationDecayTime)
			{
				return;
			}
			Notifilines = null;
			newtext = "";
			NotificationDecayTimeCounter = 0;
			Notifilines = Testtext.text.Split(Environment.NewLine.ToCharArray()).Skip(1).ToArray();
			string[] notifilines = Notifilines;
			foreach (string text in notifilines)
			{
				if (text != "")
				{
					newtext = newtext + text + "\n";
				}
			}
			Testtext.text = newtext;
		}
		else
		{
			NotificationDecayTimeCounter = 0;
		}
	}

	public static void SendNotification(string NotificationText)
	{
		if ((Object)(object)NotifiText == (Object)null)
		{
			Debug.LogError((object)"Cant Send Noti");
		}
		else
		{
			if (!(ropedelay < Time.time))
			{
				return;
			}
			ropedelay = Time.time + 0.05f;
			if (IsEnabled)
			{
				if (!NotificationText.Contains(Environment.NewLine))
				{
					NotificationText += Environment.NewLine;
				}
				NotifiText.text += NotificationText;
				PreviousNotifi = NotificationText;
			}
		}
	}

	public static void ClearAllNotifications()
	{
		NotifiText.text = "";
	}

	public static void ClearPastNotifications(int amount)
	{
		string[] array = null;
		string text = "";
		array = NotifiText.text.Split(Environment.NewLine.ToCharArray()).Skip(amount).ToArray();
		string[] array2 = array;
		foreach (string text2 in array2)
		{
			if (text2 != "")
			{
				text = text + text2 + "\n";
			}
		}
		NotifiText.text = text;
	}
}
