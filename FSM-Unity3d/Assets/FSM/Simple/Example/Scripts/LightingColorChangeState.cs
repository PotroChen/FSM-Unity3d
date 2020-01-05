using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CLToolKits.FSM.Simple;

public class LightingColorChangeState : StateBehaviour {

	private Color targetColor;
	private Color oriColor;
	private Light light;
	public LightingColorChangeState(Color TargetColor)
	{
		targetColor = TargetColor;
	}

	public override void OnEnter()
	{
		base.OnEnter();
		light = GameObject.FindObjectOfType<Light>();
		oriColor = light.color;//记录原来的颜色
		light.color = targetColor;
	}

	public override void OnLeave()
	{
		base.OnEnter();
		light.color = targetColor;//恢复颜色
	}
}
