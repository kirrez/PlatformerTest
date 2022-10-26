using UnityEngine;
using System;

namespace Platformer
{
    public class Health : MonoBehaviour
    {
        public Action Killed = () => { };
        public Action HealthChanged = () => { };

        [HideInInspector]
        public float DamageDirection;

        [SerializeField]
        private int MaxHealth;
        private int CurrentHealth;

        [SerializeField]
        private float DamageCooldown = 0.5f;
        private float DamageTimer;

        private void OnEnable()
        {
            RefillHealth();
        }

        private void FixedUpdate()
        {
            if (DamageTimer > 0)
            {
                DamageTimer -= Time.deltaTime;
            }
        }

        public int GetHealth
        {
            get { return CurrentHealth; }
        }

        [SerializeField]
        private int Type; // 0-Player, 1-Enemy, etc


        public int GetCharacterType()
        {
            return Type;
        }

        public void RefillHealth()
        {
            CurrentHealth = MaxHealth;
        }

        public void ResetDamageCooldown()
        {
            DamageTimer = DamageCooldown;
        }

        public void ReceiveDamage(int damage, float direction)
        {
            DamageDirection = direction;

            if (DamageTimer <= 0)
            {
                ResetDamageCooldown();
                CurrentHealth -= damage;
                if (CurrentHealth > 0)
                {
                    HealthChanged();
                }
                else if (CurrentHealth <= 0)
                {
                    Killed();
                }
            }
        }
    }
}