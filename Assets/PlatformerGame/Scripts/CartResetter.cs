using UnityEngine;

namespace Platformer
{
    // should be placed on the FinishZone object
    public class CartResetter : MonoBehaviour
    {
        [SerializeField]
        private Transform Target;

        [SerializeField]
        private Transform StartPoint;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Cart"))
            {
                Target.rotation = Quaternion.identity;
                Target.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
                Target.gameObject.SetActive(false); // resets wheel's speed
                Target.position = StartPoint.position;
                Target.gameObject.SetActive(true);
            }
        }
    }
}