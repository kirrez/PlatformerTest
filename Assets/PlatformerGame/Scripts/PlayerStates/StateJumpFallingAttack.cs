using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Player
{
    public class StateJumpFallingAttack : BaseState
    {
        public StateJumpFallingAttack(Player model)
        {
            Model = model;
        }

        public override void OnFixedUpdate()
        {
            Model.UpdateStateName("Jump Falling Attack");

            Model.DirectionCheck();

            Model.AttackCheck();

            // controllable horizontal
            if (Model.DirectionX != 0)
            {
                Model.Walk();
            }

            // State Attack
            if (Model.Grounded(LayerMasks.Walkable) && Model.HitAttack == 1)
            {
                Model.ResetVelocity();
                Model.UpdateInAir(false);
                Model.Animations.Attack();
                Model.SetState(Model.StateAttack);
            }

            // State Idle
            if (Model.DirectionX == 0 && Model.Grounded(LayerMasks.Walkable) && Model.HitAttack == 0)
            {
                Model.ResetVelocity();
                Model.UpdateInAir(false);
                Model.Animations.Idle();
                Model.SetState(Model.StateIdle);
            }

            // State Walk
            if (Model.DirectionX != 0 && Model.Grounded(LayerMasks.Walkable) && Model.HitAttack == 0)
            {
                Model.UpdateInAir(false);
                Model.Animations.Walk();
                Model.SetState(Model.StateWalk);
            }
        }
    }
}