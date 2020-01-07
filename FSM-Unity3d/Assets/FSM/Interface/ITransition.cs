using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CLToolKits.FSM
{
    public interface ITransition
    {
        /// <summary>
        /// The State which transition translate from
        /// </summary>
        /// <value></value>
        IState From{get;set;}

        /// <summary>
        /// The State which transition translate to
        /// </summary>
        /// <value></value>
        IState To{get;set;}

        /// <summary>
        /// Callback,invoked when translating
        /// </summary>
        /// <returns>是否过渡结束</returns>
        bool TransitionCallback();

        /// <summary>
        /// 能否开始过渡
        /// </summary>
        /// <returns>if true,transition begin.if false dont</returns>
        bool ShouldBegin();
    }
}