using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Player
{
    public class StateDamageTaken : BaseState
    {
        private float Timer;

        public StateDamageTaken(Player model)
        {
            Model = model;
        }

        public override void Activate(float time = 0f)
        {
            Model.UpdateStateName("Damage Taken");

            Timer = time;
            Model.ResetVelocity();
            Model.DamagePushBack();
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
                // Idle
                if (Model.Grounded(LayerMasks.Walkable))
                {
                    Model.DirectionX *= -1;
                    Model.DirectionCheck();
                    Model.Animations.Idle();
                    Model.SetState(Model.StateIdle);
                }
            }
        }
    }
}