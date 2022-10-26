using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class JumpingStomp : MonoBehaviour
    {
        [SerializeField]
        float Force = 13.5f;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == (int)Layers.FeetCollider)
            {
                var offset = new Vector2(0f, Force);
                collision.gameObject.GetComponent<Rigidbody2D>().velocity += offset;
            }
        }
    }
}