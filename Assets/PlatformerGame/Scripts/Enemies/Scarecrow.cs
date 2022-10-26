using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class Scarecrow : BaseEnemy
    {
        [SerializeField]
        private Animator Animator;

        [SerializeField]
        private float Speed = 15f;

        [SerializeField]
        private float RespawnTime = 3f;

        private float BlastTimer = 0f;
        private float RespawnTimer = 0f;
        private bool isBlasting = false;

        private void Update()
        {
            if (!isDead)
            {
                Move();
            }

            UpdateBlast();

            if (isDead)
            {
                UpdateRespawn();
            }

        }

        private void UpdateBlast()
        {
            if (BlastTimer > 0)
            {
                BlastTimer -= Time.deltaTime;
            }

            if ((BlastTimer <= 0) && isBlasting)
            {
                isBlasting = false;
                Animator.SetBool("isBlasting", false);
            }
        }

        private void UpdateRespawn()
        {
            if (RespawnTimer > 0)
            {
                RespawnTimer -= Time.deltaTime;
            }

            if (RespawnTimer <= 0)
            {
                Respawn();
            }
        }

        protected override void OnKilled()
        {
            base.OnKilled();
            BlastTimer = 1f;
            RespawnTimer = RespawnTime;
            isBlasting = true;
            Animator.SetBool("isBlasting", true);
            var blastPosition = new Vector2(Body.transform.position.x, Body.transform.position.y + 1.3f);
            Animator.transform.position = blastPosition;
        }

        private void Move()
        {
            var offset = new Vector2(DirectionX * Speed * Time.deltaTime, 0f);
            Body.transform.Translate(offset);
        }
    }
}