using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class DriftBorder : MonoBehaviour
    {
        [SerializeField]
        private Transform SpawnPoint;

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Teleportable"))
            {
                collision.transform.position = SpawnPoint.position;
            }
        }
    }
}