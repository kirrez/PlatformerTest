using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class DriftTeleporter : MonoBehaviour
    {
        [SerializeField]
        private Transform SpawnPoint;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Teleportable"))
            {
                collision.transform.position = SpawnPoint.position;
            }
        }
    }
}