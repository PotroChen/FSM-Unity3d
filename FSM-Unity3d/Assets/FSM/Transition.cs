using System;
using System.Collections.Generic;
using UnityEngine;

namespace CLToolKits.FSM
{
    public class Transition : ITransition
    {
        public event Func<bool> OnTransition;
        public event Func<bool> OnCheck;

        /// <summary>
        /// The State which transition translate from
        /// </summary>
        /// <value></value>
        public IState From{get;set;}

        /// <summary>
        /// The State which transition translate to
        /// </summary>
        /// <value></value>
        public IState To{get;set;}

        public Transition(IState from,IState to)
        {
            this.From = from;
            this.To = to;
        }

        public bool TransitionCallback()
        {
            if(OnTransition != null)
                return OnTransition();

            return true;
        }

        public bool ShouldBegin()
        {
            if(OnCheck!=null)
                return OnCheck();
            return false;
        }
    }
}
