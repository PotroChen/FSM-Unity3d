using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CLToolKits.FSM
{
    public interface IState
    {
        /// <summary>
        /// 状态名
        /// </summary>
        /// <value></value>
        string Name{get;}

        /// <summary>
        /// 状态标签
        /// </summary>
        /// <value></value>
        string Tag{set;get;}

        /// <summary>
        /// 当前状态的状态机
        /// </summary>
        /// <value></value>
        IStateMachine StateMachine{get;set;}

        /// <summary>
        /// 该状态从开始到现在的时间
        /// </summary>
        /// <value></value>
        float Timer{get;}

        /// <summary>
        /// 状态过渡
        /// </summary>
        /// <value>当前状态的所有过渡</value>
        List<ITransition> Transitions{get;}
        /// <summary>
        /// 进入状态时的回调
        /// </summary>
        /// <param name="prev">上一个状态</param>
        void EnterCallback(IState prev);

        /// <summary>
        /// 退出状态时的回调
        /// </summary>
        /// <param name="next">下一个状态</param>
        void ExitCallback(IState next);

        /// <summary>
        /// Update的回调
        /// </summary>
        /// <param name="deltaTime">Time.deltaTime</param>
        void UpdateCallback(float deltaTime);

        /// <summary>
        /// LateUpdate的回调
        /// </summary>
        /// <param name="deltaTime">Time.deltaTime</param>
        void LateUpdateCallback(float deltaTime);

        /// <summary>
        /// FixedUpdate的回调
        /// </summary>
        void FixedUpdateCallback();

        /// <summary>
        /// 添加过渡
        /// </summary>
        /// <param name="transition">状态过渡</param>
        void AddTransition(ITransition transition);
    }
}
