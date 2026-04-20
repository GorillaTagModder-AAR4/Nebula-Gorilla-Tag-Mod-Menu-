using System;

namespace MalachiTemp.UI;

public class ButtonInfo
{
	public string buttonText = "Error";

	public Action method = null;

	public Action disableMethod = null;

	public bool? enabled = false;

	public bool? nontoggleable = false;

	public string toolTip = "This button doesn't have a tooltip!";
}
