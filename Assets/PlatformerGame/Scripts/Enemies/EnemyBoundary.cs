using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class EnemyBoundary : MonoBehaviour
    {
        [SerializeField]
        string TagName;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(TagName))
            {
                BaseEnemy parent = collision.gameObject.GetComponentInParent<BaseEnemy>();
                parent.ChangeDirection();
            }
        }
    }
}