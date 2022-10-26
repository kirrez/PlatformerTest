using System;
using UnityEngine;


namespace Platformer
{
    public class Bat : MonoBehaviour
    {
        public Action Killed = () => { };

        private Health Health;
        private Rigidbody2D Rigidbody;
        private SpriteRenderer Renderer;
        private IResourceManager ResourceManager;

        private float DirectionX = 1f;
        private float Speed;
        private float OriginY;

        public void Initiate(float direction, Vector2 startPosition, float speed = 300f)
        {
            DirectionX = direction;
            if (DirectionX == 1f)
            {
                Renderer.flipX = false;
            } 
            if (DirectionX == -1f)
            {
                Renderer.flipX = true;
            }

            transform.position = startPosition;
            OriginY = Rigidbody.velocity.y;
            Speed = speed;
        }

        private void Awake()
        {
            ResourceManager = CompositionRoot.GetResourceManager();
            Health = GetComponent<Health>();
            Rigidbody = GetComponent<Rigidbody2D>();
            Renderer = GetComponent<SpriteRenderer>();
            Health.Killed += OnKilled;
        }

        private void FixedUpdate()
        {
            float offsetY = Mathf.Sin(Time.time * 4) * 1.5f;
            Rigidbody.velocity = new Vector2(Speed * DirectionX * Time.fixedDeltaTime, OriginY + offsetY);
        }

        private void OnKilled()
        {
            bool direction = false;

            Killed(); // BatSpawner listens to this event
            var collider = gameObject.GetComponent<Collider2D>();
            var newPosition = new Vector2(collider.bounds.center.x, collider.bounds.center.y);
            var instance = ResourceManager.GetFromPool(GFXs.BloodBlast);

            if (Rigidbody.velocity.x > 0)
            {
                direction = true;
            }
            if (Rigidbody.velocity.x < 0)
            {
                direction = false;
            }

            instance.GetComponent<BloodBlast>().Initiate(newPosition, direction);

            gameObject.SetActive(false);
        }

    }
}