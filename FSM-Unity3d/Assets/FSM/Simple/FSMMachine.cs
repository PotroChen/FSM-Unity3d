using System;
using System.Collections.Generic;

namespace CLToolKits.FSM.Simple
{
	public class FSMMachine<T>
	{
		private string fsmName;
		public bool Running {get;private set;}
		Dictionary<T,StateBehaviour> stateBehaviourDic = new Dictionary<T, StateBehaviour>();
		public T DefaultState{get; set;} = default(T);
		private T currentState;
		public T CurrentState
		{
			get
			{
				return currentState;
			} 
			set
			{
				ChangeStatus(value);
			}
		}

		//构造函数中添加默认状态
		public FSMMachine(string fsmName)
		{
			this.fsmName = fsmName;
		}

		//添加状态
		public void AddStatus(T state,StateBehaviour stateBehaviour)
		{
			stateBehaviourDic[state] = stateBehaviour;
		}

		//移除状态
		public void RemoveStatus(T state)
		{
			if(!stateBehaviourDic.ContainsKey(state))
				return;

			stateBehaviourDic.Remove(state);
		}

		//改变状态
		public void ChangeStatus(T state)
		{
			if(!stateBehaviourDic.ContainsKey(state)||stateBehaviourDic[state] == null)
				throw new InvalidOperationException(string.Format("[FSM {0}] : Can't call 'ChangeStatus' before the stateBehaviour of state has been settled.",fsmName));

			stateBehaviourDic[currentState].OnLeave();
			stateBehaviourDic[state].OnEnter();
			currentState = state;
		}

		public void Run()
		{

			if(!stateBehaviourDic.ContainsKey(DefaultState)||stateBehaviourDic[DefaultState] == null)
				throw new InvalidOperationException(string.Format("[FSM {0}] : Can't call 'ChangeStatus' before the stateBehaviour of state has been settled.",fsmName));

			stateBehaviourDic[DefaultState].OnEnter();
			currentState = DefaultState;
			Running = true;
		}

		public void Stop()
		{

			if(!stateBehaviourDic.ContainsKey(currentState)||stateBehaviourDic[currentState] == null)
				throw new InvalidOperationException(string.Format("[FSM {0}] : Can't call 'ChangeStatus' before the stateBehaviour of state has been settled.",fsmName));

			stateBehaviourDic[currentState].OnLeave();
			Running = false;
		}

		public void Process()
		{
			if(!stateBehaviourDic.ContainsKey(currentState)||stateBehaviourDic[currentState] == null)
				throw new InvalidOperationException(string.Format("[FSM {0}] : Can't call 'ChangeStatus' before the stateBehaviour of state has been settled.",fsmName));

			if(!Running)
				return;
				
			stateBehaviourDic[currentState].OnProcess();
		}
	}
}
