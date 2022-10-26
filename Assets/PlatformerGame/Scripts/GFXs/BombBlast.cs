using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class BombBlast : MonoBehaviour
    {
        [SerializeField]
        float LifeTime = 1f;

        public void Initiate(Vector2 newPosition)
        {
            transform.position = newPosition;
            Invoke("Disable", LifeTime);
        }

        private void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}