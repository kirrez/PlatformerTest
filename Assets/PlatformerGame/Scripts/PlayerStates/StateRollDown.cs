using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Player
{
    public class StateRollDown : BaseState
    {
        private float Timer;

        public StateRollDown(Player model)
        {
            Model = model;
        }

        public override void Activate(float time = 0f)
        {
            Model.UpdateStateName("Roll Down");

            Timer = time;
            Model.ResetVelocity();
        }

        public override void OnUpdate()
        {
            if (Timer <= 0)
            {
                Model.GetInput();
            }
        }

        public override void OnFixedUpdate()
        {
            Timer -= Time.fixedDeltaTime;

            if (Timer <= 0)
            {
                // Idle and Walk
                if (!Model.Ceiled(LayerMasks.Solid) && Model.DirectionY > -1)
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

                // Sit and Crouch
                if (Model.Ceiled(LayerMasks.Solid) || Model.DirectionY == -1)
                {
                    if (Model.DirectionX == 0)
                    {
                        Model.Animations.Sit();
                        Model.SetState(Model.StateSit);
                    }
                    else if (Model.DirectionX != 0)
                    {
                        Model.Animations.Crouch();
                        Model.SetState(Model.StateSitCrouch);
                    }
                }
            }

            // State Jump Rising without hitting "Jump" button ))
            if (Model.DeltaY > 0 && !Model.Grounded(LayerMasks.Walkable))
            {
                Timer = 0f;
                Model.UpdateInAir(true);
                Model.StandUp(); // no ceiling check
                Model.Animations.JumpRising();
                Model.SetState(Model.StateJumpRising);
            }

            // State Jump Falling, something disappeared right beneath your feet!
            if (Model.DeltaY < 0 && !Model.Grounded(LayerMasks.Walkable))
            {
                Timer = 0f;
                Model.UpdateInAir(true);
                Model.StandUp(); // no ceiling check
                Model.Animations.JumpFalling();
                Model.SetState(Model.StateJumpFalling);
            }
        }
    }
}