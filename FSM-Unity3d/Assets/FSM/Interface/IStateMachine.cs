using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CLToolKits.FSM
{
    /// <summary>
    /// 状态机接口
    /// </summary>
    public interface IStateMachine
    {
        /// <summary>
        /// 当前状态
        /// </summary>
        /// <value>状态机的当前状态</value>
        IState CurrentState{get;}

        /// <summary>
        /// 默认状态
        /// </summary>
        /// <value>状态机的默认状态</value>
        IState DefaultState{set;get;}

        /// <summary>
        /// 添加状态
        /// </summary>
        /// <param name="state">要添加的状态</param>
        void AddState(IState state);

        /// <summary>
        /// 删除状态
        /// </summary>
        /// <param name="state">要删除的状态</param>
        void RemoveState(IState state);

        /// <summary>
        /// 通过指定的Tag值查找状态
        /// </summary>
        /// <param name="tag">状态的tag值</param>
        /// <returns>有这个Tag的状态</returns>
        List<IState> GetStatesWithTag(string tag);
    }
}

