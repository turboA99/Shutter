using UnityEngine;
using UnityEngine.SceneManagement;

namespace Flows
{
    public class TutorialFlow : MonoBehaviour
    {
        public void FinishTutorial()
        {
            SceneManager.LoadScene("MainGame");
        }
    }
}
