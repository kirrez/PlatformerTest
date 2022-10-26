using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Platformer
{
    public class GameOverScreen : BaseView
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
                var scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.name);
            }
        }
    }
}