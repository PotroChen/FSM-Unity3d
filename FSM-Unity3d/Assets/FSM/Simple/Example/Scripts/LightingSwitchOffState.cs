using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CLToolKits.FSM.Simple;

namespace CLToolKits.FSM.Simple.Example
{
	public class LightingSwitchOffState : StateBehaviour 
	{

		private Light light;
		public override void OnEnter()
		{
			base.OnEnter();
			light = GameObject.FindObjectOfType<Light>();
			light.enabled = false;
		}

		public override void OnLeave()
		{
			base.OnEnter();
			light.enabled = true;
		}
	}
}