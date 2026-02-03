using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Flows
{
    public class TutorialFlow : MonoBehaviour
    {
        void OnEnable()
        {
            Cursor.visible = false;
        }

        public void FinishTutorial()
        {
            SceneManager.LoadScene("MainGame");
        }
    }
}
