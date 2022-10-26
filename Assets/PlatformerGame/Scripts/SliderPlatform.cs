using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class SliderPlatform : MonoBehaviour
    {
        public GameObject FirstBorder;
        public GameObject SecondBorder;
        private SliderJoint2D Joint;

        private JointMotor2D Motor;

        private void Awake()
        {
            Joint = GetComponent<SliderJoint2D>();
            Motor = Joint.motor;

            //not reacting
            Motor.motorSpeed = 15f;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject == FirstBorder)
            {
                //doesn't work
                //Motor.motorSpeed *= -1;

                Joint.angle = 180f;
            }
            if (collision.gameObject == SecondBorder)
            {
                //doesn't work
                //Motor.motorSpeed *= -1;

                Joint.angle = 0f;
            }
        }
    }
}