using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Player
{
    public class StateJumpDown : BaseState
    {
        private float Timer;

        public StateJumpDown(Player model)
        {
            Model = model;
        }

        public override void Activate(float time = 0f)
        {
            Model.UpdateStateName("Jump Down");

            Timer = time;
        }

        public override void OnFixedUpdate()
        {
            Model.DirectionCheck();

            Model.JumpDown(true);

            if (Model.DirectionX != 0)
            {
                Model.Walk();
            }

            // State Jump Falling, Timer finished in air
            if (Timer > 0)
            {
                Timer -= Time.fixedDeltaTime;
                if (Timer <= 0)
                {
                    Model.StandUp();
                    Model.JumpDown(false);
                    Model.SetState(Model.StateJumpFalling);
                }
            }

            // State Idle -> only when hit "Ground" layer
            if (Model.Grounded(LayerMasks.Ground))
            {
                Model.UpdateInAir(false);

                Model.StandUp();
                Model.JumpDown(false);
                Model.Animations.Idle();
                Model.SetState(Model.StateIdle);
            }
        }
    }
}