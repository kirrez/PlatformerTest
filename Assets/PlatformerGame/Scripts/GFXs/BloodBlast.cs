using UnityEngine;

namespace Platformer
{
    public class BloodBlast : MonoBehaviour
    {
        [SerializeField]
        float LifeTime = 1f;

        private SpriteRenderer Renderer;

        private void Awake()
        {
            Renderer = GetComponent<SpriteRenderer>();
        }

        public void Initiate(Vector2 newPosition, bool direction = false)
        {
            transform.position = newPosition;
            Renderer.flipX = direction;
            Invoke("Disable", LifeTime);
        }

        private void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}