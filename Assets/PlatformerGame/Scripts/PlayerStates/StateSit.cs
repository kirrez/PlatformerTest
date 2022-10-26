using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Player
{
    public class StateSit : BaseState
    {
        public StateSit(Player model)
        {
            Model = model;
        }

        public override void OnFixedUpdate()
        {
            Model.UpdateStateName("Sit");

            // Push Down, should i use it while sitting?
            if (Model.DirectionY < 0)
            {
                Model.PushDown();
            }

            // Carried by platform
            if (Model.Grounded(LayerMasks.PlatformOneWay))
            {
                Model.StickToPlatform();
            }

            // Crouch
            if (Model.DirectionX != 0)
            {
                Model.Animations.Crouch();
                Model.SetState(Model.StateSitCrouch);
            }

            // Idle
            if (Model.DirectionY == 0 && !Model.Ceiled(LayerMasks.Ground + LayerMasks.Slope))
            {
                Model.StandUp();
                Model.Animations.Idle();
                Model.SetState(Model.StateIdle);
            }

            // State SitAttack
            if (Model.HitAttack == 1)
            {
                Model.Animations.SitAttack();
                Model.SetState(Model.StateSitAttack);
            }

            // Jump down from platform
            if (Model.HitJump == 1 && Model.Grounded(LayerMasks.OneWay + LayerMasks.PlatformOneWay))
            {
                Model.UpdateInAir(true);
                Model.Animations.JumpFalling();
                Model.StateJumpDown.Activate(Model.GetJumpDownCooldown()); // set time?
                Model.SetState(Model.StateJumpDown);
            }

            // Roll Down while on ground !! not Solid (esp Slope)
            if (Model.HitJump == 1 && Model.Grounded(LayerMasks.Ground))
            {
                Model.StateRollDown.Activate(Model.GetRollDownTime()); // fixed!
                Model.RollDown();
                Model.Animations.RollDown();
                Model.SetState(Model.StateRollDown);
            }

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