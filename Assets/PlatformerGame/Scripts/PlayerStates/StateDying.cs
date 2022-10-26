using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Player
{
    public class StateDying : BaseState
    {
        private float Timer;

        public StateDying(Player model)
        {
            Model = model;
        }

        public override void Activate(float time = 0f)
        {
            Model.UpdateStateName("Death");

            Timer = time;
            Model.Animations.Dying();
        }

        public override void OnUpdate()
        {
            // no input!
        }

        public override void OnFixedUpdate()
        {
            Timer -= Time.fixedDeltaTime;

            if (Timer <= 0)
            {
                Model.EnableGameOver();
            }
        }
    }
}