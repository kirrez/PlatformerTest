using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Player
{
    public class StateIdle : BaseState
    {
        public StateIdle(Player model)
        {
            Model = model;
        }

        public override void OnFixedUpdate()
        {
            Model.UpdateStateName("Idle");

            Model.StandUp();

            // Carried by platform
            if (Model.Grounded(LayerMasks.PlatformOneWay))
            {
                Model.StickToPlatform();
            }

            // Steep slope
            if (Model.Grounded(LayerMasks.Slope))
            {
                Model.ResetVelocity();
            }

            // State Walk
            if (Model.DirectionX != 0 && Model.Grounded(LayerMasks.Walkable))
            {
                Model.Animations.Walk();
                Model.SetState(Model.StateWalk);
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
                Model.UpdateInAir(true);//
                Model.Jump();
                Model.Animations.JumpRising();
                Model.SetState(Model.StateJumpRising);
            }

            // State Jump Rising without hitting "Jump" button ))
            if (Model.DeltaY > 0 && !Model.Grounded(LayerMasks.Walkable))
            {
                Model.UpdateInAir(true);//
                Model.Animations.JumpRising();
                Model.SetState(Model.StateJumpRising);
            }

            // State Jump Falling, something disappeared right beneath your feet!
            if (Model.DeltaY < 0 && !Model.Grounded(LayerMasks.Walkable))
            {
                Model.UpdateInAir(true);//
                Model.Animations.JumpFalling();
                Model.SetState(Model.StateJumpFalling);
            }

            // State Attack
            if (Model.HitAttack == 1)
            {
                Model.Animations.Attack();
                Model.SetState(Model.StateAttack);
            }
        }
    }
}