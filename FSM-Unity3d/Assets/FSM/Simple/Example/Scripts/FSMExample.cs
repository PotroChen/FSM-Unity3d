using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CLToolKits.FSM.Simple;

namespace CLToolKits.FSM.Simple.Example
{
	public enum StateExample
	{
		LightOffOn,
		LightColorSwitch
	}
	public class FSMExample : MonoBehaviour 
	{
		FSMMachine<StateExample> fSMMachine;
		// Use this for initialization
		void Start () {

			fSMMachine = new FSMMachine<StateExample>("Example");
			fSMMachine.DefaultState =  StateExample.LightOffOn;
			fSMMachine.AddStatus(StateExample.LightOffOn, new LightingSwitchOffState());
			fSMMachine.AddStatus(StateExample.LightColorSwitch, new LightingColorChangeState(Color.red));

			fSMMachine.Run();

			StartCoroutine(ChangeState());
		}

		void Update()
		{
			if(fSMMachine!=null&&fSMMachine.Running)
			{
				fSMMachine.Process();
			}
		}

		IEnumerator ChangeState()
		{
			yield return new WaitForSeconds(2f);
			fSMMachine.CurrentState = StateExample.LightColorSwitch;
		}
	}
}