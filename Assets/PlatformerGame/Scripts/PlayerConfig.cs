using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class PlayerConfig
    {
        public float HorizontalSpeed { get; private set; }
        public float CrouchSpeed { get; private set; }
        public float PushDownForce { get; private set; }
        public float JumpForce { get; private set; }
        public float RollDownForce { get; private set; }

        public float PrimaryAttackCooldown { get; private set; }
        public float JumpCooldown { get; private set; }
        public float JumpDownCooldown { get; private set; }
        public float RollDownTime { get; private set; }
        public float DamageShockTime { get; private set; }
        public float DeathShockTime { get; private set; }

        public PlayerConfig()
        {
            HorizontalSpeed    = 300f;
            CrouchSpeed        = 175f;
            PushDownForce      = 50f;
            JumpForce          = 350f;
            RollDownForce      = 310f;

            PrimaryAttackCooldown = 0.5f;

            JumpCooldown = 0.5f;
            JumpDownCooldown = 0.4f;
            RollDownTime = 0.65f;

            DamageShockTime = 0.75f;
            DeathShockTime = 3f;
        }
    }
}