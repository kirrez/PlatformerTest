using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class StartScreen : BaseView
    {
        public override void Show()
        {
            base.Show();
            Time.timeScale = 0f;
        }

        private void Update()
        {
            Proceed();
        }

        private void Proceed()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                Time.timeScale = 1f;
                Hide();
            }
        }
    }
}