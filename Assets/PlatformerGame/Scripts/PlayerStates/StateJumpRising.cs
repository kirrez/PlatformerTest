using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Player
{
    public class StateJumpRising : BaseState
    {
        public StateJumpRising(Player model)
        {
            Model = model;
        }

        public override void OnFixedUpdate()
        {
            Model.UpdateStateName("Jump Rising");

            Model.DirectionCheck();

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

            // State Walk, while in jump on a steep slope
            if (Model.DirectionX != 0 && Model.Grounded(LayerMasks.Ground + LayerMasks.Slope))
            {
                Model.Animations.Walk();
                Model.SetState(Model.StateWalk);
            }

            // State Jump Falling
            if (Model.DeltaY < 0)
            {
                Model.Animations.JumpFalling();
                Model.SetState(Model.StateJumpFalling);
            }

            // State JumpRising Attack
            if (Model.HitAttack == 1)
            {
                Model.Animations.AirAttack();
                Model.SetState(Model.StateJumpRisingAttack);
            }

        }
    }
}