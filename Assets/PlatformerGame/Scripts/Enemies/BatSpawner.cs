using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class BatSpawner : MonoBehaviour
    {
        [SerializeField]
        private bool SpawnOnOneSide; // false - both sides, true - one side;

        [SerializeField]
        private bool CurrentDirection; // false : Left to Right, true - vice versa

        [SerializeField]
        private float RespawnCooldown = 2f;

        [SerializeField]
        private float BatSpeed = 300f;

        private IResourceManager ResourceManager;

        private float Timer = 0f;
        private bool isActive;
        private bool BatSpawned;
        private float StartY;
        private float StartX;
        private GameObject Player;
        private Bat Bat;

        private void Awake()
        {
            ResourceManager = CompositionRoot.GetResourceManager();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                isActive = true;
                //Debug.Log("Player INSIDE!");
                Player = collision.gameObject;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                isActive = false;
                //Debug.Log("Player OUTSIDE!");
            }
        }

        private void Update()
        {
            float direction;
            Timer -= Time.deltaTime;

            if (isActive && !BatSpawned && Timer <= 0f)
            {
                //Debug.Log("Bat Spawned!");
                BatSpawned = true;

                StartY = Player.transform.position.y + 2f;
                StartX = Player.transform.position.x;

                if (!SpawnOnOneSide)
                {
                    CurrentDirection = !CurrentDirection;
                }

                if (!CurrentDirection)
                {
                    StartX -= 15f; // magic number
                    direction = 1f;
                }
                else
                {
                    StartX += 15f;
                    direction = -1f;
                }

                var instance = ResourceManager.GetFromPool(Enemies.Bat);
                Bat = instance.GetComponent<Bat>();
                Bat.Initiate(direction, new Vector2(StartX, StartY), BatSpeed);
                // keep only one active "OnBatKilled"
                Bat.Killed -= OnBatKilled;
                Bat.Killed += OnBatKilled;
            }

        }

        public void OnBatKilled()
        {
            BatSpawned = false;
            Timer = RespawnCooldown;
        }
    }
}