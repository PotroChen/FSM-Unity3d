using System;
using System.Collections.Generic;
using UnityEngine;

namespace CLToolKits.FSM
{
    public class State :IState
    {
        public string Name{get;private set;}

        public string Tag{get;set;}

        /// <summary>
        /// 当前状态的状态机
        /// </summary>
        /// <value></value>
        public IStateMachine StateMachine
        {
            get{return stateMachine;}
            set{stateMachine = value;}
        }

        public float Timer{get;private set;}

        public List<ITransition> Transitions
        {
            get
            {
                return transitions;
            }
        }

        private IStateMachine stateMachine;
        private List<ITransition> transitions;
        /// <summary>
        /// 当进入状态时调用的事件
        /// </summary>
        public event Action<IState> OnEnter;

        /// <summary>
        /// 当离开状态时调用的事件
        /// </summary>
        public event Action<IState> OnExit;

        /// <summary>
        /// 当Update时调用的事件
        /// </summary>
        public event Action<float> OnUpdate;

        /// <summary>
        /// 当LateUpdate时调用的事件
        /// </summary>
        public event Action<float> OnLateUpdate;

        /// <summary>
        /// 当FixedUpdate时调用的事件
        /// </summary>
        public event Action OnFixedUpdate;
        public State(string name)
        {
            Name = name;
            transitions = new List<ITransition>();
        }

        /// <summary>
        /// 添加过渡
        /// </summary>
        /// <param name="transition">状态过渡</param>
        public void AddTransition(ITransition transition)
        {
            if(transitions==null || transitions.Contains(transition))
                return;

            transitions.Add(transition);
        }

        /// <summary>
        /// 进入状态时的回调
        /// </summary>
        /// <param name="prev">上一个状态</param>
        public virtual void EnterCallback(IState prev)
        {
            OnEnter?.Invoke(prev);
             //重置计时器
            Timer = 0f;

        }

        /// <summary>
        /// 退出状态时的回调
        /// </summary>
        /// <param name="next">下一个状态</param>
        public virtual void ExitCallback(IState next)
        {
            OnExit?.Invoke(next);
            //重置计时器
            Timer = 0f;
        }

        /// <summary>
        /// Update的回调
        /// </summary>
        /// <param name="deltaTime">Time.deltaTime</param>
        public virtual void UpdateCallback(float deltaTime)
        {
            OnUpdate?.Invoke(deltaTime);
            //累计计时器
            Timer +=deltaTime;
        }

        /// <summary>
        /// LateUpdate的回调
        /// </summary>
        /// <param name="deltaTime">Time.deltaTime</param>
        public virtual void LateUpdateCallback(float deltaTime)
        {
            OnLateUpdate?.Invoke(deltaTime);
        }

        /// <summary>
        /// FixedUpdate的回调
        /// </summary>
        public virtual void FixedUpdateCallback()
        {
            OnFixedUpdate?.Invoke();
        }
    }
}

