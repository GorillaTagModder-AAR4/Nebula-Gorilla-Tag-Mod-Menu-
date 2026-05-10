using UnityEngine;

namespace MalachiTemp.Utilities;

public class ColorChanger : TimedBehaviour
{
	public Renderer gameObjectRenderer;

	public Gradient colors = null;

	public Color color;

	public bool timeBased = true;

	public override void Start()
	{
		base.Start();
		if ((Object)(object)((Component)this).GetComponent<Renderer>() != (Object)null)
		{
			gameObjectRenderer = ((Component)this).GetComponent<Renderer>();
		}
	}

	public override void Update()
	{
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		base.Update();
		if (colors != null)
		{
			if (timeBased)
			{
				color = colors.Evaluate(progress);
			}
			gameObjectRenderer.material.color = color;
			gameObjectRenderer.material.SetColor("_EmissionColor", color);
		}
	}
}
