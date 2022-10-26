using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class BaseEnemy : MonoBehaviour
    {
        [SerializeField]
        protected GameObject Body;

        [SerializeField]
        private Transform RespawnPoint;

        protected Health Health;
        protected SpriteRenderer Renderer;

        protected float DirectionX = 1f;
        protected bool isDead = false;

        protected virtual void Awake()
        {
            Renderer = Body.GetComponent<SpriteRenderer>();
            Health = Body.GetComponent<Health>();
            Health.Killed += OnKilled;
        }

        protected virtual void OnEnable()
        {
            Health.RefillHealth();
        }

        protected virtual void OnKilled()
        {
            //adding score, etc
            Body.SetActive(false);
            isDead = true;
        }

        protected void Respawn()
        {
            Body.SetActive(true);
            isDead = false;
            Health.RefillHealth();
            Body.transform.position = RespawnPoint.position;
            DirectionX = 1f;
            Renderer.flipX = false;
            Debug.Log("REspawned!");
        }

        public void ChangeDirection()
        {
            DirectionX *= -1f;
            Renderer.flipX = !Renderer.flipX;
        }
    }
}