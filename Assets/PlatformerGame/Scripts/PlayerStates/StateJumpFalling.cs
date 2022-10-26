using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Player
{
    public class StateJumpFalling : BaseState
    {
        public StateJumpFalling(Player model)
        {
            Model = model;
        }

        public override void OnFixedUpdate()
        {
            Model.UpdateStateName("Jump Falling");

            Model.DirectionCheck();

            // Horizontal movement, controllable fall
            if (Model.DirectionX != 0)
            {
                Model.Walk();
            }

            // State JumpFallingAttack
            if (Model.HitAttack == 1)
            {
                Model.Animations.AirAttack();
                Model.SetState(Model.StateJumpFallingAttack);
            }

            // State Idle
            if (Model.DirectionX == 0 && Model.Grounded(LayerMasks.Walkable))
            {
                Model.ResetVelocity();
                Model.UpdateInAir(false);
                Model.Animations.Idle();
                Model.SetState(Model.StateIdle);
            }

            // State Walk
            if (Model.DirectionX != 0 && Model.Grounded(LayerMasks.Walkable))
            {
                Model.UpdateInAir(false);
                Model.Animations.Walk();
                Model.SetState(Model.StateWalk);
            }

        }
    }
}