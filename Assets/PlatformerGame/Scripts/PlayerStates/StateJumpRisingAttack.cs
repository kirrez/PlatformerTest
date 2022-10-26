using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Player
{
    public class StateJumpRisingAttack : BaseState
    {
        public StateJumpRisingAttack(Player model)
        {
            Model = model;
        }

        public override void OnFixedUpdate()
        {
            Model.UpdateStateName("Jump Rising Attack");

            Model.DirectionCheck();

            Model.AttackCheck();

            // Push Down
            if (Model.DirectionY < 0)
            {
                Model.PushDown();
            }

            // Horizontal movement, controllable jump
            if (Model.DirectionX != 0)
            {
                Model.Walk();
            }

            // State JumpFalling
            if ((Model.DeltaY < 0 && Model.HitAttack == 0))
            {
                Model.Animations.JumpFalling();
                Model.SetState(Model.StateJumpFalling);
            }

            // State JumpFalling Attack ))
            if (Model.DeltaY < 0 && Model.HitAttack == 1)
            {
                Model.Animations.AirAttack();
                Model.SetState(Model.StateJumpFallingAttack);
            }
        }
    }
}