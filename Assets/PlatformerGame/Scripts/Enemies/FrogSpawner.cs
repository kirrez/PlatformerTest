using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class FrogSpawner : MonoBehaviour
    {
        [SerializeField]
        private float RespawnCooldown = 2f;

        [SerializeField]
        private float JumpForce = 400f;

        private IResourceManager ResourceManager;

        private float Timer = 0f;
        private bool isActive;
        private bool FrogSpawned;
        private float StartY;
        private float StartX;
        private GameObject Player;
        private Frog Frog;

        private void Awake()
        {
            ResourceManager = CompositionRoot.GetResourceManager();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                isActive = true;
                Player = collision.gameObject;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                isActive = false;
            }
        }

        private void FixedUpdate()
        {
            float direction = 1f;
            Timer -= Time.fixedDeltaTime;

            if (isActive && !FrogSpawned && Timer <= 0)
            {
                FrogSpawned = true;

                StartY = Player.transform.position.y + 2f; // change
                StartX = Player.transform.position.x;

                // modify direction and StartX

                var instance = ResourceManager.GetFromPool(Enemies.Frog);
                Frog = instance.GetComponent<Frog>();

                // Frog.Initiate
                // Frog.Killed -/+= OnFrogKilled..
            }
        }

        public void OnFrogKilled()
        {
            FrogSpawned = false;
            Timer = RespawnCooldown;
        }
    }
}