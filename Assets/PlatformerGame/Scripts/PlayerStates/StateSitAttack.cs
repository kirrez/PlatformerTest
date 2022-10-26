
namespace Platformer.Player
{
    public class StateSitAttack : BaseState
    {
        public StateSitAttack(Player model)
        {
            Model = model;
        }

        public override void OnFixedUpdate()
        {
            Model.UpdateStateName("Sit Attack");

            Model.AttackCheck();

            // Push Down
            if (Model.DirectionY < 0)
            {
                Model.PushDown();
            }

            // Carried by platform
            if (Model.Grounded(LayerMasks.PlatformOneWay))
            {
                Model.StickToPlatform();
            }

            // State Attack
            if (Model.DirectionY > -1 && !Model.Ceiled(LayerMasks.Ground + LayerMasks.Slope))
            {
                Model.StandUp();
                Model.Animations.Attack();
                Model.SetState(Model.StateAttack);
            }

            // State Sit
            if (Model.HitAttack == 0)
            {
                Model.Animations.Sit();
                Model.SetState(Model.StateSit);
            }

            // Jump down from platform
            if (Model.HitJump == 1 && Model.Grounded(LayerMasks.OneWay + LayerMasks.PlatformOneWay))
            {
                Model.UpdateInAir(true);
                Model.Animations.JumpFalling();
                Model.StateJumpDown.Activate();
                Model.SetState(Model.StateJumpDown);
            }

            // State Jump Rising Attack without hitting "Jump" button ))
            if (Model.DeltaY > 0 && !Model.Grounded(LayerMasks.Walkable))
            {
                Model.StandUp(); // no ceiling check..
                Model.Animations.AirAttack();
                Model.SetState(Model.StateJumpRisingAttack);
            }

            // State Jump Falling Attack, something disappeared right beneath your feet!
            if (Model.DeltaY < 0 && !Model.Grounded(LayerMasks.Walkable))
            {
                Model.StandUp(); // no ceiling check..
                Model.Animations.AirAttack();
                Model.SetState(Model.StateJumpFallingAttack);
            }
        }
    }
}