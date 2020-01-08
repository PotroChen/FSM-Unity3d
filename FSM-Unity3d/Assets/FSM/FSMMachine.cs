using System;
using System.Collections.Generic;
using UnityEngine;

namespace CLToolKits.FSM
{
    public class FSMMachine :State,IStateMachine
    {

        /// <summary>
        /// 当前状态
        /// </summary>
        /// <value>状态机的当前状态</value>
        public IState CurrentState
        {
            get
            {
                return currentState;
            }
        }

        /// <summary>
        /// 默认状态
        /// </summary>
        /// <value>状态机的默认状态</value>
        public IState DefaultState
        {
            get
            {
                return defaultState;
            }
            set
            {
                AddState(value);
                defaultState = value;
            }
        }

        private IState defaultState;
        private IState currentState;
        private List<IState> states;
        private bool isTranslating = false;

        private ITransition currentTransition;//当前正在执行的ITransition
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="name"></param>
        public FSMMachine(string name,IState defaultState):base(name)
        {
            states = new List<IState>();
            this.defaultState = defaultState;
        }
        /// <summary>
        /// 添加状态
        /// </summary>
        /// <param name="state">要添加的状态</param>
        public void AddState(IState state)
        {
            if(states ==null || states.Contains(state))
                return;
            states.Add(state);
            state.StateMachine = this;
            if(defaultState==null)
                defaultState = state;
        }

        /// <summary>
        /// 删除状态
        /// </summary>
        /// <param name="state">要删除的状态</param>
        public void RemoveState(IState state)
        {
            if(currentState == state)
                return;
            if(states ==null || !states.Contains(state))
                return;
            states.Remove(state);
            state.StateMachine = null; 
            if(defaultState == state)
            {
                defaultState = states.Count>=1?states[0]:null;
            }
        }

        /// <summary>
        /// 通过指定的Tag值查找状态
        /// </summary>
        /// <param name="tag">状态的tag值</param>
        /// <returns>有这个Tag的状态</returns>
        /// TODO 待补齐
        public List<IState> GetStatesWithTag(string tag)
        {
            List<IState> rtn = new List<IState>();
            foreach(var state in states)
            {
                if(state.Tag == tag)
                    rtn.Add(state);
            }
            
            return rtn.Count>0?rtn:null;
        }

        public override void UpdateCallback(float deltaTime)
        {
             if(isTranslating)
            {
                if(currentTransition.TransitionCallback())
                {
                    DoTransition(currentTransition);
                    isTranslating = false;
                }
                return;
            }
            base.UpdateCallback(deltaTime);
            if (currentState == null)
                currentState = defaultState;
            foreach(var transition in currentState.Transitions)
            {
                if(transition.ShouldBegin())
                {
                    currentTransition = transition;
                    isTranslating = true;
                    return;
                }
            }
            currentState.UpdateCallback(deltaTime);
        }

        public override void LateUpdateCallback(float deltaTime)
        {
            if(isTranslating)
                return;
            base.LateUpdateCallback(deltaTime);
            currentState.LateUpdateCallback(deltaTime);
        }

        public override void FixedUpdateCallback()
        {
            if(isTranslating)
                return;
            base.FixedUpdateCallback();
            currentState.FixedUpdateCallback();
        }

        //开始进行过渡
        private void DoTransition(ITransition transition)
        {
            currentState.ExitCallback(transition.To);
            currentState = transition.To;
            currentState.EnterCallback(transition.From);
        }
    }
}
