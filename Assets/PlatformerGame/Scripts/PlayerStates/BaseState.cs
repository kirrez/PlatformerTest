using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Player
{
    public abstract class BaseState
    {
        protected Player Model;

        public virtual void Activate(float time = 0f)
        {
            
        }

        public virtual void OnUpdate()
        {
            Model.GetInput();
        }

        public virtual void OnFixedUpdate()
        {

        }
    }
}