using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Player
{
    public class StateWalk : BaseState
    {
        public StateWalk(Player model)
        {
            Model = model;
        }

        public override void OnFixedUpdate()
        {
            Model.UpdateStateName("Walk");

            Model.DirectionCheck();

            // Horizontal movement with checking platform riding
            if (Model.Grounded(LayerMasks.Platforms))
            {
                Model.Walk(true);
            }
            else
            {
                Model.Walk(false);
            }

            // State Idle
            if (Model.DirectionX == 0)
            {
                Model.Animations.Idle();
                Model.SetState(Model.StateIdle);
            }

            // State Sit
            if (Model.DirectionY == -1)
            {
                Model.SitDown();
                Model.Animations.Sit();
                Model.SetState(Model.StateSit);
            }

            // State Jump Rising, from ground
            if (Model.HitJump == 1 && Model.Grounded(LayerMasks.Walkable))
            {
                Model.UpdateInAir(true);
                Model.ResetVelocity();
                Model.Animations.JumpRising();
                Model.SetState(Model.StateJumpRising);
                Model.Jump();
            }

            // State Jump Rising without hitting "Jump" button ))
            if (Model.DeltaY > 0 && !Model.Grounded(LayerMasks.Walkable))
            {
                Model.UpdateInAir(true);
                Model.Animations.JumpRising();
                Model.SetState(Model.StateJumpRising);
            }

            // State Jump Falling
            if (Model.DeltaY < 0 && !Model.Grounded(LayerMasks.Walkable))
            {
                Model.UpdateInAir(true);
                Model.Animations.JumpFalling();
                Model.SetState(Model.StateJumpFalling);
            }
        }
    }
}