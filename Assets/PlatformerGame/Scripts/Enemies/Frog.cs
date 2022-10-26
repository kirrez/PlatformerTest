using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    enum FrogStates
    {
        Launch,
        FirstJump,
        //Decide,
        //ApproachJump,
        //Attack,
        Leave,
        LastJump
    }

    public class Frog : MonoBehaviour
    {
        private FrogStates State;

        private Rigidbody2D Rigidbody;
        private SpriteRenderer Renderer;
        private IResourceManager ResourceManager;

        private float HighJumpForce = 500f;
        private float LowJumpForce = 150f;
        private float HorizontalSpeed = 250f;
        private float DirectionX = 1f;

        private void Awake()
        {
            ResourceManager = CompositionRoot.GetResourceManager();
            //Health = GetComponent<Health>();
            Rigidbody = GetComponent<Rigidbody2D>();
            Renderer = GetComponent<SpriteRenderer>();
            //Health.Killed += OnKilled;
        }

        public void Initiate(float direction, Vector2 startPosition)
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
        }

        private void FixedUpdate()
        {
            switch (State)
            {

            }
        }


    }
}