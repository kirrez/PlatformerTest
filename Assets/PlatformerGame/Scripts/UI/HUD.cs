using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer
{
    public class HUD : BaseView
    {
        public GameObject[] Lives = new GameObject[10];
        public Text StateName;

        public void ChangeLivesAmount(int amount)
        {
            for (int i = 0; i < 10; i++)
            {
                if (Lives[i] == null) return;

                if ((i + 1) <= amount)
                {
                    Lives[i].SetActive(true);
                }
                else
                {
                    Lives[i].SetActive(false);
                }
            }
        }

        public void ChangeStateName(string name)
        {
            StateName.text = $"State : {name}";
        }
    }
}