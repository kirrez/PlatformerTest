using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Player
{
    public class StateSitCrouch : BaseState
    {
        public StateSitCrouch(Player model)
        {
            Model = model;
        }

        public override void OnFixedUpdate()
        {
            Model.UpdateStateName("Sit Crouch");

            Model.DirectionCheck();

            // Horizontal movement with checking platform riding
            if (Model.Grounded(LayerMasks.Platforms))
            {
                Model.Crouch(true);
            }
            else
            {
                Model.Crouch(false);
            }

            // Sit
            if (Model.DirectionX == 0)
            {
                Model.Animations.Sit();
                Model.SetState(Model.StateSit);
            }

            // Idle and Walk
            if (Model.DirectionY > -1 && !Model.Ceiled(LayerMasks.Solid))
            {
                if (Model.DirectionX == 0)
                {
                    Model.StandUp();
                    Model.Animations.Idle();
                    Model.SetState(Model.StateIdle);
                }
                else if (Model.DirectionX != 0)
                {
                    Model.StandUp();
                    Model.Animations.Walk();
                    Model.SetState(Model.StateWalk);
                }
            }

            // Roll Down
            if (Model.HitJump == 1 && Model.Grounded(LayerMasks.Ground))
            {
                Model.StateRollDown.Activate(Model.GetRollDownTime());
                Model.RollDown();
                Model.Animations.RollDown();
                Model.SetState(Model.StateRollDown);
            }

            // no attack
            // no jump down

            // State Jump Rising without hitting "Jump" button ))
            if (Model.DeltaY > 0 && !Model.Grounded(LayerMasks.Walkable))
            {
                Model.UpdateInAir(true);
                Model.StandUp(); // no ceiling check
                Model.Animations.JumpRising();
                Model.SetState(Model.StateJumpRising);
            }

            // State Jump Falling, something disappeared right beneath your feet!
            if (Model.DeltaY < 0 && !Model.Grounded(LayerMasks.Walkable))
            {
                Model.UpdateInAir(true);
                Model.StandUp(); // no ceiling check
                Model.Animations.JumpFalling();
                Model.SetState(Model.StateJumpFalling);
            }
        }
    }
}