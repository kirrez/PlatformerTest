using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Player
{
    public class SpikedBallTrap : MonoBehaviour
    {
        [SerializeField]
        private Animator Animator;

        [SerializeField]
        private PointEffector2D Effector;

        private float Timer = 0f;
        private bool isBlasting = false;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Teleportable"))
            {
                if (!isBlasting)
                {
                    isBlasting = true;
                    Effector.enabled = true;
                    Animator.SetBool("isBlasting", true);
                    Timer = 1f;
                }
            }

            if (collision.gameObject.CompareTag("Player"))
            {
                if (!isBlasting)
                {
                    isBlasting = true;
                    Effector.enabled = true;
                    Animator.SetBool("isBlasting", true);
                    Timer = 1f;
                }
            }
        }

        private void Update()
        {
            if (Timer > 0)
            {
                Timer -= Time.deltaTime;
            }

            if (Timer <= 0 && isBlasting)
            {
                isBlasting = false;
                Effector.enabled = false;
                Animator.SetBool("isBlasting", false);
            }
        }
    }
}