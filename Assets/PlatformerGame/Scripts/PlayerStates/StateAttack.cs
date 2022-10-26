using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Player
{
    public class StateAttack : BaseState
    {
        public StateAttack(Player model)
        {
            Model = model;
        }

        public override void OnFixedUpdate()
        {
            Model.UpdateStateName("Attack");

            Model.AttackCheck();

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

            // State Idle
            if (Model.HitAttack == 0 && Model.Grounded(LayerMasks.Walkable))
            {
                Model.Animations.Idle();
                Model.SetState(Model.StateIdle);
            }

            // State Sit Attack
            if (Model.DirectionY == -1)
            {
                Model.SitDown();
                Model.Animations.SitAttack();
                Model.SetState(Model.StateSitAttack);
            }

            // Jump Rising without hitting "Jump"
            if (Model.DeltaY > 0 && !Model.Grounded(LayerMasks.Walkable))
            {
                Model.UpdateInAir(true);
                Model.Animations.AirAttack();
                Model.SetState(Model.StateJumpRisingAttack);
            }

            // jump Falling
            if (Model.DeltaY < 0 && !Model.Grounded(LayerMasks.Walkable))
            {
                Model.UpdateInAir(true);
                Model.Animations.AirAttack();
                Model.SetState(Model.StateJumpFallingAttack);
            }
        }
    }
}